using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Employees.PresentEmpDep
{
    class Presenter
    {
        public DataBase db;
        private IView view;
        public Presenter(IView View)
        {
            this.view = View;
            db = new DataBase(100, 5);
        }
        public void Add()
        {
            switch (view.CheckDep)
            {
                case false:
                    db.AddEmployee(view.ItemName, view.Info);
                    db.CalcDepartments();
                    break;
                case true:
                    db.AddDepartment(view.ItemName);
                    break;
            }
        }
        public void Edit()
        {
            switch (view.CheckDep)
            {
                case false:
                    db.ChangeEmployee(view.VEmployee, view.Id, view.ItemName, view.Info);
                    db.CalcDepartments();
                    break;
                case true:
                    db.ChangeDepartment(view.VDepartment, view.Id, view.ItemName);
                    break;
            }
        }
        public void Remove()
        {
            switch (view.CheckDep)
            {
                case false:
                    db.RemoveEmployee(view.VEmployee);
                    db.CalcDepartments();
                    break;
                case true:
                    db.RemoveDepartment(view.VDepartment);
                    break;
            }
        }
        public ObservableCollection<Employee> GetEmployees => db.GetEmployees;
        public ObservableCollection<Department> GetDepartments => db.GetDepartments;
        /// <summary>
        /// Метод, изменяющий параметры формы после выбора сотрудника в соответствующем списке
        /// </summary>
        public void Choosing()
        {
            try
            {
                if (view.CheckDep == true)
                {
                    view.Id = db.GetDepartments[view.Index].Id;
                    view.ItemName = db.GetDepartments[view.Index].Name;
                    view.Info = db.GetDepartments[view.Index].EmpCount;
                }
                else
                {
                    view.Id = db.GetEmployees[view.Index].Id;
                    view.ItemName = db.GetEmployees[view.Index].Name;
                    view.Info = db.GetEmployees[view.Index].Department;
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                view.Id = 0;
                view.ItemName = "";
                view.Info = 0;
            }
        }
    }
}
