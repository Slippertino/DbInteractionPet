using System;
using System.Windows.Forms;
using DbInteractionPet.Models;
using DbInteractionPet.Formats;

namespace DbInteractionPet
{
    /// <summary>
    /// Форма отображения таблицы Mechanics
    /// </summary>
    public class MechanicsView : View<Mechanic>
    {
        /// <summary>
        /// Статический конструктор для установки базовых параметров
        /// </summary>
        static MechanicsView()
        {
            FormName     = "Механики"; 
            ColumnsWidth = new int[] { 188, 188, 188, 188, 188 };
        }

        /// <summary>
        /// Вызывается конструктор
        /// абстрактного класса View
        /// </summary>
        public MechanicsView() : base()
        { }

        protected override void dataDisplayer_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            string errorMessage;

            switch (e.ColumnIndex)
            {
                case 0:
                    errorMessage = String.Concat("Некорректный идентификатор механика!\nФормат: число ни менее ",
                                                 MechanicFormat.MinId.ToString(),
                                                 ".");
                    break;
                case 4:
                    errorMessage = String.Concat("Некорректный номер категории!\nФормат: число из диапазона ", 
                                                 "(", 
                                                 MechanicFormat.MinCategory.ToString(),
                                                 " - ",
                                                 MechanicFormat.MaxCategory.ToString(),
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
