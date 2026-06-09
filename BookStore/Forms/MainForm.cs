using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BookStore.Helpers;
using BookStore.Models;

namespace BookStore.Forms
{
    public partial class MainForm : Form
    {
        private readonly DatabaseHelper _database = new DatabaseHelper();
        private readonly User _user;
        private readonly LoginForm _loginForm;
        private Product _selectedProduct;
        private bool _suppressFilterEvents;

        public MainForm(User user, LoginForm loginForm)
        {
            _user = user;
            _loginForm = loginForm;
            InitializeComponent();
            ApplyRoleRestrictions();
            RefreshProducts();
        }

        private void ApplyRoleRestrictions()
        {
            lblUserInfo.Text = _user.FullName + " (" + _user.RoleName + ")";

            bool canFilter = _user.RoleId == AppConstants.RoleManager || _user.RoleId == AppConstants.RoleAdmin;
            txtSearch.Enabled = canFilter;
            cmbSort.Enabled = canFilter;
            cmbDiscountFilter.Enabled = canFilter;

            bool isAdmin = _user.RoleId == AppConstants.RoleAdmin;
            btnAddProduct.Enabled = isAdmin;
            btnDeleteProduct.Enabled = isAdmin;

            bool canViewOrders = canFilter;
            btnOrders.Enabled = canViewOrders;
        }

        private void RefreshProducts()
        {
            flowProducts.SuspendLayout();
            flowProducts.Controls.Clear();
            _selectedProduct = null;

            bool canFilter = _user.RoleId == AppConstants.RoleManager || _user.RoleId == AppConstants.RoleAdmin;
            string search = canFilter ? txtSearch.Text : string.Empty;
            string sortField = null;
            bool sortDesc = false;

            if (canFilter)
            {
                switch (cmbSort.SelectedIndex)
                {
                    case 1: sortField = "price"; break;
                    case 2: sortField = "price"; sortDesc = true; break;
                    case 3: sortField = "quantity"; break;
                    case 4: sortField = "quantity"; sortDesc = true; break;
                }
            }

            decimal? discountMin = null;
            decimal? discountMax = null;
            if (canFilter)
            {
                GetDiscountRange(cmbDiscountFilter.SelectedIndex, out discountMin, out discountMax);
            }

            List<Product> products = _database.GetProducts(search, sortField, sortDesc, discountMin, discountMax);
            if (products.Count == 0)
            {
                Label empty = new Label();
                empty.Text = "Товары не найдены";
                empty.AutoSize = true;
                empty.Padding = new Padding(10);
                flowProducts.Controls.Add(empty);
            }
            else
            {
                foreach (Product product in products)
                {
                    flowProducts.Controls.Add(CreateProductPanel(product));
                }
            }

            flowProducts.ResumeLayout();
        }

        private static void GetDiscountRange(int index, out decimal? min, out decimal? max)
        {
            switch (index)
            {
                case 1: min = 0; max = 12.99m; return;
                case 2: min = 13; max = 16.99m; return;
                case 3: min = 17; max = 100; return;
                default: min = null; max = null; return;
            }
        }

        private Panel CreateProductPanel(Product product)
        {
            Color bgColor = Color.White;
            if (product.Quantity == 0)
            {
                bgColor = AppConstants.OutOfStockColor;
            }
            else if (product.Discount > 25)
            {
                bgColor = AppConstants.DiscountHighlight;
            }

            Panel panel = new Panel();
            panel.Width = flowProducts.ClientSize.Width - 30;
            panel.Height = 110;
            panel.BackColor = bgColor;
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.Margin = new Padding(0, 0, 0, 8);
            panel.Tag = product;
            panel.Click += ProductPanel_Click;

            PictureBox picture = new PictureBox();
            picture.Location = new Point(10, 10);
            picture.Size = new Size(120, 80);
            picture.SizeMode = PictureBoxSizeMode.Zoom;
            picture.Image = ImageHelper.LoadDisplayImage(product.ImagePath, 120, 80);
            picture.Click += ProductPanel_Click;
            picture.Tag = product;

            Label title = new Label();
            title.Text = product.CategoryName + " | " + product.Name;
            title.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            title.Location = new Point(140, 10);
            title.Size = new Size(500, 20);
            title.BackColor = bgColor;
            title.Click += ProductPanel_Click;
            title.Tag = product;

            Label description = new Label();
            description.Text = product.Description;
            description.Location = new Point(140, 32);
            description.Size = new Size(500, 36);
            description.BackColor = bgColor;
            description.Click += ProductPanel_Click;
            description.Tag = product;

            Label meta = new Label();
            meta.Text = "Производитель: " + (string.IsNullOrEmpty(product.ManufacturerName) ? "—" : product.ManufacturerName)
                + " | Поставщик: " + (string.IsNullOrEmpty(product.SupplierName) ? "—" : product.SupplierName)
                + " | Ед. изм.: " + product.Unit + " | В наличии: " + product.Quantity;
            meta.Location = new Point(140, 72);
            meta.Size = new Size(500, 20);
            meta.BackColor = bgColor;
            meta.Click += ProductPanel_Click;
            meta.Tag = product;

            Label discountBox = new Label();
            discountBox.Text = "Скидка\r\n" + product.Discount.ToString("0") + "%";
            discountBox.TextAlign = ContentAlignment.MiddleCenter;
            discountBox.BorderStyle = BorderStyle.FixedSingle;
            discountBox.Location = new Point(panel.Width - 90, 20);
            discountBox.Size = new Size(70, 60);
            discountBox.BackColor = bgColor;
            discountBox.Click += ProductPanel_Click;
            discountBox.Tag = product;

            if (product.Discount > 0)
            {
                Label oldPrice = CreateRowLabel(product, bgColor, panel.Width - 190, 24, 90, 20);
                oldPrice.Text = product.Price.ToString("0.00") + " ₽";
                oldPrice.Font = new Font("Segoe UI", 9, FontStyle.Strikeout);
                oldPrice.ForeColor = Color.FromArgb(204, 0, 0);
                oldPrice.TextAlign = ContentAlignment.MiddleRight;

                Label finalPrice = CreateRowLabel(product, bgColor, panel.Width - 190, 48, 90, 24);
                finalPrice.Text = product.FinalPrice.ToString("0.00") + " ₽";
                finalPrice.Font = new Font("Segoe UI", 11, FontStyle.Bold);
                finalPrice.ForeColor = Color.Black;
                finalPrice.TextAlign = ContentAlignment.MiddleRight;

                panel.Controls.Add(oldPrice);
                panel.Controls.Add(finalPrice);
            }
            else
            {
                Label priceLabel = CreateRowLabel(product, bgColor, panel.Width - 190, 30, 90, 30);
                priceLabel.Text = product.Price.ToString("0.00") + " ₽";
                priceLabel.Font = new Font("Segoe UI", 11, FontStyle.Bold);
                priceLabel.TextAlign = ContentAlignment.MiddleRight;
                panel.Controls.Add(priceLabel);
            }

            panel.Controls.Add(picture);
            panel.Controls.Add(title);
            panel.Controls.Add(description);
            panel.Controls.Add(meta);
            panel.Controls.Add(discountBox);

            if (_user.RoleId == AppConstants.RoleAdmin)
            {
                panel.DoubleClick += ProductPanel_DoubleClick;
                picture.DoubleClick += ProductPanel_DoubleClick;
                title.DoubleClick += ProductPanel_DoubleClick;
            }

            return panel;
        }

