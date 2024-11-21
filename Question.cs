using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp3
{
    public class Question
    {
        public string Text { get; set; } // Текст вопроса
        public List<string> Answers { get; set; } // Список вариантов ответов
        public int CorrectAnswerIndex { get; set; } // Индекс правильного ответа
    }
}
