
using UnityEngine;

public class PreloaderSettings : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
        Application.runInBackground = false;
        Screen.orientation = ScreenOrientation.Portrait;
    }
}
