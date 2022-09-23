using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    float chase_time;
    int count;

    // Start is called before the first frame update
    void Start()
    {
        chase_time = 10f;
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (Hayaoshi.fight == 3 && count == 0)
        {
            this.transform.localScale = new Vector2(10, 0.5f);
            chase_time = 10f;
            this.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 255);
            count = 1;
        }
        else
        {
            if(Chase.chaseStart == 1)
            {
                chase_time -= Time.deltaTime;
                this.transform.localScale = new Vector2(chase_time, 0.5f);
            }
        }
        if(Chase.chaseStart == 0 && count == 1)
        {
            count = 0;
            this.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 255);
        }
    }
}
