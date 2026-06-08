-- Демонстрационный экзамен: магазин книг
-- СУБД: Microsoft SQL Server (T-SQL)
-- Выполнить в SSMS: создать БД и выполнить скрипт

IF DB_ID(N'BookStore') IS NULL
    CREATE DATABASE BookStore;
GO

USE BookStore;
GO

IF OBJECT_ID(N'dbo.order_items', N'U') IS NOT NULL DROP TABLE dbo.order_items;
IF OBJECT_ID(N'dbo.orders', N'U') IS NOT NULL DROP TABLE dbo.orders;
IF OBJECT_ID(N'dbo.products', N'U') IS NOT NULL DROP TABLE dbo.products;
IF OBJECT_ID(N'dbo.order_statuses', N'U') IS NOT NULL DROP TABLE dbo.order_statuses;
IF OBJECT_ID(N'dbo.suppliers', N'U') IS NOT NULL DROP TABLE dbo.suppliers;
IF OBJECT_ID(N'dbo.manufacturers', N'U') IS NOT NULL DROP TABLE dbo.manufacturers;
IF OBJECT_ID(N'dbo.categories', N'U') IS NOT NULL DROP TABLE dbo.categories;
IF OBJECT_ID(N'dbo.users', N'U') IS NOT NULL DROP TABLE dbo.users;
IF OBJECT_ID(N'dbo.roles', N'U') IS NOT NULL DROP TABLE dbo.roles;
GO

CREATE TABLE dbo.roles (
    id INT NOT NULL PRIMARY KEY,
    name NVARCHAR(100) NOT NULL UNIQUE
);

CREATE TABLE dbo.users (
    id INT IDENTITY(1,1) PRIMARY KEY,
    login NVARCHAR(50) NOT NULL UNIQUE,
    password NVARCHAR(100) NOT NULL,
    full_name NVARCHAR(200) NOT NULL,
    role_id INT NOT NULL,
    CONSTRAINT FK_users_roles FOREIGN KEY (role_id) REFERENCES dbo.roles(id)
);

CREATE TABLE dbo.categories (
    id INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(150) NOT NULL UNIQUE
);

CREATE TABLE dbo.manufacturers (
    id INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(150) NOT NULL UNIQUE
);

CREATE TABLE dbo.suppliers (
    id INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(150) NOT NULL UNIQUE
);

CREATE TABLE dbo.products (
    id INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(200) NOT NULL,
    category_id INT NOT NULL,
    description NVARCHAR(MAX) NULL,
    manufacturer_id INT NULL,
    supplier_id INT NULL,
    price DECIMAL(10,2) NOT NULL CHECK (price >= 0),
    unit NVARCHAR(20) NOT NULL DEFAULT N'шт.',
    quantity INT NOT NULL DEFAULT 0 CHECK (quantity >= 0),
    discount DECIMAL(5,2) NOT NULL DEFAULT 0 CHECK (discount >= 0 AND discount <= 100),
    image_path NVARCHAR(500) NULL,
    CONSTRAINT FK_products_categories FOREIGN KEY (category_id) REFERENCES dbo.categories(id),
    CONSTRAINT FK_products_manufacturers FOREIGN KEY (manufacturer_id) REFERENCES dbo.manufacturers(id),
    CONSTRAINT FK_products_suppliers FOREIGN KEY (supplier_id) REFERENCES dbo.suppliers(id)
);

CREATE TABLE dbo.order_statuses (
    id INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(100) NOT NULL UNIQUE
);

CREATE TABLE dbo.orders (
    id INT IDENTITY(1,1) PRIMARY KEY,
    article NVARCHAR(50) NOT NULL UNIQUE,
    status_id INT NOT NULL,
    pickup_address NVARCHAR(300) NOT NULL,
    order_date DATE NOT NULL,
    delivery_date DATE NULL,
    CONSTRAINT FK_orders_statuses FOREIGN KEY (status_id) REFERENCES dbo.order_statuses(id)
);

CREATE TABLE dbo.order_items (
    id INT IDENTITY(1,1) PRIMARY KEY,
    order_id INT NOT NULL,
    product_id INT NOT NULL,
    CONSTRAINT FK_order_items_orders FOREIGN KEY (order_id) REFERENCES dbo.orders(id) ON DELETE CASCADE,
    CONSTRAINT FK_order_items_products FOREIGN KEY (product_id) REFERENCES dbo.products(id)
);
GO

INSERT INTO dbo.roles (id, name) VALUES
    (1, N'Гость'), (2, N'Авторизованный клиент'), (3, N'Менеджер'), (4, N'Администратор');

INSERT INTO dbo.users (login, password, full_name, role_id) VALUES
    (N'client', N'client', N'Иванов Иван Иванович', 2),
    (N'manager', N'manager', N'Петров Пётр Петрович', 3),
    (N'admin', N'admin', N'Сидоров Сидор Сидорович', 4);

INSERT INTO dbo.categories (name) VALUES
    (N'Художественная литература'), (N'Учебная литература'),
    (N'Детская литература'), (N'Научная литература');

INSERT INTO dbo.manufacturers (name) VALUES
    (N'Эксмо'), (N'АСТ'), (N'Просвещение'), (N'Манн'), (N'Дрофа');

INSERT INTO dbo.suppliers (name) VALUES
    (N'ООО Книготорг'), (N'ИП Смирнов'), (N'ООО БукМаркет'), (N'ООО ЛитРес');

INSERT INTO dbo.order_statuses (name) VALUES
    (N'Новый'), (N'В обработке'), (N'Отправлен'), (N'Доставлен'), (N'Отменён');

INSERT INTO dbo.products (name, category_id, description, manufacturer_id, supplier_id, price, unit, quantity, discount, image_path) VALUES
    (N'Война и мир', 1, N'Роман-эпопея Л.Н. Толстого', 1, 1, 899.00, N'шт.', 15, 10, NULL),
    (N'Преступление и наказание', 1, N'Роман Ф.М. Достоевского', 2, 2, 650.00, N'шт.', 8, 30, NULL),
    (N'Алгебра 10 класс', 2, N'Учебник по алгебре', 3, 3, 450.50, N'шт.', 0, 5, NULL),
    (N'Физика 11 класс', 2, N'Учебник по физике', 3, 3, 520.00, N'шт.', 22, 15, NULL),
    (N'Колобок', 3, N'Русская народная сказка', 1, 4, 199.99, N'шт.', 50, 0, NULL),
    (N'Краткий курс Python', 4, N'Введение в программирование', 4, 1, 1200.00, N'шт.', 5, 28, NULL),
    (N'Базы данных', 4, N'Проектирование и SQL', 5, 2, 780.00, N'шт.', 12, 18, NULL);

INSERT INTO dbo.orders (article, status_id, pickup_address, order_date, delivery_date) VALUES
    (N'ORD-2026-001', 1, N'г. Москва, ул. Ленина, д. 10', '2026-05-01', '2026-05-05'),
    (N'ORD-2026-002', 3, N'г. Казань, пр. Победы, д. 25', '2026-05-10', '2026-05-15'),
    (N'ORD-2026-003', 2, N'г. Самара, ул. Мира, д. 7', '2026-06-01', NULL);

INSERT INTO dbo.order_items (order_id, product_id) VALUES
    (1, 1), (1, 5), (2, 2), (3, 7);
GO
