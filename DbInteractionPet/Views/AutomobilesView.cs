using System;
using System.Windows.Forms;
using DbInteractionPet.Models;
using DbInteractionPet.Formats;

namespace DbInteractionPet
{
    /// <summary>
    /// Форма отображения таблицы Automobiles
    /// </summary>
    public class AutomobilesView : View<Automobile>
    {
        /// <summary>
        /// Статический конструктор для установки базовых параметров
        /// </summary>
        static AutomobilesView()
        {
            FormName     = "Автомобили";
            ColumnsWidth = new int[]{ 235, 235, 235, 235 };
        }

        /// <summary>
        /// Вызывается конструктор
        /// абстрактного класса View
        /// </summary>
        public AutomobilesView() : base()
        { }

        protected override void dataDisplayer_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            string errorMessage;

            switch (e.ColumnIndex)
            {
                case 0:
                    errorMessage = String.Concat("Некорректный регистрационный номер!\nФормат: число из диапазона ",
                                                 "(",
                                                 AutomobileFormat.MinRegisterNumber.ToString(),
                                                 " - ",
                                                 AutomobileFormat.MaxRegisterNumber.ToString(),
                                                 ").");
                    break;
                case 3:
                    errorMessage = String.Concat("Некорректный год выпуска!\nФормат: число из диапазона ",
                                                 "(",
                                                 AutomobileFormat.MinReleaseYear.ToString(),
                                                 " - ",
                                                 AutomobileFormat.MaxReleaseYear.ToString(),
                                                 ").");
                    break;
                default:
                    errorMessage = "Встречены недопустимые символы!";
                    break;
            }

            MessageBox.Show(errorMessage, "Ошибка!", MessageBoxButtons.OK);
        }
    }
}
