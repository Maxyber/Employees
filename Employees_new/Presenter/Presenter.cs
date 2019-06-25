using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.PresentEmpDep
{
    class Presenter
    {
        private DataBase db;
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
                    db.ChangeEmployee(view.Id, view.ItemName, view.Info);
                    db.CalcDepartments();
                    break;
                case true:
                    db.ChangeDepartment(view.Id, view.ItemName);
                    break;
            }
        }
        public void Remove()
        {
            switch (view.CheckDep)
            {
                case false:
                    db.RemoveEmployee(view.Id);
                    db.CalcDepartments();
                    break;
                case true:
                    db.RemoveDepartment(view.Id);
                    break;
            }
        }
        public ObservableCollection<Employee> GetEmployees()
        {
            return db.GetEmployees;
        }
        public ObservableCollection<Department> GetDepartments()
        {
            return db.GetDepartments;
        }
        /// <summary>
        /// Метод, изменяющий параметры формы после выбора сотрудника в соответствующем списке
        /// </summary>
        public void Choosing()
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
    }
}
