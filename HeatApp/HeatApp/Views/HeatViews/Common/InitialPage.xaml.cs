using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HeatApp.Views.HeatViews.Common
{
    public partial class InitialPage : ContentPage
    {
        public InitialPage()
        {
            InitializeComponent();
            BindingContext = new InitialViewModel(navigation: Navigation);
        }
    }
}