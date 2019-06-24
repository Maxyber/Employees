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
using Employees.PresentEmpDep;

namespace Employees
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IView
    {
        Presenter p;
        public MainWindow()
        {
            InitializeComponent();
            p = new Presenter(this);

            btnAdd.Click += delegate { p.Add(); };
            btnEdit.Click += delegate { p.Edit(); };
            btnRemove.Click += delegate { p.Remove(); };
            lbEmployees.SelectionChanged += delegate { p.ChoosingEmployee(); };
        }
        public ObservableCollection<Department> Departments
        {
            get => (lbDepartments.SelectedItem as ObservableCollection<Department>);
            set => lbDepartments.ItemsSource = value;
        }
        public ObservableCollection<Employee> Employees
        {
            get => (lbDepartments.SelectedItem as ObservableCollection<Employee>);
            set => lbDepartments.ItemsSource = value;
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
            get => Int32.Parse(tbInfo.Text);
            set => tbInfo.Text = value.ToString();
        }
        public bool? CheckDep
        {
            get => checkDep.IsChecked;
            set => checkDep.IsChecked = value;
        }


        /// <summary>
        /// Метод, обновляющий списки в листбоксах после изменения данных (самостоятельно не обновляется почему-то)
        /// </summary>
        public void RefreshData()
        {
            lbEmployees.Items.Refresh();
            lbDepartments.Items.Refresh();
        }
        /*
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
        */
        // Методы, обрабатывающие события
        private void Window_Closed(object sender, EventArgs e)
        {
        }
        /*
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
        */
    }
}
