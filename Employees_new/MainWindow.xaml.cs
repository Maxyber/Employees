using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Collections.ObjectModel;
using System.Xml;

namespace Employees
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public delegate void ChangeList();
        ChangeList changing; // делегат для изменения объекта
        ChangeList choosing; // делегат обработки данных при выборе объекта
        ChangeList adding; // делегат для добавления объекта
        ChangeList removing; // делегат для удаления объекта
        public Random r = new Random();
        public ObservableCollection<Employee> employees = new ObservableCollection<Employee>(); // список сотрудников
        public ObservableCollection<Department> departments = new ObservableCollection<Department>(); // список отделов

        public MainWindow()
        {
            InitializeComponent();
            AppInit();
        }
        /// <summary>
        /// Метод, заполняющий списки отделов и сотрудников
        /// </summary>
        public void AppInit()
        {
            // AddData
            for (int i = 0; i < 3; i++)
                departments.Add(new Department($"Department_{i}", i));
            for (int i = 0; i < 10; i++)
                employees.Add(new Employee($"Name_{i}", r.Next(3), i));
            CalcDepartments();
            // привязка к листбоксам
            lbEmployees.ItemsSource = employees;
            lbDepartments.ItemsSource = departments;
            cbDepartments.ItemsSource = departments;
        }
        /// <summary>
        /// Метод, обновляющий списки в листбоксах после изменения данных (самостоятельно не обновляется почему-то)
        /// </summary>
        public void RefreshData()
        {
            lbEmployees.Items.Refresh();
            lbDepartments.Items.Refresh();
        }
        /// <summary>
        /// Метод, считающий количество сотрудников по отделам
        /// </summary>
        public void CalcDepartments()
        {
            foreach (Department item in departments)
                item.EmpCount = 0;
            foreach (Employee item in employees)
                ++departments[item.Department].EmpCount;
        }
        /// <summary>
        /// Метод, изменяющий параметры формы после выбора сотрудника в соответствующем списке
        /// </summary>
        public void ChoosingEmployee()
        {
            if (lbEmployees.SelectedItem != null)
            {
                tbId.Text = (lbEmployees.SelectedItem as Employee).Id.ToString();
                tbInfo.Text = departments[(lbEmployees.SelectedItem as Employee).Department].Name;
                tbName.Text = (lbEmployees.SelectedItem as Employee).Name.ToString();
                lblDepartmentId.Content = (lbEmployees.SelectedItem as Employee).Department;
                tbInfo.Visibility = Visibility.Hidden;
                cbDepartments.Visibility = Visibility.Visible;
                cbDepartments.SelectedIndex = (lbEmployees.SelectedItem as Employee).Department;
                lblInfo.Content = "Отд.";
            }
        }
        /// <summary>
        /// Метод, изменяющий параметры формы после выбора отдела в соответствующем списке
        /// </summary>
        public void ChoosingDepartment()
        {
            if (lbDepartments.SelectedItem != null)
            {
                tbId.Text = (lbDepartments.SelectedItem as Department).Id.ToString();
                tbInfo.Text = (lbDepartments.SelectedItem as Department).EmpCount.ToString();
                tbName.Text = (lbDepartments.SelectedItem as Department).Name.ToString();
                tbInfo.Visibility = Visibility.Visible;
                cbDepartments.Visibility = Visibility.Hidden;
                lblInfo.Content = "Кол.";
            }
        }
        /// <summary>
        /// Метод, редактирующий сотрудника после нажатия соответствующей кнопки
        /// </summary>
        public void ChangeEmployee()
        {
            int index = (lbEmployees.SelectedItem as Employee).Id;
            employees[index].Name = tbName.Text;
            employees[index].Department = Int32.Parse(lblDepartmentId.Content.ToString());
            CalcDepartments();
        }
        /// <summary>
        /// Метод, редактирующий отдел после нажатия соответствующей кнопки
        /// </summary>
        public void ChangeDepartment()
        {
            int index = (lbDepartments.SelectedItem as Department).Id;
            departments[index].Name = tbName.Text;
        }
        /// <summary>
        /// Метод, добавляющий сотрудника после нажатия соответствующей кнопки
        /// </summary>
        public void AddEmployee()
        {
            int maxId = 0;
            foreach (Employee item in employees)
                if (item.Id > maxId) maxId = item.Id;
            employees.Add(new Employee(tbName.Text, Int32.Parse(lblDepartmentId.Content.ToString()), maxId + 1));
            CalcDepartments();
        }
        /// <summary>
        /// Метод, добавляющий отдел после нажатия соответствующей кнопки
        /// </summary>
        public void AddDepartment()
        {
            int maxId = 0;
            foreach (Department item in departments)
                if (item.Id > maxId) maxId = item.Id;
            departments.Add(new Department(tbName.Text, maxId + 1));
        }
        /// <summary>
        /// Метод, удаляющий сотрудника после нажатия соответствующей кнопки
        /// </summary>
        public void RemoveEmployee()
        {
            employees.Remove(lbEmployees.SelectedItem as Employee);
        }
        /// <summary>
        /// Метод, удаляющий департамент после нажатия соответствующей кнопки, если в нем не осталось сотрудников
        /// </summary>
        public void RemoveDepartment()
        {
            if ((lbDepartments.SelectedItem as Department).EmpCount == 0)
                departments.Remove(lbDepartments.SelectedItem as Department);
            else throw new Exception("Невозможно удалить не пустой отдел. Измените отдел всех сотрудников, после этого можно удалить.");
        }
        // Методы, обрабатывающие события
        private void Window_Closed(object sender, EventArgs e)
        {
        }
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            changing?.Invoke();
            RefreshData();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            adding?.Invoke();
        }
        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            removing?.Invoke();
        }
        private void LbEmployees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            changing = ChangeEmployee;
            choosing = ChoosingEmployee;
            adding = AddEmployee;
            removing = RemoveEmployee;
            choosing?.Invoke();
        }

        private void LbDepartments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            changing = ChangeDepartment;
            choosing = ChoosingDepartment;
            adding = AddDepartment;
            removing = RemoveDepartment;
            choosing?.Invoke();
        }

        private void CbDepartments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lblDepartmentId.Content = (cbDepartments.SelectedItem as Department).Id.ToString();
        }
    }
}
