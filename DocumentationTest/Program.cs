using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*! 
 * namespace DocumentationTest
 * \mainpage Основная информация
 *
 * Данная программа выполняет поиск кратчайшего пути между вершинами в ориентированном графе с помощью созданной структуры данных \link DocumentationTest.QueueClass Очередь \endlink
 *
 * \section usage_section Использование
 *
 * \subsection step1 Шаг 1: Запустите приложение
 * \subsection step2 Шаг 2: Введите данные согласно подсказкам в консоли
 *
 */
namespace DocumentationTest
{
    //! Основной класс, содержащий метод Main.
    /*!
     * Данный класс решает задачу поиска кратчайшего пути в ориентированном графе.
    */
    class Program
    {
        static void Main(string[] args)
        {
            QueueClass<int> q = new QueueClass<int>();
            int u;
            Random rand = new Random();

            u = rand.Next(3, 11);
            //int[][] g = new int[u + 1][];

            /*for (int i = 0; i < u + 1; i++)
            {
                g[i] = new int[u + 1];
                for (int j = 0; j < u + 1; j++)
                {
                    g[i][j] = rand.Next(0, 2);
                }
                g[i][i] = 0;
                foreach (var item in g[i])
                {
                    Console.Write(" {0}", item);
                }
                Console.Write("]\n");

            }*/

            Console.WriteLine("Введите количество вершин графа");
            int x = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите номер вершины из которой необходимо найти кратчайший путь");
            u = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите номер вершины в которую необходимо найти путь");
            int finish = int.Parse(Console.ReadLine());

            int[,] g = new int[x, x];
            for (int i = 0; i < x; i++)
            {
                Console.WriteLine("Введите строку матрицы смежности графа с номером " + (i + 1));
                string[] row = Console.ReadLine().Split(' ');
                for (int j = 0; j < x; j++)
                {
                    g[i, j] = int.Parse(row[j]);
                }
            }

            bool[] usedVertices = new bool[x + 1];
            int[] predecessor = new int[x + 1];
            predecessor[u] = u;
            q.Enqueue(u);
            //Console.WriteLine("Начинаем обход с {0} вершины", u + 1);
            while (q.Count != 0)
            {
                u = q.Peek();
                q.Dequeue();
                //Console.WriteLine("Перешли к узлу {0}", u + 1);

                for (int i = 0; i < x; i++)
                {
                    if (Convert.ToBoolean(g[u, i]))
                    {
                        if (!usedVertices[i])
                        {
                            usedVertices[i] = true;
                            predecessor[i] = u;
                            q.Enqueue(i);
                            //Console.WriteLine("Добавили в очередь узел {0}", i + 1);
                        }
                    }
                }
            }

            way(finish, predecessor);
            Console.ReadLine();

        }

        static void way(int u, int[] p)
        {
            if (p[u] != u)
                way(p[u], p);
            Console.Write((u + 1) + " ");
        }
    }
}
