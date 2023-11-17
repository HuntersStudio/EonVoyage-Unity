using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{

    /* ATRIBUTOS */

    /* ----- Variables para movimiento ----- */

    // Constante para velocidad inicial
    public const float speedInitial = 3.0f;

    // Velocidad de movimiento actual
    public float speedMov = speedInitial;

    // Constante para el incremento de velocidad
    public float speedRun = 4.0f;

    // Velocidad Rotación del Ratón
    public float speedRot = 100.0f;

    // Variables para ejes de movimiento
    public float x, y;

    // Variable para la Rotación
    private float rotX;

    // Variable para correr
    public bool isRunning;

    /* ----- Variables para el ataque ----- */

    // Variable para el ataque
    public bool isAttack;

    // Variable para saber si está armado
    public bool isArmed;

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

        // Inicia sin correr
        isRunning = true;

        // Inicia sin estar armado
        isArmed = false;
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

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!isAttack)
            {
                anim.SetTrigger("slash");
                isAttack = true;
            }
            
        }

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

        // Si presiona Shift, y no está atacando, incrementará la velocidad y activará la animación
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isAttack)
        {
            speedMov = speedRun;

            // Si está en movimiento
            if (y > 0)
            {
                // Si está armado, activará la animación de correr mientras está armado.
                if (isArmed)
                {
                    anim.SetBool("isRunningArmed", true);
                }
                else
                {
                    anim.SetBool("isRunning", true);
                }
            }
            

        }

        // Si suelta Shift, dejará de correr
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speedMov = speedInitial;

            if (isArmed)
            {
                anim.SetBool("isRunningArmed", false);
            } else
            {
                anim.SetBool("isRunning", false);
            }
            
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

    public void StopSlash()
    {
        isAttack = false;
    }

}
