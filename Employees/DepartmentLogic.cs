using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace Employees
{
    class DepartmentLogic
    {
        string path; // Путь к базе данных подразделений компании
        int count; // Общее количество подразделений в компании
        List<Department> depList; // Список всех подразделений компании
        int nextId; // Следующий ID, который необходимо присвоить подразделению

        public DepartmentLogic(string vPath)
        {
            path = vPath;
            nextId = 1;
            depList = new List<Department>();
        }
        /// <summary>
        /// Возвращает список подразделений в компании
        /// </summary>
        public List<Department> DepList
        {
            get { return depList; }
        }
        /// <summary>
        /// Возвращает количество подразделений в компании
        /// </summary>
        public int Count
        {
            get { return count; }
        }
        /// <summary>
        /// Возвращает ID для присвоения новому подразделению
        /// </summary>
        public int NextID
        {
            get { return nextId; }
        }
        /// <summary>
        /// Загружает информацию о подразделениях из базы данных
        /// </summary>
        void LoadData()
        {
            using (Stream loadStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                XmlSerializer xmlFormat = new XmlSerializer(typeof(List<Employee>));
                depList = (List<Department>)xmlFormat.Deserialize(loadStream);
            }
            foreach (Department item in depList)
                if (nextId < item.Id) nextId = item.Id;
        }
        /// <summary>
        /// Сохраняет список подразделений в базу данных
        /// </summary>
        void SaveData()
        {
            using (Stream saveStream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                XmlSerializer xmlFormat = new XmlSerializer(typeof(List<Department>));
                depList = (List<Department>)xmlFormat.Deserialize(saveStream);
            }
        }
        /// <summary>
        /// Добавляет новое подразделение в список
        /// </summary>
        public void AddDepartment(Department dep)
        {
            this.depList.Add(dep);
            count++;
            nextId++;
            // SaveData();
        }
        /// <summary>
        /// Удаляет подразделение из списка
        /// </summary>
        public void RemoveDepartment(Department dep)
        {
            this.depList.Remove(dep);
            count--;
            SaveData();
        }
        public void Default()
        {
            AddDepartment(new Department("Department_1",1));
            SaveData();
        }
    }
}
