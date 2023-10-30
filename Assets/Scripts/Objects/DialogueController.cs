using System.Collections.Generic;
using UnityEngine;
using DialogueTreeSpace;

using TMPro;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private DialogueTree treeSister;
    [SerializeField] private DialogueTree treeMom;

    [SerializeField]
    private TextMeshProUGUI dialogueBox;

    [SerializeField]
    private TextMeshProUGUI option0box;

    [SerializeField]
    private TextMeshProUGUI option1box;

    [SerializeField]
    private TextMeshProUGUI consquenceBox;

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