        private Label CreateRowLabel(Product product, Color bgColor, int x, int y, int w, int h)
        {
            Label label = new Label();
            label.Location = new Point(x, y);
            label.Size = new Size(w, h);
            label.BackColor = bgColor;
            label.Click += ProductPanel_Click;
            label.Tag = product;
            return label;
        }

        private void ProductPanel_Click(object sender, EventArgs e)
        {
            Control control = sender as Control;
            Product product = control != null ? control.Tag as Product : null;
            if (product == null)
            {
                return;
            }
            _selectedProduct = product;
            foreach (var ctrl in this.Controls)
            {
                Panel row = control as Panel;
                if (row == null)
                {
                    continue;
                }
                Product rowProduct = row.Tag as Product;
                row.BackColor = rowProduct != null && rowProduct.Id == product.Id
                    ? Color.FromArgb(220, 230, 250)
                    : GetRowColor(rowProduct);
            }
        }

        private static Color GetRowColor(Product product)
        {
            if (product == null) return Color.White;
            if (product.Quantity == 0) return AppConstants.OutOfStockColor;
            if (product.Discount > 25) return AppConstants.DiscountHighlight;
            return Color.White;
        }

        private void ProductPanel_DoubleClick(object sender, EventArgs e)
        {
            Control control = sender as Control;
            Product product = control != null ? control.Tag as Product : null;
            if (product == null)
            {
                return;
            }
            OpenProductEditForm(product);
        }

        private void OpenProductEditForm(Product product)
        {
            try
            {
                ProductEditForm editForm = new ProductEditForm(product, RefreshProducts);
                editForm.ShowDialog();
            }
            catch (InvalidOperationException)
            {
            }
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            if (_suppressFilterEvents) return;
            RefreshProducts();
        }

        private void CmbSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressFilterEvents) return;
            RefreshProducts();
        }

        private void CmbDiscountFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressFilterEvents) return;
            RefreshProducts();
        }

        private void BtnAddProduct_Click(object sender, EventArgs e)
        {
            OpenProductEditForm(null);
        }

        private void BtnDeleteProduct_Click(object sender, EventArgs e)
        {
            if (_selectedProduct == null)
            {
                MessageBox.Show(
                    "Выберите товар в списке (один щелчок), затем нажмите «Удалить товар».",
                    "Предупреждение",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (_database.ProductInOrders(_selectedProduct.Id))
            {
                MessageBox.Show(
                    "Товар «" + _selectedProduct.Name + "» нельзя удалить,\nтак как он присутствует в заказе.",
                    "Предупреждение",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                "Удалить товар «" + _selectedProduct.Name + "»?\nЭто действие необратимо.",
                "Подтверждение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes)
            {
                return;
            }

            _database.DeleteProduct(_selectedProduct.Id);
            MessageBox.Show("Товар удалён.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefreshProducts();
        }

        private void BtnOrders_Click(object sender, EventArgs e)
        {
            OrdersForm ordersForm = new OrdersForm(_user);
            ordersForm.ShowDialog();
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            Close();
            _loginForm.Show();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_loginForm != null && !_loginForm.Visible)
            {
                _loginForm.Close();
            }
        }
    }
}
