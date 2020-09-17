using HeatApp.DataService;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace HeatApp.Views.Catalog
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductHomePage : ContentPage
    {
        public ProductHomePage()
        {
            InitializeComponent();
            this.BindingContext = ProductHomeDataService.Instance.ProductHomePageViewModel;
        }
    }
}