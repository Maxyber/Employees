using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Employees.PresentEmpDep
{
    class DataBase : INotifyPropertyChanged
    {
        /*
        public delegate void ChangeList();
        ChangeList changing; // делегат для изменения объекта
        ChangeList choosing; // делегат обработки данных при выборе объекта
        ChangeList adding; // делегат для добавления объекта
        ChangeList removing; // делегат для удаления объекта
        */
        public Random r = new Random();
        public static ObservableCollection<Employee> employees = new ObservableCollection<Employee>(); // список сотрудников
        public static ObservableCollection<Department> departments = new ObservableCollection<Department>(); // список отделов
        public event PropertyChangedEventHandler PropertyChanged;
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
                ++departments[item.Department].EmpCount;
        }
        /// <summary>
        /// Метод, редактирующий сотрудника после нажатия соответствующей кнопки
        /// </summary>
        public void ChangeEmployee(int itemId, string name, int depId)
        {
            employees[itemId].Name = name;
            employees[itemId].Department = depId;
            CalcDepartments();
        }
        /// <summary>
        /// Метод, редактирующий отдел после нажатия соответствующей кнопки
        /// </summary>
        public void ChangeDepartment(int itemId, string name)
        {
            departments[itemId].Name = name;
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
        public void RemoveEmployee(int itemId)
        {
            employees.Remove(employees[itemId]);
        }
        /// <summary>
        /// Метод, удаляющий департамент после нажатия соответствующей кнопки, если в нем не осталось сотрудников
        /// </summary>
        public void RemoveDepartment(int itemId)
        {
            if (departments[itemId].EmpCount != 0)
                for (int i = employees.Count - 1; i >= 0; i--)
                {
                    if (employees[i].Department == itemId)
                        employees.Remove(employees[i]);
                }
            departments.Remove(departments[itemId]);
        }
    }
}
