using HeatApp.Behaviors;
using HeatApp.Controls.PopupPages;
using HeatApp.Interfaces;
using HeatApp.Models;
using HeatApp.Views.HeatViews;
using HeatApp.Views.HeatViews.Common;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace HeatApp.ViewModels.HeatViewModels.Security
{
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel(INavigation navigation) : base(navigation)
        {
            SetProperties();
            AddValidationRules();
        }

        #region Properties
        private ISecurityService SecurityService { get; set; }
        public ICommand SignInCommand { get; set; }
        public ValidatableObject<string> Email { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> Password { get; set; } = new ValidatableObject<string>();

        #endregion

        #region Methods
        public async Task Login()
        {
            if (IsBusy) return;
            StartBusy();

            try
            {
                if (AreFieldsValid())
                {
                    var userInfo = GetUserInfo();
                    if ((await SecurityService.Login(userInfo)) != null)
                    {
                        await PopupNavigation.Instance.PushAsync(new AlertPopup("¡Ha ingresado exitosamente!", Helpers.eNotificationType.Success));

                        await navigation.PopAsync();
                        App.Current.MainPage = new RootPage();

                    }
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                EndBusy();
            }
        }
        #endregion

        #region AuxiliarMethods
        private UserDTO GetUserInfo()
        {
            return new UserDTO
            {
                UserName = Email.Value,
                Password = Password.Value
            };
        }
        public void SetProperties()
        {
            SetServices();
            SetCommands();
        }
        private void SetCommands()
        {
            SignInCommand = new Command(async () => await Login());
        }
        private void SetServices()
        {
            SecurityService = DependencyService.Get<ISecurityService>();
        }
        private void AddValidationRules()
        {
            

            //Email Validation Rules
            Email.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = Constants.Messages.Validation.EmptyEmail });
            Email.Validations.Add(new IsValidEmailRule<string> { ValidationMessage = Constants.Messages.Validation.InvalidEmail });

            //Password Validation Rules
            Password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = Constants.Messages.Validation.EmptyPassword });
        }
        private bool AreFieldsValid()
        {
            bool isEmailValid = Email.Validate();
            bool isPasswordValid = Password.Validate();

            return isEmailValid && isPasswordValid;
        }
        #endregion
    }
}
