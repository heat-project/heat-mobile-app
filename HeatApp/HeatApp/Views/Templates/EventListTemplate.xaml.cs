using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace HeatApp.Views.Templates
{
    /// <summary>
    /// Event list template.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventListTemplate : Grid
    {
        public EventListTemplate()
        {
            InitializeComponent();
        }
    }
}