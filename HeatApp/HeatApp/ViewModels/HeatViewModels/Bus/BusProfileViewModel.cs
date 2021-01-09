using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HeatApp.Controls.PopupPages;
using HeatApp.Helpers;
using HeatApp.Interfaces;
using HeatApp.Models;
using HeatApp.Models.HeatModels.Bus;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace HeatApp.ViewModels
{
    public class BusProfileViewModel : BaseViewModel
    {
        #region Constructors
        public BusProfileViewModel(INavigation navigation, BusDTO bus, Action<BusDTO> followRoute) : base(navigation)
        {
            Bus = bus;
            OnFollowRoute = followRoute;
            SetServices();
            GetBusInfo(bus);
        }
        #endregion

        #region Properties
        private Action<BusDTO> OnFollowRoute;
        private BusDTO Bus;

        private BusInfoDTO busInfo;
        public BusInfoDTO BusInfo
        {
            get => busInfo;
            set => SetProperty(ref busInfo, value);
        }

        private bool showListAtt;
        public bool ShowListAtt
        {
            get => showListAtt;
            set => SetProperty(ref showListAtt, value);

        }

        private bool showComments;
        public bool ShowComments
        {
            get => showComments;
            set => SetProperty(ref showComments, value);

        }


        private bool showFollowRoute = true;
        public bool ShowFollowRoute
        {
            get => showFollowRoute;
            set => SetProperty(ref showFollowRoute, value);

        }

        private bool showNewComment = false;
        public bool ShowNewComment
        {
            get => showNewComment;
            set => SetProperty(ref showNewComment, value);

        }

        private long commentCount = 0;
        public long CommentCount
        {
            get => commentCount;
            set => SetProperty(ref commentCount, value);

        }

        private ObservableCollection<ReviewDTO> reviews;
        public ObservableCollection<ReviewDTO> Reviews
        {
            get => reviews;
            set => SetProperty(ref reviews, value);

        }

        private ReviewDTO newReview;
        public ReviewDTO NewReview
        {
            get => newReview;
            set => SetProperty(ref newReview, value);

        }
        #endregion

        #region Services
        private IBusService BusService;
        private IReviewService ReviewService;
        #endregion

        #region Commands

        private ICommand followBusCommand;
        public ICommand FollowBusCommand => (followBusCommand ?? new Command(async () => await FollowRoute()));

        private ICommand goToCommentsCommand;
        public ICommand GoToCommentsCommand => (goToCommentsCommand ?? new Command(GoToCommments));

        private ICommand closeCommentsCommand;
        public ICommand CloseCommentsCommand => (closeCommentsCommand ?? new Command(CloseCommments));

        private ICommand loadCommentsCommand;
        public ICommand LoadCommentsCommand => (loadCommentsCommand ?? new Command(async () => await LoadComments()));

        private ICommand reportReviewCommand;
        public ICommand ReportReviewCommand => (reportReviewCommand ?? new Command<ReviewDTO>(async (review) => await ReportReview(review)));

        private ICommand newCommentCommand;
        public ICommand NewCommentCommand => (newCommentCommand ?? new Command(GoToNewComment));

        private ICommand cancelNewCommentCommand;
        public ICommand CancelNewCommentCommand => (cancelNewCommentCommand ?? new Command(CancelNewComment));

        private ICommand commentCommand;
        public ICommand CommentCommand => (commentCommand ?? new Command(async () => await Comment()));
        #endregion

        #region Methods

        private void CancelNewComment()
        {
            ShowNewComment = false;
        }

        private void GoToNewComment()
        {
            NewReview = new ReviewDTO();
            ShowNewComment = true;
        }

        private async Task Comment()
        {
            if (IsBusy) return;
            try
            {
                StartBusy();


                if (string.IsNullOrEmpty(NewReview.Text) || string.IsNullOrWhiteSpace(NewReview.Text))
                {
                    await PopupNavigation.Instance.PushAsync(new AlertPopup("¡Debe espcificar el comentario!", Helpers.eNotificationType.Error));
                }
                else
                {
                    NewReview.User = Settings.Email;
                    NewReview.BusID = Bus.ID;
                    bool isSaved = await ReviewService.Save(NewReview);

                    if (isSaved)
                    {
                        await PopupNavigation.Instance.PushAsync(new AlertPopup("¡Comentario guardado!", Helpers.eNotificationType.Success));
                        NewReview = new ReviewDTO();
                        CancelNewComment();
                        await LoadComments(true);
                    }
                    else
                    {
                        await PopupNavigation.Instance.PushAsync(new AlertPopup("¡Este comentario no pudo ser guardado!", Helpers.eNotificationType.Error));
                    }
                }
            }
            catch (Exception ex)
            {
                await PopupNavigation.Instance.PushAsync(new AlertPopup("¡Error guardando comentario!", Helpers.eNotificationType.Error));
            }
            finally
            {
                EndBusy();
            }
        }

        private async Task ReportReview(ReviewDTO review)
        {
            if (IsBusy) return;
            try
            {
                StartBusy();
                var types = await ReviewService.GetTypeReport();
                var selection = await App.Current.MainPage.DisplayActionSheet("Reportar comentario", "Cancelar", null, types.Select(a => a.Description).ToArray());

                var selected = types.Where(p => p.Description == selection).FirstOrDefault();

                if (selected != null)
                {
                    bool isSaved = await ReviewService.Report(new ReportDTO
                    {
                        ReviewID = review.ID,
                        User = Settings.Email,
                        Text = $"Reportado por {Settings.Email}",
                        TypeID = selected.ID

                    });

                    if (isSaved)
                    {
                        await PopupNavigation.Instance.PushAsync(new AlertPopup("¡Comentario Reportado!", Helpers.eNotificationType.Success));
                        Reviews.Remove(review);
                    }
                    else
                    {
                        await PopupNavigation.Instance.PushAsync(new AlertPopup("¡Este comentario no pudo ser reportado!", Helpers.eNotificationType.Error));
                    }
                }
            }
            catch (Exception ex)
            {
                await PopupNavigation.Instance.PushAsync(new AlertPopup("¡Error buscando los tipos de reporte de comentarios!", Helpers.eNotificationType.Error));
            }
            finally
            {
                EndBusy();
            }
        }

        private async Task LoadComments(bool skipBusy = false)
        {
            if (IsBusy && !skipBusy) return;
            try
            {
                StartBusy();

                var reviews = await ReviewService.Get(Bus.ID);

                if ((reviews?.Any()).GetValueOrDefault(false))
                {

                    Reviews.Clear();
                    BusInfo.Reviews = reviews;
                    foreach (var item in reviews)
                    {
                        Reviews.Add(item);
                    }
                    CommentCount = reviews.LongCount();
                }
            }
            catch (Exception ex)
            {
                await PopupNavigation.Instance.PushAsync(new AlertPopup("¡Error buscando los comentarios!", Helpers.eNotificationType.Error));
            }
            finally
            {
                EndBusy();
            }
        }

        private void GoToCommments()
        {
            ShowFollowRoute = false;
            ShowComments = true;
        }

        private void CloseCommments()
        {
            ShowComments = false;
            ShowNewComment = false;
            ShowFollowRoute = true;
        }

        private async void GetBusInfo(BusDTO bus)
        {
            if (IsBusy) return;
            try
            {
                StartBusy();

                BusInfo = await BusService.GetByID(bus.ID);
                ShowListAtt = (BusInfo?.Atributtes?.Any()).GetValueOrDefault(false);
                if ((BusInfo?.Reviews?.Any()).GetValueOrDefault(false))
                {
                    Reviews = new ObservableCollection<ReviewDTO>(BusInfo.Reviews);
                    CommentCount = BusInfo.Reviews.LongCount();
                }
            }
            catch (Exception ex)
            {
                await PopupNavigation.Instance.PushAsync(new AlertPopup("¡Error buscando la información del vehiculo!", Helpers.eNotificationType.Error));
            }
            finally
            {
                EndBusy();
            }
        }
        private async Task FollowRoute()
        {
            await navigation.PopModalAsync();
            OnFollowRoute(new BusDTO
            {
                ID = BusInfo.ID,
                Location = BusInfo.Location
            });
        }
        #endregion

        #region AuxMethods
        private void SetServices()
        {
            BusService = DependencyService.Get<IBusService>();
            ReviewService = DependencyService.Get<IReviewService>();
        }
        #endregion
    }
}
