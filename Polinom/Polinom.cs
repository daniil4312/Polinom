using System;
using System.Collections.Generic;
using System.IO;

namespace Polinom
{
    class Element
    {
        public int Coef { get; set; }
        public int Deg { get; set; }
        public Element Next { get; set; }

        public override string ToString()
        {
            return $"{Coef} * x^{Deg}";
        }
    }

    class Polinom
    {
        public Element Head;

        public Polinom() { }
        public Polinom(string filename)
        {
            var temp = Head;
            var a = File.ReadAllLines(filename);
            foreach (var row in a)
            {
                var pare = row.Split(' ');
                var el = new Element { Coef = int.Parse(pare[0]), Deg = int.Parse(pare[1]) };
                if (Head == null)
                    temp = Head = el;
                else
                {
                    temp.Next = el;
                    temp = temp.Next;
                }
            }
        }

        public void Insert(int coef, int deg)//Вставляем эл-т в начало списка
        {
            Head = new Element { Coef = coef, Deg = deg, Next = Head };
        }

        public void Combine()
        {
            var temp = Head;
            while (temp.Next != null)//Пробегаемся по списку
            {
                var f = temp;
                while (f.Next != null)//И прибавляем к нему следующие эл-ты с такой же степенью
                {
                    if (f.Next.Deg == temp.Deg)
                    {
                        temp.Coef += f.Next.Coef;
                        f.Next = f.Next.Next;
                    }
                    else
                        f = f.Next;
                }
                temp = temp.Next;
            }
        }

        public void Delete(int deg)
        {
            var temp = Head;
            if (temp.Deg == deg)//Проверяем первый эл-т
            {
                Head = Head.Next;
                return;
            }
            while (temp.Next != null)//Проверяем остальные
            {
                if (temp.Next.Deg == deg)
                {
                    temp.Next = temp.Next.Next;
                    return;
                }
                temp = temp.Next;
            }
        }

        public void Sum(Polinom p)
        {
            var tempP = p.Head;
            while (tempP != null)
            {
                var temp = Head;
                while (temp != null)
                {
                    if (temp.Deg == tempP.Deg)//Если такой уже есть, складываем коэф-ы
                    {
                        temp.Coef += tempP.Coef;
                        break;
                    }
                    if (temp.Next == null)//Если дошли до конца и не нашли похожих, то просто добавляем в конец
                    {
                        temp.Next = new Element { Coef = tempP.Coef, Deg = tempP.Deg };
                        break;
                    }
                    temp = temp.Next;
                }
                tempP = tempP.Next;
            }
        }

        public void Derivate()
        {
            var temp = Head;
            while (temp != null)//Сначала  удаляем нулевые степени, потому что они обнулятся
            {
                Delete(0);
                temp = temp.Next;
            }
            temp = Head;
            while (temp != null)//Находим проиводную от остального
            {
                temp.Coef *= temp.Deg;
                temp.Deg--;
                temp = temp.Next;
            }
        }

        public int Value(int x)
        {
            SortByDescending();
            var result = Head.Coef;
            var temp = Head.Next;
            var deg = Head.Deg;
            for (int i = 0; i < Head.Deg; i++)
            {
                result *= x;
                deg--;
                if (temp != null && deg == temp.Deg)
                {
                    result += temp.Coef;
                    temp = temp.Next;
                }
            }
            return result;
        }

        public void SortByDescending()
        {
            Combine();
            var result = new Polinom();
            while (Head != null)
            {
                var min = int.MaxValue;
                var minEl = new Element();
                var temp = Head;
                while (temp != null)
                {
                    if (temp.Deg < min)
                    {
                        min = temp.Deg;
                        minEl = temp;
                    }
                    temp = temp.Next;
                }
                result.Insert(minEl.Coef, minEl.Deg);
                Delete(minEl.Deg);
            }
            Head = result.Head;
        }

        public void DeleteOdd()
        {
            var temp = Head;
            while (temp != null && temp.Next != null)//Сначала удаляем все, кроме первого
            {
                if (temp.Next.Coef % 2 == 1)
                    temp.Next = temp.Next.Next;
                temp = temp.Next;
            }
            if (Head.Coef % 2 == 1)//А потом и первый
                Head = Head.Next;
            }

        public override string ToString()
        {
            var el = Head;
            var list = new List<string>();
            while (el != null)
            {
                list.Add(el.ToString());
                el = el.Next;
            }
            return String.Join(" + ", list);
        }
    }
}
