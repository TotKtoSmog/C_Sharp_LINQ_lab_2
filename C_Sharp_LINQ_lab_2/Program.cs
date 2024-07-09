using C_Sharp_LINQ_lab_2.Class;
using System.Data;

namespace C_Sharp_LINQ_lab_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ShowList(Task8(GetDataShop(), GetDataProduct()));
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
        /// <summary>
        /// Исходная последовательность содержит сведения об абитуриентах. Каждый
        /// элемент последовательности включает следующие поля:
        /// <Номер школы> <Год поступления> <Фамилия>
        /// Для каждого года, присутствующего в исходных данных, вывести число различных
        /// школ, которые окончили абитуриенты, поступившие в этом году(вначале указывать число
        /// школ, затем год). Сведения о каждом годе выводить на новой строке и упорядочивать по
        /// возрастанию числа школ, а для совпадающих чисел — по возрастанию номера года
        /// </summary>
        /// <param name="students"></param>
        /// <returns></returns>
        static List<string> Task5(List<Student> students)
            => students.GroupBy(s => s.Year).Select(s => new { Year = s.Key, Count = s.Count() })
            .OrderBy(s => s.Count).ThenBy(s => s.Year).Select(s => $"{s.Year} {s.Count}").ToList();
        /// <summary>
        /// Исходная последовательность содержит сведения об абитуриентах. Каждый
        /// элемент последовательности включает следующие поля:
        /// <Номер школы> <Год поступления> <Фамилия>
        /// Для каждого года, присутствующего в исходных данных, вывести число различных
        /// школ, которые окончили абитуриенты, поступившие в этом году(вначале указывать число
        /// школ, затем год). Сведения о каждом годе выводить на новой строке и упорядочивать по
        /// возрастанию числа школ, а для совпадающих чисел — по возрастанию номера года
        /// </summary>
        /// <param name="students"></param>
        /// <returns></returns>
        static List<string> Task6(List<Student> students)
        {
            return students
            .GroupBy(s => s.Year)
            .Select(s => new { Year = s.Key, Count = s.Count() })
            .Where(t => t.Count == students.GroupBy(s => s.Year).Max(g => g.Count()) || t.Count == students.GroupBy(s => s.Year).Min(g => g.Count()))
            .OrderByDescending(t => t.Count)
            .ThenBy(t => t.Year)
            .Select(t => $"{t.Year} {t.Count}")
            .ToList();
        }
        static List<string> Task7(List<Debtor> debtors)
            => debtors
            .Select(d => new
            {
                Porch = (d.Anumber + 35) / 36,
                d.Debt,
                d.Sname,
                d.Anumber
            }).GroupBy(d => d.Porch).OrderBy(d => d.Key)
            .SelectMany(d => d
                .OrderByDescending(i => i.Debt)
                .Take(3)
                .Select(i => new { item = $"{i.Debt} {d.Key} {i.Anumber} {i.Sname}", i.Debt }))
            .OrderByDescending(i => i.Debt).Select(i => i.item).ToList();
        /// <summary>
        /// Для каждой категории товаров определить количество магазинов, предлагающих товары
        /// данной категории, а также количество стран, в которых произведены товары данной
        /// категории, представленные в магазинах(вначале выводится количество магазинов, затем
        /// название категории, затем количество стран). Если для некоторой категории не найдено
        /// ни одного товара, представленного в каком- либо магазине, то информация о данной
        /// категории не выводится.Сведения о каждой категории выводить на новой строке и
        /// упорядочивать по убыванию количества магазинов, а в случае одинакового количества —
        /// по названиям категорий в алфавитном порядке.
        /// </summary>
        /// <param name="shops"></param>
        /// <param name="products"></param>
        /// <returns></returns>
        static List<string> Task8(List<Shop> shops, List<Product> products)
            => shops.GroupJoin(products, n => n.Ararticle, s => s.Ararticle, (sh, prod) => new
            {
                shop = sh.Sname,
                Category = prod.Select(n => n.Category).First(),
                Country = prod.Select(n => n.Country).First()
            }).GroupBy(n => n.Category).Select(g => new
            { 
                Category = g.Key, 
                CountContry = g.Select(n => n.Country).Distinct().Count(), 
                CountShop = g.Select(n => n.shop).Distinct().Count() 
            }).OrderByDescending(n => n.CountShop).ThenBy(n => n.Category)
            .Select(i => $"Категория: {i.Category} кол-во производителей: {i.CountContry} кол-во магазинов {i.CountShop}").ToList();
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
        static List<Student> GetDataStudent()
            => new List<Student>()
            {
                new Student(1, 2001, "Иванов"),
                new Student(1, 2001, "Петров"),
                new Student(1, 2001, "Сидоров"),
                new Student(2, 2001, "Иванов"),
                new Student(2, 2001, "Петров"),
                new Student(3, 2001, "Сидоров"),
                new Student(2, 2002, "Иванов"),
                new Student(2, 2002, "Петров"),
                new Student(3, 2002, "Сидоров"),
                new Student(2, 2002, "Иванов"),
                new Student(2, 2003, "Петров"),
                new Student(3, 2003, "Сидоров")
            };
        static List<Debtor> GetDataDebtor()
            => new List<Debtor>()
            {
                new Debtor(1234.80, "Петухов", 90),
                new Debtor(520.50, "Иванов", 1),
                new Debtor(200.00, "Антонова", 107),
                new Debtor(765.20, "Бочаров", 130),
                new Debtor(444.00, "Карасева", 144),
                new Debtor(920.35, "Петров", 9),
                new Debtor(1290.45, "Яковлев", 21),
                new Debtor(693.98, "Ларионова", 98),
                new Debtor(290.32, "Шилов", 38),
                new Debtor(881.00, "Сидоров", 14),
                new Debtor(836.23, "Малахов", 88),
                new Debtor(1500.00, "Савельева", 72),
                new Debtor(10.15, "Бирюкова", 48),
                new Debtor(150.12, "Дмитриев", 50),
                new Debtor(233.12, "Воронина", 109),
                new Debtor(500.00, "Мельников", 121),
            };
        static List<Shop> GetDataShop()
            => new List<Shop>()
            {
                new Shop(1, 100, "Магазин 1"),
                new Shop(2, 200, "Магазин 1"),
                new Shop(3, 400, "Магазин 1"),
                new Shop(1, 400, "Магазин 2"),
                new Shop(2, 500, "Магазин 2"),
                new Shop(3, 600, "Магазин 2"),
                new Shop(1, 700, "Магазин 3"),
                new Shop(2, 800, "Магазин 3"),
                new Shop(3, 900, "Магазин 3")
            };
        static List<Product> GetDataProduct()
            => new List<Product>()
            {
                new Product("Дом", 1,"Россия"),
                new Product("Авто", 2,"Россия"),
                new Product("Семья", 3,"Китай")
            };
    }
}