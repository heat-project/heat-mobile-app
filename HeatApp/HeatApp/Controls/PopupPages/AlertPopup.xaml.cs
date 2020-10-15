using HeatApp.Converters;
using HeatApp.Helpers;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace HeatApp.Controls.PopupPages
{
    public partial class AlertPopup : PopupPage
    {
        #region Variables
        private static NotificationConverter converter;
        #endregion

        #region Constructors
        public AlertPopup()
        {
            InitializeComponent();
            converter = new NotificationConverter();
        }
        public AlertPopup(string message, eNotificationType type)
        {
            InitializeComponent();
            converter = new NotificationConverter();
            SetNotificationTheme(message, type);
        }
        #endregion

        #region Methods
        private void SetNotificationTheme(string message, eNotificationType type)
        {
            imgAlert.Text = converter.Convert(type, typeof(string), null, null).ToString();
            Mainstk.BackgroundColor = (Color)converter.Convert(type, typeof(Color), null, null);
            LblMsg.Text = message;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            HidePopup();
        }
        private async void HidePopup()
        {
            await Task.Delay(4000);
            if (PopupNavigation.Instance.PopupStack.Any())
                await PopupNavigation.Instance.PopAllAsync();
        }
        #endregion

    }
}