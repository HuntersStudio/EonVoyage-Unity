using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{

    /* ATRIBUTOS */

    /* ----- Variables para movimiento ----- */

    // Constante para velocidad inicial
    public const float speedInitial = 5.0f;

    // Velocidad de movimiento actual
    public float speedMov = speedInitial;

    // Velocidad Rotación del Ratón
    public float speedRot = 100.0f;

    // Variables para ejes de movimiento
    public float x, y;

    // Variable para la Rotación
    private float rotX;

    /* ----- Objetos Multifunción ----- */

    // Animator para realizar las animaciones
    public Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        // Al comenzar, el ratón estará bloqueado y oculto.
        Mouse(false);

        // Se asigna el componente de animación
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        // Movimiento de la posición del personaje
        transform.Translate(new Vector3(x, 0.0f, y) * Time.deltaTime * speedMov);

        // Rotación
        rotX = Input.GetAxis("Mouse X");
        transform.Rotate(new Vector3(0, rotX, 0) * Time.deltaTime * speedRot);

    }

    // Update is called once per frame
    void Update()
    {
        // Asignación de ejes a las variables de movimiento
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        // Animación del movimiento
        anim.SetFloat("SpeedX", x);
        anim.SetFloat("SpeedY", y);

        /* ----- DETECCIÓN DE TECLAS ----- */

        // Al presionar la tecla Alt izquierda, se desbloquea el ratón
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {   
            Mouse(true);
        }
        
        // Al soltar la tecla Alt izquierda, se bloquea el ratón
        if (Input.GetKeyUp(KeyCode.RightAlt))
        {
            Mouse(false);
        }
    }

    // Método para ocultar y bloquear el Ratón y viceversa
    public void Mouse(bool state)
    {
        // Si el estado enviado es true, se muestra y activa el ratón
        if (state)
        {
            // Se activa el ratón
            Cursor.lockState = CursorLockMode.None;

            // Se hace visible el ratón
            Cursor.visible = true;
        }
        // Si es false, se bloquea y oculta el ratón.
        else
        {
            // Se bloquea el ratón
            Cursor.lockState = CursorLockMode.Locked;

            // Se oculta el ratón
            Cursor.visible = false;
        }
    }

}
