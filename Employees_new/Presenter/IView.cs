using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Employees;
using System.Collections.ObjectModel;

namespace Employees.PresentEmpDep
{
    public interface IView
    {
        ObservableCollection<Department> Departments { get; set; }
        ObservableCollection<Employee> Employees { get; set; }
        int Id { get; set; }
        int Info { get; set; }
        string ItemName { get; set; }
        bool? CheckDep { get; set; }
    }
}
