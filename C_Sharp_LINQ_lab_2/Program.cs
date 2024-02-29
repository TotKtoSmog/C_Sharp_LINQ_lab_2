using C_Sharp_LINQ_lab_2.Class;
using System.Security.Cryptography;

namespace C_Sharp_LINQ_lab_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //ShowList(Task1(new List<string>() { "A2", "A1", "A3" }, new List<string>() { "B1", "B2", "B3" }));
            //ShowList(Task2(new List<int>() {1,2,3,4,5 }, new List<int>() {11,19,21,31,55 }));
            //ShowList(Task3(GetDataDepartment(), GetDataEmployees()));
            ShowList(Task4(GetDataClients(), 3));
        }
        static void ShowList<T>(List<T> list)
        {
            foreach(var item in list)
                Console.WriteLine(item);
        }
        /// <summary>
        /// Даны строковые последовательности A и B; все строки в каждой
        /// последовательности различны, имеют ненулевую длину и содержат только цифры и
        /// заглавные буквы латинского алфавита.Получить последовательность всевозможных
        /// комбинаций вида «EA=EB», где EA – некоторый элемент из A, EB – некоторый элемент из B,
        /// причем оба элемента оканчиваются цифрой(например, «AF3= D78»). Упорядочить
        /// полученную последовательность в лексикографическом порядке по возрастанию
        /// элементов EA, а при одинаковых элементах EA – в лексикографическом порядке по
        /// убыванию элементов EB(для перебора комбинаций использовать методы SelectMany и
        /// Select).
        /// </summary>
        /// <param name="A">Строковая последовательность A</param>
        /// <param name="B">Строковая Последовательность B</param>
        /// <returns></returns>
        static List<string> Task1(List<string> A, List<string> B)
            => A.OrderBy(a => a).SelectMany(a1 => B.OrderByDescending(b => b).Select(b => $"{a1}={b}")).ToList();
        /// <summary>
        /// Даны последовательности положительных целых чисел A и B; все числа в
        /// последовательности A различны.Получить последовательность строк вида «S:E», где S
        /// обозначает среднее арифметическое тех чисел из B, которые оканчиваются на ту же
        /// цифру, что и число E – один из элементов последовательности A(например, «74:23»); если
        /// для числа E не найдено ни одного подходящего числа из последовательности B, то в
        /// качестве S указать 0. Расположить элементы полученной последовательности по
        /// возрастанию значений найденных сумм, а при равных суммах – по убыванию значений
        /// элементов A.
        /// </summary>
        /// <param name="A">Последовательности положительных целых чисел</param>
        /// <param name="B">Последовательности положительных целых чисел</param>
        /// <returns></returns>
        static List<string> Task2(List<int> A, List<int> B)
            => A.Select(a1 => $"{A.SelectMany(a => B.Where(b => a1 % 10 == b % 10)).DefaultIfEmpty(0).Average()}" +
            $":{a1}").OrderBy(a => Convert.ToInt32(a.Split(':').First())).
            ThenByDescending(a => Convert.ToInt32(a.Split(':').Last())).ToList();
        /// <summary>
        /// В организации имеется 3 отдела. В каждом отделе имеется от 3 до 5
        /// сотрудников.Используя группировку по отделу, вывести список сотрудников и средний
        /// оклад по каждому отделу.Определите долю суммы окладов всех сотрудников одного
        /// отдела в общей сумме окладов по всему предприятию.
        /// </summary>
        /// <param name="departments">Лист отделов</param>
        /// <param name="employees">Лист сотрудников</param>
        /// <returns></returns>
        static List<string> Task3(List<Department> departments, List<Employee> employees)
            => departments.Join(employees, d => d.Id_Department, e => e.Id_department, (d, e) 
                => new { Name = d.Name_Department, salary = e.Salary}).GroupBy(res => res.Name).
            Select(res => $"{res.Key} - {Math.Round(res.Average(n => n.salary),2)}").ToList();
        /// <summary>
        /// Дано целое число K – код одного из клиентов фитнес-центра. Исходная
        /// последовательность содержит сведения о клиентах этого фитнес-центра.Каждый элемент
        /// последовательности включает следующие целочисленные поля:
        /// <Код клиента> <Год> <Номер месяца>
        /// <Продолжительность занятий(в часах)>
        /// Для каждого года, в котором клиент с кодом K посещал центр, определить месяц, в
        /// котором продолжительность занятий данного клиента была наименьшей для данного года
        /// (если таких месяцев несколько, то выбирать первый из этих месяцев в исходном наборе;
        /// месяцы с нулевой продолжительностью занятий не учитывать). Сведения о каждом годе
        /// выводить на новой строке в следующем порядке: наименьшая продолжительность
        /// занятий, год, номер месяца.Упорядочивать сведения по возрастанию продолжительности
        /// занятий, а при равной продолжительности – по возрастанию номера года. Если данные о
        /// клиенте с кодом K отсутствуют, то записать в результирующий файл строку «Нет данных»
        /// </summary>
        /// <param name="clients">Лист клиентов</param>
        /// <param name="k">Код клиента</param>
        /// <returns></returns>
        static List<string> Task4(List<Client> clients, int k)
            => clients.Where(c => c.Id == k).GroupBy(c => c.Year).Select(c => new { tr = c.MinBy(b => b.Hour) }).
            OrderBy(c => c.tr.Month).Select(c => $"{c.tr.Hour} {c.tr.Year} {c.tr.Hour}").DefaultIfEmpty("Нет данных").ToList();
        static List<Department> GetDataDepartment()
            => new List<Department>()
            {
                new Department(1,"ИТ-отдел"),
                new Department(2, "Отдел-продаж"),
                new Department(3,"Бухгалтерия")
            };
        static List<Employee> GetDataEmployees()
            => new List<Employee>()
            {
                new Employee(1, "Тюрин", "Софон", 25000, 1),
                new Employee(2, "Байков", "Пров", 20000, 1),
                new Employee(3, "Беляева", "Владислава", 23000, 1),
                new Employee(4, "Брежнева", "Марта", 27000, 2),
                new Employee(5, "Екимов", "Фома", 30000, 2),
                new Employee(6, "Емельянова", "Рада", 32000, 2),
                new Employee(7, "Бровина", "Домна", 20000, 3),
                new Employee(8, "Меньшов", "Никанор", 24000, 3),
                new Employee(9, "Вихирева", "Прасковья", 20060, 3),
            };
        static List<Client> GetDataClients()
            => new List<Client>() 
            { 
                new Client(1,2012,1,5),
                new Client(1,2012,2,15),
                new Client(1,2012,3,2),
                new Client(1,2013,1,5),
                new Client(1,2016,1,8),
                new Client(1,2019,1,1),
                new Client(1,2019,2,1),
                new Client(2,3000,1,5),
                new Client(2,3000,2,8),
            };


    }
}