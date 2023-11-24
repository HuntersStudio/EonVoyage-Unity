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

    // Velocidad Rotaci�n del Rat�n
    public float speedRot = 100.0f;

    // Variables para ejes de movimiento
    public float x, y;

    // Variable para la Rotaci�n
    private float rotX;

    // Variable para correr
    public bool isRunning;

    /* ----- Variables para el ataque ----- */

    // Variable para saber si est� armado
    public bool isArmed;

    // Variable para saber si est� atacando
    public bool isAttacking;

    public GameObject[] weapons;

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

        // Inicia sin correr
        isRunning = false;

        // Inicia sin estar armado
        isArmed = false;

    }

    private void FixedUpdate()
    {

        // Movimiento de la posici�n del personaje
        transform.Translate(new Vector3(x, 0.0f, y) * Time.deltaTime * speedMov);

        // Rotaci�n
        rotX = Input.GetAxis("Mouse X");
        transform.Rotate(new Vector3(0, rotX, 0) * Time.deltaTime * speedRot);

        // Siempre har� que deje de atacar para evitar un problema de bucle infinito con el ataque.
        isAttacking = false;
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

        // Si presiona el click izquierdo, y no est� atacando.
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isAttacking )
        {
            // Si est� armado, activar� el trigger para el Slash, y se pondr� a atacar.
            if (isArmed)
            {
                anim.SetTrigger("slash");
                isAttacking = true;
            }

            // Si no est� armado, activar� el trigger para el Punch, y se pondr� a atacar.
            else
            {
                anim.SetTrigger("punch");
                isAttacking = true;
            }
        }

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

        // Si presiona Shift, incrementar� la velocidad y activar� la animaci�n
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRunning = true;
            speedMov = speedRun;

            // Si est� en movimiento
            if (y > 0)
            {
                // Si est� armado, activar� la animaci�n de correr mientras est� armado.
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

        // Si suelta Shift, dejar� de correr
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
