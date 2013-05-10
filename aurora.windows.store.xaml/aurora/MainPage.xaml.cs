namespace aurora
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.WindowsAzure.MobileServices;

    using Windows.UI.Popups;
    using Windows.UI.Text;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Input;
    using Windows.UI.Xaml.Navigation;

    public sealed partial class MainPage : Page
    {
        #region Fields

        private readonly IMobileServiceTable<vTagCloud> vTagCloudTable = App.MobileService.GetTable<vTagCloud>();

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
            var tags = new List<TagViewModel>();

            try
            {
                var tagCloud = await this.vTagCloudTable.ToCollectionAsync();
                double maxPosts = tagCloud.Max(_ => _.posts);
                double minPosts = tagCloud.Min(_ => _.posts);

                var random = new Random();

                foreach (var vTag in tagCloud.OrderBy(_ => random.Next()))
                {
                    var vm = new TagViewModel();
                    vm.Id = vTag.id;
                    vm.Name = vTag.tagName;
                    vm.Posts = vTag.posts;
                    vm.NormalizedPosts = (vm.Posts - minPosts) / maxPosts;
                    vm.FontSize = 18.0 + (54.0 * vm.NormalizedPosts);
                    if (vm.NormalizedPosts > 0.75)
                    {
                        vm.FontWeight = FontWeights.Black;
                    }
                    else if (vm.NormalizedPosts > 0.50)
                    {
                        vm.FontWeight = FontWeights.Normal;
                    }
                    else
                    {
                        vm.FontWeight = FontWeights.Light;
                    }
                    double leftMargin = 10.0 + random.Next(50);
                    vm.Margin = new Thickness(leftMargin, 0, 10, 0);

                    int alignment = random.Next(3);
                    if (alignment == 0)
                        vm.VerticalAlignment = VerticalAlignment.Top;
                    else if (alignment == 1)
                        vm.VerticalAlignment = VerticalAlignment.Center;
                    else
                        vm.VerticalAlignment = VerticalAlignment.Bottom;

                    tags.Add(vm);
                }

                this.DataContext = tags;
            }
            catch (MobileServiceInvalidOperationException e)
            {
                var dialog = new MessageDialog(e.Message);
                dialog.Title = "ERROR";
                dialog.ShowAsync();
            }
            finally
            {
                this.ProgressRing.IsActive = false;
            }
        }

        #endregion

        private void TagTapped(object sender, TappedRoutedEventArgs e)
        {
            TextBlock button = sender as TextBlock;
            if (button == null) return;
            TagViewModel vm = button.Tag as TagViewModel;
            if (vm == null) return;

            MessageDialog dialog = new MessageDialog(vm.Name);
            dialog.ShowAsync();
        }
    }
}