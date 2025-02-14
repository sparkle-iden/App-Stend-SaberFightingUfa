namespace MauiApp3.Page;

public partial class XenoZoo : ContentPage
{
    private Query_SQL _query_sql;
    private string _userName;
    private List<Question> _questions;
    int complet=0;

    public XenoZoo(string name)
	{
		InitializeComponent();
          _userName = name;
        _query_sql = new Query_SQL(name);
        Login_user.Text = name;

        LoadQuestions();

        List<string> words = new List<string>
        {
            "Кабановолк",
            "Бларрг",
            "Кондор-дракон",
            "Вздыбливающийся паук",
            "Арикс",
            "Вампа",
            "Таунтаун"
        };

        // Создаем объект Random
        Random random = new Random();

        // Генерируем случайный индекс
        int randomIndex = random.Next(words.Count);

        // Выбираем случайное слово
        string randomWord = words[randomIndex];
        zoo.Text = randomWord;
            switch (randomWord)
        {
            case "Кабановолк":
                ShowQuestions(0, 2);
                break;
            case "Бларрг":
                ShowQuestions(3, 5);
                break;
            case "Кондор-дракон":
                ShowQuestions(6, 8);
                break;
            case "Вздыбливающийся паук":
                ShowQuestions(9, 11);
                break;
            case "Арикс":
                ShowQuestions(12, 14);
                break;
            case "Вампа":
                ShowQuestions(15, 17);
                break;
            case "Таунтаун":
                ShowQuestions(18, 20);
                break;
        }
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
            int userMoney = await _query_sql.GetMoneyUser(_userName);
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

    string questName = "Ксенозоология";

    private void DisableOtherButtons(Button clickedButton, Button button2, Button button3)
    {
        clickedButton.IsEnabled = false; // Оставляем нажатую кнопку активной
        button2.IsEnabled = false; // Деактивируем остальные кнопки
        button3.IsEnabled = false;
    }
    private void Fail_Question(object sender, EventArgs e)
    {
        var button = sender as Button;
        complet++;
        if (button != null)
        {
            button.BackgroundColor = Colors.Red;  // Задаём красный цвет для неправильного ответа
        }
        var button1 = this.FindByName<Button>($"question_{complet}_answer_1");
        var button2 = this.FindByName<Button>($"question_{complet}_answer_2");
        var button3 = this.FindByName<Button>($"question_{complet}_answer_3");
        DisableOtherButtons(button, button2, button3);
        if (complet == 3)
        {
            Text_scroll.IsVisible = false;
            end.IsVisible = true;
            Button_scan.IsVisible = false;
            end.Text = "Вы провалили испытания КсеноЗоолога";
            _query_sql.AddQuestProgressUser(_userName, questName);
        }

    }

    private async void Right_answer(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button != null)
        {
            button.BackgroundColor = Colors.Green;  // Задаём зелёный цвет для правильного ответа
        }
        int coinsToAdd = 1000; // Количество монет за правильный ответ

        // Обновляем деньги пользователя в базе данных
        _query_sql.AddMoneyToUser(_userName, coinsToAdd);
        complet++;
        var button1 = this.FindByName<Button>($"question_{complet}_answer_1");
        var button2 = this.FindByName<Button>($"question_{complet}_answer_2");
        var button3 = this.FindByName<Button>($"question_{complet}_answer_3");
        DisableOtherButtons(button, button2, button3);
        // Обновляем отображение денег в интерфейсе
        await UpdateUserMoney();
        if (complet == 3)
        {
            Text_scroll.IsVisible = false;
            end.IsVisible = true;
            end.Text = "Вы успешно прошли испытания КсеноЗоолога";
            Button_scan.IsVisible = false;
            _query_sql.AddQuestProgressUser(_userName, questName);
        }
    }


    private void ShowQuestions(int startIndex, int endIndex)
    {
        // Показать вопросы на странице в зависимости от выбранного животного
        for (int i = startIndex; i <= endIndex; i++)
        {
            var question = _questions[i];
            string questionLabelName = $"qustions_{i - startIndex +1}";
            string answerButton1Name = $"question_{i - startIndex + 1}_answer_1";
            string answerButton2Name = $"question_{i - startIndex + 1}_answer_2";
            string answerButton3Name = $"question_{i - startIndex + 1}_answer_3";
            
            // Присваиваем текст вопроса
            var questionLabel = this.FindByName<Label>(questionLabelName);
            Console.WriteLine($"Показывается вопрос с индексом: {i}");
            questionLabel.Text = question.Text;

            // Присваиваем текст для кнопок ответов
            var button1 = this.FindByName<Button>(answerButton1Name);
            var button2 = this.FindByName<Button>(answerButton2Name);
            var button3 = this.FindByName<Button>(answerButton3Name);
            button1.Text = question.Answers[0];
            button2.Text = question.Answers[1];
            button3.Text = question.Answers[2];

            // Привязываем обработчики к кнопкам
            if (question.CorrectAnswerIndex == 0)
            {
                button1.Clicked += Right_answer;
            }
            else {
                button1.Clicked += Fail_Question;
            }
            if (question.CorrectAnswerIndex == 1)
            {
                button2.Clicked += Right_answer;
            }
            else {
                button2.Clicked += Fail_Question;
            }
            if (question.CorrectAnswerIndex == 2)
            {
                button3.Clicked += Right_answer;
            }else
            {
                button3.Clicked += Fail_Question;
            }
        }
    }
 

