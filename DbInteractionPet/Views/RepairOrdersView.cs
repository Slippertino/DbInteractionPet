using System;
using System.Windows.Forms;
using DbInteractionPet.Models;
using DbInteractionPet.Formats;

namespace DbInteractionPet
{
    /// <summary>
    /// Форма отображения таблицы RepairOrders
    /// </summary>
    public class RepairOrdersView : View<RepairOrder>
    {
        /// <summary>
        /// Статический конструктор для установки базовых параметров
        /// </summary>
        static RepairOrdersView()
        {
            FormName     = "Заказы";
            ColumnsWidth = new int[] { 200, 200, 100, 220, 220 };
        }

        /// <summary>
        /// Вызывается конструктор
        /// абстрактного класса View
        /// </summary>
        public RepairOrdersView() : base()
        { }

        protected override void dataDisplayer_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            string errorMessage;

            switch (e.ColumnIndex)
            {
                case 0:
                    errorMessage = String.Concat("Некорректный регистрационный номер!\nФормат: целое число из диапазона ",
                                                 "(",
                                                 AutomobileFormat.MinRegisterNumber.ToString(),
                                                 " - ",
                                                 AutomobileFormat.MaxRegisterNumber.ToString(),
                                                 ").");
                    break;

                case 1:
                    errorMessage = String.Concat("Некорректный идентификатор механика!\nФормат: целое число ни менее ",
                                                 MechanicFormat.MinId.ToString(),
                                                 ".");
                    break;

                case 2:
                    errorMessage = String.Concat("Некорректная цена!\nФормат: вещественное число ни менее ",
                                                 RepairOrderFormat.MinCost.ToString(),
                                                 ".");
                    break;

                case 3:
                    errorMessage = "Некорректная дата!\nФормат: dd.mm.yyyy .";
                    break;

                case 4:
                    errorMessage = "Некорректная дата!\nФормат: dd.mm.yyyy .";
                    break;

                default:
                    errorMessage = "Встречены недопустимые символы!";
                    break;
            }

            MessageBox.Show(errorMessage, "Ошибка!", MessageBoxButtons.OK);
        }
    }
}
