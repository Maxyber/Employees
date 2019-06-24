using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using static System.Convert;

namespace Employees.PresentEmpDep
{
    class DepartmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string departmentName = "";
            return departmentName;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int departmentId = 0;
            string departmentName = value.ToString();
            /*
            foreach (Department item in departments)
                if (item.Name == departmentName) departmentId = item.Id;
                */
            return departmentId;
        }
    }
}
