using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    private DialogueTree.Node root;
    [SerializeField] private DialogueTree tree;

    private void Awake()
    {
        root = tree.GetTree();
    }

    void Start()
    {
        print(root.dialogue);
        print(root.children[0].dialogue);
    }

    void Update()
    {
        
    }
}
