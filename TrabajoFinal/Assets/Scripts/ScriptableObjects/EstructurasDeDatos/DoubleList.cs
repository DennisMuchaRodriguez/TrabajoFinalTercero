using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleList<T>
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
    private Node tail;
    private int count;

    public int Count
    {
        get { return count; }
    }

    public DoubleList()
    {
        head = null;
        tail = null;
        count = 0;
    }

    public void InsertNodeAtStart(T value)
    {
        Node newNode = new Node(value);
        if (head == null)
        {
            head = newNode;
            tail = newNode;
        }
        else
        {
            newNode.Next = head;
            head.Previous = newNode;
            head = newNode;
        }
        count++;
    }

    public void InsertNodeAtEnd(T value)
    {
        Node newNode = new Node(value);
        if (tail == null)
        {
            head = newNode;
            tail = newNode;
        }
        else
        {
            newNode.Previous = tail;
            tail.Next = newNode;
            tail = newNode;
        }
        count++;
    }

    public void InsertNodeAtPosition(T value, int position)
    {
        if (position < 0 || position > count)
        {
            throw new IndexOutOfRangeException("Error");
        }
        if (position == 0)
        {
            InsertNodeAtStart(value);
        }
        else if (position == count)
        {
            InsertNodeAtEnd(value);
        }
        else
        {
            Node newNode = new Node(value);
            Node current = head;
            for (int i = 0; i < position - 1; i++)
            {
                current = current.Next;
            }
            newNode.Next = current.Next;
            newNode.Previous = current;
            current.Next.Previous = newNode;
            current.Next = newNode;
            count++;
        }
    }

    public void ModifyAtStart(T value)
    {
        if (head == null)
        {
            throw new InvalidOperationException("Error");
        }
        head.Value = value;
    }

    public void ModifyAtEnd(T value)
    {
        if (tail == null)
        {
            throw new InvalidOperationException("Error");
        }
        tail.Value = value;
    }

    public void ModifyAtPosition(T value, int position)
    {
        if (position < 0 || position >= count)
        {
            throw new IndexOutOfRangeException("Error");
        }
        Node current = head;
        for (int i = 0; i < position; i++)
        {
            current = current.Next;
        }
        current.Value = value;
    }

    public T GetNodeAtStart()
    {
        if (head == null)
        {
            throw new InvalidOperationException("Error");
        }
        return head.Value;
    }

    public T GetNodeAtEnd()
    {
        if (tail == null)
        {
            throw new InvalidOperationException("Error");
        }
        return tail.Value;
    }

    public T GetNodeAtPosition(int position)
    {
        if (position < 0 || position >= count)
        {
            throw new IndexOutOfRangeException("Error");
        }
        Node current = head;
        for (int i = 0; i < position; i++)
        {
            current = current.Next;
        }
        return current.Value;
    }

    public void DeleteNodeAtStart()
    {
        if (head == null)
        {
            throw new InvalidOperationException("Error");
        }
        if (head.Next == null)
        {
            head = null;
            tail = null;
        }
        else
        {
            head = head.Next;
            head.Previous = null;
        }
        count--;
    }

    public void DeleteNodeAtEnd()
    {
        if (tail == null)
        {
            throw new InvalidOperationException("Error");
        }
        if (tail.Previous == null)
        {
            head = null;
            tail = null;
        }
        else
        {
            tail = tail.Previous;
            tail.Next = null;
        }
        count--;
    }

    public void DeleteNodeAtPosition(int position)
    {
        if (position < 0 || position >= count)
        {
            throw new IndexOutOfRangeException("Error");
        }
        if (position == 0)
        {
            DeleteNodeAtStart();
        }
        else if (position == count - 1)
        {
            DeleteNodeAtEnd();
        }
        else
        {
            Node current = head;
            for (int i = 0; i < position; i++)
            {
                current = current.Next;
            }
            current.Previous.Next = current.Next;
            current.Next.Previous = current.Previous;
            count--;
        }
    }

    public void DisplayList()
    {
        Node current = head;
        while (current != null)
        {
            Console.Write(current.Value + " ");
            current = current.Next;
        }
        Console.WriteLine();
    }
}