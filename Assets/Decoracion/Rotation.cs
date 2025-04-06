using UnityEngine;

public class SlowRotation : MonoBehaviour
{
    public float rotationSpeed = 10f;  // Velocidad de rotación

    void Update()
    {
        // Girar el objeto alrededor del eje Y (por ejemplo, de izquierda a derecha)
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
