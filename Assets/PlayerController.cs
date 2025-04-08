using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public RectTransform PanelCTA;
    public RectTransform PanelInfo;
    public bool nearObject;

    public Vector3 CameraOffset = new Vector3(0, 1f, -4f);
    public Camera myCamera;

    private Animator animatorController;

    public float moveSpeed = 5f;
    private float rotationSpeed = 700f;

    private CharacterController characterController;

    public float jumpHeight = 2f;  // Altura del salto
    public float gravity = -9.81f; // Gravedad
    private float verticalSpeed = 0f;  // Velocidad vertical

    void Start()
    {
        if (myCamera != null)
        {
            myCamera.transform.position = transform.position + CameraOffset;
        }

        animatorController = GetComponent<Animator>();

        characterController = GetComponent<CharacterController>();

        if (characterController == null)
        {
            Debug.LogError("No se ha encontrado un CharacterController en el personaje.");
        }
    }

    void Update()
    {
        float moveInputVertical = Input.GetAxis("Vertical");
        float moveInputHorizontal = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(moveInputHorizontal, 0, moveInputVertical);

        if (movement.magnitude > 0)
        {
            Vector3 forward = myCamera.transform.forward;
            Vector3 right = myCamera.transform.right;

            forward.y = 0f;
            right.y = 0f;

            forward.Normalize();
            right.Normalize();

            Vector3 moveDirection = (forward * moveInputVertical + right * moveInputHorizontal).normalized;

            characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

            if (moveDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

            animatorController.SetBool("moving", true);
        }
        else
        {
            animatorController.SetBool("moving", false);
        }

        if (characterController.isGrounded)
        {
            verticalSpeed = -0.5f;

            if (Input.GetButtonDown("Jump"))
            {
                verticalSpeed = Mathf.Sqrt(jumpHeight * -2f * gravity); // Aplicar la velocidad inicial del salto
            }
        }
        else
        {
            verticalSpeed += gravity * Time.deltaTime;
        }

        Vector3 move = new Vector3(0, verticalSpeed, 0);

        characterController.Move(move * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && nearObject)
        {
            //PanelInfo.gameObject.SetActive(true);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        nearObject = true;
    }

    public void OnTriggerExit(Collider other)
    {
        nearObject = false;
    }
}
