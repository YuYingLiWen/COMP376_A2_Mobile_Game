using System.Collections.Generic;
using DialogueTreeSpace;
using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    GameManager gameManager;

    [SerializeField] private DialogueTree treeSister;
    [SerializeField] private DialogueTree treeMom;

    [SerializeField] private TextMeshProUGUI nameSpeaker;
    [SerializeField] private TextMeshProUGUI dialogueBox;
    [SerializeField] private TextMeshProUGUI option0box;
    [SerializeField] private TextMeshProUGUI option1box;
    [SerializeField] private TextMeshProUGUI winrateBox;
    [SerializeField] private TextMeshProUGUI dialogueCounterBox;
    [SerializeField] private TextMeshProUGUI consequenceBox; // shows what player gain or lose for option chosen

    [SerializeField] private Button option0Button;
    [SerializeField] private Button option1Button;

/*    [SerializeField] private Button option0ButtonMom;
    [SerializeField] private Button option1ButtonMom;*/

   /* [SerializeField] private GameObject momPanel;
    [SerializeField] private GameObject sisterPanel;*/


    private int dialogueCounter = 1;
    private int sumPoints = 0;

    Node currentNode;
    DialogueTree currentTree;

/*    UnityEngine.Events.UnityAction option0Action;
    UnityEngine.Events.UnityAction option1Action;*/

    private void Awake()
    {
    }

    void Start()
    {
        //ReadTree(treeSister);
        //ReadTree(treeMom);

        gameManager = GameManager.GetInstance();

        PlayTree(treeSister);
    }


    void PlayTree(DialogueTree tree)
    {
        currentTree = tree;
        currentNode = tree.GetTree();
        nameSpeaker.text = tree.GetName();
        Debug.Log(currentNode == null);

        //SetOptionButtons(tree);
        //if (tree.GetName() != "Mom") SetOptionButtons(tree);
        //else SetOptionMomButtons(tree);

        DisplayDialogueNode(currentNode);
    }

    private void SetOptionButtons(DialogueTree tree)
    {
        Debug.Log($"{tree.GetName()} {currentNode} {currentNode.options}");

        //option0Action = new UnityEngine.Events.UnityAction(() => Option(tree, currentNode.options[0].nextId));
        //option1Action = new UnityEngine.Events.UnityAction(() => Option(tree, currentNode.options[1].nextId));

        //option0ButtonSister.onClick.AddListener(option0Action);
        //option0ButtonSister.onClick.AddListener(IncreaseDialogueCounter);
        
        //option1ButtonSister.onClick.AddListener(option1Action);
        //option1ButtonSister.onClick.AddListener(IncreaseDialogueCounter);
        Debug.Log("Listener added.");
    }

/*    private void SetOptionMomButtons(DialogueTree tree)
    {
        //sisterPanel.SetActive(false); // Disable the sister button panel
        //momPanel.SetActive(true);

        Debug.Log($"{tree.GetName()} {currentNode} {currentNode.options}");

        //option0Action = new UnityEngine.Events.UnityAction(() => Option(tree, currentNode.options[0].nextId));
        //option1Action = new UnityEngine.Events.UnityAction(() => Option(tree, currentNode.options[1].nextId));

        option0ButtonMom.onClick.AddListener(option0Action);
        option0ButtonMom.onClick.AddListener(IncreaseDialogueCounter);
                     
        option1ButtonMom.onClick.AddListener(option1Action);
        option1ButtonMom.onClick.AddListener(IncreaseDialogueCounter);
        Debug.Log("Listener added.");
    }*/

    void DisplayDialogueNode(Node node)
    {
        if (node == null) return;

        dialogueBox.text = node.dialogue;
        option0box.text = node.options[0].dialogue;
        option1box.text = node.options[1].dialogue;

        if (dialogueCounter == 0) winrateBox.text = "Win rate: 50%";
        else winrateBox.text = "Win rate: " + ((double)sumPoints / (double)dialogueCounter) + "%";

        dialogueCounterBox.text = "Dialogue: " + dialogueCounter.ToString();

        consequenceBox.text = node.modifier.category.ToString() + " " + node.modifier.value;
    }

    void Option(DialogueTree tree, int nextId)
    {
        if (nextId == -1 && tree.GetName() == "Mom") SceneDirector.GetInstance().Load("Chapter2", true) ; // Load chapter 2;
        if (nextId == -1)
        {
            PlayTree(treeMom);
            return;
        }

        currentNode = GetNode(nextId);
        DisplayDialogueNode(currentNode);
    }

    void DEBUG()
    {

        Debug.Log($"{currentNode == null}");
        Debug.Log($"{currentTree == null}");
        Debug.Log($"{currentTree.GetName()} {currentTree}");
        Debug.Log($"Curr Tree: {currentTree} | Curr Node: {currentNode} | CurrNode Options: {currentNode.options}");
    }

    void IncreaseDialogueCounter() { Debug.Log("Increased Counter.");  dialogueCounter += 1; }

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

    // Persistent
    public void ClickedOption0Button()
    {
        DEBUG();
        Option(currentTree, currentNode.options[0].nextId);
        IncreaseDialogueCounter();
    }

    // Persistent
    public void ClickedOption1Button()
    {
        DEBUG();
        Option(currentTree, currentNode.options[1].nextId);
        IncreaseDialogueCounter();
    }
}
