namespace C_Sharp_LINQ_lab_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ShowList(Task1(new string[] { "A2", "A1", "A3" }, new string[] { "B1", "B2", "B3" }));
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
        static List<string> Task1(string[] A, string[] B)
            => A.OrderBy(a => a).SelectMany(a1 => B.OrderByDescending(b => b).Select(b => $"{a1}={b}")).ToList();
    }
}