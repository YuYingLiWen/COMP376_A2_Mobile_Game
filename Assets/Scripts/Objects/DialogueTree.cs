using System;
using System.Collections.Generic;

using UnityEngine;

public class DialogueTree : MonoBehaviour
{
    private const String path = "Dialogues";
    private Node root;


    private void Awake()
    {
        try 
        {
            var dialogueFile = Resources.Load(path) as TextAsset;
            var lines = dialogueFile.text.Split(';');

            List<Node> nodes = new(); // Cache for ease of access

            foreach ( var line in lines )
            {
                String[] splits = line.Split(','); // id = 0; parent = 1; modifier = 2; modifier value = 3; dialogue = 4

                Node.Modifier modifier = new()
                {
                    category = Node.Convert(splits[2]),
                    value = Convert.ToInt32(splits[3])
                };

                Node n = new (Convert.ToInt32(splits[0]), modifier, splits[4]);

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

    public Node GetTree() => root;
    //////



    //////
    public struct Node
    {
        public int id;
        public String dialogue;
        public List<Node> children;
        public Modifier modifier;

        public Node(int id, Modifier modifier, String dialogue)
        {
            this.id = id;
            this.dialogue = dialogue;
            this.children = new();
            this.modifier = modifier;
        }

        public struct Modifier
        {
            public enum Category { NONE, ATTACK, HIT_POINTS, SPEED, SPREAD }

            public Category category;
            public int value;
        }

        public static Modifier.Category Convert(string modifier)
        {
            if (modifier == "ATTACK") return Modifier.Category.ATTACK;
            else if (modifier == "HP") return Modifier.Category.HIT_POINTS;
            else if (modifier == "SPEED") return Modifier.Category.SPEED;
            else if (modifier == "SPREAD") return Modifier.Category.SPREAD;
            else return Modifier.Category.NONE;
        }
    }
}
