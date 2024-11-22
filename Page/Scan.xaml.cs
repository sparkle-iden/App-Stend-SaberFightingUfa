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
            // ���������, �������� �� QR-��� �������� � �������
            if (qrCode.Contains(",") && qrCode.Contains("����� ���������"))
            {
                if (qrCode.Contains("����� ���������"))
                {
                    // ��������� ������ �� �����
                    var parts = qrCode.Split(',');
                    string questName = "����� ���������";
                    if (parts.Length == 3 &&
                        int.TryParse(parts[0].Trim(), out int firstValue) &&
                        int.TryParse(parts[1].Trim(), out int secondValue))
                    {

                        // ��������� ��� ������� ������
                        MainThread.BeginInvokeOnMainThread(async() =>
                        {
                             Navigation.PopAsync();
                            _query_sql.AddMoneyToUser(_userName, firstValue);
                            _query_sql.AddManuscriptsToUser(_userName, secondValue);
                            _query_sql.AddQuestProgressUser(_userName, questName);
                            int progress = Convert.ToInt32(await _query_sql.GetSearchManuscriptsProgress(_userName, "����� ���������"));

                            if (progress == 10)
                            {
                                _query_sql.AddQuestProgressUser(_userName, "����������");
                            }
                        });
                        return;
                    }
                    else
                    {
                        Console.WriteLine("������: QR-��� �������� ������������ ������.");
                    }
                }
            }
            if (qrCode.Contains(",") && qrCode.Contains("��������"))
            {
               
                    // ��������� ������ �� �����
                    var parts = qrCode.Split(',');
                    if (parts.Length == 2 &&
                        int.TryParse(parts[0].Trim(), out int firstValue))
                    {

                        // ��������� ��� ������� ������
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            Navigation.PopAsync();
                            _query_sql.AddMoneyToUser(_userName, firstValue);
                        });
                        return;
                    }
                    else
                    {
                        Console.WriteLine("������: QR-��� �������� ������������ ������.");
                    }
                
            }
            if (qrCode.Contains(",") && qrCode.Contains("��������")){
                if (qrCode.Contains("��������"))
                {
                    // ��������� ������ �� �����
                    var parts = qrCode.Split(',');
                    string questName = "��������";
                    if (parts.Length == 2 &&
                        int.TryParse(parts[0].Trim(), out int firstValue) )
                    {

                        // ��������� ��� ������� ������
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            Navigation.PopAsync();
                            _query_sql.AddMoneyToUser(_userName, firstValue);
                            _query_sql.AddQuestProgressUser(_userName, questName);
                            _query_sql.AddQuestProgressUser(_userName, "����������");
                        });
                        return;
                    }
                    else
                    {
                        Console.WriteLine("������: QR-��� �������� ������������ ������.");
                    }
                }
            }
           
            if (qrCode.Contains(",") && qrCode.Contains("����� ����������"))
            {
                if (qrCode.Contains("����� ����������"))
                {
                    // ��������� ������ �� �����
                    var parts = qrCode.Split(',');
                    string questName = "����� ����������";
                    if (parts.Length == 2 &&
                        int.TryParse(parts[0].Trim(), out int firstValue))
                    {

                        // ��������� ��� ������� ������
                        MainThread.BeginInvokeOnMainThread(async() =>
                        {
                             Navigation.PopAsync();
                            _query_sql.AddMoneyToUser(_userName, firstValue);
                            _query_sql.AddQuestProgressUser(_userName, questName);
                            int progress = Convert.ToInt32(await _query_sql.GetSearchManuscriptsProgress(_userName, "����� ����������"));

                            if (progress == 2)
                            {
                                _query_sql.AddQuestProgressUser(_userName, "����������");
                            }

                        });
                        return;
                    }
                    else
                    {
                        Console.WriteLine("������: QR-��� �������� ������������ ������.");
                    }
                }
            }
            if (qrCode.Contains(",") && qrCode.Contains("���"))
            {
                if (qrCode.Contains("���"))
                {
                    // ��������� ������ �� �����
                    var parts = qrCode.Split(',');
                    string questName = "���";
                    if (parts.Length == 2 &&
                        int.TryParse(parts[0].Trim(), out int firstValue))
                    {

                        // ��������� ��� ������� ������
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            Navigation.PopAsync();
                            _query_sql.AddMoneyToUser(_userName, firstValue);
                            _query_sql.AddQuestProgressUser(_userName, questName);
                            _query_sql.AddQuestProgressUser(_userName, "�������");
                        });
                        return;
                    }
                    else
                    {
                        Console.WriteLine("������: QR-��� �������� ������������ ������.");
                    }
                }
            }
            if (qrCode.Contains(",") && qrCode.Contains("����������"))
            {
                if (qrCode.Contains("����������"))
                {
                    // ��������� ������ �� �����
                    var parts = qrCode.Split(',');
                    string questName = "����������";
                    if (parts.Length == 2 &&
                        int.TryParse(parts[0].Trim(), out int firstValue) )
                    {

                        // ��������� ��� ������� ������
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            Navigation.PopAsync();
                            _query_sql.AddMoneyToUser(_userName, firstValue);
                            _query_sql.AddQuestProgressUser(_userName, questName);
                            _query_sql.AddQuestProgressUser(_userName, "�������");
                        });
                        return;
                    }
                    else
                    {
                        Console.WriteLine("������: QR-��� �������� ������������ ������.");
                    }
                }
            }
            if (qrCode.Contains(",") && qrCode.Contains("����-����"))
            {
                if (qrCode.Contains("����-����"))
                {
                    // ��������� ������ �� �����
                    var parts = qrCode.Split(',');
                    string questName = "����-����";
                    if (parts.Length == 2 &&
                        int.TryParse(parts[0].Trim(), out int firstValue) )
                    {

                        // ��������� ��� ������� ������
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
                        Console.WriteLine("������: QR-��� �������� ������������ ������.");
                    }
                }
            }
            if (qrCode.Contains(",") && qrCode.Contains("����� �� ��������"))
            {
                if (qrCode.Contains("����� �� ��������"))
                {
                    // ��������� ������ �� �����
                    var parts = qrCode.Split(',');
                    string questName = "����� �� ��������";
                    if (parts.Length == 2 &&
                        int.TryParse(parts[0].Trim(), out int firstValue))
                    {

                        // ��������� ��� ������� ������
                        MainThread.BeginInvokeOnMainThread(async() =>
                        {
                            Navigation.PopAsync();
                            _query_sql.AddMoneyToUser(_userName, firstValue);
                            _query_sql.AddQuestProgressUser(_userName, questName);
                            int progress = Convert.ToInt32(await _query_sql.GetSearchManuscriptsProgress(_userName, "����� �� ��������"));

                            if (progress == 3)
                            {
                                _query_sql.AddQuestProgressUser(_userName, "����� �� ��������");
                            }
                        });
                        return;
                    }
                    else
                    {
                        Console.WriteLine("������: QR-��� �������� ������������ ������.");
                    }
                }
            }
            if (qrCode.Contains(",") && qrCode.Contains("�����"))
            {
                if (qrCode.Contains("�����"))
                {
                    // ��������� ������ �� �����
                    var parts = qrCode.Split(',');
                    string questName = "�����";
                    if (parts.Length == 2 &&
                        int.TryParse(parts[0].Trim(), out int firstValue))
                    {

                        // ��������� ��� ������� ������
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            Navigation.PopAsync();
                            _query_sql.AddMoneyToUser(_userName, firstValue);
                            _query_sql.AddQuestProgressUser(_userName, questName);
                            _query_sql.AddQuestProgressUser(_userName, "�������");
                        });
                        return;
                    }
                    else
                    {
                        Console.WriteLine("������: QR-��� �������� ������������ ������.");
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
                Console.WriteLine("������: QR-��� �� �������� ����������� �������� �����.");
            }
        }
    }


    private void Nazad(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}