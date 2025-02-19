using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorOrderChecker : MonoBehaviour
{
    public GameObject[] cubes;  // Cubos en la escena
    public GameObject[] circles;  // Círculos que cambian el color de los cubos
    public GameObject rotationButton; // Objeto que permitirá la rotación de la cámara

    private List<Color> correctColorOrder = new List<Color>();  // El orden correcto de colores
    private List<Color> paintedColors = new List<Color>();  // Colores pintados en los cubos
    private bool isOrderCorrect = false; // Controla si el orden es correcto

    void Start()
    {
        // Llenamos el orden correcto de colores basado en los círculos
        foreach (GameObject circle in circles)
        {
            Color circleColor = circle.GetComponent<Renderer>().material.color;
            correctColorOrder.Add(circleColor);
        }

        // Ocultar el botón de rotación al inicio
        if (rotationButton != null)
        {
            rotationButton.SetActive(false);
        }
    }

    void Update()
    {
        CheckColorOrder();
    }

    void CheckColorOrder()
    {
        paintedColors.Clear();

        foreach (GameObject cube in cubes)
        {
            Color currentColor = cube.GetComponent<Renderer>().material.color;
            paintedColors.Add(currentColor);
        }

        if (paintedColors.Count == cubes.Length)
        {
            bool correctOrder = true;

            for (int i = 0; i < paintedColors.Count; i++)
            {
                if (!AreColorsEqual(paintedColors[i], correctColorOrder[i]))
                {
                    correctOrder = false;
                    break;
                }
            }

            // Si el orden es correcto y no se ha activado antes
            if (correctOrder && !isOrderCorrect)
            {
                isOrderCorrect = true;
                Debug.Log("¡El orden de los colores es correcto!");
                
                // Activar el objeto clickeable para rotar la cámara
                if (rotationButton != null)
                {
                    rotationButton.SetActive(true);
                }
            }
            else if (!correctOrder && isOrderCorrect) // Si el orden vuelve a ser incorrecto
            {
                isOrderCorrect = false;
                Debug.Log("Orden incorrecto, desactivando el botón.");

                if (rotationButton != null)
                {
                    rotationButton.SetActive(false);
                }
            }
        }
    }

    // Método para comparar dos colores RGB con un margen de tolerancia
    bool AreColorsEqual(Color color1, Color color2)
    {
        float tolerance = 0.05f;  

        return Mathf.Abs(color1.r - color2.r) < tolerance &&
               Mathf.Abs(color1.g - color2.g) < tolerance &&
               Mathf.Abs(color1.b - color2.b) < tolerance;
    }
}
