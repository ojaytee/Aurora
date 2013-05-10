namespace aurora
{
    using System.Threading.Tasks;

    using Microsoft.WindowsAzure.MobileServices;

    using Windows.UI.Popups;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    public sealed partial class MainPage : Page
    {
        #region Fields

        private readonly IMobileServiceTable<Posts> postsTable = App.MobileService.GetTable<Posts>();

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
                this.DataContext = await this.postsTable.OrderByDescending(_ => _.creationDate).ToCollectionAsync(20);
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
    }
}