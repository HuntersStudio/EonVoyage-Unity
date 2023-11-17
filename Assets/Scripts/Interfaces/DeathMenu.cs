using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class DeathMenu : MonoBehaviour
{
    public GameObject deathMenu;

    public GameObject dmRebootButton;

    public GameObject dmExitButton;

    public HPLogic hpLogic;

    public GameObject cameraDeath;

    public PlayerLogic playerLogic;

    // Start is called before the first frame update
    void Start()
    {

        hpLogic = GameObject.FindGameObjectWithTag("Player").GetComponent<HPLogic>();
        playerLogic = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hpLogic.HP <= 0 && playerLogic.isActiveAndEnabled)
        {
            deathMenu.SetActive(true);
            cameraDeath.SetActive(true);
            playerLogic.Mouse(true);
            Debug.Log("La condición en Update de DeathMenu se ha ejecutado");
        }
    }

    public void Reboot ()
    {
        // Lógica para reiniciar el juego
        Debug.Log("La Función Reboot de DeathMenu se ha ejecutado");
        SceneManager.LoadScene("SampleScene");
        
    }

    public void ExitButton ()
    {
        Debug.Log("La Función ExitButton de DeathMenu se ha ejecutado");
        Application.Quit();
    }


}
