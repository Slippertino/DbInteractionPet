using System.Text.RegularExpressions;
using DbInteractionPet.Formats;
using DbInteractionPet.Database;

namespace DbInteractionPet.Models
{
    /// <summary>
    /// Модель - Автомобиль
    /// </summary>
    public class Automobile : Entity<Automobile>
    {
        /// <summary>
        /// Регистрационный номер (первичный ключ в БД)
        /// </summary>
        [PrimaryKey]
        public int RegisterNumber { get; set; }

        /// <summary>
        /// Марка
        /// </summary>
        public string Mark { get; set; }

        /// <summary>
        /// Цвет
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Год выпуска
        /// </summary>
        public int ReleaseYear { get; set; }

        /// <summary>
        /// Конструктор принимает массив объектов параметров
        /// в порядке их определения:
        /// -RegisterNumber,
        /// -Mark,
        /// -Color,
        /// -ReleaseYear
        /// </summary>
        /// <param name="parametres"></param>
        public Automobile(object[] parametres)
        {
            RegisterNumber = (int)   parametres[0];
            Mark           = (string)parametres[1];
            Color          = (string)parametres[2];
            ReleaseYear    = (int)   parametres[3];
        }

        public override bool IsValidInstance()
        {
            bool res = true;

            res &= RegisterNumber >= AutomobileFormat.MinRegisterNumber && 
                   RegisterNumber <= AutomobileFormat.MaxRegisterNumber;

            res &= new Regex(AutomobileFormat.MarkPattern).IsMatch(Mark);

            res &= new Regex(AutomobileFormat.ColorPattern).IsMatch(Color);

            res &= ReleaseYear >= AutomobileFormat.MinReleaseYear && 
                   ReleaseYear <= AutomobileFormat.MaxReleaseYear;

            return res;
        }

        public override bool IsUniqueInstance()
        {
            return Automobile.GetData(
                delegate (Automobile obj) 
                { 
                    return obj.RegisterNumber == RegisterNumber; 
                }).Length == 0;
        }
    }
}
