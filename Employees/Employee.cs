using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees
{
    [Serializable]
    public class Employee // Класс сотрудников компании (model)
    {
        string name; // ФИО сотрудника
        int department; // ID отдела, в котором работает сотрудник
        int id; // уникальный ID сотрудника в базе

        public Employee()
        {
        }
        public Employee(string vName, int vDepartment, int vID)
        {
            name = vName;
            department = vDepartment;
            id = vID;
        }
        /// <summary>
        /// Возвращает или присваивает ФИО сотрудника
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        /// <summary>
        /// Возвращает ID сотрудника в базе
        /// </summary>
        public int Id
        {
            get { return id; }
        }
        /// <summary>
        /// Возвращает или прикрепляет сотрудника к отделу
        /// </summary>
        public int Department
        {
            get { return id; }
            set { department = value; }
        }
    }
}
