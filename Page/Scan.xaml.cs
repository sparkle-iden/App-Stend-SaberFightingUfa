using MySql.Data.MySqlClient;
using ZXing.Net.Maui.Controls;
using ZXing.Net.Maui;

namespace MauiApp3.Page;

public partial class Scan : ContentPage
{
    private string _userName;
    private Query_SQL _query_sql;
    private string _zooName;
    public Scan(string name)
    {
        InitializeComponent();
        _userName = name;
        _query_sql = new Query_SQL(name);

    }


    private async void BarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
    {
        cameraBarcodeReaderView.Options = new BarcodeReaderOptions
        {
            Formats = BarcodeFormats.OneDimensional,
            AutoRotate = true,
            Multiple = true
        };
        cameraBarcodeReaderView.IsVisible = false;
        int number_manuscript = 0;
        var qrCode = e.Results.FirstOrDefault()?.Value;
        if (!string.IsNullOrEmpty(qrCode))
        {
            switch (qrCode)
            {
                case var code when code.Contains(",") && code.Contains("Поиск рукописей"):
                    await HandleSearchManuscripts(qrCode);
                    break;

                case var code when code.Contains(",") && code.Contains("Списание"):
                    await HandleWriteOff(qrCode);
                    break;

                case var code when code.Contains(",") && code.Contains("Раскопки"):
                    await HandleExcavation(qrCode);
                    break;

                case var code when code.Contains(",") && code.Contains("Поиск артефактов"):
                    await HandleArtifactSearch(qrCode);
                    break;

                case var code when code.Contains(",") && code.Contains("Тир"):
                    await HandleShootingRange(qrCode);
                    break;

                case var code when code.Contains(",") && code.Contains("Фехтование"):
                    await HandleFencing(qrCode);
                    break;

                case var code when code.Contains(",") && code.Contains("Фото-зона"):
                    await HandlePhotoZone(qrCode);
                    break;

                case var code when code.Contains(",") && code.Contains("Охота за головами"):
                    await HandleBountyHunt(qrCode);
                    break;

                case var code when code.Contains(",") && code.Contains("Дуэль"):
                    await HandleDuel(qrCode);
                    break;

                // Обработка других зоопарков
                case var code when code.Contains("blarg") || code.Contains("condor") || code.Contains("wolf") ||
                      code.Contains("spider") || code.Contains("vampa") || code.Contains("arics") || code.Contains("tauntaunt"):
                    HandleZooInfo(qrCode);
                    break;

                default:
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await DisplayAlert("Ошибка", "Неправильное значение QR кода", "OK");
                    });
                    break;
            }

