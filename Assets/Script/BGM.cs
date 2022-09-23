using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Hayaoshi.fight == 0 && count == 0)
        {
            this.GetComponent<AudioSource>().Play();
            count = 1;
        }
        else if (Hayaoshi.fight >= 2)
        {
            this.GetComponent<AudioSource>().Stop();
            count = 0;
        }
    }
}
