﻿using System;
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
        Employees.Employees_serviceSoapClient p = new Employees.Employees_serviceSoapClient();
        public ObservableCollection<Employee> employees;
        public ObservableCollection<Department> departments;
        public MainWindow()
        {
            InitializeComponent();
            p = new Employees.Employees_serviceSoapClient();
            LoadData();

            btnAdd.Click += delegate { p.Add(); };
            btnEdit.Click += delegate { p.Edit(); if (checkDep.IsChecked == true) lbEmployees.Items.Refresh(); };
            btnRemove.Click += delegate { p.Remove(); };
            lbEmployees.SelectionChanged += delegate { checkDep.IsChecked = false; cbDepartments.Visibility = Visibility.Visible; p.Choosing(); };
            lbDepartments.SelectionChanged += delegate { checkDep.IsChecked = true; cbDepartments.Visibility = Visibility.Hidden; p.Choosing(); };
        }
        void LoadData()
        {
            Employee[] xEmployees = p.GetEmployees() as Employee[];
            ObservableCollection<Employee> employees = new ObservableCollection<Employee>();
            foreach (Employee item in xEmployees)
            {

            }
        }
        public Department VDepartment
        {
            get => (lbDepartments.SelectedItem as Department);
            set { }
        }
        public Employee VEmployee
        {
            get => (lbEmployees.SelectedItem as Employee);
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
        public int Index
        {
            get
            {
                try
                {
                    if (CheckDep.Value) return p.GetDepartments().IndexOf(VDepartment);
                    else return p.GetEmployees().IndexOf(VEmployee);
                }
                catch (NullReferenceException)
                {
                    return 0;
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("MainWindow.Index exception");
                    return 0;
                }
            }
            set
            {
            }
        }
        // Методы, обрабатывающие события
        private void Window_Closed(object sender, EventArgs e)
        {
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            p.Save();
        }
    }
}