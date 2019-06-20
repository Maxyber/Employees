using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace Employees
{
    class EmployeeLogic // Класс методов работы с Employee
    {
        string path; // Путь к базе данных сотрудников
        int count; // Общее количество сотрудников в компании
        List<Employee> empList; // Список всех сотрудников компании
        int nextId; // Следующий ID, который необходимо присвоить сотруднику

        public EmployeeLogic(string vPath)
        {
            path = vPath;
            nextId = 1;
            empList = new List<Employee>();
        }
        /// <summary>
        /// Возвращает список сотрудников компании
        /// </summary>
        public List<Employee> EmpList
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
        /*
        public void LoadData()
        {
            using (Stream loadStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                XmlSerializer xmlFormat = new XmlSerializer(typeof(List<Employee>));
                empList = (List<Employee>)xmlFormat.Deserialize(loadStream);
            }
            foreach (Employee item in empList)
                if (nextId < item.Id) nextId = item.Id;
        }
        /// <summary>
        /// Сохраняет список сотрудников в базу данных
        /// </summary>
        public void SaveData()
        {
            using (Stream saveStream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                XmlSerializer xmlFormat = new XmlSerializer(typeof(List<Employee>));
                empList = (List<Employee>)xmlFormat.Deserialize(saveStream);
            }
        }
        */
        /// <summary>
        /// Добавляет нового сотрудника в список
        /// </summary>
        public void AddEmployee(Employee person)
        {
            this.empList.Add(person);
            count++;
            nextId++;
            // SaveData();
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
        public void Default()
        {
            AddEmployee(new Employee("Name_1", 1, 1));
            SaveData();
        }
    }
}
