using HeatApp.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HeatApp.Converters
{
    public class NotificationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var eNotificationType = (eNotificationType)value;

            if (targetType == typeof(Color))
            {
                return GetColor(eNotificationType);
            }
            else if (targetType == typeof(string))
            {
                return GetIcon(eNotificationType);
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        private string GetIcon(eNotificationType eNotificationType)
        {
            switch (eNotificationType)
            {
                case eNotificationType.Error:
                    return IconFont.Close;

                case eNotificationType.Notification:
                    return IconFont.Bell;

                case eNotificationType.Success:
                    return IconFont.Sent;

                case eNotificationType.Warning:
                    return IconFont.Error;

                default: // Error
                    return IconFont.Close;
            }
        }

        private Color GetColor(eNotificationType eNotificationType)
        {
            string resourceName;

            switch (eNotificationType)
            {

                case eNotificationType.Notification:
                    resourceName = "NotificationColor";
                    break;

                case eNotificationType.Success:
                    resourceName = "OkColor";
                    break;

                case eNotificationType.Warning:
                    resourceName = "WarningColor";
                    break;

                default:
                    resourceName = "ErrorColor";
                    break;
            }

            return ResourceHelper.FindResource<Color>(resourceName);
        }
    }
}
