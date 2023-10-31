using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reload : MonoBehaviour
{
    [SerializeField] private GameObject[] bullets;
    [SerializeField] private Image reload;

    const int count = 5;
    int currentBullet = 5;

    private bool canFire = true;
    [SerializeField] private float reloadTime = 1.0f;
    private float elapsedReloadTime = 0.0f;

    public bool CanFire => canFire;

    public void OnFire()
    {
        currentBullet -= 1;
        bullets[currentBullet].SetActive(false);

        if(currentBullet == 0) canFire = false;
    }

    private void Update()
    {
        Reloading();
    }

    private void Reloading()
    {
        if (canFire) return; // If can fire then don't need reload.

        if (elapsedReloadTime >= reloadTime)
        {
            canFire = true;
            elapsedReloadTime = 0.0f;
            OnReloaded();
            return;
        }

        reload.fillAmount = elapsedReloadTime / reloadTime;
        elapsedReloadTime += Time.deltaTime;
    }

    void OnReloaded()
    {
        currentBullet = count;
        reload.fillAmount = 0.0f;
        foreach(GameObject obj in bullets) obj.SetActive(true);
    }

}
