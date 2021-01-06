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
            GetRoutes();
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
        #endregion

        #region Methods
        private async void GetRoutes()
        {
            StartBusy();
            Routes = new ObservableCollection<RouteDTO>(await routeService.GetAll());
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