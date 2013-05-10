namespace aurora
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using aurora.Models;

    using Microsoft.WindowsAzure.MobileServices;

    using Windows.UI.Popups;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Input;
    using Windows.UI.Xaml.Navigation;

    public sealed partial class MainPage
    {
        #region Static Fields

        private static readonly Random random = new Random();

        #endregion

        #region Constructors and Destructors

        public MainPage()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Methods

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await this.LoadAsync();
        }

        private async Task LoadAsync()
        {
            try
            {
                var tags = (from _ in await App.MobileService.GetTable<vTag>().ToCollectionAsync()
                            select
                                new TagViewModel
                                    {
                                        Id = _.id, 
                                        Title = _.title, 
                                        PostCount = _.postCount, 
                                        MostViewedPosts = _.mostViewedPosts,
                                        NewPosts = _.newPosts,
                                    }).ToList();

                double maxPosts = tags.Max(_ => _.PostCount);
                double minPosts = tags.Min(_ => _.PostCount);

                foreach (var tag in tags)
                {
                    tag.Normalize(minPosts, maxPosts);
                }

                this.DataContext = tags.OrderBy(_ => random.Next());
            }
            catch (MobileServiceInvalidOperationException e)
            {
                var dialog = new MessageDialog(e.Message) { Title = "ERROR" };
                dialog.ShowAsync();
            }
            finally
            {
                this.ProgressView.Visibility = Visibility.Collapsed;
            }
        }

        private void OnTagTapped(object sender, TappedRoutedEventArgs e)
        {
            var senderAsTextBlock = sender as TextBlock;
            if (senderAsTextBlock != null)
            {
                var tagViewModel = senderAsTextBlock.Tag as TagViewModel;
                if (tagViewModel != null)
                {
                    string title = tagViewModel.Title + " [#" + tagViewModel.Id + "]";
                    var dialog = new MessageDialog(tagViewModel.ToString()) { Title = title };
                    dialog.ShowAsync();
                }
            }
        }

        #endregion
    }
}