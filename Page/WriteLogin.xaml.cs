using MySql.Data.MySqlClient;
using System.Threading.Tasks;

namespace MauiApp3.Page
{
    public partial class WriteLogin : ContentPage
    {
        private Query_SQL _query_sql;
        private bool _isRequestInProgress = false; // Флаг активности запроса

        public WriteLogin()
        {
            InitializeComponent();
        }

        private async void CounterBtn_Clicked(object sender, EventArgs e)
        {
            if (_isRequestInProgress) return; // Блокируем повторный ввод

            _isRequestInProgress = true; // Устанавливаем флаг
            ToggleUI(false); // Отключаем элементы интерфейса

            try
            {
                string login = Login_entry.Text;
                if (string.IsNullOrEmpty(login))
                {
                    await DisplayAlert("Ошибка", "Введите имя.", "OK");
                    return;
                }

                if (string.IsNullOrEmpty(Password_entry.Text))
                {
                    await DisplayAlert("Ошибка", "Введите пин-код.", "OK");
                    return;
                }

                if (!int.TryParse(Password_entry.Text, out int password))
                {
                    await DisplayAlert("Ошибка", "Пин-код должен быть числом.", "OK");
                    return;
                }

                _query_sql = new Query_SQL(login);
                await SaveLoginToDatabaseAsync(login, password);
                Preferences.Set("UserName", login);
            }
            finally
            {
                _isRequestInProgress = false; // Сбрасываем флаг
                ToggleUI(true); // Включаем элементы интерфейса
            }
        }

        private async Task SaveLoginToDatabaseAsync(string login, int password)
        {
            string connectionString = "Server=server269.hosting.reg.ru;Database=u2917647_Sparkle;User ID=u2917647_default;Password=1tB6J7OD3cmt3JD1;Charset=utf8mb4;";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    string query = "INSERT INTO User (UserName, UserMoney, Password) VALUES (@login, 0, @password)";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@login", login);
                        command.Parameters.AddWithValue("@password", password);
                        await _query_sql.InitializeUserQuests(login);
                        await command.ExecuteNonQueryAsync();

                        await Navigation.PushAsync(new Page.Quest(login));
                    }
                }
            }
            catch
            {
                await DisplayAlert("Ошибка", "Такой ник уже существует, введите другой.", "OK");
            }
        }

        private async void Return_Button(object sender, EventArgs e)
        {
            if (_isRequestInProgress) return; // Блокируем повторный ввод

            _isRequestInProgress = true;
            ToggleUI(false);

            try
            {
                if (string.IsNullOrEmpty(Login_entry.Text))
                {
                    await DisplayAlert("Ошибка", "Введите ник.", "OK");
                    return;
                }

                if (string.IsNullOrEmpty(Password_entry.Text))
                {
                    await DisplayAlert("Ошибка", "Введите пин-код.", "OK");
                    return;
                }

                string connectionString = "Server=server269.hosting.reg.ru;Database=u2917647_Sparkle;User ID=u2917647_default;Password=1tB6J7OD3cmt3JD1;Charset=utf8mb4;";
                string query = "SELECT Password FROM User WHERE UserName = @UserName";
                string enteredPassword = Password_entry.Text;
                string login = Login_entry.Text;
                string storedPassword = "0";

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@UserName", login);
                        object result = await cmd.ExecuteScalarAsync();
                        if (result != null)
                        {
                            storedPassword = Convert.ToString(result);
                        }
                    }
                }

                if (enteredPassword == storedPassword)
                {
                    await Navigation.PushAsync(new Page.Quest(login));
                }
                else
                {
                    await DisplayAlert("Ошибка", "Неправильный ник или пин-код.", "OK");
                }
            }
            finally
            {
                _isRequestInProgress = false;
                ToggleUI(true);
            }
        }

        private void ToggleUI(bool isEnabled)
        {
            Login_entry.IsEnabled = isEnabled;
            Password_entry.IsEnabled = isEnabled;
        }
    }
}
