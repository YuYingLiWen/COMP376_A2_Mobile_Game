using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
