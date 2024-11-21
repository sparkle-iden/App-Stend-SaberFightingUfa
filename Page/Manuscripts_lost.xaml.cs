using MySql.Data.MySqlClient;
using ZXing.Net.Maui;
using ZXing.Net.Maui.Controls;

namespace MauiApp3.Page;

public partial class Manuscripts_lost : ContentPage
{

    string server_login;
    private Query_SQL _query_sql;
    public Manuscripts_lost(string name)
	{
		InitializeComponent();
        _query_sql = new Query_SQL(name);
       
        Login_user.Text = name;
        server_login = name;
    }

    private async Task DisableButtonsIfNeededAsync()
    {
        var lostManuscripts = await _query_sql.GetLostManuscriptsAsync(server_login);
        for (int i = 1; i <= 10; i++)
        {
            var button = (ImageButton)FindByName($"manuscript_{i}");
            if (button != null)
            {
                button.IsEnabled = !lostManuscripts.Contains(i);
            }
        }
    }
    // Метод для получения и отображения денег пользователя
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        UpdateUserMoney(); // Обновление данных о деньгах
        DisableButtonsIfNeededAsync();
    }

    // Метод для получения и отображения денег пользователя
    private async Task UpdateUserMoney()
    {
        try
        {
            // Получение денег пользователя асинхронно
            int userMoney = await _query_sql.GetMoneyUser(server_login);
            User_Money.Text = userMoney.ToString();
        }
        catch (Exception ex)
        {
            // Логирование ошибок
            await DisplayAlert("Ошибка", $"Не удалось обновить деньги пользователя: {ex.Message}", "OK");
        }
    }
    private void Nazad(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }

    private void QR_Scan(object sender, EventArgs e)
    {
        string Login = Login_user.Text;
     
        Navigation.PushAsync(new Page.Scan(Login));

    }
}