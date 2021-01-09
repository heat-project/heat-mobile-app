using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeatApp.Models;
using HeatApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HeatApp.Views.HeatViews.Bus
{
    public partial class BusProfilePage : ContentPage
    {
        private Command OnTap => new Command(() => OnItemTapped());
        private const uint AnimationDuration = 250;
        private bool _processingTag = false;

        public BusProfilePage(BusDTO bus, Action<BusDTO> followRoute)
        {
            InitializeComponent();
            BindingContext = new BusProfileViewModel(navigation: Navigation, bus: bus, followRoute: followRoute);
            var recognizer = new TapGestureRecognizer();
            grid.GestureRecognizers.Add(recognizer);
            recognizer.Command = OnTap;
        }

        private async void SfButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void OnItemTapped()
        {
            if (_processingTag)
            {
                return;
            }

            _processingTag = true;

            await AnimateItem(grid, AnimationDuration);
            if ((BindingContext as BusProfileViewModel).GoToCommentsCommand.CanExecute(null))
                (BindingContext as BusProfileViewModel).GoToCommentsCommand.Execute(null);

            _processingTag = false;
        }

        private async Task AnimateItem(View uiElement, uint duration)
        {
            var originalOpacity = uiElement.Opacity;
            await uiElement.FadeTo(.5, duration / 2, Easing.CubicIn);
            await uiElement.FadeTo(originalOpacity, duration / 2, Easing.CubicIn);
        }
    }
}