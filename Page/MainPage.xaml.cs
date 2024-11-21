using MauiApp3;

namespace MauiApp3.Page
{
    public partial class MainPage : ContentPage
    {

        
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WriteLogin());
        }
    }

}
