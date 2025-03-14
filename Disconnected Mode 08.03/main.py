import sqlite3
import tkinter as tk
from tkinter import ttk, messagebox


def load_data():
    conn = sqlite3.connect("sales.db")
    cursor = conn.cursor()
    cursor.execute(f"SELECT * FROM {table_name.get()}")
    rows = cursor.fetchall()
    conn.close()


    tree.delete(*tree.get_children())

    for row in rows:
        tree.insert("", "end", values=row)


def add_record():
    conn = sqlite3.connect("sales.db")
    cursor = conn.cursor()
    values = (entry1.get(), entry2.get(), entry3.get())
    cursor.execute(f"INSERT INTO {table_name.get()} VALUES (NULL, ?, ?)", values)
    conn.commit()
    conn.close()
    load_data()

root = tk.Tk()
root.title("Управління базою Продажів")


table_name = tk.StringVar(value="employees")
tables = ["employees", "clients", "sales"]
table_dropdown = ttk.Combobox(root, textvariable=table_name, values=tables)
table_dropdown.pack()
table_dropdown.bind("<<ComboboxSelected>>", lambda e: load_data())


tree = ttk.Treeview(root, columns=("col1", "col2", "col3"), show="headings")
tree.heading("col1", text="ID")
tree.heading("col2", text="Name")
tree.heading("col3", text="Position/Sales")
tree.pack()


entry1 = tk.Entry(root)
entry2 = tk.Entry(root)
entry3 = tk.Entry(root)
entry1.pack()
entry2.pack()
entry3.pack()


btn_add = tk.Button(root, text="Додати", command=add_record)
btn_add.pack()

load_data()

root.mainloop()
