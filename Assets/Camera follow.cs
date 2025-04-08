using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;         
    public Vector3 offset;          
    public float rotationSpeed = 5f; 

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position - target.forward * offset.z + Vector3.up * offset.y;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * rotationSpeed);

            Quaternion lookRotation = Quaternion.LookRotation(target.position - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

            transform.LookAt(target);
        }
    }
}
