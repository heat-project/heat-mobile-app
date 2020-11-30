using HeatApp.Models;
using HeatApp.ViewModels;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace HeatApp
{
    public class ConfigViewModel : BaseViewModel
    {
        #region Fields

        /// <summary>
        /// To store the health profile data collection.
        /// </summary>
        private ObservableCollection<HealthProfile> cardItems;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="HealthProfileViewModel" /> class.
        /// </summary>
        public ConfigViewModel(INavigation navigation) : base(navigation)
        {
            cardItems = new ObservableCollection<HealthProfile>()
            {
                new HealthProfile()
                {
                    Category = "Correo Electronico",
                    CategoryValue = "cacosta9822@gmail",
                    ImagePath = "CaloriesEaten.svg"
                },
                new HealthProfile()
                {
                    Category = "Telefono",
                    CategoryValue = "(829)-973-5865",
                    ImagePath = "HeartRate.svg"
                }
            };

            this.ProfileImage = App.BaseImageUrl + "ProfileImage16.png";
            this.ProfileName = "Carlos Acosta";
            this.State = "Santo Domingo";
            this.Country = "DO";
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the health profile items collection.
        /// </summary>
        public ObservableCollection<HealthProfile> CardItems
        {
            get
            {
                return this.cardItems;
            }

            set
            {
                this.cardItems = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the profile image.
        /// </summary>
        public string ProfileImage { get; set; }

        /// <summary>
        /// Gets or sets the profile name.
        /// </summary>
        public string ProfileName { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the age.
        /// </summary>
        public string Age { get; set; }

        /// <summary>
        /// Gets or sets the weight.
        /// </summary>
        public string Weight { get; set; }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        public string Height { get; set; }

        #endregion
    }
}