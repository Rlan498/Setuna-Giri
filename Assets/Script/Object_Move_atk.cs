using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Move_atk : MonoBehaviour
{
    public float speed;//ˆÚ“®‘¬“x
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float xSpeed = 0.0f;
        float ySpeed = 0.0f;

        if (Chase.chaseStart == 0)
        {
            speed = 7.5f;
            rb.velocity = new Vector2(0.0f, 0.0f);
        }


        if (Chase.chaseStart == 1)
        {
            if (speed == 7.5)
            {
                if (Hayaoshi.timer <= 0.4) //‘¬‚³‚ÌãŒÀ
                {
                    speed = speed + 6.25f * Hayaoshi.timer;
                }
                else
                {
                    speed = speed + 2.5f;
                }
            }
            Debug.Log("speed = " + speed);
        
            if (Hayaoshi.ATK1 == 1)
            {
                if (Input.GetKey(KeyCode.S))
                {
                    ySpeed = -speed;
                }
                else if (Input.GetKey(KeyCode.W))
                {
                    ySpeed = speed;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    xSpeed = speed;
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    xSpeed = -speed;
                }

                rb.velocity = new Vector2(xSpeed, ySpeed);
            }

            if (Hayaoshi.ATK2 == 1)
            {
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    ySpeed = -speed;
                }
                else if (Input.GetKey(KeyCode.UpArrow))
                {
                    ySpeed = speed;
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    xSpeed = speed;
                }
                else if (Input.GetKey(KeyCode.LeftArrow))
                {
                    xSpeed = -speed;
                }

                rb.velocity = new Vector2(xSpeed, ySpeed);
            }
        }
        
    }
}
