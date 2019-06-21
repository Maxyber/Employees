using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace Employees
{
    public class EmployeeLogic // Класс методов работы с Employee
    {
        string path; // Путь к базе данных сотрудников
        int count; // Общее количество сотрудников в компании
        ObservableCollection<Employee> empList; // Список всех сотрудников компании
        int nextId; // Следующий ID, который необходимо присвоить сотруднику

        public EmployeeLogic(string vPath)
        {
            path = vPath;
            nextId = 1;
            empList = new ObservableCollection<Employee>();
        }
        /// <summary>
        /// Возвращает список сотрудников компании
        /// </summary>
        public ObservableCollection<Employee> EmpList
        {
            get { return empList; }
        }
        /// <summary>
        /// Возвращает количество сотрудников в компании
        /// </summary>
        public int Count
        {
            get { return count; }
        }
        /// <summary>
        /// Возвращает ID для присвоения новому сотруднику
        /// </summary>
        public int NextID
        {
            get { return nextId; }
        }
        /// <summary>
        /// Загружает информацию о сотрудниках из базы данных
        /// </summary>
        public void LoadData()
        {

        }
        /// <summary>
        /// Сохраняет список сотрудников в базу данных
        /// </summary>
        public void SaveData()
        {

        }

        /// <summary>
        /// Добавляет нового сотрудника в список
        /// </summary>
        public void AddEmployee(Employee person)
        {
            this.empList.Add(person);
            count++;
            nextId++;
            SaveData();
        }
        /// <summary>
        /// Удаляет сотрудника из списка
        /// </summary>
        public void RemoveEmployee(Employee person)
        {
            this.empList.Remove(person);
            count--;
            SaveData();
        }
        public ObservableCollection<string> ToList()
        {
            ObservableCollection<string> result = new ObservableCollection<string>();
            for (int i = 0; i < count; i++)
                result.Add(empList[i].Name);
            return result;
        }
    }
}
