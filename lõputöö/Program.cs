namespace lõputöö
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    namespace LINQAndFileOperations
    {
        // Klass andmete jaoks
        class Data
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        class Program
        {
            static void Main(string[] args)
            {
                while (true)
                {
                    Console.WriteLine("Vali meetod:");
                    Console.WriteLine("1. Where ja Average");
                    Console.WriteLine("2. Skip ja Join");
                    Console.WriteLine("3. GroupBy ja FirstOrDefault");
                    Console.WriteLine("4. Min ja GroupJoin");
                    Console.WriteLine("5. Count ja Sum");
                    Console.WriteLine("6. ThenBy ja OrderBy");
                    Console.WriteLine("7. Select ja ToLookup");
                    Console.WriteLine("8. SkipWhile ja Take");
                    Console.WriteLine("9. TakeWhile ja Where");
                    Console.WriteLine("10. If ja else faili meetod");
                    Console.WriteLine("11. For loopi meetod (numbri püramiid)");
                    Console.WriteLine("12. Välju");

                    int valik = int.Parse(Console.ReadLine());

                    switch (valik)
                    {
                        case 1:
                            WhereAndAverage();
                            break;
                        case 2:
                            SkipAndJoin();
                            break;
                        case 3:
                            GroupByAndFirstOrDefault();
                            break;
                        case 4:
                            MinAndGroupJoin();
                            break;
                        case 5:
                            CountAndSum();
                            break;
                        case 6:
                            ThenByAndOrderBy();
                            break;
                        case 7:
                            SelectAndToLookup();
                            break;
                        case 8:
                            SkipWhileAndTake();
                            break;
                        case 9:
                            TakeWhileAndWhere();
                            break;
                        case 10:
                            FileMethod();
                            break;
                        case 11:
                            NumberPyramid();
                            break;
                        case 12:
                            return;
                        default:
                            Console.WriteLine("Vale valik, proovi uuesti.");
                            break;
                    }
                }
            }

            static void WhereAndAverage()
            {
                List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                var evenNumbers = numbers.Where(n => n % 2 == 0);
                double average = evenNumbers.Average();
                Console.WriteLine("Paarisarvud: " + string.Join(", ", evenNumbers));
                Console.WriteLine("Keskmine: " + average);
            }

            static void SkipAndJoin()
            {
                List<string> list1 = new List<string> { "a", "b", "c" };
                List<string> list2 = new List<string> { "c", "d", "e" };
                var result = list1.Skip(1).Join(list2, l1 => l1, l2 => l2, (l1, l2) => l1);
                Console.WriteLine("Join tulemus: " + string.Join(", ", result));
            }

            static void GroupByAndFirstOrDefault()
            {
                List<Data> dataList = new List<Data>
            {
                new Data { Id = 1, Name = "A" },
                new Data { Id = 2, Name = "B" },
                new Data { Id = 3, Name = "A" },
                new Data { Id = 4, Name = "C" }
            };

                var groupedData = dataList.GroupBy(d => d.Name);
                foreach (var group in groupedData)
                {
                    var first = group.FirstOrDefault();
                    Console.WriteLine($"Grupp: {group.Key}, Esimene element: {first.Id}, {first.Name}");
                }
            }

            static void MinAndGroupJoin()
            {
                List<Data> data1 = new List<Data>
            {
                new Data { Id = 1, Name = "A" },
                new Data { Id = 2, Name = "B" }
            };
                List<Data> data2 = new List<Data>
            {
                new Data { Id = 1, Name = "X" },
                new Data { Id = 1, Name = "Y" }
            };

                var minId = data1.Min(d => d.Id);
                Console.WriteLine("Min Id: " + minId);

                var groupJoin = data1.GroupJoin(
                    data2,
                    d1 => d1.Id,
                    d2 => d2.Id,
                    (d1, d2s) => new { d1, d2s });

                foreach (var item in groupJoin)
                {
                    Console.WriteLine($"Id: {item.d1.Id}, Name: {item.d1.Name}");
                    foreach (var subItem in item.d2s)
                    {
                        Console.WriteLine($"  Sub Name: {subItem.Name}");
                    }
                }
            }

            static void CountAndSum()
            {
                List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
                int count = numbers.Count();
                int sum = numbers.Sum();
                Console.WriteLine("Count: " + count);
                Console.WriteLine("Sum: " + sum);
            }

            static void ThenByAndOrderBy()
            {
                List<Data> dataList = new List<Data>
            {
                new Data { Id = 3, Name = "C" },
                new Data { Id = 1, Name = "A" },
                new Data { Id = 2, Name = "B" }
            };

                var orderedData = dataList.OrderBy(d => d.Name).ThenBy(d => d.Id);
                foreach (var data in orderedData)
                {
                    Console.WriteLine($"Id: {data.Id}, Name: {data.Name}");
                }
            }

            static void SelectAndToLookup()
            {
                List<Data> dataList = new List<Data>
            {
                new Data { Id = 1, Name = "A" },
                new Data { Id = 2, Name = "B" },
                new Data { Id = 3, Name = "A" }
            };

                var selectedData = dataList.Select(d => new { d.Id, d.Name });
                var lookup = selectedData.ToLookup(d => d.Name, d => d.Id);

                foreach (var group in lookup)
                {
                    Console.WriteLine($"Key: {group.Key}, Values: {string.Join(", ", group)}");
                }
            }

            static void SkipWhileAndTake()
            {
                List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                var skipped = numbers.SkipWhile(n => n < 5);
                var taken = skipped.Take(3);
                Console.WriteLine("Pärast esimest nelja vahelejätmist ja kolm järgnevat elementi: " + string.Join(", ", taken));
            }

            static void TakeWhileAndWhere()
            {
                List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                var taken = numbers.TakeWhile(n => n < 5);
                var evenNumbers = taken.Where(n => n % 2 == 0);
                Console.WriteLine("Eemaldati esimesed n arvud, mis on alla 5 ja jäeti ainult paarisarvud: " + string.Join(", ", evenNumbers));
            }

            static void FileMethod()
            {
                Console.WriteLine("Sisesta tekst, mida soovid salvestada:");
                string text = Console.ReadLine();
                Console.WriteLine("Sisesta failitee:");
                string path = Console.ReadLine();

                try
                {
                    File.WriteAllText(path, text);
                    Console.WriteLine("Fail edukalt salvestatud.");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Tekkis viga: {e.Message}");
                }
            }

            static void NumberPyramid()
            {
                int n = 3;
                int number = 1;
                for (int i = 1; i <= n; i++)
                {
                    for (int j = i; j < n; j++)
                    {
                        Console.Write(" ");
                    }
                    for (int k = 1; k <= i; k++)
                    {
                        Console.Write(number++ + " ");
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
