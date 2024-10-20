using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    [SerializeField] private List<Transform> cameraPositions; // List of positions for the camera
    [SerializeField] private float transitionSpeed = 2f; // Speed of the transition
    private int currentPositionIndex = 0; // Index of the current position

    // Start is called before the first frame update
    void Start()
    {
        if (cameraPositions.Count > 0)
        {
            SetCameraToPosition(cameraPositions[currentPositionIndex]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Change the key as needed
        {
            MoveToNextPosition();
        }
    }

    private void MoveToNextPosition()
    {
        currentPositionIndex = (currentPositionIndex + 1) % cameraPositions.Count;
        StartCoroutine(TransitionToPosition(cameraPositions[currentPositionIndex]));
    }

    private IEnumerator TransitionToPosition(Transform targetPosition)
    {
        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;
        Vector3 endPosition = targetPosition.position;
        Quaternion endRotation = targetPosition.rotation;

        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime);
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, elapsedTime);
            elapsedTime += Time.deltaTime * transitionSpeed;
            yield return null;
        }

        SetCameraToPosition(targetPosition);
    }

    private void SetCameraToPosition(Transform targetPosition)
    {
        transform.position = targetPosition.position;
        transform.rotation = targetPosition.rotation;
    }
}