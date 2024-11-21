namespace MauiApp3.Page;

public partial class Quiz_Page : ContentPage
{
    private string _userName;
    private Query_SQL _query_sql;
    public Quiz_Page(string name)
	{
        InitializeComponent();
        _query_sql = new Query_SQL(name);
        Login_user.Text = name;
        _userName = name;

        LoadQuestions();
        DisplayQuestion();
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

    private void Fail_Question(object sender, EventArgs e)
    {
        count_qustions++;
        Fail_answer.IsVisible = true;

        Dispatcher.DispatchDelayed(TimeSpan.FromSeconds(2), () =>
        {
            Fail_answer.IsVisible = false;

            // Удаляем текущий вопрос
            _questions.RemoveAt(_currentQuestionIndex);

            if (_questions.Count > 0&&count_qustions<=4)
            {
                // Переходим к следующему вопросу
                DisplayQuestion();
            }
            else
            {
                // Завершаем викторину
                QuizEndLabel.IsVisible = true;
                QuestionLabel.IsVisible = false;
                Button1.IsVisible = false;
                Button2.IsVisible = false;
                Button3.IsVisible = false;
                Button4.IsVisible = false;
                strelka_pravo.IsVisible = true;
                strelka_vlevo.IsVisible = true;
                Nazad_Button.IsVisible = true;
                _query_sql.AddQuestProgressUser(_userName, questName);
            }
        });
       
    }
    private List<Question> _questions;
    private int _currentQuestionIndex;

    private void LoadQuestions()
    {
        _questions = new List<Question>
    {
        new Question
        {
            Text = "С какой фразы начинается космическая киносага Джорджа Лукаса?",
            Answers = new List<string>
            {
                "Все счастливые семьи похожи друг на друга, каждая несчастливая семья несчастлива по-своему…",
                "В тридевятом царстве, в тридесятом государстве…",
                "Давным-давно в далекой-далекой галактике…",
                "Совсем недавно на соседней планете…"
            },
            CorrectAnswerIndex = 2
        },

        new Question
        {
            Text = "Квай Гон Джин при побеге с Набу вместе с королевой Амидалой, выбрал эту планету для того, чтобы отремонтировать поврежденный ранее при прорыве блокады гипердвигатель ",
            Answers = new List<string>
            {
                "Коррусант",
                "Татуин",
                "Куат",
                "Альдераан"
            },
            CorrectAnswerIndex = 1
        },
        new Question
        {
            Text = "Именно на эту планету после третьего эпизода саги Магистр Йода отправился в изгнание",
            Answers = new List<string>
            {
                "Коррибан",
                "Беспин",
                "Энодор",
                "Дагоба"
            },
            CorrectAnswerIndex = 3
        },
        new Question
        {
            Text = "На этой планете находилась арена Петранаки, в которой проходили казни и выступления гладитаоров местной инсектоидной расы. Она же стала местом спасения для Оби Вана Кеноби, Энакина Скайвокера и Падме Амидалы, nармией клонов во втором эпизоде.",
            Answers = new List<string>
            {
                "Илум",
                "Датомир",
                "Джеонозис",
                "Кессель"
            },
            CorrectAnswerIndex = 2
        },
        new Question
        {
            Text = "Священная планета для Джедаев, именно на ней юнлинги проходят испытание под названием «Сбор» для поиска кристаллов для своих мечей ",
            Answers = new List<string>
            {
                "Илум",
                "Анаксис",
                "Ботавуи",
                "Нар-Шада"
            },
            CorrectAnswerIndex = 0
        },

        new Question
        {
            Text = "Первая ММО по вселенной Звездных Войн вышедшая 26 июня 2003 года, и закрытая спустя восемь лет с выходом новой он-лайн игры.",
            Answers = new List<string>
            {
                "Star Wars Galaxies",
                "Star Wars The Old Republic",
                "Star Wars: Republic Heroes",
                "Star Wars Republic Commando"
            },
            CorrectAnswerIndex = 0
        },
        new Question
        {
            Text = "В одной из кампаний этой игры мы выступаем от лица боевого дроида ООМ-9, сама же игра представляет собой стратегию в реальном времени, где от кампании к кампании мы руководим армиями разных сторон конфликта во временном промежутке от кризиса на Набу, до падения Империи. ",
            Answers = new List<string>
            {
                "Star Wars: Original Trilogy",
                "Star Wars: Galactic Battlegrounds",
                "Star Wars: Uprising",
                "Angry Birds Star Wars"
            },
            CorrectAnswerIndex = 1
        },
        
        new Question
        {
            Text = "Прообразом для этого корабля, по слухам, послужил недоеденный гамбургер с маслиной на зубочистке.",
            Answers = new List<string>
            {
                "Тысячелетний Сокол",
                "Звезда Смерти",
                "Экс-вин",
                "Истребитель Тай"
            },
            CorrectAnswerIndex = 0
        },
        new Question
        {
            Text = "Первоначальное название этой части фильма звучало как «Месть джедая», но позднее она обрела своё каноничное название.",
            Answers = new List<string>
            {
                "Эпизод 5. Империя наносит ответный удар",
                "Эпизод 4. Новая надежда",
                "Эпизод 6. Возвращение джедая",
                "Эпизод 3. Месть ситхов"
            },
            CorrectAnswerIndex = 2
        },
        new Question
        {
            Text = "В этой игре мы можем повстречать таких персонажей, как Реван, Сатель Шан и Дарт Анграл. А сами можем побывать в роли ситха-инквизитора, контрабандиста или джедая-рыцаря, а также других классов.",
            Answers = new List<string>
            {
                "Star Wars: The Old Republic",
                "Star Wars: Jedi Academy",
                "Star Wars: Republic Commando",
                "Star Wars: Empire at war"
            },
            CorrectAnswerIndex = 0
        },
        new Question
        {
            Text = "Эта планета была родиной расы Таунги, позже перебравшейся на Рун и в дальнейшем на Мандалор. А её название происходит от латинского слова, которое переводится как 'Сверкать, искриться'.",
            Answers = new List<string>
            {
                "Корусант",
                "Мандалор",
                "Набу",
                "Татуин"
            },
            CorrectAnswerIndex = 0
        },
        new Question
        {
            Text = "Тактический шутер от первого лица, в котором мы играем за клона-командоса РК-1138 по прозвищу Босс.",
            Answers = new List<string>
            {
                "Star Wars: Republic Commando",
                "Star Wars: Battlefront",
                "Star Wars: Jedi Outcast",
                "Star Wars: The Force Unleashed"
            },
            CorrectAnswerIndex = 0
        },
        new Question
        {
            Text = "Игра, сделанная на движке Quake III Arena, где мы можем побывать в роли Джейдена Корра, который только поступил в академию после самостоятельного сбора светового меча.",
            Answers = new List<string>
            {
                "Star Wars: Jedi Knight: Jedi Academy",
                "Star Wars: The Old Republic",
                "Star Wars: Jedi Fallen Order",
                "Star Wars: Uprising"
            },
            CorrectAnswerIndex = 0
        },
        new Question
        {
            Text = "Во время съёмки последнего фильма трилогии, этот актёр просто умолял Лукаса убить своего персонажа, чтобы концовка была более драматической, но ему отказали. Его просьбу удовлетворили только в седьмом эпизоде.",
            Answers = new List<string>
            {
                "Харрисон Форд",
                "Марк Хэмилл",
                "Иэн Макдиармид",
                "Юэн Макгрегор"
            },
            CorrectAnswerIndex = 0
        },
        new Question
        {
            Text = "Встреча Леи, Хана и Трипио с этим космическим животным является аллюзией на миф об Ионе и ките.",
            Answers = new List<string>
            {
                "Космический слизень (Экзогорт)",
                "Кракен",
                "Сарлакк",
                "Ранкор"
            },
            CorrectAnswerIndex = 0
        },
        new Question
        {
            Text = "В качестве астероидов в первом фильме трилогии сыграл именно этот продукт.",
            Answers = new List<string>
            {
                "Шоколад",
                "Яблоко",
                "Картофелина",
                "Арбуз"
            },
            CorrectAnswerIndex = 2
        },
        new Question
        {
            Text = "В годы трилогии приквелов этот маленький джедай носил титул гранд-мастера уже более пятисот лет.",
            Answers = new List<string>
            {
                "Ки-Ади-Мунди",
                "Мейс Винду",
                "Квай-Гон Джин",
                "Йода"
            },
            CorrectAnswerIndex = 3
        },
        new Question
        {
            Text = "Он является первым джедаем, которого увидел зритель, посетивший премьеру Звёздных Войн в 1977 году.",
            Answers = new List<string>
            {
                "Оби-Ван Кеноби",
                "Йода",
                "Энакин Скайуокер",
                "Люк Скайуокер"
            },
            CorrectAnswerIndex = 0
        },
        new Question
        {
            Text = "Согласно легенде, на световом мече этого джедая были выгравированы три символа «BMF».",
            Answers = new List<string>
            {
                "Оби-Ван Кеноби",
                "Квай-Гон Джин",
                "Мейс Винду",
                "Энакин Скайуокер"
            },
            CorrectAnswerIndex = 2
        },
        new Question
        {
            Text = "Несмотря на запрет, у этого магистра джедая было несколько жён и детей.",
            Answers = new List<string>
            {
                "Ки-Ади-Мунди",
                "Квай-Гон Джин",
                "Мейс Винду",
                "Йода"
            },
            CorrectAnswerIndex = 0
        },
        new Question
        {
            Text = "Имя этому комичному персонажу, которого невзлюбили многие фанаты Звёздных Войн, придумал пятилетний сын Лукаса.",
            Answers = new List<string>
            {
                "Эвок",
                "Ватто",
                "Сарлакк",
                "Джа-Джа Бинкс"
            },
            CorrectAnswerIndex = 3
        },
        new Question
        {
            Text = "Сам Джордж Лукас не отказался сыграть камео в фильме. Это была незначительная сцена, где он сыграл роль барона Папанойды. Назовите эпизод, в котором была эта сцена.",
            Answers = new List<string>
            {
                "Эпизод 3. Месть ситхов",
                "Эпизод 1. Скрытая угроза",
                "Эпизод 6. Возвращение джедая",
                "Эпизод 4. Новая надежда"
            },
            CorrectAnswerIndex = 0
        },
        new Question
        {
            Text = "Говорят, что как только Лукас увидел обложку комикса с этим персонажем-джедаем на ней, то был настолько впечатлен, что включил ее во второй эпизод саги",
            Answers = new List<string>
            {
                "Баррис Оффи",
                "Асока Тано",
                "Орра Синг",
                "Эйла Секура"
            },
            CorrectAnswerIndex = 3
        },
        new Question
        {
            Text = "После того, как Квай Гон Джин взял образец крови для анализа у юного Энакина Скайвокера, мы узнаем о Мидихлорианах, новом явлении в Мире Звездных Войн, так же мы узнаем, что у Избранного их концентрация более ...., что больше, чем у Магистра Йоды, назовите пропущеннуюцифру",
            Answers = new List<string>
            {
                "20 000",
                "200 000",
                "10 000",
                "5 000"
            },
            CorrectAnswerIndex = 0
        },
        new Question
        {
            Text = "Отличительной особенностью Севтового меча была то, что он с легкостью проходил сквозь любой материал. Однако в Легендах (или старом каноне) говорится об одном металле, который мог сдерживать всепроходимость джедайского оружия. В новом же каноне из металла он превратился в минерал, но своих свойств не потерял.",
            Answers = new List<string>
            {
                "Вибраниум",
                "Кортозис",
                "Террасталь",
                "Бескар"
            },
            CorrectAnswerIndex = 1
        },
        new Question
        {
            Text = "Не сразу заметно, но именно из-за этой особенности штурмовой винтовки Е-11, все штурмовики в оригинальной трилогии - левши. Что это за особенность? ",
            Answers = new List<string>
            {
                "В мире звездных войн больше левшей",
                "Чтоб обосновать их пропахи",
                "Магазин оружия вставлялся слева",
                "Неправльно произвели реквезит"
            },
            CorrectAnswerIndex = 2
        },
        new Question
        {
            Text = "В игре Star Wars: Republic Commando Модульная оружейная система DC-17m может трансформироваться в гранатомет,автомат и снайперскую винтовку, только вот штык не прикрутили и поэтому оружие ближнего боя располагалось ... Где было расположено оружие ближнего боя у Командо? ",
            Answers = new List<string>
            {
                "Перчатка",
                "На конец оружия",
                "На поясе",
                "На шее"
            },
            CorrectAnswerIndex = 0
        },
        new Question
        {
            Text = "Штурмовика Первого ордена под номером FN-2187 мы встречаем почти с самых первых кадров фильма, назовите имя, которое он носил в дальнейшем в трилогии фильмов ",
            Answers = new List<string>
            {
                "Сноук",
                "Финн",
                "Плэгас",
                "Бен"
            },
            CorrectAnswerIndex = 1
        },
        new Question
        {
            Text = "Рабочее название этого эпизода было «Черный Брилиант», назовите его порядковый номер?",
            Answers = new List<string>
            {
                "Эпизод 7",
                "Эпизод 6",
                "Эпизод 3",
                "Эпизод 9"
            },
            CorrectAnswerIndex = 3
        },
        new Question
        {
            Text = "Во всех фильмах саги, звучит одна и та же фраза устами разных персонажей - «У меня плохое предчувствие», но при просмотре этого эпизода фанаты не услышали знакомую фразу, а все из-за того, что эту фразу произносит ..., назовите, кто произносит эту фразу в восьмом эпизоде ",
            Answers = new List<string>
            {
                "BB-8",
                "Джабба Хат",
                "R2-D2",
                "Её забыли вставить"
            },
            CorrectAnswerIndex = 0
        },

        new Question
        {
            Text = " На этой планете Дарт Сидиус готовил свой план и скрывал свой флот для повторного покорения галактики, одним из пунктов плана было вырастить Верховного лидера Сноука в лаборатории. ",
            Answers = new List<string>
            {
                "Экзегол",
                "Датомир",
                "Галатор III",
                "Корвус"
            },
            CorrectAnswerIndex = 0
        },
    };
    }
    int count_qustions;
    private void DisplayQuestion()
    {
        if (_questions.Count == 0)
            return;

        var random = new Random();
        _currentQuestionIndex = random.Next(_questions.Count);
        var question = _questions[_currentQuestionIndex];

        // Обновляем текст вопроса
        QuestionLabel.Text = question.Text;

        // Устанавливаем текст кнопок
        Button1.Text = question.Answers[0];
        Button2.Text = question.Answers[1];
        Button3.Text = question.Answers[2];
        Button4.Text = question.Answers[3];

        // Сбрасываем предыдущие обработчики
        Button1.Clicked -= HandleAnswerClick;
        Button2.Clicked -= HandleAnswerClick;
        Button3.Clicked -= HandleAnswerClick;
        Button4.Clicked -= HandleAnswerClick;

        // Привязываем обработчики к кнопкам
        Button1.Clicked += HandleAnswerClick;
        Button2.Clicked += HandleAnswerClick;
        Button3.Clicked += HandleAnswerClick;
        Button4.Clicked += HandleAnswerClick;

        // Помечаем правильную кнопку
        Button1.ClassId = "0";
        Button2.ClassId = "1";
        Button3.ClassId = "2";
        Button4.ClassId = "3";
    }
    private void HandleAnswerClick(object sender, EventArgs e)
    {
        if (sender is Button clickedButton)
        {
            int selectedAnswerIndex = int.Parse(clickedButton.ClassId);

            // Проверяем, правильный ли это ответ
            if (selectedAnswerIndex == _questions[_currentQuestionIndex].CorrectAnswerIndex)
            {
                Right_answer(sender, e);
            }
            else
            {
                Fail_Question(sender, e);
            }
        }
    }

    string questName = "Викторина";

    private void Right_answer(object sender, EventArgs e)
    {
        int coinsToAdd = 1000; // Количество монет за правильный ответ
        count_qustions++;
        // Обновляем деньги пользователя в базе данных
        _query_sql.AddMoneyToUser(_userName, coinsToAdd);

        // Обновляем отображение денег в интерфейсе
        UpdateUserMoney();

        // Удаляем текущий вопрос и отображаем следующий
        _questions.RemoveAt(_currentQuestionIndex);

        if (_questions.Count > 0 && count_qustions <= 4)
        {
            DisplayQuestion();
        }
        else
        {
            // Завершаем викторину
            QuizEndLabel.IsVisible = true;
            QuestionLabel.IsVisible = false;
            Button1.IsVisible = false;
            Button2.IsVisible = false;
            Button3.IsVisible = false;
            Button4.IsVisible = false;
            strelka_pravo.IsVisible = true;
            strelka_vlevo.IsVisible = true;
            Nazad_Button.IsVisible = true;
            _query_sql.AddQuestProgressUser(_userName, questName);
        }
       
    }

}