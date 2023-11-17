using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPLogic : MonoBehaviour
{

    public int HPMax;
    public float HP;
    public Image imageHP;

    // Start is called before the first frame update
    void Start()
    {
        HP = HPMax;
    }

    // Update is called once per frame
    void Update()
    {
        CheckHealth();

        if (HP <= 0)
        {
            gameObject.SetActive(false);
            Debug.Log("La condición en Update de HPLogic se ha ejecutado");
        }
    }

    public void CheckHealth()
    {
        imageHP.fillAmount = HP / HPMax;
    }
}
