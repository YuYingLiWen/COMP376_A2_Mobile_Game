
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    private SceneDirector sceneDirector;


    private void Awake()
    {
        sceneDirector = SceneDirector.GetInstance();
        if (!sceneDirector) Debug.LogError("Missing SceneDirector.", gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel1()
    {
        Debug.Log("Clieck load ");
        sceneDirector.Load(SceneDirector.SceneNames.LEVEL1_SCENE, true);
    }
}
