using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Configuration;

namespace Employees.PresentEmpDep
{
    class DataBase
    {
        /*
        <connectionString>
            <add name ="DefaultConnection"
            connectionString="Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=lesson7; Integrated Security=True; Pooling=False"/>
        </connectionString>
        */
        public Random r = new Random();
        public static ObservableCollection<Employee> employees = new ObservableCollection<Employee>(); // список сотрудников
        public static ObservableCollection<Department> departments = new ObservableCollection<Department>(); // список отделов
        private string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString; // строка подключения к БД, прописанная в App.config
        //private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=lesson7; Integrated Security=True; Pooling=False"; // строка подключения к БД, прописанная вручную

        /// <summary>
        /// Конструктор базы, создающий empCount сотрудников, входящих в depCount отделов
        /// </summary>
        public DataBase(int empCount, int depCount)
        {
            DataBaseDataRead();
            CalcDepartments();
        }
        public ObservableCollection<Employee> GetEmployees => employees;
        public ObservableCollection<Department> GetDepartments => departments;
        /// <summary>
        /// Метод, считающий количество сотрудников по отделам
        /// </summary>
        public void CalcDepartments()
        {
            foreach (Department item in departments)
                item.EmpCount = 0;
            foreach (Employee item in employees)
                ++departments[IndexById(item.Department)].EmpCount;
        }
        /// <summary>
        /// Метод, редактирующий сотрудника после нажатия соответствующей кнопки
        /// </summary>
        public void ChangeEmployee(Employee vEmployee, int itemId, string name, int depId)
        {
            employees[employees.IndexOf(vEmployee)].Name = name;
            employees[employees.IndexOf(vEmployee)].Department = GetDepartments[depId].Id;
            CalcDepartments();
        }
        /// <summary>
        /// Метод, редактирующий отдел после нажатия соответствующей кнопки
        /// </summary>
        public void ChangeDepartment(Department vDepartment, int itemId, string name)
        {
            departments[departments.IndexOf(vDepartment)].Name = name;
        }
        /// <summary>
        /// Метод, добавляющий сотрудника после нажатия соответствующей кнопки
        /// </summary>
        public void AddEmployee(string name, int depId)
        {
            int maxId = 0;
            foreach (Employee item in employees)
                if (item.Id > maxId) maxId = item.Id;
            employees.Add(new Employee(name, depId, maxId + 1));
            CalcDepartments();
        }
        /// <summary>
        /// Метод, добавляющий отдел после нажатия соответствующей кнопки
        /// </summary>
        public void AddDepartment(string name)
        {
            int maxId = 0;
            foreach (Department item in departments)
                if (item.Id > maxId) maxId = item.Id;
            departments.Add(new Department(name, maxId + 1));
        }
        /// <summary>
        /// Метод, удаляющий сотрудника после нажатия соответствующей кнопки
        /// </summary>
        public void RemoveEmployee(Employee rEmployee)
        {
            employees.Remove(rEmployee);
        }
        /// <summary>
        /// Метод, удаляющий департамент вместе со всеми сотрудниками после нажатия соответствующей кнопки
        /// </summary>
        public void RemoveDepartment(Department rDepartment)
        {
            if (rDepartment.EmpCount != 0)
                for (int i = employees.Count - 1; i >= 0; i--)
                {
                    if (employees[i].Department == rDepartment.Id)
                        employees.Remove(employees[i]);
                }
            departments.Remove(rDepartment);
        }
        public static string DepNameById(int id)
        {
            string result = "";
            foreach (Department item in departments)
                if (item.Id == id)
                {
                    result = item.Name;
                    break;
                }
            return result;
        }
        private int IndexById(int id)
        {
            int depIndex = 0;
            for (int i = 0; i < departments.Count; i++)
                if (departments[i].Id == id) depIndex = i;
            return depIndex;
        }
        #region Методы работы с БД
        // Метод, добавляющий в БД отделы в количестве depCount и сотрудников в количестве empCount, запускается один раз
        public void DataBaseDataCreate(int depCount, int empCount)
        {
            try
            {
                var random = new Random();
                for (int i = 0; i < depCount; i++)
                {
                    var department = new Department($"Department_{i}", i);

                    var sql = String.Format("INSERT INTO Departments (Id, Name, EmpCount) VALUES (N'{0}', '{1}', '{2}')", department.Id, department.Name, department.EmpCount);
                    Debug.WriteLine(sql);

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        SqlCommand command = new SqlCommand(sql, connection);
                        command.ExecuteNonQuery();
                    }
                }
                for (int i = 0; i < empCount; i++)
                {
                    var employee = new Employee($"Name_{i}", random.Next(10), i);

                    var sql = String.Format("INSERT INTO Employees (Id, Name, Department) VALUES (N'{0}', '{1}', '{2}')", employee.Id, employee.Name, employee.Department);
                    Debug.WriteLine(sql);

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        SqlCommand command = new SqlCommand(sql, connection);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.WriteLine("exit");
            }
        }
        // Метод чтения данных из БД
        public void DataBaseDataRead()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT * FROM Departments";
                    SqlCommand command = new SqlCommand(sql, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader.GetValue(0)} | {reader["Name"]} | {reader[2]}");
                        departments.Add(new Department(reader["Name"].ToString(), Int32.Parse(reader["Id"].ToString())));
                    }
                }
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT * FROM Employees";
                    SqlCommand command = new SqlCommand(sql, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader.GetValue(0)} | {reader["Name"]} | {reader[2]}");
                        employees.Add(new Employee(reader["Name"].ToString(), Int32.Parse(reader["Department"].ToString()), Int32.Parse(reader["Id"].ToString())));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        // Метод записи данных в БД
        public void DataBaseDataSave()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var sql = @"TRUNCATE TABLE Departments";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.ExecuteNonQuery();
                    sql = @"TRUNCATE TABLE Employees";
                    command = new SqlCommand(sql, connection);
                    command.ExecuteNonQuery();
                    foreach (Department item in departments)
                    {
                        sql = String.Format("INSERT INTO Departments (Id, Name, EmpCount) VALUES (N'{0}', '{1}', '{2}')", item.Id, item.Name, item.EmpCount);
                        command = new SqlCommand(sql, connection);
                        command.ExecuteNonQuery();
                    }
                    foreach (Employee item in employees)
                    {
                        sql = String.Format("INSERT INTO Employees (Id, Name, Department) VALUES (N'{0}', '{1}', '{2}')", item.Id, item.Name, item.Department);
                        command = new SqlCommand(sql, connection);
                        command.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        #endregion
    }
}
