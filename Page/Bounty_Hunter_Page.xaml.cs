namespace MauiApp3.Page;

public partial class Bounty_Hunter_Page : ContentPage
{
    private string _userName;

    private Query_SQL _query_sql;
    public Bounty_Hunter_Page(string name)
	{
		InitializeComponent();
        _query_sql = new Query_SQL(name);
        _userName = name;
        Login_user.Text = name;
    }
    private void QR_scan(object sender, EventArgs e)
    {
        string Login = Login_user.Text;

        Navigation.PushAsync(new Page.Scan(Login));
    }
    private void Nazad(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await UpdateUserMoney(); // Обновление данных о деньгах
        await UpdateQuestProgress();
    }
    private async Task UpdateQuestProgress()
    {
        try
        {
            var progress = await _query_sql.GetQuestProgress(_userName);

            // Обновляем статус каждого квеста
            UpdateQuestStatus(Shoting_Progress, Shoting_Button, "Тир", progress);
            UpdateQuestStatus(Head_Hunter_Progress, Head_Hunter_Button, "Охота за головами", progress);
            UpdateQuestStatus(Duel_Progress, Duel_Button, "Дуэль", progress);
            UpdateQuestStatus(Fighting_Progress, Fightin_Button, "Фехтование", progress);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ошибка", $"Не удалось загрузить прогресс квестов: {ex.Message}", "OK");
        }
    }

    private void UpdateQuestStatus(Label progressLabel, ImageButton questButton, string questName, Dictionary<string, (int current, int max)> progress)
    {
        if (progress.ContainsKey(questName))
        {
            var (current, max) = progress[questName];
            progressLabel.Text = $"{current}/{max}";
            questButton.IsEnabled = current < max; // Отключаем кнопку, если квест выполнен
        }
        else
        {
            progressLabel.Text = "0/0";
            questButton.IsEnabled = false; // Отключаем кнопку, если квеста нет
        }
    }

    // Метод для получения и отображения денег пользователя
    private async Task UpdateUserMoney()
    {
        try
        {
            // Получение денег пользователя асинхронно
            int userMoney = await _query_sql.GetMoneyUser(_userName);
            User_Money.Text = userMoney.ToString();
        }
        catch (Exception ex)
        {
            // Логирование ошибок
            await DisplayAlert("Ошибка", $"Не удалось обновить деньги пользователя: {ex.Message}", "OK");
        }
    }
}