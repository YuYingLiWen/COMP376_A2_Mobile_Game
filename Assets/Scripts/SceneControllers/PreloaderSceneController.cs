using UnityEngine;

public class PreloaderSceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneDirector instance = SceneDirector.GetInstance();
        instance.Load(SceneDirector.SceneNames.MAIN_MENU_SCENE, true);
    }
}
