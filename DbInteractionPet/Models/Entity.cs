using System;
using DbInteractionPet.Database;
using DbInteractionPet.Formats;

namespace DbInteractionPet.Models
{
    /// <summary>
    /// Родительский класс всех моделей,
    /// определяющий общий функционал,
    /// включая взаимодействие с БД и валидацию
    /// </summary>
    /// <typeparam name="T"> Тип модели </typeparam>
    public abstract class Entity<T> 
    {
        /// <summary>
        /// Объект для взаимодействия с БД
        /// </summary>
        private static DbContext<T> _context;

        /// <summary>
        /// Статический конструктор для инициализации _context
        /// </summary>
        static Entity()
        {
            Type type = typeof(T);

            if (type == typeof(Automobile))
            {
                _context = new DbContext<T>(AutomobileFormat.TableName);
            }
            else if (type == typeof(Mechanic))
            {
                _context = new DbContext<T>(MechanicFormat.TableName);
            }
            else
            {
                _context = new DbContext<T>(RepairOrderFormat.TableName);
            }
        }

        /// <summary>
        /// Возвращает массив объектов типа T по предикату predicate из БД
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static T[] GetData(Predicate<T> predicate)
        {
            return _context.Select(predicate);
        }

        /// <summary>
        /// Добавляет текущий объект в БД
        /// </summary>
        public static void Add(T obj)    => _context.Add(obj);

        /// <summary>
        /// Удаляет текущий объект из БД
        /// </summary>
        public static void Delete(T obj) => _context.Delete(obj);

        /// <summary>
        /// Обновляет текущий объект в БД
        /// </summary>
        public static void Update(T obj) => _context.Update(obj);

        /// <summary>
        /// Проверка на валидность данных
        /// </summary>
        /// <returns></returns>
        public abstract bool IsValidInstance();

        /// <summary>
        /// Проверка на уникальность данных
        /// </summary>
        /// <returns></returns>
        public abstract bool IsUniqueInstance();
    }
}
