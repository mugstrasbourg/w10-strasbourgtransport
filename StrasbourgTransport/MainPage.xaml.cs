using StrasbourgTransport.Models;
using StrasbourgTransport.ViewModels;
using StrasbourgTransport.Views;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace StrasbourgTransport
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage(Frame frame)
        {
            this.InitializeComponent();
            this.StrasSplitView.Content = frame;

            frame.Navigated += Frame_Navigated;

            this.HoraireRadioButton.IsChecked = true;
        }

        private void Frame_Navigated(object sender, Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            var frame = sender as Frame;

            // Pour récupérer l'élément sélectionner dans la page des Favoris
            // On navigue ensuite vers la page des horaires pré-chargés
            if (frame.Content is FavorisPage)
            {
                var favorisPage = frame.Content as FavorisPage;
                favorisPage.FavoriteLV.ItemClick += FavoriteLV_ItemClick;
            }
        }

        private void FavoriteLV_ItemClick(object sender, ItemClickEventArgs e)
        {
            var selectedFavorite = e.ClickedItem as StopResult;
            var frame = this.StrasSplitView.Content as Frame;

            frame.Navigate(typeof(HomePage), selectedFavorite);

        }

        private void RadioMenuButton_Click(object sender, RoutedEventArgs e)
        {
            this.StrasSplitView.IsPaneOpen = !this.StrasSplitView.IsPaneOpen;
        }

        private void DontCheck(object s, RoutedEventArgs e)
        {
            (s as RadioButton).IsChecked = false;
        }

        private void RadioInfoTraficButton_Click(object sender, RoutedEventArgs e)
        {
            var frame = this.StrasSplitView.Content as Frame;

            frame.Navigate(typeof(InfoTraficPage));
        }

        private void RadioHorairesButton_Click(object sender, RoutedEventArgs e)
        {
            var frame = this.StrasSplitView.Content as Frame;

            frame.Navigate(typeof(HomePage));
        }

        private void RadioAboutButton_Click(object sender, RoutedEventArgs e)
        {
            var frame = this.StrasSplitView.Content as Frame;

            frame.Navigate(typeof(AboutPage));
        }

        private void RadioFavorisButton_Click(object sender, RoutedEventArgs e)
        {
            var frame = this.StrasSplitView.Content as Frame;

            frame.Navigate(typeof(FavorisPage));
        }
    }
}
