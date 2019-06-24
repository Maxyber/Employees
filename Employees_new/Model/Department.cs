using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees
{
    [Serializable]
    public class Department // Класс отделов компании (model)
    {
        string name; // наименование отдела
        int empCount; // количество сотрудников, работающих в отделе
        int id; // уникальный ID отдела в базе

        public Department()
        {
        }
        public Department(string vName, int vID)
        {
            name = vName;
            empCount = 0;
            id = vID;
        }
        /// <summary>
        /// Возвращает или присваивает название отдела
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        /// <summary>
        /// Возвращает ID отдела
        /// </summary>
        public int Id
        {
            get { return id; }
        }
        /// <summary>
        /// Возвращает или присваивает количество сотрудников в отделе
        /// </summary>
        public int EmpCount
        {
            get { return empCount; }
            set { empCount = value; }
        }
        public override string ToString()
        {
            return $"{name} .. {EmpCount}";
        }
    }
}
