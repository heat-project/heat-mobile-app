using System;
using System.Collections.Generic;
using HeatApp.ViewModels.HeatViewModels.Common;
using Xamarin.Forms;

namespace HeatApp.Views.HeatViews.Help
{
    public partial class HelpPage : ContentPage
    {
        public HelpPage()
        {
            InitializeComponent();
            BindingContext = new HelpViewModel(Navigation);
        }
    }
}
