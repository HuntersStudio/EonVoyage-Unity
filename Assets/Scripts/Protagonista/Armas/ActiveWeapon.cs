using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{

    public PickWeapon pickweapons;
    public int numWeapon;

    // Start is called before the first frame update
    void Start()
    {
        pickweapons = GameObject.FindGameObjectWithTag("Player").GetComponent<PickWeapon>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            pickweapons.ActiveWeapon(numWeapon);
            Destroy(gameObject);
        }
    }
}
