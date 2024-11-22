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

        var qrCode = e.Results.FirstOrDefault()?.Value;
        if (!string.IsNullOrEmpty(qrCode))
        {
            // Проверяем, содержит ли QR-код значение с запятой
            if (qrCode.Contains(",") && qrCode.Contains("Поиск рукописей"))
            {
                if (qrCode.Contains("Поиск рукописей"))
                {
                    // Разбиваем строку на части
                    var parts = qrCode.Split(',');
                    string questName = "Поиск рукописей";
                    if (parts.Length == 3 &&
                        int.TryParse(parts[0].Trim(), out int firstValue) &&
                        int.TryParse(parts[1].Trim(), out int secondValue))
                    {

                        // Ветвление для второго случая
                        MainThread.BeginInvokeOnMainThread(async() =>
                        {
                             Navigation.PopAsync();
                            _query_sql.AddMoneyToUser(_userName, firstValue);
                            _query_sql.AddManuscriptsToUser(_userName, secondValue);
                            _query_sql.AddQuestProgressUser(_userName, questName);
                            int progress = Convert.ToInt32(await _query_sql.GetSearchManuscriptsProgress(_userName, "Поиск рукописей"));

                            if (progress == 10)
                            {
                                _query_sql.AddQuestProgressUser(_userName, "Археология");
                            }
                        });
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Ошибка: QR-код содержит некорректные данные.");
                    }
                }
            }
            if (qrCode.Contains(",") && qrCode.Contains("Списание"))
            {
               
                    // Разбиваем строку на части
                    var parts = qrCode.Split(',');
                    if (parts.Length == 2 &&
                        int.TryParse(parts[0].Trim(), out int firstValue))
                    {

                        // Ветвление для второго случая
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            Navigation.PopAsync();
                            _query_sql.AddMoneyToUser(_userName, firstValue);
                        });
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Ошибка: QR-код содержит некорректные данные.");
                    }
                
            }
            if (qrCode.Contains(",") && qrCode.Contains("Раскопки")){
                if (qrCode.Contains("Раскопки"))
                {
                    // Разбиваем строку на части
                    var parts = qrCode.Split(',');
                    string questName = "Раскопки";
                    if (parts.Length == 2 &&
                        int.TryParse(parts[0].Trim(), out int firstValue) )
                    {

                        // Ветвление для второго случая
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            Navigation.PopAsync();
                            _query_sql.AddMoneyToUser(_userName, firstValue);
                            _query_sql.AddQuestProgressUser(_userName, questName);
                            _query_sql.AddQuestProgressUser(_userName, "Археология");
                        });
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Ошибка: QR-код содержит некорректные данные.");
                    }
                }
            }
           
            if (qrCode.Contains(",") && qrCode.Contains("Поиск артефактов"))
            {
                if (qrCode.Contains("Поиск артефактов"))
                {
                    // Разбиваем строку на части
                    var parts = qrCode.Split(',');
                    string questName = "Поиск артефактов";
                    if (parts.Length == 2 &&
                        int.TryParse(parts[0].Trim(), out int firstValue))
                    {

                        // Ветвление для второго случая
                        MainThread.BeginInvokeOnMainThread(async() =>
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
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Ошибка: QR-код содержит некорректные данные.");
                    }
                }
            }
            if (qrCode.Contains(",") && qrCode.Contains("Тир"))
            {
                if (qrCode.Contains("Тир"))
                {
                    // Разбиваем строку на части
                    var parts = qrCode.Split(',');
                    string questName = "Тир";
                    if (parts.Length == 2 &&
                        int.TryParse(parts[0].Trim(), out int firstValue))
                    {

                        // Ветвление для второго случая
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            Navigation.PopAsync();
                            _query_sql.AddMoneyToUser(_userName, firstValue);
                            _query_sql.AddQuestProgressUser(_userName, questName);
                            _query_sql.AddQuestProgressUser(_userName, "Наемник");
                        });
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Ошибка: QR-код содержит некорректные данные.");
                    }
                }
            }
            if (qrCode.Contains(",") && qrCode.Contains("Фехтование"))
            {
                if (qrCode.Contains("Фехтование"))
                {
                    // Разбиваем строку на части
                    var parts = qrCode.Split(',');
                    string questName = "Фехтование";
                    if (parts.Length == 2 &&
                        int.TryParse(parts[0].Trim(), out int firstValue) )
                    {

                        // Ветвление для второго случая
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            Navigation.PopAsync();
                            _query_sql.AddMoneyToUser(_userName, firstValue);
                            _query_sql.AddQuestProgressUser(_userName, questName);
                            _query_sql.AddQuestProgressUser(_userName, "Наемник");
                        });
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Ошибка: QR-код содержит некорректные данные.");
                    }
                }
            }
            if (qrCode.Contains(",") && qrCode.Contains("Фото-зона"))
            {
                if (qrCode.Contains("Фото-зона"))
                {
                    // Разбиваем строку на части
                    var parts = qrCode.Split(',');
                    string questName = "Фото-зона";
                    if (parts.Length == 2 &&
                        int.TryParse(parts[0].Trim(), out int firstValue) )
                    {

                        // Ветвление для второго случая
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            Navigation.PopAsync();
                            _query_sql.AddMoneyToUser(_userName, firstValue);
                            _query_sql.AddQuestProgressUser(_userName, questName);
                        });
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Ошибка: QR-код содержит некорректные данные.");
                    }
                }
            }
            if (qrCode.Contains(",") && qrCode.Contains("Охота за головами"))
            {
                if (qrCode.Contains("Охота за головами"))
                {
                    // Разбиваем строку на части
                    var parts = qrCode.Split(',');
                    string questName = "Охота за головами";
                    if (parts.Length == 2 &&
                        int.TryParse(parts[0].Trim(), out int firstValue))
                    {

                        // Ветвление для второго случая
                        MainThread.BeginInvokeOnMainThread(async() =>
                        {
                            Navigation.PopAsync();
                            _query_sql.AddMoneyToUser(_userName, firstValue);
                            _query_sql.AddQuestProgressUser(_userName, questName);
                            int progress = Convert.ToInt32(await _query_sql.GetSearchManuscriptsProgress(_userName, "Охота за головами"));

                            if (progress == 3)
                            {
                                _query_sql.AddQuestProgressUser(_userName, "Охота за головами");
                            }
                        });
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Ошибка: QR-код содержит некорректные данные.");
                    }
                }
            }
            if (qrCode.Contains(",") && qrCode.Contains("Дуэль"))
            {
                if (qrCode.Contains("Дуэль"))
                {
                    // Разбиваем строку на части
                    var parts = qrCode.Split(',');
                    string questName = "Дуэль";
                    if (parts.Length == 2 &&
                        int.TryParse(parts[0].Trim(), out int firstValue))
                    {

                        // Ветвление для второго случая
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            Navigation.PopAsync();
                            _query_sql.AddMoneyToUser(_userName, firstValue);
                            _query_sql.AddQuestProgressUser(_userName, questName);
                            _query_sql.AddQuestProgressUser(_userName, "Наемник");
                        });
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Ошибка: QR-код содержит некорректные данные.");
                    }
                }
            }


            else if (qrCode.Contains("blarg"))
            {
                _zooName = qrCode;
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Navigation.PushAsync(new Page.Zoo_Info(_zooName));
                });
            }
            else if (qrCode.Contains("condor"))
            {
                _zooName = qrCode;
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Navigation.PushAsync(new Page.Zoo_Info(_zooName));
                });
            }
            else if (qrCode.Contains("wolf"))
            {
                _zooName = qrCode;
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Navigation.PushAsync(new Page.Zoo_Info(_zooName));
                });
            }
            else if (qrCode.Contains("spider"))
            {
                _zooName = qrCode;
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Navigation.PushAsync(new Page.Zoo_Info(_zooName));
                });
            }
            else if (qrCode.Contains("vampa"))
            {
                _zooName = qrCode;
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Navigation.PushAsync(new Page.Zoo_Info(_zooName));
                });
            }
            else if (qrCode.Contains("arics"))
            {
                _zooName = qrCode;
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Navigation.PushAsync(new Page.Zoo_Info(_zooName));
                });
            }
            else if (qrCode.Contains("tauntaunt"))
            {
                _zooName = qrCode;
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Navigation.PushAsync(new Page.Zoo_Info(_zooName));
                });
            }
            else
            {
                Console.WriteLine("Ошибка: QR-код не содержит корректного значения монет.");
            }
        }
    }


    private void Nazad(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}