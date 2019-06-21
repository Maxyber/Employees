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
        public EmployeeLogic employees;
        public DepartmentLogic departments;
        public MainWindow()
        {
            InitializeComponent();
            // конец теста, удалить после завершения приложения
            AppInit();
        }
        /// <summary>
        /// Метод, проверяющий наличие всех нужных файлов баз данных и заполняющий их начальными значениями (тестовый слой)
        /// </summary>
        public void AppInit()
        {
            // Database Check
            if (!Directory.Exists("data"))
            {
                Directory.CreateDirectory(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data"));
            }
            if (!File.Exists(@"data\employees.db"))
            {
                using (var mf = XmlWriter.Create(@"data\employees.db"))
                {
                    mf.WriteStartDocument(true);
                }
            }
            if (!File.Exists(@"data\departments.db"))
            {
                using (var mf = XmlWriter.Create(@"data\departments.db"))
                {
                    mf.WriteStartDocument(true);
                }
            }
            // AddData
            employees = new EmployeeLogic(@"data\employees.db");
            departments = new DepartmentLogic(@"data\departments.db");

            for(int i =0;i<3;i++)
            departments.AddDepartment(new Department($"Подразделение_{i}", departments.NextID));
            for (int i=0; i<10; i++)
                employees.AddEmployee(new Employee($"Name_{i}", 1, employees.NextID));

            lbEmployees.ItemsSource = employees.ToList();
            lbDepartments.ItemsSource = departments.ToList();
        }
        public void ChoosingItem()
        {
            tbId.Text = "0";
            tbInfo.Text = "Info";
            tbName.Text = "Name";
            lblInfo.Content = "Count";

        }
        /// <summary>
        /// Метод, обрабатывающий событие закрытия приложения
        /// </summary>
        private void Window_Closed(object sender, EventArgs e)
        {
            // На время тестов процедура удаления файлов баз данных
            File.Delete(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"data\employees.db"));
            File.Delete(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"data\departments.db"));
            Directory.Delete(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data"));
        }
        private void BtnEditEmployee_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAddEmployee_Click(object sender, RoutedEventArgs e)
        {

        }
        private void BtnAddDepartment_Click(object sender, RoutedEventArgs e)
        {

        }
        private void BtnEditDepartment_Click(object sender, RoutedEventArgs e)
        {

        }
        private void BtnRemoveEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (lbEmployees.SelectedItem != null)
            employees.RemoveEmployee(employees.EmpList[lbEmployees.Items.IndexOf(lbEmployees.SelectedItem)]);
            lbEmployees.ItemsSource = employees.ToList();
        }
        private void BtnRemoveDepartment_Click(object sender, RoutedEventArgs e)
        {
            if (lbDepartments.SelectedItem != null)
                departments.RemoveDepartment(departments.DepList[lbDepartments.Items.IndexOf(lbDepartments.SelectedItem)]);
            lbDepartments.ItemsSource = departments.ToList();
        }
        private void LbEmployees_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (lbEmployees.SelectedItem != null)
                ChoosingItem();
        }

        private void LbEmployees_LostFocus(object sender, RoutedEventArgs e)
        {
            tbId.Text = "";
            tbInfo.Text = "";
            tbName.Text = "";
            lblInfo.Content = "";
        }
    }
}
