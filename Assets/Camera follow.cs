using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;         // El personaje a seguir
    public Vector3 offset;          // La distancia entre la c�mara y el personaje
    public float rotationSpeed = 5f; // Velocidad de rotaci�n de la c�mara

    void LateUpdate()
    {
        if (target != null)
        {
            // Mantener la c�mara a la misma distancia, pero ajustada a la rotaci�n del personaje
            //transform.position = target.position - target.forward * offset.z + Vector3.up * offset.y;
            Vector3 desiredPosition = target.position - target.forward * offset.z + Vector3.up * offset.y;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * rotationSpeed);

            // Asegurar que la c�mara siempre mire al personaje
            Quaternion lookRotation = Quaternion.LookRotation(target.position - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);


            // Asegurarse de que la c�mara est� mirando al personaje
            transform.LookAt(target);
        }
    }
}
