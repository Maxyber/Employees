using System;
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
using System.Xml;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Data.SqlClient;
using Employees.PresentEmpDep;

namespace Employees
{
    // строка подключения к БД
    // Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=lesson7;Integrated Security=True;Pooling=False
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IView
    {
        Employees_server.Employees_serviceSoapClient p = new Employees_server.Employees_serviceSoapClient();
        public ObservableCollection<Employees_server.Employee> employees = new ObservableCollection<Employees_server.Employee>();
        public ObservableCollection<Employees_server.Department> departments = new ObservableCollection<Employees_server.Department>();
        object uItem;

        public MainWindow()
        {
            InitializeComponent();
            p = new Employees_server.Employees_serviceSoapClient();
            LoadData();

            btnAdd.Click += delegate { p.Add(CheckDep.Value,tbName.Text,Int32.Parse(tbInfo.Text)); };
            btnEdit.Click += delegate { p.Edit(CheckDep.Value, uItem, tbName.Text,Int32.Parse(tbInfo.Text)); if (checkDep.IsChecked == true) lbEmployees.Items.Refresh(); };
            btnRemove.Click += delegate { p.Remove(CheckDep.Value, uItem); };
            lbEmployees.SelectionChanged += delegate { checkDep.IsChecked = false; cbDepartments.Visibility = Visibility.Visible; Choosing(); };
            lbDepartments.SelectionChanged += delegate { checkDep.IsChecked = true; cbDepartments.Visibility = Visibility.Hidden; Choosing(); };
        }
        void LoadData()
        {
            Employees_server.Employee[] xEmployees = p.GetEmployees();
            Employees_server.Department[] xDepartments = p.GetDepartments();
            foreach (Employees_server.Employee item in xEmployees)
            {
                employees.Add(item);
            }
            foreach (Employees_server.Department item in xDepartments)
            {
                departments.Add(item);
            }
        }
        public Employees_server.Department VDepartment
        {
            get => (lbDepartments.SelectedItem as Employees_server.Department);
            set { }
        }
        public Employees_server.Employee VEmployee
        {
            get => (lbEmployees.SelectedItem as Employees_server.Employee);
            set { }
        }
        public int Id
        {
            get => Int32.Parse(tbId.Text);
            set => tbId.Text = value.ToString();
        }
        public string ItemName
        {
            get => tbName.Text;
            set => tbName.Text = value;
        }
        public int Info
        {
            get
            {
                if (CheckDep.Value) return Int32.Parse(tbInfo.Text);
                else return cbDepartments.SelectedIndex;
            }
            set
            {
                if (CheckDep.Value) tbInfo.Text = value.ToString();
                else
                {
                    try
                    {
                        cbDepartments.SelectedIndex = VEmployee.Department;
                    }
                    catch (NullReferenceException)
                    {
                        cbDepartments.SelectedIndex = 0;
                    }
                }
            }
        }
        public bool? CheckDep
        {
            get => checkDep.IsChecked;
            set => checkDep.IsChecked = value;
        }
        public void Choosing()
        {
            try
            {
                if (CheckDep.Value)
                {
                    uItem = lbDepartments.SelectedItem as object;
                    tbId.Text = (lbDepartments.SelectedItem as Employees_server.Department).Id.ToString();
                    tbName.Text = (lbDepartments.SelectedItem as Employees_server.Department).Name;
                    tbInfo.Text = (lbDepartments.SelectedItem as Employees_server.Department).EmpCount.ToString();
                }
                else
                {
                    uItem = lbEmployees.SelectedItem as object;
                    tbId.Text = (lbEmployees.SelectedItem as Employees_server.Employee).Id.ToString();
                    tbName.Text = (lbEmployees.SelectedItem as Employees_server.Employee).Name;
                    tbInfo.Text = (lbEmployees.SelectedItem as Employees_server.Employee).Department.ToString();
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                tbId.Text = "";
                tbName.Text = "";
                tbInfo.Text = "";
            }
        }
        // Методы, обрабатывающие события
        private void Window_Closed(object sender, EventArgs e)
        {
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            // p.Save();
        }
    }
}
