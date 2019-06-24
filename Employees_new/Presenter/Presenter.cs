using System;
using System.Collections.Generic;
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
                    db.AddEmployee(view.ItemName,view.Info);
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
                    break;
                case true:
                    db.RemoveDepartment(view.Id);
                    break;
            }
        }
        /// <summary>
        /// Метод, изменяющий параметры формы после выбора сотрудника в соответствующем списке
        /// </summary>
        public void ChoosingEmployee()
        {
            /*
                view.Id = (lbEmployees.SelectedItem as Employee).Id.ToString();
                tbInfo.Text = departments[(lbEmployees.SelectedItem as Employee).Department].Name;
                tbName.Text = (lbEmployees.SelectedItem as Employee).Name.ToString();
                lblDepartmentId.Content = (lbEmployees.SelectedItem as Employee).Department;
                tbInfo.Visibility = Visibility.Hidden;
                cbDepartments.Visibility = Visibility.Visible;
                cbDepartments.SelectedIndex = (lbEmployees.SelectedItem as Employee).Department;
                lblInfo.Content = "Отд.";
                */
        }
        /// <summary>
        /// Метод, изменяющий параметры формы после выбора отдела в соответствующем списке
        /// </summary>
        public void ChoosingDepartment()
        {
            /*
            if (lbDepartments.SelectedItem != null)
            {
                tbId.Text = (lbDepartments.SelectedItem as Department).Id.ToString();
                tbInfo.Text = (lbDepartments.SelectedItem as Department).EmpCount.ToString();
                tbName.Text = (lbDepartments.SelectedItem as Department).Name.ToString();
                tbInfo.Visibility = Visibility.Visible;
                cbDepartments.Visibility = Visibility.Hidden;
                lblInfo.Content = "Кол.";
            }
            */
        }
    }
}
