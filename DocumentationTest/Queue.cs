using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentationTest
{
    //!  Очередь.
    /*!
       Представляет абстрактный тип данных с дисциплиной доступа к элементам «первый пришёл — первый вышел» (first in, first out)
    */
    public class QueueClass<T> : ICloneable, IEnumerable<T>
    {
        private class Node
        {
            public T Data { get; set; }
            public Node Next { get; set; }

            public Node(T item)
            {
                Data = item;
                Next = null;
            }
        }

        private readonly int basicCapacity = 4;
        public int Capacity { get; private set; } // At least 1, Grow 2x
        public int Count { get; private set; }

        private Node head;
        private Node tail;
        private IEqualityComparer<T> comparer = EqualityComparer<T>.Default;

        //! Инициализирует новый пустой экземпляр QueueClass<T> с базовой начальной емкостью(4).
        /*!
        */
        public QueueClass()
        {
            Capacity = basicCapacity;
            Count = 0;
            head = null;
        }
        //! Инициализирует новый пустой экземпляр QueueClass<T> с указанной начальной емкостью.
        /*!
          \param capacity Количество элементов, которые изначально может хранить новая очередь.
        */
        public QueueClass(int capacity)
        {
            if (capacity <= 0) throw new ArgumentOutOfRangeException("capacity", "Ёмкость коллекции должна выражаться положительным числом");
            Capacity = capacity;
            Count = 0;
            head = null;
        }

        //! Инициализирует новый экземпляр QueueClass<T> с элементами другой очереди.
        /*!
          \param queue Очередь, элементами которой будет заполнена новая очередь.
        */
        public QueueClass(QueueClass<T> queue)
        {
            Capacity = queue.Capacity;
            Node node = queue.head;

            while (node != null)
            {
                Enqueue(node.Data);
                node = node.Next;
            }
        }

        //! Добавляет объект в конец QueueClass<T>.
        /*!
         \param item Объект, который необходимо добавить в конец QueueClass<T>.
        */
        public void Enqueue(T item)
        {
            Node node = new Node(item);

            if (head == null)
            {
                head = node;
                tail = node;
                Count++;
                return;
            }

            Count++;
            if (Count == Capacity) Capacity *= 2;
            tail.Next = node;
            tail = node;
        }

        //! Удаляет и возвращает объект из начала QueueClass<T>.
        /*!
        */
        public T Dequeue()
        {
            if (head == null) throw new NullReferenceException("Очередь пуста");

            T item = head.Data;
            head = head.Next;
            Count--;

            return item;
        }

        //! Возвращает объект из начала без удаления QueueClass<T>.
        /*!
         \param item Объект, который необходимо добавить в конец QueueClass<T>.
        */
        public T Peek()
        {
            if (head == null) throw new NullReferenceException("Очередь пуста");

            T item = head.Data;
            return item;
        }

        //! Определяет присутствует ли объект в QueueClass<T>.
        /*!
         \param item Объект, наличие которого необходимо проверить в QueueClass<T>.
        */
        public bool isExists(T item)
        {
            if (item == null) throw new NullReferenceException();

            bool ok = false;
            Node look = head;

            while (look != null && !ok)
            {
                ok = comparer.Equals(look.Data, item);
                look = look.Next;
            }

            return ok;
        }

        //! Метод для копирования QueueClass<T>.
        /*!
        */
        public object Clone()
        {
            return new QueueClass<T>(this);
        }

        //! Метод для поверхностного копирования QueueClass<T>.
        /*!
        */
        public QueueClass<T> SurfaceClone()
        {
            return this;
        }

        //! Очищает QueueClass<T>. Устанавливается начальная емкость.
        /*!
        */
        public void Clean()
        {
            Node node = head;

            while (node != null)
            {
                Node prev = node;
                node = node.Next;
                prev.Next = null;
            }

            head = null;
            tail = null;
            Count = 0;
            Capacity = basicCapacity;

            GC.Collect();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        class Enumerator : IEnumerator<T>
        {
            private Node head;
            private Node curr;

            public Enumerator(QueueClass<T> queue)
            {
                head = queue.head;
                curr = null;
            }

            public T Current
            {
                get
                {
                    if (curr != null) return curr.Data;
                    throw new InvalidOperationException();
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    if (curr == null) throw new InvalidOperationException();
                    return new Node(curr.Data);
                }
            }

            public bool MoveNext()
            {
                if (curr == null)
                {
                    curr = head;
                    return true;
                }

                if (curr.Next != null)
                {
                    curr = curr.Next;
                    return true;
                }
                else
                {
                    Reset();
                    return false;
                }
            }

            public void Reset()
            {
                curr = null;
            }

            public void Dispose() { }
        }
    }
}
