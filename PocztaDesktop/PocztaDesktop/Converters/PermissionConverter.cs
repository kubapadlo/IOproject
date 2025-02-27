using System;
using System.Globalization;

namespace PocztaDesktop.Converters
{
    public class PermissionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int idUprawnienia)
            {
                return idUprawnienia switch
                {
                    1 => "Admin",
                    2 => "Kierownik",
                    4 => "Pracownik magazynu",
                    _ => "Nieznane"
                };
            }
            return "Nieznane";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

