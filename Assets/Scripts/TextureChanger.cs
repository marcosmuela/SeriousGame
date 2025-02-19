using System.Collections;
using UnityEngine;

public class TextureChanger : MonoBehaviour
{
    public Renderer planeRenderer; // El material del plano
    public Texture[] textures; // Array con las 4 texturas en orden
    public float textureDuration = 6f; // Duración de cada textura en segundos
    public GameObject clickableObject; // Objeto que se podrá hacer clic para rotar la cámara
    
    private int currentTextureIndex = 0; // Índice de la textura actual
    private bool isClickable = false; // Indica si el objeto ya se puede pulsar

    void Start()
    {
        // Desactivar el objeto clickeable hasta que el ciclo se complete
        if (clickableObject != null)
        {
            clickableObject.SetActive(false);
        }

        // Iniciar la secuencia de cambio de texturas
        StartCoroutine(ChangeTextureRoutine());
    }

    IEnumerator ChangeTextureRoutine()
    {
        while (currentTextureIndex < textures.Length)
        {
            // Cambiar la textura del material del plano
            planeRenderer.material.mainTexture = textures[currentTextureIndex];

            // Esperar los segundos antes de cambiar la textura
            yield return new WaitForSeconds(textureDuration);

            // Pasar a la siguiente textura
            currentTextureIndex++;
        }

        // Una vez que se han mostrado todas las texturas en orden, habilitar el objeto clickeable
        if (clickableObject != null)
        {
            clickableObject.SetActive(true);
            isClickable = true; // Ahora el objeto puede ser clickeado
        }

        Debug.Log("Todas las texturas han sido mostradas. Objeto clickeable activado.");
    }

    void Update()
    {
        if (isClickable && Input.GetMouseButtonDown(0)) // Si se puede hacer clic y se pulsa el botón izquierdo
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == clickableObject) // Si se hace clic en el objeto correcto
                {
                    Debug.Log("Objeto clickeado. Rotando cámara...");
                    clickableObject.GetComponent<CameraRotationOnClick>().RotateCamera();
                    isClickable = false; // Evitar más clics después de activar la rotación
                }
            }
        }
    }
}
