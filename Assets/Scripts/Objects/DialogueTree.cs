using System;
using System.Collections.Generic;
using System.IO;

using UnityEngine;

public class DialogueTree : MonoBehaviour
{
    private const String path = "Dialogues";
    public struct Node
    {
        public int id;
        public String dialogue;
        public List<Node> children;

        public Node(int id, String dialogue)
        {
            this.id = id;
            this.dialogue = dialogue;
            this.children = new List<Node>();
        }
    }

    private Node root;

    public Node GetTree() => root;

    private void Awake()
    {
        print("ASDF");
        try 
        {
            var dialogueFile = Resources.Load(path) as TextAsset;
            var lines = dialogueFile.text.Split(';');

            List <Node> nodes = new List<Node>();

            foreach ( var line in lines )
            {
                print(line);
                String[] splits = line.Split(','); // id = 0; parent = 1; dialogue = 2
                Node n = new Node(Convert.ToInt32(splits[0]), splits[2]);

                nodes.Add(n);

                if (n.id == 0) root = n;

                // Attach to parent
                foreach (Node nd in nodes)
                {
                    if (nd.id == Convert.ToInt32(splits[1]))
                    {
                        nd.children.Add(n);
                    }
                }
            }
        }
        catch(Exception e)
        {
            print(e.ToString() + " aka path DNE.");
        }
    }
}
