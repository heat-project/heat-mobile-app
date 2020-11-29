using HeatApp.Behaviors;
using HeatApp.Controls.PopupPages;
using HeatApp.Interfaces;
using HeatApp.Models;
using HeatApp.Views.HeatViews;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace HeatApp.ViewModels
{
    public class SignUpViewModel : BaseViewModel
    {
        #region Constructors
        public SignUpViewModel(INavigation navigation) : base(navigation)
        {
            SetProperties();
            AddValidationRules();
        }
        #endregion

        #region Properties
        private ISecurityService SecurityService { get; set; }
        public ICommand SignUpCommand { get; set; }
        public ValidatableObject<string> FirstName { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> LastName { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> PhoneNumber { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> Email { get; set; } = new ValidatableObject<string>();
        public ValidatablePair<string> Password { get; set; } = new ValidatablePair<string>();

        #endregion

        #region Methods
        public async Task SignUp()
        {
            if (IsBusy) return;
            StartBusy();

            try
            {
                if (AreFieldsValid())
                {
                    var newUserInfo = GetNewUserInfo();
                    if ((await SecurityService.Register(newUserInfo)) != null)
                    {
                        await PopupNavigation.Instance.PushAsync(new AlertPopup("¡Su cuenta fue creada exitosamente!", Helpers.eNotificationType.Success));

                        await navigation.PopAsync();
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
        private UserDTO GetNewUserInfo()
        {
            return new UserDTO
            {
                UserName = Email.Value,
                Password = Password.Item1.Value,
                Email = Email.Value,
                Name = FirstName.Value,
                LastName = FirstName.Value,
                Phone = PhoneNumber.Value,
                Sex = "M"
            };
        }
        public void SetProperties()
        {
            SetServices();
            SetCommands();
        }
        private void SetCommands()
        {
            SignUpCommand = new Command(async () => await SignUp());
        }
        private void SetServices()
        {
            SecurityService = DependencyService.Get<ISecurityService>();
        }
        private void AddValidationRules()
        {
            FirstName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = Constants.Messages.Validation.EmptyName });
            LastName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = Constants.Messages.Validation.EmptyLastName });
            PhoneNumber.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = Constants.Messages.Validation.EmptyPhone });

            //Email Validation Rules
            Email.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = Constants.Messages.Validation.EmptyEmail });
            Email.Validations.Add(new IsValidEmailRule<string> { ValidationMessage = Constants.Messages.Validation.InvalidEmail });

            //Password Validation Rules
            Password.Item1.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = Constants.Messages.Validation.EmptyPassword });
            Password.Item2.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = Constants.Messages.Validation.EmptyConfirmPassword });
            Password.Validations.Add(new MatchPairValidationRule<string> { ValidationMessage = Constants.Messages.Validation.PasswordsDontMatch });
        }
        private bool AreFieldsValid()
        {
            bool isFirstNameValid = FirstName.Validate();
            bool isLastNameValid = LastName.Validate();
            bool isPhoneNumberValid = PhoneNumber.Validate();
            bool isEmailValid = Email.Validate();
            bool isPasswordValid = Password.Validate();

            return isFirstNameValid && isLastNameValid && isPhoneNumberValid
                && isEmailValid && isPasswordValid;
        }
        #endregion
    }
}