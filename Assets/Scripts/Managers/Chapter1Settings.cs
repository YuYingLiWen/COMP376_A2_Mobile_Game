
using UnityEngine;

public class Chapter1Settings : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
        Screen.orientation = ScreenOrientation.LandscapeRight;
    }
}
