using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{

    TextMeshProUGUI m_TextMeshProUGUI;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        Application.runInBackground = false;
        Screen.orientation = ScreenOrientation.Portrait;
    }

    private void Start()
    {
      m_TextMeshProUGUI = GetComponent<TextMeshProUGUI>();  
    }

    // Update is called once per frame
    void Update()
    {
        m_TextMeshProUGUI.text =""+ 1.0f / Time.deltaTime;
    }
}
