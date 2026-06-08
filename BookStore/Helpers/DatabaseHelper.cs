using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using BookStore.Models;

namespace BookStore.Helpers
{
    public class DatabaseHelper
    {
        private readonly string _connectionString;

        public DatabaseHelper()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["BookStore"].ConnectionString;
        }

        public User Authenticate(string login, string password)
        {
            const string sql = @"
                SELECT u.id, u.login, u.full_name, u.role_id, r.name AS role_name
                FROM users u
                INNER JOIN roles r ON r.id = u.role_id
                WHERE u.login = @login AND u.password = @password";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@login", login.Trim());
                command.Parameters.AddWithValue("@password", password);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        return null;
                    }
                    return MapUser(reader);
                }
            }
        }

        public static User CreateGuestUser()
        {
            return new User
            {
                Id = 0,
                Login = "guest",
                FullName = "Гость",
                RoleId = AppConstants.RoleGuest,
                RoleName = "Гость"
            };
        }

        public List<Product> GetProducts(string search, string sortField, bool sortDesc, decimal? discountMin, decimal? discountMax)
        {
            string sql = @"
                SELECT p.id, p.name, p.category_id, c.name AS category_name,
                       p.description, p.manufacturer_id, m.name AS manufacturer_name,
                       p.supplier_id, s.name AS supplier_name,
                       p.price, p.unit, p.quantity, p.discount, p.image_path
                FROM products p
                INNER JOIN categories c ON c.id = p.category_id
                LEFT JOIN manufacturers m ON m.id = p.manufacturer_id
                LEFT JOIN suppliers s ON s.id = p.supplier_id
                WHERE 1 = 1";

            List<SqlParameter> parameters = new List<SqlParameter>();

            if (!string.IsNullOrWhiteSpace(search))
            {
                sql += @" AND (
                    p.name LIKE @search OR c.name LIKE @search OR
                    ISNULL(p.description, '') LIKE @search OR
                    ISNULL(m.name, '') LIKE @search OR
                    ISNULL(s.name, '') LIKE @search OR
                    p.unit LIKE @search OR
                    CAST(p.price AS NVARCHAR(20)) LIKE @search OR
                    CAST(p.quantity AS NVARCHAR(20)) LIKE @search OR
                    CAST(p.discount AS NVARCHAR(20)) LIKE @search)";
                parameters.Add(new SqlParameter("@search", "%" + search.Trim() + "%"));
            }

            if (discountMin.HasValue && discountMax.HasValue)
            {
                sql += " AND p.discount >= @discountMin AND p.discount <= @discountMax";
                parameters.Add(new SqlParameter("@discountMin", discountMin.Value));
                parameters.Add(new SqlParameter("@discountMax", discountMax.Value));
            }

            if (sortField == "price")
            {
                sql += " ORDER BY p.price " + (sortDesc ? "DESC" : "ASC");
            }
            else if (sortField == "quantity")
            {
                sql += " ORDER BY p.quantity " + (sortDesc ? "DESC" : "ASC");
            }
            else
            {
                sql += " ORDER BY p.id";
            }

            return ExecuteProductQuery(sql, parameters.ToArray());
        }

        public Product GetProductById(int productId)
        {
            const string sql = @"
                SELECT p.id, p.name, p.category_id, c.name AS category_name,
                       p.description, p.manufacturer_id, m.name AS manufacturer_name,
                       p.supplier_id, s.name AS supplier_name,
                       p.price, p.unit, p.quantity, p.discount, p.image_path
                FROM products p
                INNER JOIN categories c ON c.id = p.category_id
                LEFT JOIN manufacturers m ON m.id = p.manufacturer_id
                LEFT JOIN suppliers s ON s.id = p.supplier_id
                WHERE p.id = @id";

            List<Product> products = ExecuteProductQuery(sql, new SqlParameter("@id", productId));
            return products.Count > 0 ? products[0] : null;
        }

        public bool ProductInOrders(int productId)
        {
            const string sql = "SELECT COUNT(*) FROM order_items WHERE product_id = @productId";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@productId", productId);
                connection.Open();
                return Convert.ToInt32(command.ExecuteScalar()) > 0;
            }
        }

        public void InsertProduct(Product product)
        {
            const string sql = @"
                INSERT INTO products (name, category_id, description, manufacturer_id, supplier_id,
                    price, unit, quantity, discount, image_path)
                VALUES (@name, @categoryId, @description, @manufacturerId, @supplierId,
                    @price, @unit, @quantity, @discount, @imagePath)";
            ExecuteProductCommand(sql, product);
        }

        public void UpdateProduct(Product product)
        {
            const string sql = @"
                UPDATE products SET name = @name, category_id = @categoryId, description = @description,
                    manufacturer_id = @manufacturerId, supplier_id = @supplierId,
                    price = @price, unit = @unit, quantity = @quantity, discount = @discount,
                    image_path = @imagePath
                WHERE id = @id";
            ExecuteProductCommand(sql, product, true);
        }

        public void DeleteProduct(int productId)
        {
            const string sql = "DELETE FROM products WHERE id = @id";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@id", productId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public List<KeyValuePair<int, string>> GetLookup(string tableName)
        {
            string sql = "SELECT id, name FROM " + tableName + " ORDER BY name";
            List<KeyValuePair<int, string>> result = new List<KeyValuePair<int, string>>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new KeyValuePair<int, string>(reader.GetInt32(0), reader.GetString(1)));
                    }
                }
            }
            return result;
        }

        public List<Order> GetOrders()
        {
            const string sql = @"
                SELECT o.id, o.article, o.status_id, s.name AS status_name,
                       o.pickup_address, o.order_date, o.delivery_date
                FROM orders o
                INNER JOIN order_statuses s ON s.id = o.status_id
                ORDER BY o.id";

            List<Order> orders = new List<Order>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        orders.Add(MapOrder(reader));
                    }
                }
            }
            return orders;
        }

        public Order GetOrderById(int orderId)
        {
            const string sql = @"
                SELECT o.id, o.article, o.status_id, s.name AS status_name,
                       o.pickup_address, o.order_date, o.delivery_date
                FROM orders o
                INNER JOIN order_statuses s ON s.id = o.status_id
                WHERE o.id = @id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@id", orderId);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        return null;
                    }
                    return MapOrder(reader);
                }
            }
        }

        public void InsertOrder(Order order)
        {
            const string sql = @"
                INSERT INTO orders (article, status_id, pickup_address, order_date, delivery_date)
                VALUES (@article, @statusId, @pickupAddress, @orderDate, @deliveryDate)";
            ExecuteOrderCommand(sql, order, false);
        }

        public void UpdateOrder(Order order)
        {
            const string sql = @"
                UPDATE orders SET article = @article, status_id = @statusId,
                    pickup_address = @pickupAddress, order_date = @orderDate, delivery_date = @deliveryDate
                WHERE id = @id";
            ExecuteOrderCommand(sql, order, true);
        }

        public void DeleteOrder(int orderId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand deleteItems = new SqlCommand("DELETE FROM order_items WHERE order_id = @id", connection))
                {
                    deleteItems.Parameters.AddWithValue("@id", orderId);
                    deleteItems.ExecuteNonQuery();
                }
                using (SqlCommand deleteOrder = new SqlCommand("DELETE FROM orders WHERE id = @id", connection))
                {
                    deleteOrder.Parameters.AddWithValue("@id", orderId);
                    deleteOrder.ExecuteNonQuery();
                }
            }
        }

        private List<Product> ExecuteProductQuery(string sql, params SqlParameter[] parameters)
        {
            List<Product> products = new List<Product>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(MapProduct(reader));
                    }
                }
            }
            return products;
        }

        private void ExecuteProductCommand(string sql, Product product, bool includeId = false)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@name", product.Name);
                command.Parameters.AddWithValue("@categoryId", product.CategoryId);
                command.Parameters.AddWithValue("@description", (object)product.Description ?? DBNull.Value);
                command.Parameters.AddWithValue("@manufacturerId", (object)product.ManufacturerId ?? DBNull.Value);
                command.Parameters.AddWithValue("@supplierId", (object)product.SupplierId ?? DBNull.Value);
                command.Parameters.AddWithValue("@price", product.Price);
                command.Parameters.AddWithValue("@unit", product.Unit);
                command.Parameters.AddWithValue("@quantity", product.Quantity);
                command.Parameters.AddWithValue("@discount", product.Discount);
                command.Parameters.AddWithValue("@imagePath", (object)product.ImagePath ?? DBNull.Value);
                if (includeId)
                {
                    command.Parameters.AddWithValue("@id", product.Id);
                }
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private void ExecuteOrderCommand(string sql, Order order, bool includeId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@article", order.Article);
                command.Parameters.AddWithValue("@statusId", order.StatusId);
                command.Parameters.AddWithValue("@pickupAddress", order.PickupAddress);
                command.Parameters.AddWithValue("@orderDate", order.OrderDate);
                command.Parameters.AddWithValue("@deliveryDate", (object)order.DeliveryDate ?? DBNull.Value);
                if (includeId)
                {
                    command.Parameters.AddWithValue("@id", order.Id);
                }
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private static User MapUser(SqlDataReader reader)
        {
            return new User
            {
                Id = reader.GetInt32(0),
                Login = reader.GetString(1),
                FullName = reader.GetString(2),
                RoleId = reader.GetInt32(3),
                RoleName = reader.GetString(4)
            };
        }

        private static Product MapProduct(SqlDataReader reader)
        {
            return new Product
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                CategoryId = reader.GetInt32(2),
                CategoryName = reader.GetString(3),
                Description = reader.IsDBNull(4) ? string.Empty : reader.GetString(4),
                ManufacturerId = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5),
                ManufacturerName = reader.IsDBNull(6) ? string.Empty : reader.GetString(6),
                SupplierId = reader.IsDBNull(7) ? (int?)null : reader.GetInt32(7),
                SupplierName = reader.IsDBNull(8) ? string.Empty : reader.GetString(8),
                Price = reader.GetDecimal(9),
                Unit = reader.GetString(10),
                Quantity = reader.GetInt32(11),
                Discount = reader.GetDecimal(12),
                ImagePath = reader.IsDBNull(13) ? null : reader.GetString(13)
            };
        }

        private static Order MapOrder(SqlDataReader reader)
        {
            return new Order
            {
                Id = reader.GetInt32(0),
                Article = reader.GetString(1),
                StatusId = reader.GetInt32(2),
                StatusName = reader.GetString(3),
                PickupAddress = reader.GetString(4),
                OrderDate = reader.GetDateTime(5),
                DeliveryDate = reader.IsDBNull(6) ? (DateTime?)null : reader.GetDateTime(6)
            };
        }
    }
}
