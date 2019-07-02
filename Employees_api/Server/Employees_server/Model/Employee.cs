using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace Employees
{
    [Serializable]
    public class Employee : INotifyPropertyChanged // Класс сотрудников компании (model)
    {
        public string name; // ФИО сотрудника
        public int department; // ID отдела, в котором работает сотрудник
        public int id; // уникальный ID сотрудника в базе

        public Employee()
        {
        }
        public Employee(string vName, int vDepartment, int vID)
        {
            name = vName;
            department = vDepartment;
            id = vID;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Возвращает или присваивает ФИО сотрудника
        /// </summary>
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Name)));
            }
        }
        /// <summary>
        /// Возвращает ID сотрудника в базе
        /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        /// <summary>
        /// Возвращает или прикрепляет сотрудника к отделу
        /// </summary>
        public int Department
        {
            get { return department; }
            set
            {
                department = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Department)));
            }
        }
    }
}
