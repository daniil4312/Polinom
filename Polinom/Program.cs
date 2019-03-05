using System;

namespace Polinom
{
    class Program
    {
        static void Main(string[] args)
        {
            //Создание списков из файлов
            var p1 = new Polinom("In1.txt");
            Console.WriteLine(p1);
            Console.WriteLine();
            var p2 = new Polinom("In2.txt");
            Console.WriteLine(p2);
            Console.WriteLine();

            //Возврат строкового представления полинома
            var str = p1.ToString();
            Console.WriteLine(str);
            Console.WriteLine();

            //Вставка монома в полином
            p1.Insert(2, 3);
            Console.WriteLine(p1);
            Console.WriteLine();

            //Приведение подобных членов
            p1.Combine();
            Console.WriteLine(p1);
            Console.WriteLine();

            //Удалить эл-т с подобным показателем степени
            p1.Delete(3);
            Console.WriteLine(p1);
            Console.WriteLine();

            //Сложить полиномы
            p1.Sum(p2);
            Console.WriteLine(p1);
            Console.WriteLine();

            //Взять поизводную
            p1.Derivate();
            Console.WriteLine(p1);
            Console.WriteLine();

            //Вычислить значение полинома в точке x способом (схема Горнера)
            var value = p2.Value(2);
            Console.WriteLine(value);
            Console.WriteLine();

            //Удалить из списка все эл-ты с нечётной степенью
            Console.WriteLine(p2);
            p2.DeleteOdd();
            Console.WriteLine(p2);
        }
    }
}
