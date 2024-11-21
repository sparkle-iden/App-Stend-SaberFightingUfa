namespace MauiApp3
{
    public partial class StartPage : ContentPage
    {
        public StartPage()
        {
            InitializeComponent();
            
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
             Navigation.PushAsync(new Page.MainPage());
        }
    }
}

