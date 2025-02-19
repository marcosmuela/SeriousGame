using System.Collections;
using UnityEngine;

public class CameraRotation180Trigger : MonoBehaviour
{
    public Camera cameraToRotate; // Cámara a rotar
    public float rotationSpeed = 1f; // Velocidad de la animación

    private bool hasRotated = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Detecta clic
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject) // Si el objeto fue clickeado
                {
                    Debug.Log("Clic en el botón de rotación. Rotando cámara...");
                    RotateCamera();
                }
            }
        }
    }

    public void RotateCamera()
    {
        if (!hasRotated)
        {
            StartCoroutine(RotateCameraCoroutine());
        }
    }

    IEnumerator RotateCameraCoroutine()
    {
        hasRotated = true;

        Quaternion targetRotation = Quaternion.Euler(0, 180, 0);
        Quaternion initialRotation = cameraToRotate.transform.rotation;

        float timeElapsed = 0f;

        while (timeElapsed < rotationSpeed)
        {
            cameraToRotate.transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, timeElapsed / rotationSpeed);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        cameraToRotate.transform.rotation = targetRotation;
        Debug.Log("Cámara rotada a 180 grados.");
    }
}
