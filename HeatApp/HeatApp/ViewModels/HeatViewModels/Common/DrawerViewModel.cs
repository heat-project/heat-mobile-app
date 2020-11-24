using HeatApp.Views.HeatViews.Routes;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using ProfileModel = HeatApp.Models.Profile;

namespace HeatApp.ViewModels.HeatViewModels.Common
{
    /// <summary>
    /// ViewModel for social profile pages.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class DrawerViewModel : BaseViewModel
    {
        #region Fields

        private ObservableCollection<ProfileModel> menu;

        private string profileImage;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="SocialProfileViewModel" /> class.
        /// </summary>
        public DrawerViewModel(INavigation navigation) : base(navigation)
        {
            //this.HeaderImagePath = "Album2.png";
            ProfileImage = "ProfileImage16.png";

            Menu = new ObservableCollection<ProfileModel>()
            {
                new ProfileModel { Name = "Rutas", ImagePath = "routes.png" },
                new ProfileModel { Name = "Ayuda", ImagePath = "help.png" },
                new ProfileModel { Name = "Notificaciones", ImagePath = "notif.png" },
                new ProfileModel { Name = "Configuraciones", ImagePath = "settings.png" },
            };

            MenuItemTapCommand = new Command<ProfileModel>(async (profile) => await OnMenuItemTapped(profile));
        }

        #endregion

        #region Commands
        /// <summary>
        /// Gets or sets the command that is executed when the profile item is tapped.
        /// </summary>
        public ICommand MenuItemTapCommand { get; set; }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the interests collection.
        /// </summary>
        public ObservableCollection<ProfileModel> Menu
        {
            get
            {
                return menu;
            }

            set
            {
                menu = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the connections collection.
        /// </summary>
        //public ObservableCollection<ProfileModel> Connections
        //{
        //    get
        //    {
        //        return this.connnections;
        //    }

        //    set
        //    {
        //        this.connnections = value;
        //        this.NotifyPropertyChanged();
        //    }
        //}

        /// <summary>
        /// Gets or sets the header image path.
        /// </summary>
        //public string HeaderImagePath
        //{
        //    get { return App.BaseImageUrl + this.headerImagePath; }
        //    set { this.headerImagePath = value; }
        //}

        /// <summary>
        /// Gets or sets the profile image.
        /// </summary>
        public string ProfileImage
        {
            get { return App.BaseImageUrl + profileImage; }
            set { profileImage = value; }
        }

        /// <summary>
        /// Gets or sets the background image.
        /// </summary>
        //public string BackgroundImage
        //{
        //    get { return App.BaseImageUrl + this.backgroundImage; }
        //    set { this.backgroundImage = value; }
        //}

        /// <summary>
        /// Gets or sets the profile name
        /// </summary>
        public string ProfileName { get; set; }

        /// <summary>
        /// Gets or sets the designation
        /// </summary>
        public string Designation { get; set; }

        /// <summary>
        /// Gets or sets the state
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the country
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the about
        /// </summary>
        public string About { get; set; }

        /// <summary>
        /// Gets or sets the posts count
        /// </summary>
        public int PostsCount { get; set; }

        /// <summary>
        /// Gets or sets the followers count
        /// </summary>
        public int FollowersCount { get; set; }

        /// <summary>
        /// Gets or sets the followings count
        /// </summary>
        public int FollowingCount { get; set; }

        #endregion

        #region Methods
        private async Task OnMenuItemTapped(ProfileModel profile)
        {
            switch (profile.Name)
            {
                case "Rutas":
                    await navigation.PushModalAsync(new RoutePage());
                    break;
            }

        }
        #endregion
    }
}
