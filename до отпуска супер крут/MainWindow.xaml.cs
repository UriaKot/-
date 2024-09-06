using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace до_отпуска_супер_крут
{

    public partial class MainWindow : Window
    {
        private DateTime _vacationDate;
        private List<DateTime> _holidays;

        public MainWindow()
        {
            InitializeComponent();
            // Загрузка праздничных дней из файла или другого источника
            _holidays = LoadHolidays();

            // Инициализация даты по умолчанию
            _vacationDate = new DateTime(2025, 06, 1);
            datePicker.SelectedDate = _vacationDate;
            UpdateCountdown();
        }

        // Обработчик события изменения даты в DatePicker
        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            _vacationDate = datePicker.SelectedDate.Value;
            UpdateCountdown();
        }

        // Обновление обратного отсчета
        private void UpdateCountdown()
        {
            // Получение текущей даты и времени
            DateTime now = DateTime.Now;

            // Проверка, если дата отпуска уже прошла
            if (_vacationDate < now)
            {
                countdownLabel.Content = "Отпуск уже прошел!";
                return;
            }

            // Подсчет оставшихся дней
            TimeSpan remainingTime = _vacationDate - now;
            int remainingDays = GetWorkingDays(_vacationDate, now);

            // Отображение обратного отсчета
            countdownLabel.Content = $"До отпуска осталось: {remainingDays} дней";
        }

        // Метод для расчета рабочих дней
        private int GetWorkingDays(DateTime endDate, DateTime startDate)
        {
            int workingDays = 0;

            // Перебираем каждый день в интервале
            for (DateTime date = startDate.Date; date <= endDate.Date; date = date.AddDays(1))
            {
                // Проверка, является ли день выходным
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                {
                    // Проверка, является ли день праздником
                    if (!_holidays.Contains(date))
                    {
                        workingDays++;
                    }
                }
            }

            return workingDays;
        }

        // Метод для загрузки списка праздничных дней
        private List<DateTime> LoadHolidays()
        {
            // Загрузка праздничных дней из файла или другого источника
            // Например, можно использовать текстовый файл с датами праздничных дней
            // или получить их из API.
            List<DateTime> holidays = new List<DateTime>();
            // Добавляем пример праздничных дней
            holidays.Add(new DateTime(2024, 1, 1)); // Новый год
            holidays.Add(new DateTime(2024, 5, 1)); // Праздник труда
            holidays.Add(new DateTime(2024, 12, 25)); // Рождество
            return holidays;
        }
    }
}