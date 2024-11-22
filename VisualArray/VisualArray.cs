
using System.Data;

namespace практика_9
{
    public static class VisualArray
    {
        //ћетод дл€ двухмерного массива
        public static DataTable ToDataTable<T>(this T[,] matrix)
        {
            var res = new DataTable();//создание нового объекта и добавление в него столбцов 
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                res.Columns.Add("col" + (i + 1), typeof(T));//столбцы
            }
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                var row = res.NewRow();//создаетс€ нова€ строка
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    row[j] = matrix[i, j];//присваивает значени€ элементов из соответствующих €чеек матрицы
                }
                res.Rows.Add(row);// row добавл€етс€ в таблицу res.
            }
            return res;
        }
    }
}
