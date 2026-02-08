CREATE DATABASE IF NOT EXISTS legacydb;
USE legacydb;

-- CUSTOMERS
DROP TABLE IF EXISTS customers;
CREATE TABLE customers (
  id INT AUTO_INCREMENT PRIMARY KEY,
  name VARCHAR(150) NOT NULL,
  surname VARCHAR(150) NOT NULL,
  email VARCHAR(255) NOT NULL,
  age INT NULL,
  phone VARCHAR(50) NOT NULL,
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP NOT NULL,
  deleted TINYINT(1) NOT NULL DEFAULT 0
) ENGINE=InnoDB;

-- PRODUCTS
DROP TABLE IF EXISTS products;
CREATE TABLE products (
  id INT AUTO_INCREMENT PRIMARY KEY,
  country VARCHAR(100) NOT NULL,
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP NOT NULL,
  price DECIMAL(12,2) NOT NULL DEFAULT 0.00,
  best_before DATE NULL,
  deleted TINYINT(1) NOT NULL DEFAULT 0
) ENGINE=InnoDB;

-- PRODUCT_CUSTOMERS 
DROP TABLE IF EXISTS product_customers;
CREATE TABLE product_customers (
  product_id INT NOT NULL,
  customer_id INT NOT NULL,
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP NOT NULL,
  PRIMARY KEY (product_id, customer_id)
) ENGINE=InnoDB;

-- PRODUCT TRANSLATIONS
DROP TABLE IF EXISTS product_translations;
CREATE TABLE product_translations (
  product_id INT NOT NULL,
  language_code VARCHAR(10) NOT NULL,
  name VARCHAR(300) NULL,
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP NOT NULL,
  PRIMARY KEY (product_id, language_code)
) ENGINE=InnoDB;


INSERT INTO customers (name, surname, email, age, phone) VALUES
('Ansh', 'Petro', 'ivan@example.com', 34, '+420123456789'),
('Piro', 'Loki', 'loki@example.com', 28, '+420987654321'),
('Perev', 'Tika', 'perev@example.com', 34, '+420123456789'),
('Giga', 'Reki', 'reki@example.com', NULL, '+420987654321'),
('Amo', 'Has', 'has@example.com', 34, '+420987654322'),
('Viro', 'Barr', 'barr@example.com', 28, '+420987654321');

INSERT INTO products (country, price, best_before) VALUES
('Czech Republic', 100.00, '2026-06-01'),
('Russia', 50.50, '2027-02-13'),
('Ukraine', 70.50, NULL);

INSERT INTO product_translations (product_id, language_code, name) VALUES
(1, 'en', 'Product A'),
(1, 'cs', 'Produkt A'),
(2, 'cs', 'Produkt B'),
(2, 'en', NULL);

INSERT INTO product_customers (product_id, customer_id) VALUES
(1,1),
(2,1),
(2,2);