using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DoubleCircularList<T>
{
    class Node
    {
        public T Value { get; set; }
        public Node Next { get; set; }
        public Node Previous { get; set; }

        public Node(T value)
        {
            Value = value;
            Next = null;
            Previous = null;
        }
    }

    private Node head;
    public int count;

    public void InsertNodeAtStart(T value)
    {
        Node newNode = new Node(value);
        if (head == null)
        {
            head = newNode;
            head.Next = head;
            head.Previous = head;
        }
        else
        {
            Node lastNode = head.Previous;
            newNode.Next = head;
            head.Previous = newNode;
            lastNode.Next = newNode;
            newNode.Previous = lastNode;
            head = newNode;
        }
        count++;
    }

    public void InsertNodeAtEnd(T value)
    {
        Node newNode = new Node(value);
        if (head == null)
        {
            InsertNodeAtStart(value);
        }
        else
        {
            Node lastNode = head.Previous;
            lastNode.Next = newNode;
            newNode.Previous = lastNode;
            newNode.Next = head;
            head.Previous = newNode;
        }
        count++;
    }

    public void InsertNodeAtPosition(T value, int position)
    {
        if (position == 0)
        {
            InsertNodeAtStart(value);
        }
        else if (position == count - 1)
        {
            InsertNodeAtEnd(value);
        }
        else if (position > count || position < 0)
        {
            throw new IndexOutOfRangeException("Invalid position");
        }
        else
        {
            Node previousNode = head;
            for (int i = 0; i < position - 1; i++)
            {
                previousNode = previousNode.Next;
            }
            Node newNode = new Node(value);
            Node nextNode = previousNode.Next;

            previousNode.Next = newNode;
            newNode.Previous = previousNode;
            newNode.Next = nextNode;
            nextNode.Previous = newNode;
            count++;
        }
    }

    public void ModifyAtStart(T value)
    {
        if (head == null)
        {
            throw new InvalidOperationException("List is empty");
        }
        else
        {
            head.Value = value;
        }
    }

    public void ModifyAtEnd(T value)
    {
        if (head == null)
        {
            ModifyAtStart(value);
        }
        else
        {
            Node lastNode = head.Previous;
            lastNode.Value = value;
        }
    }

    public void ModifyAtPosition(T value, int position)
    {
        if (position == 0)
        {
            ModifyAtStart(value);
        }
        else if (position == count - 1)
        {
            ModifyAtEnd(value);
        }
        else if (position >= count || position < 0)
        {
            throw new IndexOutOfRangeException("Invalid position");
        }
        else
        {
            Node tmp = head;
            for (int i = 0; i < position; i++)
            {
                tmp = tmp.Next;
            }
            tmp.Value = value;
        }
    }

    public T GetNodeAtStart()
    {
        if (head == null)
        {
            throw new InvalidOperationException("List is empty");
        }
        else
        {
            return head.Value;
        }
    }

    public T GetNodeAtEnd()
    {
        if (head == null)
        {
            return GetNodeAtStart();
        }
        else
        {
            Node lastNode = head.Previous;
            return lastNode.Value;
        }
    }

    public T GetNodeAtPosition(int position)
    {
        if (position == 0)
        {
            return GetNodeAtStart();
        }
        else if (position == count - 1)
        {
            return GetNodeAtEnd();
        }
        else if (position >= count || position < 0)
        {
            throw new IndexOutOfRangeException("Invalid position");
        }
        else
        {
            Node tmp = head;
            for (int i = 0; i < position; i++)
            {
                tmp = tmp.Next;
            }
            return tmp.Value;
        }
    }

    public void DeleteNodeAtStart()
    {
        if (head == null)
        {
            throw new InvalidOperationException("List is empty");
        }

        Node oldHead = head;
        Node lastNode = head.Previous;
        Node newHead = head.Next;

        lastNode.Next = newHead;
        newHead.Previous = lastNode;

        head = newHead;
        oldHead.Next = null;
        oldHead.Previous = null;

        count--;

        if (count == 0)
        {
            head = null;
        }
    }

    public void DeleteNodeAtEnd()
    {
        if (head == null)
        {
            throw new InvalidOperationException("List is empty");
        }

        Node previousLastNode = head.Previous;
        Node lastNode = previousLastNode;

        Node newLastNode = previousLastNode.Previous;
        newLastNode.Next = head;
        head.Previous = newLastNode;

        lastNode.Previous = null;
        lastNode.Next = null;

        count--;

        if (count == 0)
        {
            head = null;
        }
    }

    public void DeleteNodeAtPosition(int position)
    {
        if (position == 0)
        {
            DeleteNodeAtStart();
        }
        else if (position == count - 1)
        {
            DeleteNodeAtEnd();
        }
        else if (position >= count || position < 0)
        {
            throw new IndexOutOfRangeException("Invalid position");
        }
        else
        {
            Node previousNode = head;
            for (int i = 0; i < position - 1; i++)
            {
                previousNode = previousNode.Next;
            }
            Node nodeToDelete = previousNode.Next;
            Node nextNode = nodeToDelete.Next;

            previousNode.Next = nextNode;
            nextNode.Previous = previousNode;

            nodeToDelete.Next = null;
            nodeToDelete.Previous = null;

            count--;

            if (count == 0)
            {
                head = null;
            }
        }
    }

    private Node GetLastNode()
    {
        if (head == null)
        {
            throw new InvalidOperationException("List is empty");
        }

        Node lastNode = head.Previous;
        return lastNode;
    }
}

