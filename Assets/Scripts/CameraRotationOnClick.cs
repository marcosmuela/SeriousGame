using System.Collections;
using UnityEngine;

public class CameraRotationOnClick : MonoBehaviour
{
    public Camera camera; // Cámara ortográfica a rotar
    public float rotationSpeed = 1f; // Velocidad de la animación

    private bool hasRotated = false;

    public void RotateCamera() // Ahora es público
    {
        if (!hasRotated)
        {
            StartCoroutine(RotateCameraCoroutine());
        }
    }

    IEnumerator RotateCameraCoroutine()
    {
        hasRotated = true;

        Quaternion targetRotation = Quaternion.Euler(0, -90, 0);
        Quaternion initialRotation = camera.transform.rotation;

        float timeElapsed = 0f;

        while (timeElapsed < rotationSpeed)
        {
            camera.transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, timeElapsed / rotationSpeed);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        camera.transform.rotation = targetRotation;
        Debug.Log("Cámara rotada a -90 grados.");
    }
}
