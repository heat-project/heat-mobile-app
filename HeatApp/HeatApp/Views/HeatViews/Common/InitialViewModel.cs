﻿using HeatApp.ViewModels;
using HeatApp.Views.HeatViews;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace HeatApp
{
    public class InitialViewModel: BaseViewModel
    {
        #region Constructors
        public InitialViewModel(INavigation navigation) : base(navigation: navigation)
        {
            this.navigation = navigation;
        }
        #endregion

        #region Commands
        private ICommand goToLoginCommand;
        public ICommand GoToLoginCommand => goToLoginCommand ?? (goToLoginCommand = new Command(async () => await GoToLogin()));
        #endregion

        #region Methods
        public async Task GoToLogin() 
        {
            try
            {
                await navigation.PushAsync(new LoginPage());
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        #endregion
    }
}