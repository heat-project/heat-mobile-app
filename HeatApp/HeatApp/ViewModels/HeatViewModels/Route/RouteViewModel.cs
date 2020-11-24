using HeatApp.Interfaces.Routes;
using HeatApp.Models.HeatModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HeatApp.ViewModels.HeatViewModels.Route
{
    public class RouteViewModel : BaseViewModel
    {
        #region Constructors
        public RouteViewModel(INavigation navigation) : base(navigation)
        {
            SetServices();
            GetRoutes().Wait();
        }
        #endregion

        #region Services
        private IRouteService routeService;
        #endregion

        #region Propertties
        private ObservableCollection<RouteDTO> routes;
        public ObservableCollection<RouteDTO> Routes
        {
            get => routes;
            set => SetProperty(ref routes, value);
        }
        private ObservableCollection<RouteDTO> routesFavorite;
        public ObservableCollection<RouteDTO> RoutesFavorite
        {
            get => routesFavorite;
            set => SetProperty(ref routesFavorite, value);
        }
        private ObservableCollection<RouteDTO> routesSeen;
        public ObservableCollection<RouteDTO> RoutesSeen
        {
            get => routesSeen;
            set => SetProperty(ref routesSeen, value);
        }
        #endregion

        #region Methods
        private async Task GetRoutes()
        {
            StartBusy();
            Routes = new ObservableCollection<RouteDTO>(await routeService.GetAll());
            RoutesFavorite = new ObservableCollection<RouteDTO>(await routeService.GetFavorites());
            RoutesSeen = new ObservableCollection<RouteDTO>(await routeService.GetPreviouslySeen());
            EndBusy();
        }
        #endregion

        #region AuxiliarMethods
        private void SetServices()
        {
            routeService = DependencyService.Get<IRouteService>();
        }
        #endregion
    }
}