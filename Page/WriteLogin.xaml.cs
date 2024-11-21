using MySql.Data.MySqlClient;
using static System.Net.Mime.MediaTypeNames;

namespace MauiApp3.Page
{
    public partial class WriteLogin : ContentPage
    {
        private Query_SQL _query_sql;
        public WriteLogin()
        {
            InitializeComponent();
        }


        private async void CounterBtn_Clicked(object sender, EventArgs e)
        {
            _query_sql = new Query_SQL(Login_entry.Text);
            string login = Login_entry.Text;
            string password = Password_entry.Text;
            if (!string.IsNullOrEmpty(login) && password!=null)
            {
                // ���������� ������ � ���� ������
                await SaveLoginToDatabaseAsync(login, password);
                Preferences.Set("UserName", login);
                // ������� �� ��������� ��������

            }
            else
            {
                await DisplayAlert("������", "������� �����.", "OK");
            }
        }

        private async Task SaveLoginToDatabaseAsync(string login, string password)
        {
            string connectionString = "Server=192.168.0.100;Database=Stend_sfu;User ID=root;Password=;Pooling=false;";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    await connection.OpenAsync(); // ����������� �������� ����������
                    Console.WriteLine("����������� �������!");

                    string query = "INSERT INTO User (UserName, UserMoney, Password) VALUES (@login, 0, @password)";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@login", login);
                        command.Parameters.AddWithValue("@password", password);
                        await _query_sql.InitializeUserQuests(login);
                        // ����������� ���������� �������
                        await command.ExecuteNonQueryAsync();
                        Console.WriteLine("����� ������� ������� � ���� ������.");
                    }
                }
                await Navigation.PushAsync(new Page.Quest(login));
            }
            catch (Exception ex)
            {
                Console.WriteLine("������ �����������: " + ex.Message);
                await DisplayAlert("������", "����� ��� ��� ����������, ������� ������.", "OK");
            }
        }

        private async void Return_Button(object sender, EventArgs e)
        {
            if (Login_entry.Text != null )
            {
                if (Password_entry.Text != null)
                {
                    string connectionString = "Server=192.168.0.100;Database=Stend_sfu;User ID=root;Password=;Pooling=false;";
                    string query = "SELECT Password FROM User WHERE UserName = @UserName";
                    string Password=Password_entry.Text;
                    string login = Login_entry.Text;
                    string PasswordUser="0";
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        try
                        {
                           await connection.OpenAsync();
                            using (MySqlCommand cmd = new MySqlCommand(query, connection))
                            {
                                cmd.Parameters.AddWithValue("@UserName", login);
                                object result = await cmd.ExecuteScalarAsync();
                                if (result != null)
                                {
                                    PasswordUser = Convert.ToString(result);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                    }
                    Console.WriteLine(PasswordUser);
                    if (Password==PasswordUser)
                    {
                        login = Login_entry.Text;
                        await Navigation.PushAsync(new Page.Quest(login));
                    }
                    else
                    {
                        await DisplayAlert("������", "������������ ����� ��� ���-���.", "OK");
                    }

                }
            }
          
        }
    }
}
