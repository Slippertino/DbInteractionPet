using System.Text.RegularExpressions;
using DbInteractionPet.Formats;
using DbInteractionPet.Database;

namespace DbInteractionPet.Models
{
    /// <summary>
    /// Модель - Механик
    /// </summary>
    public class Mechanic : Entity<Mechanic>
    {
        /// <summary>
        /// Номер механика (первичный ключ в БД)
        /// </summary>
        [PrimaryKey]
        public int Id { get; set; }

        /// <summary>
        /// Полное имя
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Номер
        /// </summary>
        public string Number  { get; set; }

        /// <summary>
        /// Категория
        /// </summary>
        public int Category { get; set; }

        /// <summary>
        /// Конструктор принимает массив объектов параметров
        /// в порядке их определения:
        /// -Id,
        /// -FullName,
        /// -Address,
        /// -Number,
        /// -Category
        /// </summary>
        /// <param name="parametres"></param>
        public Mechanic(object[] parametres)
        {
            Id       = (int)   parametres[0];
            FullName = (string)parametres[1];
            Address  = (string)parametres[2];
            Number   = (string)parametres[3];
            Category = (int)   parametres[4];
        }

        public override bool IsValidInstance()
        {
            bool res = true;

            res &= (Id >= MechanicFormat.MinId);

            res &= new Regex(MechanicFormat.FullNamePattern).IsMatch(FullName);
            res &= new Regex(MechanicFormat.AddressPattern).IsMatch(Address);
            res &= new Regex(MechanicFormat.NumberPattern).IsMatch(Number);

            res &= Category >= MechanicFormat.MinCategory &&
                   Category <= MechanicFormat.MaxCategory;

            return res;
        }

        public override bool IsUniqueInstance()
        {
            return Mechanic.GetData(
                delegate (Mechanic obj) 
                { 
                    return obj.Id == Id; 
                }).Length == 0;
        }
    }
}
