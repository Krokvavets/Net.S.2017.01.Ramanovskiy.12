using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class Queue<T> : IEnumerable<T>, IEnumerable
    {
        private T[] array;
        private int head, tail;
        private const int defaultCapacity = 50;
        private int capacity;

        public int Count { get; private set; }

        public Queue()
        {
            capacity = defaultCapacity;
            Count = 0;
            array = new T[defaultCapacity];
            head = 0;
            tail = -1;
        }
        public Queue(int capacity)
        {
            if (capacity < 0) throw new ArgumentException("capacity well be more then zero");
            array = new T[capacity];
            this.capacity = capacity;
            head = 0;
            tail = -1;
        }
        public Queue(IEnumerable<T> collection)
        {
            if (ReferenceEquals(collection, null)) throw new ArgumentNullException();
            capacity = collection.Count();
            array = new T[capacity];
            int i = 0;
            foreach (var element in collection)
                array[i++] = element;
            head = 0;
            tail = array.Length - 1;
        }
        /// <summary>
        /// Removes all objects from the Queue.
        /// </summary>
        public void Clear()
        {
            Count = 0;
            head = 0;
            tail = -1;
        }
        /// <summary>
        /// Determines whether an element is in the Queue.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(T item)
        {
            return Array.IndexOf(array, item) >= 0;
        }
        /// <summary>
        /// Copies the Queue elements to an existing one-dimensional Array, starting at the specified array index.
        /// </summary>
        /// <param name="array">The one-dimensional Array that is the destination of the elements copied from Queue. The Array must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (ReferenceEquals(array, null)) throw new ArgumentNullException();
            if (arrayIndex < 0) throw new ArgumentOutOfRangeException();
            if (array.Length < arrayIndex + Count) throw new ArgumentException();
            for (int i = arrayIndex; i < Count; i++)
                array[i] = this.array[i];
        }
        void CopyTo(Array array, int index)
        {
            CopyTo((T[])array, index);
        }
        /// <summary>
        /// Removes and returns the object at the beginning of the Queue.
        /// </summary>
        /// <returns>The object that is removed from the beginning of the Queue.</returns>
        public T Dequeue()
        {
            if (tail == -1) throw new Exception("Queue is empty");
            int i = head;
            if (tail == head)
            {
                tail = -1;
                head = 0;
                Count--;
                return array[i];               
            }
            head++;
            Count--;
            return array[i];           
        }
        /// <summary>
        /// Adds an object to the end of the Queue.
        /// </summary>
        /// <param name="item">The object to add to the Queue. The value can be null.</param>
        public void Enqueue(T item)
        {
            if (ReferenceEquals(item, null)) throw new ArgumentNullException();
            if (tail == -1)
            {
                tail = head;
                array[head] = item;
                Count++;
                return;
            }
           if( (tail + 1 == head) || (tail == array.Length - 1 && head == 0))
            {
                T [] newArray = new T[2 * capacity];
                int j = 0;
                for (int i = head; i < array.Length; i++)
                    newArray[j++] = array[i];
                if (tail < head)
                    for (int i = 0; i <= tail; i++)
                        newArray[j++] = array[i];
                newArray[j] = item;
                head = 0;
                tail = j;
                array = newArray;
                Count++;
                return;
            }
            if (tail == array.Length - 1)
            {
                tail = 0;
                array[tail] = item;
                Count++;
                return;
            }
            array[++tail] = item;
            Count++;
        }
        /// <summary>
        /// Copies the Queue elements to a new array.
        /// </summary>
        /// <returns>A new array containing elements copied from the Queue.</returns>
        public T[] ToArray()
        {
            T[] newArray = new T[Count];
            for (int i = 0; i < Count; i++)
                newArray[newArray.Length - 1 - i] = this.Dequeue();
            return newArray;
        }
        /// <summary>
        /// Sets the capacity to the actual number of elements in the Queue.
        /// </summary>
        public void TrimExcess()
        {
            if ((Count * 100 / capacity) < 90)
            {
                T[] temp = new T[Count];
                CopyTo(temp, 0);
                array = temp;
            }

        }
        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            return new CustomIterator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new CustomIterator(this);
        }

        public T Peek()
        {
            return array[head];
        }

        private struct CustomIterator : IEnumerator<T>
        {
            private readonly Queue<T> collection;
            private int currentIndex;
            private int count;

            public CustomIterator(Queue<T> collection)
            {
                this.collection = collection;
                this.currentIndex = -1;
                count = collection.Count;
            }

            public T Current
            {
                get
                {
                    if (currentIndex == -1 || currentIndex == count)
                    {
                        throw new InvalidOperationException();
                    }
                    return collection.Dequeue();
                }
            }

            object System.Collections.IEnumerator.Current
            {
                get { return Current; }
            }

            public void Reset()
            {
                currentIndex = -1;
            }

            public bool MoveNext()
            {
                return ++currentIndex < count;
            }
            public void Dispose() { }
        }
    }
}

