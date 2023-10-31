using System;
using System.Collections.Generic;
using DialogueTreeSpace;
using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    GameManager gameManager;

    [SerializeField] private DialogueTree treeSister;
    [SerializeField] private DialogueTree treeWilliam;

    [SerializeField] private TextMeshProUGUI nameSpeaker;
    [SerializeField] private TextMeshProUGUI dialogueBox;
    [SerializeField] private TextMeshProUGUI option0box;
    [SerializeField] private TextMeshProUGUI option1box;
    [SerializeField] private TextMeshProUGUI winrateBox;
    [SerializeField] private TextMeshProUGUI dialogueCounterBox;
    [SerializeField] private TextMeshProUGUI consequenceBox; // shows what player gain or lose for option chosen

    [SerializeField] private Button option0Button;
    [SerializeField] private Button option1Button;


    private int dialogueCounter = 1;
    private int sumPoints = 0;

    Node currentNode;
    DialogueTree currentTree;

    void Start()
    {
        //ReadTree(treeSister);
        //ReadTree(treeWilliam);

        gameManager = GameManager.GetInstance();

        PlayTree(treeSister);
    }


    void PlayTree(DialogueTree tree)
    {
        currentTree = tree;
        currentNode = tree.GetTree();
        nameSpeaker.text = tree.GetName();

        SetOptionButtons(tree);
        DisplayDialogueNode(currentNode);
    }

    private void SetOptionButtons(DialogueTree tree)
    {
        //Debug.Log($"{tree.GetName()} {currentNode} {currentNode.options}");

        option0Button.onClick.RemoveAllListeners();
        option1Button.onClick.RemoveAllListeners();

        option0Button.onClick.AddListener(() => Option(tree, currentNode.options[0].nextId));
        option0Button.onClick.AddListener(IncreaseDialogueCounter);

        option1Button.onClick.AddListener(() => Option(tree, currentNode.options[1].nextId));
        option1Button.onClick.AddListener(IncreaseDialogueCounter);
        //Debug.Log("Listener added.");
    }

    void DisplayDialogueNode(Node node)
    {
        if (node == null) return;

        dialogueBox.text = node.dialogue;
        option0box.text = node.options[0].dialogue;
        option1box.text = node.options[1].dialogue;

        if (dialogueCounter == 0) winrateBox.text = "Win rate: 50%";
        else winrateBox.text = "Win rate: " + (int)(((double)sumPoints / (double)dialogueCounter) * 100.0f) +"%";// * 5.0f > 100.0f? 100.0f : (double)sumPoints * 5.0f)   + "%";

        dialogueCounterBox.text = "Dialogue: " + dialogueCounter.ToString();

        consequenceBox.text = node.modifier.category.ToString() + " " + node.modifier.value;
    }

    void Option(DialogueTree tree, int nextId)
    {
        if (nextId == -1 && tree.GetName() == "William")
        {
            SceneDirector.GetInstance().Load("Chapter2", true); // Load chapter 2;
            return;
        }
        else if (nextId == -1)
        {
            PlayTree(treeWilliam);
            return;
        }

        currentNode = GetNode(nextId);
        DisplayDialogueNode(currentNode);
        AddPlayerModifiers(currentNode);
    }

    private void AddPlayerModifiers(Node currentNode)
    {
        sumPoints += currentNode.modifier.value;
        if (currentNode.modifier.category == DialogueModifier.Category.SPREAD) 
        {
            GameManager.GetInstance().playerModifier.spread += currentNode.modifier.value;
        }
        else if(currentNode.modifier.category == DialogueModifier.Category.SPEED)
        {
            GameManager.GetInstance().playerModifier.speed += currentNode.modifier.value;
        }
        else if(currentNode.modifier.category == DialogueModifier.Category.HIT_POINTS)
        {
            GameManager.GetInstance().playerModifier.hp += currentNode.modifier.value;
        }
        else if(currentNode.modifier.category == DialogueModifier.Category.ATTACK)
        {
            GameManager.GetInstance().playerModifier.attack += currentNode.modifier.value;
        }
        Debug.Log($"{currentNode.modifier.category} | {currentNode.modifier.value} ");
    }

    /*void DEBUG()
    {

        Debug.Log($"{currentNode == null}");
        Debug.Log($"{currentTree == null}");
        Debug.Log($"{currentTree.GetName()} {currentTree}");
        Debug.Log($"Curr Tree: {currentTree} | Curr Node: {currentNode} | CurrNode Options: {currentNode.options}");
    }*/

    void IncreaseDialogueCounter() { dialogueCounter += 1; }

    // For button click
    public Node GetNode( int nextId)
    {
        return currentTree.GetNode(nextId);
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

/*    // Persistent
    public void ClickedOption0Button()
    {
        //DEBUG();
        Option(currentTree, currentNode.options[0].nextId);
        IncreaseDialogueCounter();
    }

    // Persistent
    public void ClickedOption1Button()
    {
        //DEBUG();
        Option(currentTree, currentNode.options[1].nextId);
        IncreaseDialogueCounter();
    }*/
}
