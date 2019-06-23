using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using static System.Convert;

namespace Employees
{
    class DepartmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string departmentName = "";
            var departmentId = ToInt32(value);
            
            return departmentName;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string departmentName = value.ToString();
            int departmentId = ToInt32(value);
            return departmentId;
        }
    }
}
