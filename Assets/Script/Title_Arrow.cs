using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title_Arrow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Title_Select.place == 0)
        {
            this.transform.position = new Vector2(-3, -1);
        }else if(Title_Select.place == 1)
        {
            this.transform.position = new Vector2(-3, -2);
        }else if(Title_Select.place == 2)
        {
            this.transform.position = new Vector2(-3, -3);
        }
    }
}
