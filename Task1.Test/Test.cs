using NUnit.Framework;
using System;
using Task1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Test
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void Positive_Test_Int()
        {
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Enqueue(4);
            queue.Dequeue();
            string str = String.Empty;
            foreach (var element in queue)
                str += element;

            Assert.AreEqual("234", str);
        }
        [Test]
        public void Positive_Test_String()
        {
            Queue<string> queue = new Queue<string>(3);
            queue.Enqueue("1");
            queue.Enqueue("2");
            queue.Enqueue("3");
            queue.Dequeue();
            queue.Enqueue("4");
            queue.Enqueue("4");
            queue.Enqueue("4");
            queue.Dequeue();
            queue.Enqueue("2");

            string str = String.Empty;
            foreach (var element in queue)
                str += element;

            Assert.AreEqual("34442", str);
        }
    }

}
