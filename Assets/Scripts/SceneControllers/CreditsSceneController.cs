using System.Collections;
using UnityEngine;
using TMPro;

public class CreditsSceneController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI winlosetext;
    [SerializeField] private float transitionTime = 3.0f;
    private void OnEnable()
    {
        if (GameManager.GetInstance().HasWon) winlosetext.text = "You've satisfied your kill quota and have shipped back home.";
        else winlosetext.text = "You have become one with the battlefield.";

        StartCoroutine(DisplayRoutine());
    }

    private IEnumerator DisplayRoutine()
    {
        float elapsedTime = 0.0f;
        Color currentColor = winlosetext.color;
        while (elapsedTime < transitionTime)
        {
            winlosetext.color = Color.Lerp(currentColor, Color.white, elapsedTime / transitionTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(3f);
        elapsedTime = 0.0f;

        while (elapsedTime < transitionTime)
        {
            winlosetext.color = Color.Lerp(Color.white, currentColor, elapsedTime / transitionTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        SceneDirector.GetInstance().Load(SceneDirector.SceneNames.MAIN_MENU_SCENE, true);
    }
}


