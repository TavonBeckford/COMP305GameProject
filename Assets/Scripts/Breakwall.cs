using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakwall : MonoBehaviour
{

    public GameObject mob;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mob = GameObject.FindGameObjectWithTag("Enemy");
        if (mob == null)
        {
            this.gameObject.SetActive(false);
        }

    }
}
