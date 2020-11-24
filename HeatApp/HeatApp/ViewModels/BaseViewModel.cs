using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace HeatApp.ViewModels
{
    /// <summary>
    /// This viewmodel extends in another viewmodels.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region Variables
        public INavigation navigation;
        #endregion

        #region Constructors
        public BaseViewModel() { }
        public BaseViewModel(INavigation navigation)
        {
            this.navigation = navigation;
        }
        #endregion

        #region Event handler

        /// <summary>
        /// Occurs when the property is changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Commands
        private ICommand closePageCommand;
        public ICommand ClosePageCommand => closePageCommand ?? new Command(async () => await navigation.PopAsync());
        private ICommand closeModalCommand;
        public ICommand CloseModalCommand => closeModalCommand ?? new Command(async () => await navigation.PopModalAsync());
        #endregion

        #region Properties
        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                SetProperty(ref _title, value);
            }
        }
        private string _loadingMessage;
        public string LoadingMessage
        {
            get
            {
                return _loadingMessage;
            }
            private set
            {
                SetProperty(ref _loadingMessage, value);
            }
        }
        private bool _isBusy;
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            private set
            {
                SetProperty(ref _isBusy, value);
            }
        }
        public bool IsNotBusy
        {
            get
            {
                return !IsBusy;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// The PropertyChanged event occurs when changing the value of property.
        /// </summary>
        /// <param name="propertyName">The PropertyName</param>
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }
            storage = value;
            NotifyPropertyChanged(propertyName);

            return true;
        }
        public void SetLoadingMessage(string message = Constants.Messages.Loading)
            => LoadingMessage = message;
        public void StartBusy()
            => IsBusy = true;
        public void EndBusy()
            => IsBusy = false;
        #endregion
    }
}