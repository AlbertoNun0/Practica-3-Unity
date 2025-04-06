using UnityEngine;

public class OrbFloat : MonoBehaviour
{
    public float floatSpeed = 1f;
    public float floatHeight = 0.5f;
    public float rotationSpeed = 50f; // Velocidad de rotación en grados por segundo

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Movimiento de flotación
        float y = Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = startPos + new Vector3(0, y, 0);

        // Rotación en el eje Y
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}


