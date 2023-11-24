using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{

    /* ATRIBUTOS */

    /* ----- Variables para movimiento ----- */

    // Constante para velocidad inicial
    public const float speedInitial = 1.8f;

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

    // Variable para saber si está armado
    public bool isArmed;

    // Variable para saber si está atacando
    public bool isAttacking;

    public GameObject[] weapons;

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
        isRunning = false;

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

        // Siempre hará que deje de atacar para evitar un problema de bucle infinito con el ataque.
        isAttacking = false;
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

        // Si presiona el click izquierdo, y no está atacando.
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isAttacking )
        {
            // Si está armado, activará el trigger para el Slash, y se pondrá a atacar.
            if (isArmed)
            {
                anim.SetTrigger("slash");
                isAttacking = true;
            }

            // Si no está armado, activará el trigger para el Punch, y se pondrá a atacar.
            else
            {
                anim.SetTrigger("punch");
                isAttacking = true;
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

        // Si presiona Shift, incrementará la velocidad y activará la animación
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRunning = true;
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
        else
        {
            isRunning = false;
        }

        // Si suelta Shift, dejará de correr
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
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

    public void ActivateAttack()
    {
        weapons[0].GetComponent<BoxCollider>().enabled = true;
        weapons[1].GetComponent<BoxCollider>().enabled = true;
    }

    public void DeactivateAttack ()
    {
        weapons[0].GetComponent<BoxCollider>().enabled = false;
        weapons[1].GetComponent<BoxCollider>().enabled = false;
    }

}
