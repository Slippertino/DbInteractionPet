using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Data.SqlClient;

namespace DbInteractionPet.Database
{
    /// <summary>
    /// Шаблонный класс для взаимодействия моделей 
    /// c соответствующими им таблицами из БД
    /// </summary>
    /// <typeparam name="T"> Тип модели </typeparam>
    public class DbContext<T>
    {
        /// <summary>
        /// Строка подключения к БД
        /// </summary>
        private const string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;
                                                  AttachDbFilename=|DataDirectory|\Database\AutoRepairShopDb.mdf;
                                                  Integrated Security=True";
        
        /// <summary>
        /// Название таблицы из БД
        /// </summary>
        private readonly string tableName;

        public DbContext(string _tableName)
        {
            tableName = _tableName;
        }

        /// <summary>
        /// Вспомогательная функция для выполнения определенной операции в БД
        /// </summary>
        /// <param name="sqlExpression"></param>
        private void ExecuteChanges(string sqlExpression)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                new SqlCommand(sqlExpression, connection).ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Функция выбора массива объектов модели по предикату
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public T[] Select(Predicate<T> predicate)
        {
            List<T> res = new List<T>();

            SqlDataReader reader;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sqlExpression = $"SELECT * FROM {tableName}";

                reader = new SqlCommand(sqlExpression, connection).ExecuteReader();

                while (reader.Read())
                {
                    object[] parametres = new object[reader.FieldCount];

                    reader.GetValues(parametres);

                    Type type = typeof(T);

                    ConstructorInfo constructor = type.GetConstructor(new Type[] { typeof(object[]) });

                    T cur = (T)constructor.Invoke(new object[] { parametres.ToArray() });

                    if (predicate(cur))
                        res.Add(cur);
                }
            }

            return res.ToArray();
        }

        /// <summary>
        /// Добавление объекта текущей модели в таблицу
        /// </summary>
        /// <param name="obj"></param>
        public void Add(T obj)
        {
            Type type = obj.GetType();

            string sqlExpression = $"INSERT INTO {tableName} VALUES (";

            foreach (var cur in type.GetProperties())
            {
                if (cur.PropertyType == typeof(string))
                {
                    sqlExpression += $"'{cur.GetValue(obj)}',";
                }
                else if (cur.PropertyType == typeof(DateTime))
                {
                    DateTime date = (DateTime)(cur.GetValue(obj));
                    sqlExpression += $"'{date.ToString("yyyy-MM-dd")}',";
                }
                else
                {
                    sqlExpression += $"{cur.GetValue(obj)},";
                }
            }

            if (sqlExpression.Length != 0)
            {
                sqlExpression = sqlExpression.Remove(sqlExpression.LastIndexOf(',')) + ')';
            }

            ExecuteChanges(sqlExpression);
        }

        /// <summary>
        /// Удаление объекта текущей модели из таблицы
        /// </summary>
        /// <param name="obj"></param>
        public void Delete(T obj)
        {
            string sqlExpression = $"DELETE {tableName} WHERE ";

            Type keyType = typeof(PrimaryKeyAttribute);

            var keysArray = obj.GetType()
                               .GetProperties()
                               .Where(property => property.IsDefined(keyType, false));

            foreach (var cur in keysArray)
            {
                sqlExpression += $"{cur.Name} = {cur.GetValue(obj)} AND ";
            }

            sqlExpression = sqlExpression.Remove((sqlExpression.LastIndexOf("AND")));

            ExecuteChanges(sqlExpression);
        }

        /// <summary>
        /// Обновление объекта текущей модели в таблице
        /// </summary>
        /// <param name="obj"></param>
        public void Update(T obj)
        {
            string sqlExpression = $"UPDATE {tableName} SET ";

            foreach (var cur in obj.GetType().GetProperties())
            {
                sqlExpression += $"{cur.Name} = ";

                if (cur.PropertyType == typeof(string))
                {
                    sqlExpression += $"'{cur.GetValue(obj)}',";
                }
                else if (cur.PropertyType == typeof(DateTime))
                {
                    DateTime date = (DateTime)(cur.GetValue(obj));
                    sqlExpression += $"'{date.ToString("yyyy-MM-dd")}',";
                }
                else
                    sqlExpression += $"{cur.GetValue(obj)},";
            }

            sqlExpression = sqlExpression.Remove((sqlExpression.LastIndexOf(",")));

            Type keyType = typeof(PrimaryKeyAttribute);

            var keysArray = obj.GetType()
                               .GetProperties()
                               .Where(property => property.IsDefined(keyType, false));

            sqlExpression += " WHERE ";

            foreach (var cur in keysArray)
            {
                sqlExpression += $"{cur.Name} = {cur.GetValue(obj)} AND ";
            }

            sqlExpression = sqlExpression.Remove((sqlExpression.LastIndexOf("AND")));

            ExecuteChanges(sqlExpression);
        }

    }
}
