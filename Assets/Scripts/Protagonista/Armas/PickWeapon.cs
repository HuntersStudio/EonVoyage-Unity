using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickWeapon : MonoBehaviour
{

    public GameObject[] weapons;
    public PlayerLogic playerLogic;

    // Start is called before the first frame update
    void Start()
    {
        playerLogic = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActiveWeapon(int num)
    {
        // Desactiva todas las armas
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }

        // Activa el arma indicada.
        weapons[num].SetActive(true);

        // El jugador está armado
        playerLogic.isArmed = true;

    }

    public void DeactivateWeapon ()
    {
        // Desactiva todas las armas
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }
        
        // El jugador deja de estar armado
        playerLogic.isArmed = false;
    }
}
