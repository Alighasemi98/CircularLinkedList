using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CircularLinkedList
{
    public partial class Form1 : Form
    {
        CircularLinkedList<int> list = new CircularLinkedList<int>();
        public Form1()
        {
            InitializeComponent();
        }
        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            if (int.TryParse(txtValue.Text, out int value))
            {
                list.Add(value);
                txtValue.Clear();
                DisplayList();
            }
            else
            {
                MessageBox.Show("Please enter a valid number.");
            }
        }
        private void btnRemove_Click_1(object sender, EventArgs e)
        {
            if (int.TryParse(txtValue.Text, out int value))
            {
                list.Remove(value);
                txtValue.Clear();
                DisplayList();
            }
            else
            {
                MessageBox.Show("Please enter a valid number.");
            }

        }

        private void DisplayList()
        {
            lstDisplay.Items.Clear();
            foreach (var item in list)
            {
                lstDisplay.Items.Add(item);
            }
        }

        private void lstDisplay_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }

    public class CircularLinkedList<T> : System.Collections.Generic.IEnumerable<T> where T : IComparable<T>
    {
        private Node head;

        private class Node
        {
            public T Value;
            public Node Next;

            public Node(T value)
            {
                Value = value;
                Next = null;
            }
        }

        public void Add(T value)
        {
            Node newNode = new Node(value);
            if (head == null)
            {
                head = newNode;
                head.Next = head;
            }
            else if (head.Value.CompareTo(value) >= 0)
            {
                Node temp = head;
                while (temp.Next != head)
                {
                    temp = temp.Next;
                }
                newNode.Next = head;
                head = newNode;
                temp.Next = head;
            }
            else
            {
                Node current = head;
                while (current.Next != head && current.Next.Value.CompareTo(value) < 0)
                {
                    current = current.Next;
                }
                newNode.Next = current.Next;
                current.Next = newNode;
            }
        }

        public void Remove(T value)
        {
            if (head == null) return;

            if (head.Value.Equals(value))
            {
                if (head.Next == head)
                {
                    head = null;
                }
                else
                {
                    Node temp = head;
                    while (temp.Next != head)
                    {
                        temp = temp.Next;
                    }
                    head = head.Next;
                    temp.Next = head;
                }
                return;
            }

            Node current = head;
            Node previous = null;
            do
            {
                previous = current;
                current = current.Next;

                if (current.Value.Equals(value))
                {
                    previous.Next = current.Next;
                    return;
                }
            } while (current != head);
        }

        public System.Collections.Generic.IEnumerator<T> GetEnumerator()
        {
            if (head == null) yield break;

            Node current = head;
            do
            {
                yield return current.Value;
                current = current.Next;
            } while (current != head);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}