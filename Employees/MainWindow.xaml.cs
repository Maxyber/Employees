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
using System.Xml;

namespace Employees
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AppInit();
        }
        /// <summary>
        /// Метод, проверяющий наличие всех нужных файлов баз данных и заполняющий их начальными значениями (тестовый слой)
        /// </summary>
        public static void AppInit()
        {
            // FilesCheck
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
            EmployeeLogic employees = new EmployeeLogic(@"data\employees.db");
            DepartmentLogic departments = new DepartmentLogic(@"data\departments.db");
            departments.AddDepartment(new Department($"Подразделение_1", departments.NextID));
            for (int i=0; i<10; i++)
            {
                employees.AddEmployee(new Employee($"Name_{i}", 1, employees.NextID));
            }
            employees.SaveData();
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
    }
}
