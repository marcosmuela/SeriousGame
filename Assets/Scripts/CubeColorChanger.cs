using UnityEngine;

public class CubeColorChanger : MonoBehaviour
{
    private Renderer cubeRenderer;

    void Start()
    {
        cubeRenderer = GetComponent<Renderer>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Circle"))
        {
            // Obtener el color del material del círculo
            Color circleColor = other.GetComponent<Renderer>().material.color;

            // Asignar ese color al cubo
            cubeRenderer.material.color = circleColor;
            Debug.Log("Cubo pintado con el color: " + circleColor);
        }
    }
}
