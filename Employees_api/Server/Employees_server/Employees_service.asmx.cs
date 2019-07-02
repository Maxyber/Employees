using Employees.PresentEmpDep;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Services;
using Employees;

namespace Employees_server
{
    /// <summary>
    /// Сводное описание для Employees_service
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Чтобы разрешить вызывать веб-службу из скрипта с помощью ASP.NET AJAX, раскомментируйте следующую строку. 
    // [System.Web.Script.Services.ScriptService]
    public class Employees_service : System.Web.Services.WebService
    {
        public DataBase db;
        //private IView view;
        //public Employees_service()
        public Employees_service()
        {
            // this.view = View;
            db = new DataBase(100, 5);
        }
        [WebMethod]
        public void AddDep(string name)
        {
            db.AddDepartment(name);
        }
        [WebMethod]
        public void AddEmp(string name, int info)
        {
            db.AddEmployee(name, info);
            db.CalcDepartments();
        }
        [WebMethod]
        public void EditDep()
        {
            db.ChangeDepartment(view.VDepartment, view.Id, view.ItemName);
        }
        [WebMethod]
        public void EditEmp()
        {
            db.ChangeEmployee(view.VEmployee, view.Id, view.ItemName, view.Info);
            db.CalcDepartments();
        }
        [WebMethod]
        public void RemoveDep()
        {
            db.RemoveDepartment(view.VDepartment);
        }
        [WebMethod]
        public void RemoveEmp()
        {
            db.RemoveEmployee(view.VEmployee);
            db.CalcDepartments();
        }
        [WebMethod]
        public Employee GetEmployee(int i)
        {
            return db.GetEmployees[i];
        }
        [WebMethod]
        public Department GetDepartment(int i)
        {
            return db.GetDepartments[i];
        }
        [WebMethod]
        public ObservableCollection<Employee> GetEmployees()
        {
            return db.GetEmployees;
        }
        [WebMethod]
        public ObservableCollection<Department> GetDepartments()
        {
            return db.GetDepartments;
        }
        /// <summary>
        /// Метод, изменяющий параметры формы после выбора сотрудника в соответствующем списке
        /// </summary>
        [WebMethod]
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
        [WebMethod]
        public void Save()
        {
            db.DataBaseDataSave();
        }
    }
}
