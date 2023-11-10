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

    // Velocidad Rotaci�n del Rat�n
    public float speedRot = 100.0f;

    // Variables para ejes de movimiento
    public float x, y;

    // Variable para la Rotaci�n
    private float rotX;

    /* ----- Objetos Multifunci�n ----- */

    // Animator para realizar las animaciones
    public Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        // Al comenzar, el rat�n estar� bloqueado y oculto.
        Mouse(false);

        // Se asigna el componente de animaci�n
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        // Movimiento de la posici�n del personaje
        transform.Translate(new Vector3(x, 0.0f, y) * Time.deltaTime * speedMov);

        // Rotaci�n
        rotX = Input.GetAxis("Mouse X");
        transform.Rotate(new Vector3(0, rotX, 0) * Time.deltaTime * speedRot);

    }

    // Update is called once per frame
    void Update()
    {
        // Asignaci�n de ejes a las variables de movimiento
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        // Animaci�n del movimiento
        anim.SetFloat("SpeedX", x);
        anim.SetFloat("SpeedY", y);

        /* ----- DETECCI�N DE TECLAS ----- */

        // Al presionar la tecla Alt izquierda, se desbloquea el rat�n
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {   
            Mouse(true);
        }
        
        // Al soltar la tecla Alt izquierda, se bloquea el rat�n
        if (Input.GetKeyUp(KeyCode.RightAlt))
        {
            Mouse(false);
        }
    }

    // M�todo para ocultar y bloquear el Rat�n y viceversa
    public void Mouse(bool state)
    {
        // Si el estado enviado es true, se muestra y activa el rat�n
        if (state)
        {
            // Se activa el rat�n
            Cursor.lockState = CursorLockMode.None;

            // Se hace visible el rat�n
            Cursor.visible = true;
        }
        // Si es false, se bloquea y oculta el rat�n.
        else
        {
            // Se bloquea el rat�n
            Cursor.lockState = CursorLockMode.Locked;

            // Se oculta el rat�n
            Cursor.visible = false;
        }
    }

}
