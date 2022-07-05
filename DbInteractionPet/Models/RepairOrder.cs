using System;
using System.Text.RegularExpressions;
using DbInteractionPet.Formats;
using DbInteractionPet.Database;

namespace DbInteractionPet.Models
{
    /// <summary>
    /// Модель - Заказ
    /// </summary>
    public class RepairOrder : Entity<RepairOrder>
    {
        /// <summary>
        /// Регистрационный номер автомобиля (первичный ключ в БД)
        /// </summary>
        [PrimaryKey]
        public int AutoRegisterNumber  { get; set; }

        /// <summary>
        /// Идентификатор механика (первичный ключ в БД)
        /// </summary>
        [PrimaryKey]
        public int AutoMechanicId { get; set; }

        /// <summary>
        /// Стоимость ремонта
        /// </summary>
        public decimal  Cost { get; set; }

        /// <summary>
        /// Планируемая дата окончания ремонта
        /// </summary>
        public DateTime PlannedEndDate { get; set; }

        /// <summary>
        /// Реальная дата окончания ремонта
        /// </summary>
        public DateTime RealEndDate { get; set; }

        /// <summary>
        /// Конструктор принимает массив объектов параметров
        /// в порядке их определения:
        /// -AutoRegisterNumber,
        /// -AutoMechanicId,
        /// -Cost,
        /// -PlannedEndDate,
        /// -RealEndDate
        /// </summary>
        /// <param name="parametres"></param>
        public RepairOrder(object[] parametres)
        {
            AutoRegisterNumber = (int)     parametres[0];
            AutoMechanicId     = (int)     parametres[1];
            Cost               = (decimal) parametres[2];
            PlannedEndDate     = (DateTime)parametres[3];
            RealEndDate        = (DateTime)parametres[4];
        }

        public override bool IsValidInstance()
        {
            bool res = true;

            res &=  AutoRegisterNumber >= AutomobileFormat.MinRegisterNumber &&
                    AutoRegisterNumber <= AutomobileFormat.MaxRegisterNumber;

            res &=  AutoMechanicId >= MechanicFormat.MinId;

            res &=  Cost >= RepairOrderFormat.MinCost;

            res &=  new Regex(RepairOrderFormat.PlannedEndDatePattern).IsMatch(PlannedEndDate.ToShortDateString());

            res &=  new Regex(RepairOrderFormat.RealEndDatePattern).IsMatch(((DateTime)RealEndDate).ToShortDateString());

            return res;
        }

        public override bool IsUniqueInstance()
        {
            return RepairOrder.GetData(
                delegate (RepairOrder obj) 
                {
                    return obj.AutoRegisterNumber == AutoRegisterNumber &&
                           obj.AutoMechanicId == AutoMechanicId;
                }).Length == 0;
        }
    }
}
