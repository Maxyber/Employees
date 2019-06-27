using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Employees.PresentEmpDep
{
    class DataBase
    {
        public Random r = new Random();
        public static ObservableCollection<Employee> employees = new ObservableCollection<Employee>(); // список сотрудников
        public static ObservableCollection<Department> departments = new ObservableCollection<Department>(); // список отделов

        /// <summary>
        /// Конструктор базы, создающий empCount сотрудников, входящих в depCount отделов
        /// </summary>
        public DataBase(int empCount, int depCount)
        {
            for (int i = 0; i < depCount; i++)
                departments.Add(new Department($"Department_{i}", i));
            for (int i = 0; i < empCount; i++)
                employees.Add(new Employee($"Name_{i}", r.Next(depCount), i));
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
            employees[employees.IndexOf(vEmployee)].Department = depId;
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
    }
}
