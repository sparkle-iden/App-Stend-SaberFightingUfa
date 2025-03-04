namespace MauiApp3.Page;

public partial class Card_game : ContentPage
{
    private string _userName;
    private Query_SQL _query_sql;
    public Card_game(string name)
	{
		InitializeComponent();
        _query_sql = new Query_SQL(name);
        Login_user.Text = name;
        _userName = name;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await UpdateUserMoney(); // ���������� ������ � �������
    }

    // ����� ��� ��������� � ����������� ����� ������������
    private async Task UpdateUserMoney()
    {
        try
        {
            // ��������� ����� ������������ ����������
            int userMoney = await _query_sql.GetMoneyUser(_userName);
            User_Money.Text = userMoney.ToString();
        }
        catch (Exception ex)
        {
            // ����������� ������
            await DisplayAlert("������", $"�� ������� �������� ������ ������������: {ex.Message}", "OK");
        }
    }

    private void Paazak_next(object sender, EventArgs e)
    {
        string Login = Login_user.Text;

        Navigation.PushAsync(new Page.Paazak(Login));
    }

    private void Sabbak_next(object sender, EventArgs e)
    {
        string Login = Login_user.Text;

        Navigation.PushAsync(new Page.Sabbak(Login));
    }

    private void Nazad(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}