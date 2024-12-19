using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp3
{
    class Query_SQL(string login)
    {
        string connectionString = "Server=server269.hosting.reg.ru;Database=u2917647_Sparkle;User ID=u2917647_default;Password=1tB6J7OD3cmt3JD1;Charset=utf8mb4;";
       

        public async Task<int> GetMoneyUser(string login)
        {
            string query = "SELECT UserMoney FROM User WHERE UserName = @UserName";
            int userMoney = 0;
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
                            userMoney = Convert.ToInt32(result);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            return userMoney;
        }


        public void AddMoneyToUser(string login, int coinsToAdd)
        {
            string query = "UPDATE User SET UserMoney = UserMoney + @coinsToAdd WHERE UserName = @UserName";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@coinsToAdd", coinsToAdd);
                        cmd.Parameters.AddWithValue("@UserName", login);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine($"Успешно добавлено {coinsToAdd} монет пользователю {login}.");
                        }
                        else
                        {
                            Console.WriteLine($"Не удалось обновить данные для пользователя {login}.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка выполнения запроса: {ex.Message}");
                }
            }
        }

        public void AddManuscriptsToUser(string login, int Manuscript)
        {
            string query = $"INSERT INTO User_Manuscripts (UserName, Manuscript) VALUES ('{login}', {Manuscript})";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка выполнения запроса: {ex.Message}");
                }
            }
        }

        public bool IsManuscriptLost(string UserName, int Manuscript)
        {
            string query = "SELECT COUNT(*) FROM User_Manuscripts WHERE UserName = @UserName AND Manuscript = @Manuscript";
            bool isLost = false;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@UserName", UserName);
                        cmd.Parameters.AddWithValue("@Manuscript", Manuscript);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        isLost = count > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка выполнения запроса: {ex.Message}");
                }
            }

            return isLost;
        }

        public void AddQuestProgressUser(string login, string QuestName)
        {
            string query = $"UPDATE UserQuests SET CurrentProgress = CurrentProgress + 1 WHERE UserName = @UserName AND QuestName =@QuestName";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@UserName", login);
                        cmd.Parameters.AddWithValue("@QuestName", QuestName);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка выполнения запроса: {ex.Message}");
                }
            }
        }

        // Метод для получения всех манускриптов пользователя
        public void GetUserManuscripts(string UserName)
        {
            string query = "SELECT Manuscript FROM User_Manuscripts WHERE UserName = @UserName";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@UserName", UserName);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine($"Manuscript: {reader["Manuscript"]}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка выполнения запроса: {ex.Message}");
                }
            }
        }

        public async Task<HashSet<int>> GetLostManuscriptsAsync(string userName)
        {
            string query = "SELECT Manuscript FROM User_Manuscripts WHERE UserName = @UserName";
            var manuscripts = new HashSet<int>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@UserName", userName);
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                manuscripts.Add(reader.GetInt32(0));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка выполнения запроса: {ex.Message}");
                }
            }

            return manuscripts;
        }

        public async Task<Dictionary<string, (int current, int max)>> GetQuestProgress(string userName)
        {
            string query = @"
        SELECT QuestName, CurrentProgress, MaxProgress 
        FROM UserQuests 
        WHERE UserName = @UserName";

            var progress = new Dictionary<string, (int current, int max)>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@UserName", userName);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                string questName = reader.GetString("QuestName");
                                int current = reader.GetInt32("CurrentProgress");
                                int max = reader.GetInt32("MaxProgress");
                                progress[questName] = (current, max);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка выполнения запроса: {ex.Message}");
                }
            }

            return progress;
        }

        public async Task<int> GetSearchManuscriptsProgress(string userName, string QuestName)
        {
            string query = @"
    SELECT CurrentProgress
    FROM UserQuests
    WHERE UserName = @UserName AND QuestName = @QuestName";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@UserName", userName);
                        cmd.Parameters.AddWithValue("@QuestName", QuestName);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                int current = reader.GetInt32("CurrentProgress");

                                return current;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка выполнения запроса: {ex.Message}");
                }
            }

            // Если данных нет, возвращаем 0
            return (0);
        }

        public async Task InitializeUserQuests(string userName)
        {

            var defaultQuests = new List<(string QuestName, int MaxProgress)>
    {
        ("Археология", 4),
        ("Раскопки", 1),
        ("Поиск артефактов", 2),
        ("Поиск рукописей", 10),
        ("Расшифровка таблички", 1),
        ("Ксенозоология", 2),
        ("Наемник", 4),
        ("Тир", 1),
        ("Охота за головами", 3),
        ("Дуэль", 1),
        ("Фехтование", 1),
        ("Фото-зона", 1),
        ("Викторина", 1)
    };

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    foreach (var quest in defaultQuests)
                    {
                        string query = @"
INSERT INTO UserQuests (UserName, QuestName, CurrentProgress, MaxProgress)
SELECT @UserName, @QuestName, 0, @MaxProgress
WHERE NOT EXISTS (
    SELECT 1 FROM UserQuests 
    WHERE UserName = @UserName AND QuestName = @QuestName
);";

                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@UserName", userName);
                            command.Parameters.AddWithValue("@QuestName", quest.QuestName);
                            command.Parameters.AddWithValue("@MaxProgress", quest.MaxProgress);

                            await command.ExecuteNonQueryAsync();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка инициализации квестов: {ex.Message}");
            }
        }



    }
}
