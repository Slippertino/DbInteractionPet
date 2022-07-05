using System;
using System.Windows.Forms;

namespace DbInteractionPet
{
    /// <summary>
    /// Форма главного меню
    /// </summary>
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик клика по кнопке,
        /// которая переводит пользователя
        /// в им выбранную категорию базы данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Click(object sender, EventArgs e)
        {
            this.Hide();

            switch (((Button)sender).Text)
            {
                case "Автомобили":
                     new AutomobilesView()  .ShowDialog();
                     break;
                case "Механики":
                     new MechanicsView()    .ShowDialog();
                    break;
                case "Заказы":
                     new RepairOrdersView() .ShowDialog();
                    break;
            }

            this.Show();
        }
    }
}
