using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.Data;
using DbInteractionPet.Models;

namespace DbInteractionPet
{
    /// <summary>
    /// Родительский класс для всех форм
    /// </summary>
    /// <typeparam name="T"> Тип модели </typeparam>
    public abstract partial class View<T> : Form 
                                  where T : Entity<T>
    {
        /// <summary>
        /// Текущая таблица
        /// </summary>
        private DataTable table;

        /// <summary>
        /// Индексы добавленных в таблицу строчек
        /// </summary>
        private List<int> addedIndexs    = new List<int>();

        /// <summary>
        /// Копии удаленных из таблицы объектов
        /// </summary>
        private List<T>   deletedObjects = new List<T>();

        /// <summary>
        /// Название формы
        /// </summary>
        protected static string FormName     { get; set; }

        /// <summary>
        /// Пользовательская разметка столбцов
        /// </summary>
        protected static int[]  ColumnsWidth { get; set; }

        protected View()
        {
            InitializeComponent();

            this.Text = FormName;

            InitDataView();
            FillDataView();
            MarkUpDataView();
        }

        /// <summary>
        /// Инциализирует столбцы таблицы
        /// </summary>
        private void InitDataView()
        {
            table = new DataTable();

            foreach (var cur in typeof(T).GetProperties())
            {
                DataColumn column = new DataColumn(cur.Name, cur.PropertyType);
                column.AllowDBNull = true;

                table.Columns.Add(column);
            }
        }

        /// <summary>
        /// Заполняет таблицы данными из БД
        /// </summary>
        private void FillDataView()
        {
            T[] objs;
            try
            {
                objs = Entity<T>.GetData((T obj) => { return true; });
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Инициализация таблицы не удалась!", "Ошибка!", MessageBoxButtons.OK);
                return;
            }

            table.Clear();

            for (int i = 0; i < objs.Length; i++)
            {
                DataRow row = table.NewRow();

                List<object> values = new List<object>();

                foreach (var cur in typeof(T).GetProperties())
                    values.Add(cur.GetValue(objs[i]));

                row.ItemArray = values.ToArray();

                table.Rows.Add(row);
            }

            dataDisplayer.DataSource = table;
        }

        /// <summary>
        /// Устанавливает ширину столбцов
        /// </summary>
        private void MarkUpDataView()
        {
            for (int i = 0; i < dataDisplayer.Columns.Count; i++)
            {
                dataDisplayer.Columns[i].Width = ColumnsWidth[i];
            }
        }

        /// <summary>
        /// Обработчик события форматирования ячейки DataGridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            e.CellStyle.Font = new Font(dataDisplayer.ColumnHeadersDefaultCellStyle.Font.FontFamily, 9f, FontStyle.Regular);
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        /// <summary>
        /// Обработчик события ошибки заполнения ячейки DataGridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected abstract void dataDisplayer_DataError(object sender, DataGridViewDataErrorEventArgs e);

        /// <summary>
        /// Добавляет новые объекты в БД
        /// </summary>
        private void AddObjects()
        {
            foreach (var cur in addedIndexs)
            {
                try
                {
                    T obj = InitModel(cur);

                    if (obj.IsValidInstance() && obj.IsUniqueInstance())
                    {
                        Entity<T>.Add(obj);
                    }
                }
                catch (ArgumentException)
                {
                    MessageBox.Show("Добавление не удалось!", "Ошибка!", MessageBoxButtons.OK);
                    return;
                }
            }
        }

        /// <summary>
        /// Удаляет выбранные объекты из БД
        /// </summary>
        private void DeleteObjects()
        {
            foreach (var cur in deletedObjects)
            {
                try
                {
                    Entity<T>.Delete(cur);
                }
                catch (ArgumentException)
                {
                    MessageBox.Show("Удаление не удалось!", "Ошибка!", MessageBoxButtons.OK);
                    return;
                }
            }
        }

        /// <summary>
        /// Обновляет все объекты в БД
        /// </summary>
        private void UpdateObjects()
        {
            for (int i = 0; i < dataDisplayer.Rows.Count; i++)
            {
                try
                {
                    T obj = InitModel(i);

                    if (obj.IsValidInstance())
                    {
                        Entity<T>.Update(obj);
                    }
                }
                catch (ArgumentException)
                {
                    MessageBox.Show("Обновление не удалось!", "Ошибка!", MessageBoxButtons.OK);
                    return;
                }
            }
        }

        /// <summary>
        /// Обновляет состояние формы
        /// </summary>
        private void Reset()
        {
            addedIndexs.Clear();
            deletedObjects.Clear();

            FillDataView();
            MarkUpDataView();

            dataDisplayer.Invalidate();
        }

        /// <summary>
        /// Инициализирует объект модели типа T,
        /// беря данные по номеру строки в DataGridView
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        private T InitModel(int rowIndex)
        {
            List<object> parametres = new List<object>();

            for (int i = 0; i < dataDisplayer.Rows[rowIndex].Cells.Count; i++)
            {
                if (dataDisplayer.Rows[rowIndex].Cells[i].Value == null)
                {
                    throw new Exception();
                }
                parametres.Add(dataDisplayer.Rows[rowIndex].Cells[i].Value);
            }

            Type type = typeof(T);

            ConstructorInfo constructor = type.GetConstructor(new Type[] {typeof(object[])});

            object result = constructor.Invoke(new object[] { parametres.ToArray() });

            return (T)result;
        }

        /// <summary>
        /// Обработчик клика по кнопке "Добавить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addButton_Click(object sender, EventArgs e)
        {
            if (dataDisplayer.Columns.Count != 0)
            {
                DataRow row = table.NewRow();
                addedIndexs.Add(table.Rows.Count);
                table.Rows.Add(row);
            }
        }

        /// <summary>
        /// Обработчик клика по кнопке "Удалить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataDisplayer.SelectedRows)
            {
                if (addedIndexs.Contains(row.Index))
                {
                    addedIndexs.Remove(row.Index);
                }
                else
                {
                    deletedObjects.Add(InitModel(row.Index));
                }

                for (int i = 0; i < addedIndexs.Count; i++)
                {
                    if (addedIndexs[i] > row.Index)
                        --addedIndexs[i];
                }

                dataDisplayer.Rows.Remove(row);
            }
        }

        /// <summary>
        /// Обработчик клика по кнопке "Сохранить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveButton_Click(object sender, EventArgs e)
        {
            AddObjects();
            DeleteObjects();
            UpdateObjects();
            Reset();
        }
    }
}
