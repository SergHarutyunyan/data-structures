﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Containers.Tree
{
    class BinarySearchTree
    {
        internal class Node
        {
            internal int value;
            internal Node leftLink;
            internal Node rightLink;
            internal Node parent;
            internal Node() { }
            internal Node(int value, Node leftLink = null, Node rightLink = null, Node parent = null)
            {
                this.value = value;
                this.leftLink = leftLink;
                this.rightLink = rightLink;
                this.parent = parent;
            }
        }

        Node root;

        public Node Successor(Node node)           // The nearest higher element
        {
            if (node.rightLink != null)
            {
                node = node.rightLink;
                while (node.leftLink != null)
                    node = node.leftLink;
            }               
            return node;
        }
        public Node Predecessor(Node node)         // The nearest less element
        {
            if (node.leftLink != null)
            {
                node = node.leftLink;
                while (node.rightLink != null)
                    node = node.rightLink;
            }
                
            return node;
        }

        public void Insert(int elem)
        {
            Node current = root;
            Node previous = null;
            while (current != null)
            {
                previous = current;
                current = (elem > current.value) ? current.rightLink : current.leftLink;
            }

            current = new Node(elem, null, null, previous);
            if (previous == null)
                root = current;
            else
            {
                if (elem < previous.value)
                    previous.leftLink = current;
                else
                    previous.rightLink = current;
            }
        }

        public void Remove(int elem)
        {
            Node current = Find(elem);
            if (current == null)
            {
                Console.WriteLine("Specified node doesn't exist in tree.");
                return;
            }

            if (current.parent == null)                                              // if current node is a root
                root = current.rightLink;

            Node next;

            if (current.leftLink == null && current.rightLink == null)                // if current node hasn't children
            {                
                next = current.parent;
                if (current == next.leftLink)
                    next.leftLink = null;
                else
                    next.rightLink = null;
            }
            else if (current.leftLink == null || current.rightLink == null)         // if current node has one child
            {               
                if(current == current.parent.rightLink)
                    current.parent.rightLink = (current.leftLink != null) ? current.leftLink : current.rightLink;
                else
                    current.parent.leftLink = (current.leftLink != null) ? current.leftLink : current.rightLink;
            }
            else if (current.leftLink != null && current.rightLink != null)         // if current node has two children
            {
                Node replacingNode = (elem > root.value) ? Successor(current) : Predecessor(current);
                current.value = replacingNode.value;

                if (replacingNode == replacingNode.parent.leftLink)
                    replacingNode.parent.leftLink = null;
                else
                    replacingNode.parent.rightLink = null;
            }           
        }

        public Node Find(int elem)
        {
            Node current = root;

            while (current != null)
            {
                if (current.value == elem) return current;
                current = (elem > current.value) ? current.rightLink : current.leftLink;
            }
            return null;
        }
        
        public void PrintInorder(Node root)
        {
            if (root != null)
            {
                PrintInorder(root.leftLink);
                Console.Write(root.value + " ");
                PrintInorder(root.rightLink);
            }
        }

    }
}
