using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Set
{
    /// Множество.
    public class Set<T> : IEnumerable<T>
    {
        /// Коллекция хранимых данных.
        private List<T> _items = new List<T>();
        /// Количество элементов.
        public int Count => _items.Count;
        /// Добавить данные во множество.
        public void Add(T item)
        {
            // Проверяем входные данные на пустоту.
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            // Множество может содержать только уникальные элементы,
            // поэтому если множество уже содержит такой элемент данных, то не добавляем его.
            if (!_items.Contains(item))
            {
                _items.Add(item);
            }
        }

        /// Удалить элемент из множества.
        public void Remove(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            // Если коллекция не содержит данный элемент, то мы не можем его удалить.
            if (!_items.Contains(item))
            {
                throw new KeyNotFoundException($"Элемент {item} не найден в множестве.");
            }

            // Удаляем элемент из коллекции.
            _items.Remove(item);
        }
        /// Объединение множеств.
        public static Set<T> Union(Set<T> set1, Set<T> set2)
        {
            if (set1 == null)
            {
                throw new ArgumentNullException(nameof(set1));
            }

            if (set2 == null)
            {
                throw new ArgumentNullException(nameof(set2));
            }
            // Возвращаемое множество.
            var resultSet = new Set<T>();
            // Элементы данных возвращаемого множества.
            var items = new List<T>();
            // Добвляем элементы из первого множества в возвращаемое
            if (set1._items != null && set1._items.Count > 0)
            {
                // т.к. список является ссылочным типом, 
                // то необходимо не просто передавать данные, а создавать их дубликаты.
                items.AddRange(new List<T>(set1._items));
            }

            // Из 2 в итоговое множество.
            if (set2._items != null && set2._items.Count > 0)
            {
                items.AddRange(new List<T>(set2._items));
            }
            // Удаляем все дубликаты из возвращаемого множества
            resultSet._items = items.Distinct().ToList();
            return resultSet;
        }
        /// Пересечение множеств.
        public static Set<T> Intersection(Set<T> set1, Set<T> set2)
        {
            if (set1 == null)
            {
                throw new ArgumentNullException(nameof(set1));
            }

            if (set2 == null)
            {
                throw new ArgumentNullException(nameof(set2));
            }

            var resultSet = new Set<T>();
            // Выбираем множество содержащее наименьшее количество элементов.
            if (set1.Count < set2.Count)
            {
                // Первое меньше.
                // Проверяем все элементы выбранного множества.
                foreach (var item in set1._items)
                {
                    // Если элемент из первого множества содержится во втором множестве,
                    // то добавляем его в результирующее множество.
                    if (set2._items.Contains(item))
                    {
                        resultSet.Add(item);
                    }
                }
            }
            else
            {
                // Второе множество меньше или множества равны.
                // Проверяем все элементы выбранного множества.
                foreach (var item in set2._items)
                {
                    // Если элемент из второго множества содержится в первом множестве,
                    // то добавляем его в результирующее множество.
                    if (set1._items.Contains(item))
                    {
                        resultSet.Add(item);
                    }
                }
            }
            return resultSet;
        }

        /// Разность множеств.
        public static Set<T> Difference(Set<T> set1, Set<T> set2)
        {
            if (set1 == null)
            {
                throw new ArgumentNullException(nameof(set1));
            }

            if (set2 == null)
            {
                throw new ArgumentNullException(nameof(set2));
            }
            var resultSet = new Set<T>();
            // Проходим по всем элементам первого множества.
            foreach (var item in set1._items)
            {
                // Если элемент из первого множества не содержится во втором множестве,
                // то добавляем его в результирующее множество.
                if (!set2._items.Contains(item))
                {
                    resultSet.Add(item);
                }
            }

            // Проходим по всем элементам второго множества.
            foreach (var item in set2._items)
            {
                // Если элемент из второго множества не содержится в первом множестве,
                // то добавляем его в результирующее множество.
                if (!set1._items.Contains(item))
                {
                    resultSet.Add(item);
                }
            }

            // Удаляем все дубликаты из возвращаемого множества элементов данных.
            resultSet._items = resultSet._items.Distinct().ToList();
            return resultSet;
        }
        /// Подмножество.
        public static bool Subset(Set<T> set1, Set<T> set2)
        {
            if (set1 == null)
            {
                throw new ArgumentNullException(nameof(set1));
            }

            if (set2 == null)
            {
                throw new ArgumentNullException(nameof(set2));
            }

            // Перебираем элементы первого множества.
            // Если все элементы первого множества содержатся во втором,
            // то это подмножество. Возвращаем истину, иначе ложь.
            var result = set1._items.All(s => set2._items.Contains(s));
            return result;
        }

        /// Вернуть перечислитель, выполняющий перебор всех элементов множества.
        public IEnumerator<T> GetEnumerator()
        {
            // Используем перечислитель списка элементов данных множества.
            return _items.GetEnumerator();
        }
        /// Вернуть перечислитель, который осуществляет итерационный переход по множеству.
        IEnumerator IEnumerable.GetEnumerator()
        {
            // Используем перечислитель списка элементов данных множества.
            return _items.GetEnumerator();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // Создаем множества.
            var set1 = new Set<int>();
            var set2 = new Set<int>();
            var set3 = new Set<int>();
            
            Console.WriteLine($"Введите значения первого множества .");
            int[] input = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            foreach (int item in input)
            {
                set1.Add(item);
            }
            Console.WriteLine($"Введите значения второго множества .");
            input = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            foreach (int item in input)
            {
                set2.Add(item);
            }
            Console.WriteLine($"Введите значения третьего множества .");
            input = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            foreach (int item in input)
            {
                set3.Add(item);
            }
            // Выполняем операции со множествами.
            var union = Set<int>.Union(set1, set2);
            var difference = Set<int>.Difference(set1, set2);
            var intersection = Set<int>.Intersection(set1, set2);
            var subset1 = Set<int>.Subset(set3, set1);
            var subset2 = Set<int>.Subset(set3, set2);
            // Выводим исходные множества на консоль.
            PrintSet(set1, "Первое множество: ");
            PrintSet(set2, "Второе множество: ");
            PrintSet(set3, "Третье множество: ");
            // Выводим итоговые множества на консоль.
            PrintSet(union, "Объединение первого и второго множества: ");
            PrintSet(difference, "Разность первого и второго множества: ");
            PrintSet(intersection, "Пересечение первого и второго множества: ");
            // Выводим результаты проверки на подмножества.
            if (subset1)
            {
                Console.WriteLine("Третье множество является подмножеством первого.");
            }
            else
            {
                Console.WriteLine("Третье множество не является подмножеством первого.");
            }

            if (subset2)
            {
                Console.WriteLine("Третье множество является подмножеством второго.");
            }
            else
            {
                Console.WriteLine("Третье множество не является подмножеством второго.");
            }

            Console.ReadLine();
        }
        /// Вывод множества на консоль.
        private static void PrintSet(Set<int> set, string title)
        {
            Console.Write(title);
            foreach (var item in set)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine();
        }
    }
}