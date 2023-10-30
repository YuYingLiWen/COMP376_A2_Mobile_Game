using System;
using System.Collections.Generic;

using UnityEngine;

namespace DialogueTreeSpace 
{
    public class DialogueTree : MonoBehaviour
    {
        [SerializeField] private string treeName;
        [SerializeField] private string path;

        private Node root;
        public Node GetTree() => root;

        [SerializeField] private List<Node> dialogueNodes = new();
        public Node GetNode(int id)
        {
            foreach(Node n in dialogueNodes)
            {
                if(n.id == id) return n;
            }

            return null;
        }

        private void Awake()
        {
            ReadFromFile(path);
        }


        void ReadFromFile(string fileName)
        {
            try
            {
                var dialogueFile = Resources.Load(fileName) as TextAsset;
                var lines = dialogueFile.text.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

                for (int i = 1; i < lines.Length; i++) // Skips file header
                {
                    if (string.IsNullOrEmpty(lines[i])) continue;

                    string[] chunks = lines[i].Split('\t');

                    string temp = "";
                    foreach (string s in chunks) temp += s + " || ";
                    print(temp);

                    Node newNode = new()
                    {
                        id = int.Parse(chunks[0]),
                        modifier = new DialogueModifier()
                        {
                            category = DialogueModifier.Convert(chunks[1]),
                            value = int.Parse(chunks[2])
                        },
                        dialogue = chunks[3]
                    };

                    newNode.options.Add(new DialogueOption()
                    {
                        nextId = int.Parse(chunks[4]),
                        dialogue = chunks[5],
                    });

                    newNode.options.Add(new DialogueOption()
                    {
                        nextId = int.Parse(chunks[6]),
                        dialogue = chunks[7],
                    });

                    dialogueNodes.Add(newNode);

                    if (newNode.id == 0) root = newNode;
                }
            }
            catch (Exception e)
            {
                print(e.ToString());
            }
        }
    }

    public struct DialogueOption
    {
        public string dialogue;
        public int nextId;
    }

    public class Node
    {
        public int id;
        public string dialogue;
        public DialogueModifier modifier;
        public List<DialogueOption> options = new(); // The dialogue option to pick in order to get to this node
    }

    public struct DialogueModifier
    {
        public enum Category { NONE, ATTACK, HIT_POINTS, SPEED, SPREAD }

        public Category category;
        public int value;

        public static Category Convert(string modifier)
        {
            modifier = modifier.ToUpper();
            if (modifier == "ATTACK") return Category.ATTACK;
            else if (modifier == "HP") return Category.HIT_POINTS;
            else if (modifier == "SPEED") return Category.SPEED;
            else if (modifier == "SPREAD") return Category.SPREAD;
            else return Category.NONE;
        }
    }
}
