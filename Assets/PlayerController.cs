using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public RectTransform PanelCTA;
    public RectTransform PanelInfo;
    public bool nearObject;

    public Vector3 CameraOffset = new Vector3(0, 1f, -4f); // Ajusta la distancia de la cámara
    public Camera myCamera;

    private Animator animatorController;

    public float moveSpeed = 5f;
    private float rotationSpeed = 700f;

    private CharacterController characterController;  // Añadido CharacterController

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

        // Obtener el CharacterController
        characterController = GetComponent<CharacterController>();

        // Asegurarse de que el CharacterController esté habilitado y funcionando
        if (characterController == null)
        {
            Debug.LogError("No se ha encontrado un CharacterController en el personaje.");
        }
    }

    void Update()
    {
        // Obtener la entrada de movimiento del jugador (W, A, S, D)
        float moveInputVertical = Input.GetAxis("Vertical");
        float moveInputHorizontal = Input.GetAxis("Horizontal");

        // Crear un vector de movimiento relativo a la cámara
        Vector3 movement = new Vector3(moveInputHorizontal, 0, moveInputVertical);

        if (movement.magnitude > 0)
        {
            // Obtener la dirección hacia la que está mirando la cámara
            Vector3 forward = myCamera.transform.forward;
            Vector3 right = myCamera.transform.right;

            // Mantener la dirección en el plano horizontal (evitar que la cámara se mueva hacia arriba o abajo)
            forward.y = 0f;
            right.y = 0f;

            forward.Normalize();
            right.Normalize();

            // Calcular el movimiento relativo a la cámara
            Vector3 moveDirection = (forward * moveInputVertical + right * moveInputHorizontal).normalized;

            // Asegurarse de que el personaje se mueva en la dirección correcta
            characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

            // Rotar el personaje hacia la dirección del movimiento
            if (moveDirection != Vector3.zero)
            {
                // Obtener la rotación de destino en función del movimiento
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

            animatorController.SetBool("moving", true); // Iniciar animación de caminar
        }
        else
        {
            animatorController.SetBool("moving", false); // Detener animación de caminar
        }

        // Manejo de salto
        if (characterController.isGrounded)
        {
            verticalSpeed = -0.5f;  // Pequeña corrección para mantener al personaje pegado al suelo

            if (Input.GetButtonDown("Jump"))
            {
                verticalSpeed = Mathf.Sqrt(jumpHeight * -2f * gravity); // Aplicar la velocidad inicial del salto
            }
        }
        else
        {
            // Aplicar la gravedad cuando el personaje no está en el suelo
            verticalSpeed += gravity * Time.deltaTime;
        }

        // Aplicar el movimiento vertical (salto/gravedad)
        Vector3 move = new Vector3(0, verticalSpeed, 0);

        // Aplicar el movimiento en el eje Y (vertical) usando CharacterController
        characterController.Move(move * Time.deltaTime);

        // Verificar si el jugador presiona la tecla de salto y está cerca de un objeto
        if (Input.GetButtonDown("Jump") && nearObject)
        {
            //PanelInfo.gameObject.SetActive(true);
        }
    }

    // Detectar cuando el jugador entra en contacto con un objeto
    public void OnTriggerEnter(Collider other)
    {
        //PanelCTA.gameObject.SetActive(true);
        nearObject = true;
    }

    // Detectar cuando el jugador sale del contacto con un objeto
    public void OnTriggerExit(Collider other)
    {
        //PanelCTA.gameObject.SetActive(false);
        nearObject = false;
        //PanelInfo.gameObject.SetActive(false);
    }
}