            // Метод для поиска рукописей
            async Task HandleSearchManuscripts(string qrCode)
            {
                var parts = qrCode.Split(',');
                string questName = "Поиск рукописей";
                var lostManuscripts = await _query_sql.GetLostManuscriptsAsync(_userName);
               
                if (number_manuscript == 0 && parts.Length == 3 && int.TryParse(parts[0].Trim(), out int firstValue) &&
                    int.TryParse(parts[1].Trim(), out int secondValue))
                {
                    if (!lostManuscripts.Contains(secondValue))
                    {
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            await Navigation.PopAsync();
                            _query_sql.AddMoneyToUser(_userName, firstValue);
                            _query_sql.AddManuscriptsToUser(_userName, secondValue);
                            _query_sql.AddQuestProgressUser(_userName, questName);
                            int progress = Convert.ToInt32(await _query_sql.GetSearchManuscriptsProgress(_userName, "Поиск рукописей"));
                            if (progress == 10)
                            {
                                _query_sql.AddQuestProgressUser(_userName, "Археология");
                            }
                        });
                    }
                    else
                    {
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            await DisplayAlert("Ошибка", "Такой манкускрипт уже отсканирован.", "OK");
                        });
                    }

                }
                else
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await DisplayAlert("Ошибка", "Неправильный формат.", "OK");
                    });
                }
            }

            // Метод для списания
            async Task HandleWriteOff(string qrCode)
            {
                var parts = qrCode.Split(',');
                if (parts.Length == 2 && int.TryParse(parts[0].Trim(), out int firstValue))
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        Navigation.PopAsync();
                        _query_sql.AddMoneyToUser(_userName, firstValue);
                    });
                }
                else
                {
                    Console.WriteLine("Ошибка: QR-код содержит некорректные данные.");
                }
            }

            // Метод для раскопок
            async Task HandleExcavation(string qrCode)
            {
                var parts = qrCode.Split(',');
                string questName = "Раскопки";
                if (parts.Length == 2 && int.TryParse(parts[0].Trim(), out int firstValue))
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        Navigation.PopAsync();
                        _query_sql.AddMoneyToUser(_userName, firstValue);
                        _query_sql.AddQuestProgressUser(_userName, questName);
                        _query_sql.AddQuestProgressUser(_userName, "Археология");
                    });
                }
                else
                {
                    Console.WriteLine("Ошибка: QR-код содержит некорректные данные.");
                }
            }

            // Метод для поиска артефактов
            async Task HandleArtifactSearch(string qrCode)
            {
                var parts = qrCode.Split(',');
                string questName = "Поиск артефактов";
                if (parts.Length == 2 && int.TryParse(parts[0].Trim(), out int firstValue))
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        Navigation.PopAsync();
                        _query_sql.AddMoneyToUser(_userName, firstValue);
                        _query_sql.AddQuestProgressUser(_userName, questName);
                        int progress = Convert.ToInt32(await _query_sql.GetSearchManuscriptsProgress(_userName, "Поиск артефактов"));
                        if (progress == 2)
                        {
                            _query_sql.AddQuestProgressUser(_userName, "Археология");
                        }
                    });
                }
                else
                {
                    Console.WriteLine("Ошибка: QR-код содержит некорректные данные.");
                }
            }

            // Метод для тира
            async Task HandleShootingRange(string qrCode)
            {
                var parts = qrCode.Split(',');
                string questName = "Тир";
                if (parts.Length == 2 && int.TryParse(parts[0].Trim(), out int firstValue))
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        Navigation.PopAsync();
                        _query_sql.AddMoneyToUser(_userName, firstValue);
                        _query_sql.AddQuestProgressUser(_userName, questName);
                        _query_sql.AddQuestProgressUser(_userName, "Наемник");
                    });
                }
                else
                {
                    Console.WriteLine("Ошибка: QR-код содержит некорректные данные.");
                }
            }

            // Метод для фехтования
            async Task HandleFencing(string qrCode)
            {
                var parts = qrCode.Split(',');
                string questName = "Фехтование";
                if (parts.Length == 2 && int.TryParse(parts[0].Trim(), out int firstValue))
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        Navigation.PopAsync();
                        _query_sql.AddMoneyToUser(_userName, firstValue);
                        _query_sql.AddQuestProgressUser(_userName, questName);
                        _query_sql.AddQuestProgressUser(_userName, "Наемник");
                    });
                }
                else
                {
                    Console.WriteLine("Ошибка: QR-код содержит некорректные данные.");
                }
            }

            // Метод для фото-зоны
            async Task HandlePhotoZone(string qrCode)
            {
                var parts = qrCode.Split(',');
                string questName = "Фото-зона";
                if (parts.Length == 2 && int.TryParse(parts[0].Trim(), out int firstValue))
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        Navigation.PopAsync();
                        _query_sql.AddMoneyToUser(_userName, firstValue);
                        _query_sql.AddQuestProgressUser(_userName, questName);
                    });
                }
                else
                {
                    Console.WriteLine("Ошибка: QR-код содержит некорректные данные.");
                }
            }

            // Метод для охоты за головами
            async Task HandleBountyHunt(string qrCode)
            {
                var parts = qrCode.Split(',');
                string questName = "Охота за головами";
                if (parts.Length == 2 && int.TryParse(parts[0].Trim(), out int firstValue))
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        Navigation.PopAsync();
                        _query_sql.AddMoneyToUser(_userName, firstValue);
                        _query_sql.AddQuestProgressUser(_userName, questName);
                        int progress = Convert.ToInt32(await _query_sql.GetSearchManuscriptsProgress(_userName, "Охота за головами"));
                        if (progress == 3)
                        {
                            _query_sql.AddQuestProgressUser(_userName, "Наемник");
                        }
                    });
                }
                else
                {
                    Console.WriteLine("Ошибка: QR-код содержит некорректные данные.");
                }
            }

            // Метод для дуэли
            async Task HandleDuel(string qrCode)
            {
                var parts = qrCode.Split(',');
                string questName = "Дуэль";
                if (parts.Length == 2 && int.TryParse(parts[0].Trim(), out int firstValue))
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        Navigation.PopAsync();
                        _query_sql.AddMoneyToUser(_userName, firstValue);
                        _query_sql.AddQuestProgressUser(_userName, questName);
                        _query_sql.AddQuestProgressUser(_userName, "Наемник");
                    });
                }
                else
                {
                    Console.WriteLine("Ошибка: QR-код содержит некорректные данные.");
                }
            }

            // Метод для зоопарков
            void HandleZooInfo(string qrCode)
            {
                _zooName = qrCode;
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Navigation.PushAsync(new Page.Zoo_Info(_zooName));
                });
            }
            
        }
    }
    private void Nazad(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}
