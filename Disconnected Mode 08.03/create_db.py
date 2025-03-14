import sqlite3

# Підключення до БД (якщо файлу немає — він створиться)
conn = sqlite3.connect("sales.db")
cursor = conn.cursor()

# 📌 Створення таблиці "Працівники"
cursor.execute("""
CREATE TABLE IF NOT EXISTS employees (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name TEXT NOT NULL,
    position TEXT NOT NULL
)
""")

# 📌 Створення таблиці "Клієнти"
cursor.execute("""
CREATE TABLE IF NOT EXISTS clients (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name TEXT NOT NULL,
    email TEXT NOT NULL
)
""")

# 📌 Створення таблиці "Продажі"
cursor.execute("""
CREATE TABLE IF NOT EXISTS sales (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    client_id INTEGER,
    employee_id INTEGER,
    amount REAL,
    date TEXT,
    FOREIGN KEY(client_id) REFERENCES clients(id),
    FOREIGN KEY(employee_id) REFERENCES employees(id)
)
""")

# 📌 Додавання тестових даних
cursor.execute("INSERT INTO employees (name, position) VALUES ('Іван Петренко', 'Менеджер')")
cursor.execute("INSERT INTO employees (name, position) VALUES ('Марія Коваленко', 'Продавець')")
cursor.execute("INSERT INTO clients (name, email) VALUES ('ТОВ Рога і Копита', 'client1@example.com')")
cursor.execute("INSERT INTO clients (name, email) VALUES ('ФОП Іванов', 'client2@example.com')")
cursor.execute("INSERT INTO sales (client_id, employee_id, amount, date) VALUES (1, 1, 5000, '2024-03-14')")
cursor.execute("INSERT INTO sales (client_id, employee_id, amount, date) VALUES (2, 2, 2500, '2024-03-13')")

# Збереження змін
conn.commit()
conn.close()

print("✅ База даних створена успішно!")
