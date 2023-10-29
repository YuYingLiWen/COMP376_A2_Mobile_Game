using System.Collections;
using UnityEngine;

public class CameraMovements : MonoBehaviour
{
    [SerializeField] Transform groundView;
    [SerializeField] Transform birdView;

    [SerializeField] float startTransitionTime = 5.0f;
    [SerializeField] float groundViewTransitionTime = 3.0f;
    [SerializeField] float birdViewTransitionTime = 1.0f;

    [SerializeField] private float height = 20.0f;
    [SerializeField] private float lookAheadDistance = 10.0f;

    [SerializeField] private float bobDistance = 1.0f;

    private Vector3 cameraOriginalVals;

    private void Awake()
    {
        cameraOriginalVals = transform.localPosition;
    }

    void Start()
    {
        //StartCoroutine(ToViewRoutine(startTransitionTime, groundView));
    }

    public void WalkAnimation()
    {
        StartCoroutine(WalkAnimationRoutine());
    }

    private IEnumerator WalkAnimationRoutine()
    {
        Transform cam = Camera.main.transform;

        float timeElapsed = 0.0f;

        while (true)
        {
            cam.Translate(Vector3.up * Mathf.Sin(timeElapsed));
            timeElapsed += Time.deltaTime;
            yield return null;
        }
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
