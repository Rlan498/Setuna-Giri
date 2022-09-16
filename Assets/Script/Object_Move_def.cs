using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Move_def : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Hayaoshi.ATK1 == 0)
        {
            if (Input.GetKey(KeyCode.S))
            {
                this.transform.position += new Vector3(0, -0.01f, 0);
            }

            if (Input.GetKey(KeyCode.W))
            {
                this.transform.position += new Vector3(0, 0.01f, 0);
            }

            if (Input.GetKey(KeyCode.A))
            {
                this.transform.position += new Vector3(-0.01f, 0, 0);
            }

            if (Input.GetKey(KeyCode.D))
            {
                this.transform.position += new Vector3(0.01f, 0, 0);
            }
        }

        if (Hayaoshi.ATK2 == 0)
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                this.transform.position += new Vector3(0, -0.01f, 0);
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                this.transform.position += new Vector3(0, 0.01f, 0);
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                this.transform.position += new Vector3(-0.01f, 0, 0);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                this.transform.position += new Vector3(0.01f, 0, 0);
            }
        }
    }
}
