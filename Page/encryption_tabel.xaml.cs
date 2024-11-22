using MySql.Data.MySqlClient;

namespace MauiApp3.Page;

public partial class encryption_tabel : ContentPage
{
    private Query_SQL _query_sql;
    string server_login;
    public encryption_tabel(string name)
	{
		InitializeComponent();
        _query_sql = new Query_SQL(name);
        Login_user.Text = name;
        server_login = name;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await UpdateUserMoney(); // Обновление данных о деньгах
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

    private void Answer(object sender, EventArgs e)
    {
        string answer = "Ответ";
        if(Query_answer.Text == answer){
            Answer_right.IsVisible =true;
            string questName = "Расшифровка таблички";
            _query_sql.AddQuestProgressUser(server_login, questName);

            // Выполняем действия в основном потоке
            MainThread.BeginInvokeOnMainThread(() =>
                {
                    
                    // Обновляем данные в базе данных
                    string connectionString = "Server=192.168.0.100;Database=Stend_sfu;User ID=root;Password=;Pooling=false;";
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            string query = "UPDATE User SET UserMoney = UserMoney + @coinsToAdd WHERE UserName = @name";
                            MySqlCommand command = new MySqlCommand(query, connection);
                            command.Parameters.AddWithValue("@coinsToAdd", 2000);
                            command.Parameters.AddWithValue("@name", server_login);
                            _query_sql.AddQuestProgressUser(server_login, "Археология");
                            command.ExecuteNonQuery();
                          
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Ошибка подключения: " + ex.Message);
                        }
                    }
                });

            
        }
      
       
     
    }
}