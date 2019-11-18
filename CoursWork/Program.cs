using System;
using System.Collections.Generic;

namespace CoursWork
{
    class Program
    {
        
        static bool[] arrayBoolInput;
        static string[,] arrayInput;
        static List<int> prevs = new List<int>();
        static void Main(string[] args)
        {
            Console.WriteLine("Введите количество вершин");
            int k = int.Parse(Console.ReadLine()); //k - количество вершин
            Console.WriteLine("Введите вершины");
            arrayInput = new string[2, k];
            // 0,1 - номер элемента в строке
            // k - номер строки
            arrayBoolInput = new bool[k];
            // Массив нужен для того, чтобы потом не проходиться по тем же элементам.
            // False - ещё не дошли. True - уже прошлись по этому элементу 
            for (int i = 0; i < k; i++)
            {
                arrayInput[1, i] = Console.ReadLine();
                arrayInput[0, i] = (arrayInput[1, i][0]).ToString();
                arrayInput[1, i] = (arrayInput[1, i][2]).ToString();
            }
            Console.WriteLine(arrayInput[0, 0]);
            if (k > 1)
            {
                recursiveFindingPath(0, 0, 0, true);
            }

            Console.ReadKey();
        }

        private static void recursiveFindingPath(int VertexParent, int level, int indentation, bool first)
        {
            // VertexParent - индекс вершины, относительно которой я сравниваю. 0 - начальная вершина
            // level - уровень текущей глубины вершины, чтобы мы могли вернуться по дереву
            // indentation - сколько я должен буду отступить (в символах)
            // first - юзается для адекватной отрисовки. Показывает первая ли ветка в или нет.                                                                                                                                                                                                                      й ли это ЧЛЕН вершины или нет


            //Эта проверка нужна для того, чтобы мы пройти проблему с "1 - ", ибо эта хуйня стоит на 0 позиции.
            //else как раз рассматривает этот случай.
            if (level > 0)
            {
                for (int i = 1; i < arrayInput.GetLength(1); i++)
                {
                    //i - строка 
                    if (arrayInput[1, i] == arrayInput[0, VertexParent])
                    {
                        useAlgorithm(i, VertexParent, level, indentation, first);
                    }
                    // Т.е. если мы дошли до последнего элемента и не нашли куда углубиться 
                    else if (arrayInput.GetLength(1) - i == 1)
                    {
                        recursiveFindingPath(prevs.IndexOf(level-1), level - 1, 0, false);
                    }
                }
            }
            else if (level == 0)
            {
                for (int i = 1; i < arrayInput.GetLength(1); i++)
                {
                    //i - строка 
                    if (arrayInput[1, i] == arrayInput[0, 0])
                    {
                        useAlgorithm(i, VertexParent, level, indentation, first);
                    }
                }
            }
        }

        private static void useAlgorithm(int index, int VertexParent, int level, int indentation, bool first)
        {
            if (!arrayBoolInput[index])
            {
                if (first)
                {
                    Console.WriteLine("|");
                    Console.WriteLine(arrayInput[0, index]);
                    arrayBoolInput[index] = true;
                    prevs.Add(VertexParent);
                    recursiveFindingPath(index, level + 1, indentation, true);
                }
                else
                {
                    indentation += 2;
                    Console.Write("—");
                    Console.WriteLine(arrayInput[0, index]);
                    arrayBoolInput[index] = true;
                    prevs.Add(VertexParent);
                    recursiveFindingPath(index, level, indentation, true);
                }
            }
        }
    }
}
