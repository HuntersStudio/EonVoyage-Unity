using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class OldManLogic : MonoBehaviour
{

    // Contrlador del protagonista para inmovilizarlo mientras ocurre el diálogo
    public PlayerLogic playerLogic;

    // Interaccion
    public GameObject Interaccion;

    // Panel de mensaje
    public GameObject panelMensaje;

    // Variable para detectar cuando el personaje esté cerca del NPC
    public bool nearPlayer;

    // Variable para saber si ya ha hablado con el NPC
    public bool talked;

    // Start is called before the first frame update
    void Start()
    {
        playerLogic = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLogic>();

        talked = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        // Cuando se presione la tecla E
        if (Input.GetKeyDown(KeyCode.E) && nearPlayer)
        {
            Vector3 positionPlayer = new Vector3(transform.position.x, playerLogic.gameObject.transform.position.y, transform.position.z);
            playerLogic.gameObject.transform.LookAt(positionPlayer);

            playerLogic.enabled = false;
            playerLogic.Mouse(true);

            Interaccion.SetActive(false);

            panelMensaje.SetActive(true);
        }
    }

    // Trigger cuando detecta una colisión
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            nearPlayer = true;

            if (!talked)
            {
                Interaccion.SetActive(true) ;
            }
        }
    }

    // Trigger cuando sale de la colisión
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            nearPlayer = false;

            Interaccion.SetActive(false);
        }
    }

    public void Continuar ()
    {
        Debug.Log("Continuar() se ha ejecutado");
        playerLogic.enabled = true;

        playerLogic.Mouse(false);

        panelMensaje.SetActive(false);

        talked = true;
    }
}
