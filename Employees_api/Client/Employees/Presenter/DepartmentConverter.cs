using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using static System.Convert;
using Employees.PresentEmpDep;

namespace Employees
{
    class DepartmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string departmentName = ""; //  NameById(Int32.Parse(value.ToString()));
           // string departmentName = DataBase.DepNameById((value as Employee).Department);
            return departmentName;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int departmentId = 0;
            string departmentName = value.ToString();
            return departmentId;
        }
    }
}
