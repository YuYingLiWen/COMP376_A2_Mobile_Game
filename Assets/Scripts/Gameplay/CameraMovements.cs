using System.Collections;
using UnityEngine;

public class CameraMovements : MonoBehaviour
{
    [SerializeField] Transform groundView;
    [SerializeField] Transform birdView;

    [SerializeField] float startTransitionTime = 5.0f;
    [SerializeField] float groundViewTransitionTime = 3.0f;
    [SerializeField] float birdViewTransitionTime = 1.0f;

    Transform player;

    [SerializeField] private float height = 20.0f;
    [SerializeField] private float lookAheadDistance = 10.0f;



    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;

        //StartCoroutine(ToViewRoutine(startTransitionTime, groundView));
    }

    private void LateUpdate()
    {
        var direction2d = InputSystem.Instance.RotationAxis.normalized;
        var direction = new Vector3(direction2d.x, transform.position.y, direction2d.z);

        transform.position = Vector3.Lerp(transform.position, new Vector3(
            player.position.x + direction.x * lookAheadDistance, 
            player.position.y + height, 
            player.position.z + direction.z * lookAheadDistance), 0.2f);
    }


    [ContextMenu("To Ground View")]
    private void ToGroundView()
    {
        StartCoroutine(ToViewRoutine(groundViewTransitionTime, groundView));
    }

    [ContextMenu("To Bird View")]
    private void ToBirdView()
    {
        StartCoroutine (ToViewRoutine(birdViewTransitionTime, birdView));
    }

    private IEnumerator ToViewRoutine(float transtitionTime, Transform to)
    {
        Transform cam = Camera.main.transform;
        Vector3 start = cam.position;
        Quaternion startRot = cam.rotation;

        float timeElapsed = 0f;
        float t = 0.0f;

        while (timeElapsed < transtitionTime)
        {
            cam.SetPositionAndRotation(Vector3.Slerp(start, to.position, t), Quaternion.Slerp(startRot, to.localRotation, t));

            timeElapsed += Time.deltaTime;
            t = Mathf.Clamp(timeElapsed / transtitionTime, 0.0f, 1.0f);
            yield return null;
        }
    }
}
