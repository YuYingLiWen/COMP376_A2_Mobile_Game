using System;
using System.Collections.Generic;
using DialogueTreeSpace;
using TMPro;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DialogueController : MonoBehaviour
{
    GameManager gameManager;

    [SerializeField] private DialogueTree treeSister;
    [SerializeField] private DialogueTree treeMom;

    [SerializeField] private TextMeshProUGUI dialogueBox;
    [SerializeField] private TextMeshProUGUI option0box;
    [SerializeField] private TextMeshProUGUI option1box;
    [SerializeField] private TextMeshProUGUI winrateBox;
    [SerializeField] private TextMeshProUGUI dialogueCounterBox;
    [SerializeField] private TextMeshProUGUI consequenceBox; // shows what player gain or lose for option chosen

    [SerializeField] private Button option0Button;
    [SerializeField] private Button option1Button;


    private int dialogueCounter = 0;
    private int sumPoints = 0;

    Node currentNode;


    void Start()
    {
        //ReadTree(treeSister);
        //ReadTree(treeMom);

        gameManager = GameManager.GetInstance();

        PlayTree(treeSister);
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

    void PlayTree(DialogueTree tree)
    {
        currentNode = tree.GetTree();
        DisplayDialogueNode(currentNode);
        SetOptionButtons(tree);
    }

    private void SetOptionButtons(DialogueTree tree)
    {
        option0Button.onClick.AddListener(() => Option(tree, currentNode.options[0].nextId));
        option0Button.onClick.AddListener(() => IncreaseDialogueCounter());

        option1Button.onClick.AddListener(() => Option(tree, currentNode.options[1].nextId));
        option1Button.onClick.AddListener(() => IncreaseDialogueCounter());

        Debug.Log("Listener added.");
    }

    void DisplayDialogueNode(Node node)
    {
        if (node == null) return;

        dialogueBox.text = node.dialogue;
        option0box.text = node.options[0].dialogue;
        option1box.text = node.options[1].dialogue;

        if (dialogueCounter == 0) winrateBox.text = "50%";
        else winrateBox.text = "" + ((double)sumPoints / (double)dialogueCounter) + "%";

        dialogueCounterBox.text = dialogueCounter.ToString();

        consequenceBox.text = node.modifier.category.ToString() + " " + node.modifier.value;
    }

    void Option(DialogueTree tree, int nextId)
    {
        currentNode = GetNode(tree, nextId);

        DisplayDialogueNode(currentNode);
    }

    void IncreaseDialogueCounter() { dialogueCounter += 1; }

    // For button click
    public Node GetNode(DialogueTree tree, int nextId)
    {
        return tree.GetNode(nextId);
    }
}