    private void LoadQuestions()
    {
        _questions = new List<Question>
    {
       
        new Question
        {
            Text = "Отличительная особенность кабановолков",
            Answers = new List<string>
            {
                "Грива вдоль горбатой спины, длинная морда с пятачком на конце и полная крупных острых клыков пасть",
                "Они могут двигаться бесшумно, несмотря на свои массивные размеры.",
                "Имеют мощные клыки, как у кабанов, и острые когти, как у волков.",
            },
            CorrectAnswerIndex = 0
        },
        new Question
        {
            Text = "На какую звезду/планету/спутник воют по ночам кабановолки?",
            Answers = new List<string>
            {
                "Сириус",
                "Беспин",
                "Эндор",
            },
            CorrectAnswerIndex = 2
        },
        new Question
        {
            Text = "Как можно одомашнить кабановолка?",
            Answers = new List<string>
            {
                "Постепенно завоевывать доверие, предлагая мясо и избегая агрессивных действий.",
                "Убить самку кабановолка и забирать её маленьких детёнышей",
                "Использовать запах трав и цветов, которые успокаивают их (например, ягоды селериума).",
            },
            CorrectAnswerIndex = 1
        },
        new Question
        {
            Text = "Сколько ног у бларрга?",
            Answers = new List<string>
            {
                "Две ноги",
                "Четыре ноги",
                "Три ноги",
            },
            CorrectAnswerIndex = 1
        },
        new Question
        {
            Text = "Сколько яиц может отложить самка бларрга?",
            Answers = new List<string>
            {
                "5-6",
                "1-4",
                "Больше 6",
            },
            CorrectAnswerIndex = 0
        },
        new Question
        {
            Text = "Кем или чем питаются бларги?",
            Answers = new List<string>
            {
                "Насекомые",
                "Другие виды животных",
                "Травы, сорняки и молодые деревья",
            },
            CorrectAnswerIndex = 2
        },
        new Question
        {
            Text = "Какого цвета кожа?",
            Answers = new List<string>
            {
                "Синяя",
                "Белая",
                "Голубо-бежевая",

            },
            CorrectAnswerIndex = 2
        },
        new Question
        {
            Text = "Размах крыльев?",
            Answers = new List<string>
            {
                "Два метра",
                "Три метра",
                "Пять метров",
            },
            CorrectAnswerIndex = 1
        },
        new Question
        {
            Text = "Основная добыча?",
            Answers = new List<string>
            {
                "Вздыбливающийся пауки",
                "Эвоки",
                "Эндорские пони",
            },
            CorrectAnswerIndex = 0
        },
        new Question
        {
            Text = "Какая часть тела у паука светится?",
            Answers = new List<string>
            {
                "Лапы",
                "Глаза",
                "Пасть"
            },
            CorrectAnswerIndex = 1
        },
        new Question
        {
            Text = "Способ охоты, который отличается от других пауков?",
            Answers = new List<string>
            {
                "Паук также поднимался на задние лапы и обхватывал жертву передними.",
                "Использует светящееся брюшко, чтобы заманивать ночных насекомых.",
                "Выплёвывает липкую нить на расстояние до метра, чтобы ловить добычу."
            },
            CorrectAnswerIndex = 0
        },
        new Question
        {
            Text = "В какой местности обитают?",
            Answers = new List<string>
            {
                "В подземных пещерах с высокой влажностью.",
                "Пещеры в пустыни",
                "В тропических лесах с густой растительностью."
            },
            CorrectAnswerIndex = 1
        },
        new Question
        {
            Text = "Сколько пальцев на ногах у арикса?",
            Answers = new List<string>
            {
                "Пять пальцев",
                "Один палец",
                "Три пальца"
            },
            CorrectAnswerIndex = 2
        },
        new Question
        {
            Text = "Как использовались ариксы?",
            Answers = new List<string>
            {
                "Ездовые животные",
                "Выращивание на фермах",
                "Украшение"
            },
            CorrectAnswerIndex = 0
        },
        new Question
        {
            Text = "Цвет оперения?",
            Answers = new List<string>
            {
                "Желтый",
                "Синий",
                "Белый"
            },
            CorrectAnswerIndex = 2
        },
        new Question
        {
            Text = "Как охотится вампа?",
            Answers = new List<string>
            {
                "Загоняет добычу в ловушку, пользуясь местностью.",
                "Нападал из засады и оглушал жертву",
                "Использует мощные лапы для оглушения жертвы."
            },
            CorrectAnswerIndex = 1
        },
        new Question
        {
            Text = "Рост и вес?",
            Answers = new List<string>
            {
                "150 киллограмм и 3 метра роста",
                "100 киллограмм и 2.5 метра роста",
                "150 киллограмм и 4 метра роста"
            },
            CorrectAnswerIndex = 0
        },
        new Question
        {
            Text = "Основное место обитания?",
            Answers = new List<string>
            {
                "Леса",
                "Болота",
                "Пещеры"
            },
            CorrectAnswerIndex = 2
        },
        new Question
        {
            Text = "Уникальная особенность таунтаунов?",
            Answers = new List<string>
            {
                "Состав крови",
                "Особая шерсть",
                "Плевок в глаза"
            },
            CorrectAnswerIndex = 0
        },
        new Question
        {
            Text = "Особенности брачных игр?",
            Answers = new List<string>
            {
                "Бодаются рогами",
                "Плевок",
                "Удар когтями"
            },
            CorrectAnswerIndex = 1
        },
        new Question
        {
            Text = "Эндемиками какой планеты являются?",
            Answers = new List<string>
            {
                "Илум",
                "Кристофсис",
                "Хот",
            },
            CorrectAnswerIndex = 2
        },
       
    };
    }
    


    private void QR_scan(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Page.Scan(_userName));
      
    }

    private void Visible_quest(object sender, EventArgs e)
    {
        Quest_rule.IsVisible = false;
        Button_start.IsVisible = false;
        Text_scroll.IsVisible = true;
        Button_scan.IsVisible = true;
    }
}