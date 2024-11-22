using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
namespace практика_9
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Student[] student = new Student[10]; // Объявляет массив из 10 объектов типа Student для хранения данных студентов.
        string[,] matrix = new string[10, 4]; // Объявляет двумерный массив для хранения информации о студентах (фамилия, улица, дом, квартира).

        public MainWindow() 
        {
            InitializeComponent();
        }

        private void btn_Exit(object sender, RoutedEventArgs e) 
        {
            this.Close(); 
        }

        private void btn_Info(object sender, RoutedEventArgs e) 
        {
            MessageBox.Show("Программу подготовил студент группы ИСП-31 Лотаков Артемий\nПрактическая 9 Вариант 13\nОписать, используя структуру, данные на учеников (фамилия, улица, дом, квартира). Вывести результат на экран. Вывести информацию, сколько учеников живет на заданной улице", "Информация");
        }

        private void btn_DeleteLine(object sender, RoutedEventArgs e) // Обработчик события нажатия кнопки "Удалить строку".
        {
            // Проверяет, может ли текст из поля outPosition быть преобразован в целое число и находится ли оно в допустимом диапазоне (1-10).
            if (int.TryParse(outPosition.Text, out int position) && position > 0 && position <= 10)
            {
                student[position - 1] = new Student(); // Заменяет элемент массива student по индексу position - 1 на новый объект Student.
                for (int i = 0; i <= 3; i++) // Цикл для обнуления значений ячеек двумерного массива matrix.
                {
                    matrix[position - 1, i] = string.Empty; // Обнуляет ячейки строки в массиве matrix для выбранного студента.
                }
                // Обновляет источник данных для dataGrid, создавая DataTable из массива matrix.
                dataGrid.ItemsSource = VisualArray.ToDataTable(matrix).DefaultView;
                // Устанавливает заголовки столбцов для dataGrid.
                dataGrid.Columns[0].Header = "Фамилия";
                dataGrid.Columns[1].Header = "Улица";
                dataGrid.Columns[2].Header = "Номер дома";
                dataGrid.Columns[3].Header = "Номер квартиры";
            }
            else
            {
                // Если ввод неверен, отображает сообщение об ошибке.
                MessageBox.Show("Номер строки не выбран или выбран неверно", "Ошибка");
            }
            outAmount.Clear(); // Очищает поле outAmount.
        }

        private void btn_FillTable(object sender, RoutedEventArgs e) // Обработчик события нажатия кнопки "Заполнить таблицу".
        {
            // Проверяет ввод пользователем и валидирует данные перед их сохранением.
            if (int.TryParse(outPosition.Text, out int position)
                && position <= 10 && position > 0 // Проверяет, что позиция в допустимом диапазоне.
                && outSername.Text != string.Empty // Проверяет, чтобы фамилия не была пустой.
                && outStreet.Text != string.Empty // Проверяет, чтобы улица не была пустой.
                && int.TryParse(outHouse.Text, out int house) // Проверяет, чтобы номер дома был целым числом.
                && int.TryParse(outRoom.Text, out int room)) // Проверяет, чтобы номер квартиры был целым числом.
            {
                // Создает новый объект Student с введенными данными и сохраняет его в соответствующем элементе массива.
                student[position - 1] = new Student(outSername.Text, outStreet.Text, house, room);
                // Присваивает значения из объекта Student в соответствующие ячейки двумерного массива.
                matrix[position - 1, 0] = student[position - 1].Sername;
                matrix[position - 1, 1] = student[position - 1].Street;
                matrix[position - 1, 2] = student[position - 1].House.ToString();
                matrix[position - 1, 3] = student[position - 1].Room.ToString();

                // Обновляет источник данных для dataGrid и устанавливает заголовки столбцов.
                dataGrid.ItemsSource = VisualArray.ToDataTable(matrix).DefaultView;
                dataGrid.Columns[0].Header = "Фамилия";
                dataGrid.Columns[1].Header = "Улица";
                dataGrid.Columns[2].Header = "Номер дома";
                dataGrid.Columns[3].Header = "Номер квартиры";
            }
            else
            {
                // Если ввод неверен, отображает сообщение об ошибке.
                MessageBox.Show("Введены неверные данные", "Ошибка");
            }
            outAmount.Clear(); // Очищает поле outAmount.
        }

        private void btn_Search(object sender, RoutedEventArgs e) // Обработчик события нажатия кнопки "Поиск".
        {
            int amount = 0; // Переменная для хранения количества найденных совпадений.
            if (searchStreet.Text != string.Empty) // Проверяет, что поле для поиска не пустое.
            {
                for (int i = 0; i < 10; i++) // Цикл по всем строкам массива matrix.
                {
                    if (matrix[i, 1] == null) // Проверяет, если значение в текущей строке не задано, пропускает цикл.
                    {
                        continue;
                    }
                    if (matrix[i, 1] == searchStreet.Text) // Если текущее значение улицы совпадает с искомым.
                    {
                        amount++; // Увеличивает счетчик найденных совпадений.
                    }
                }
                outAmount.Text = amount.ToString(); // Обновляет поле outAmount с количеством найденных студентов.
            }
            else
            {
                // Если поле для поиска пустое, отображает сообщение об ошибке.
                MessageBox.Show("Введите нужную улицу", "Ошибка");
            }
        }

        private void outPosition_TextChanged(object sender, TextChangedEventArgs e) // Обработчик события изменения текста в поле outPosition.
        {
            // Очищает поля ввода информации о студенте при изменении позиции.
            outSername.Clear();
            outStreet.Clear();
            outHouse.Clear();
            outRoom.Clear();
        }

        private void searchStreet_TextChanged(object sender, TextChangedEventArgs e) // Обработчик события изменения текста в поле для поиска улицы.
        {
            outAmount.Clear(); // Очищает поле outAmount при каждом изменении текста, чтобы не сохранять старые результаты.
        }
    }
}