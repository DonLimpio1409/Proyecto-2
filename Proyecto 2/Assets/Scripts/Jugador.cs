using UnityEngine;

public class Jugador : MonoBehaviour
{
    [Header("Movimiento y Gravedad")]
    [SerializeField] float velocidad = 5f;
    [SerializeField] float gravedad = 9.81f;
    [SerializeField] float fuerzaSalto = 5f;
    [SerializeField] bool enSuelo;

    [Header("Cámara")]
    [SerializeField] Transform camara;
    [SerializeField] float sensibilidadHorizontal = 100f;
    [SerializeField] float sensibilidadVertical = 100f;

    CharacterController controlador;
    Vector3 velocidadY;
    float rotacionVertical;

    void Start()
    {
        controlador = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Mirar();
        Mover();
    }

    void Mover()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 direccion = transform.right * x + transform.forward * z;

        // Comprobar si estamos tocando el suelo con un raycast
        enSuelo = Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1.1f);
        Debug.DrawRay(transform.position, Vector3.down * 1.1f, Color.red);

        if (enSuelo && velocidadY.y <= 0f && Input.GetButtonDown("Jump"))
        {
            velocidadY.y = Mathf.Sqrt(fuerzaSalto * 2f * gravedad);
        }

        velocidadY.y -= gravedad * Time.deltaTime;

        Vector3 movimiento = direccion * velocidad + velocidadY;
        controlador.Move(movimiento * Time.deltaTime);
    }

    void Mirar()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensibilidadHorizontal * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidadVertical * Time.deltaTime;

        transform.Rotate(0f, mouseX, 0f);

        rotacionVertical -= mouseY;
        rotacionVertical = Mathf.Clamp(rotacionVertical, -90f, 90f);
        camara.localRotation = Quaternion.Euler(rotacionVertical, 0f, 0f);
    }
}

