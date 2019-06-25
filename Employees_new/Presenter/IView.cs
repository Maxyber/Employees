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
        Department VDepartment { get; set; }
        Employee VEmployee { get; set; }
        int Id { get; set; }
        int Info { get; set; }
        string ItemName { get; set; }
        bool? CheckDep { get; set; }
        int Index { get; set; }
    }
}
