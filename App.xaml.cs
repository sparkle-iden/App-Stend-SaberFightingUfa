using MauiApp3.Page;

namespace MauiApp3
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            AdjustGlobalFontSize();

            // Подписка на изменение ориентации и разрешения экрана
            DeviceDisplay.MainDisplayInfoChanged += (s, e) => AdjustGlobalFontSize();
            MainPage = new NavigationPage(new MainPage()); ;   
        }
        private void AdjustGlobalFontSize()
        {
            double screenWidth = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;
            double dynamicFontSize = screenWidth * 0.05; // 5% от ширины экрана
            double dynamicFontSizeButton = screenWidth * 0.07; // 5% от ширины экрана

            // Обновляем глобальный ресурс
            Current.Resources["GlobalFontSize"] = dynamicFontSize;
            Current.Resources["GlobalFontSizeButton"] = dynamicFontSizeButton;
        }
    }
}
