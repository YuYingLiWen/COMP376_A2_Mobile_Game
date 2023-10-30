using System.Collections.Generic;
using UnityEngine;
using DialogueTreeSpace;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private DialogueTree treeSister;
    [SerializeField] private DialogueTree treeMom;


    void Start()
    {
        ReadTree(treeSister);
        ReadTree(treeMom);
    }

    void ReadTree(DialogueTree tree)
    {
        Node root = tree.GetTree();

        Queue<Node> queue = new();
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            Node pointer = queue.Dequeue();

            if (pointer == null) break;

            Debug.Log($"{pointer.dialogue} | Child count: {(pointer.options == null ? "Null" : pointer.options.Count)}");

            var children = pointer.options;

            foreach (DialogueOption option in children)
                queue.Enqueue(tree.GetNode(option.nextId));
        }
    }
}
