using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Used in a test scene. 
/// 
/// Please duplicate this instead of writing in it.
/// 
/// Has basic functionality:
/// 
///     - Camera Panning
///     - Left mouse click
/// 
/// </summary>

public class BAREBONE_TEST_SCRIPT : MonoBehaviour
{
    //Cache
    private Vector2 axis;
    private Camera cam;
    private Ray mouseRay;


    public float cameraSpeed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        MouseClick();
    }

    void MouseClick()
    {
        mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        Debug.DrawLine(mouseRay.origin, mouseRay.direction * 1000f, Color.red);

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            RaycastHit hit;

            if (Physics.Raycast(mouseRay, out hit, float.MaxValue))
            {
                var gameObject = hit.collider.gameObject;

                if (gameObject.CompareTag("Enemy")) // Example 
                {
                    gameObject.GetComponent<TestEnemy>().Click();
                }
                else
                {
                    Debug.Log(gameObject.name); // Example
                }
            }
        }
    }

    private void LateUpdate()
    {
        axis.x = Input.GetAxis("Horizontal");
        axis.y = Input.GetAxis("Vertical");

        cam.transform.Translate(axis * cameraSpeed * Time.deltaTime);
    }
}
