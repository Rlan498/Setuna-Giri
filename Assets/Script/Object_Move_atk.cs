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

        if (Hayaoshi.ATK1 == 1)
        {
            if (Input.GetKey(KeyCode.S))
            {
                xSpeed = 0.0f;
                ySpeed = -speed;
            }
            else if (Input.GetKey(KeyCode.W))
            {
                xSpeed = 0.0f;
                ySpeed = speed;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                xSpeed = -speed;
                ySpeed = 0.0f;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                xSpeed = speed;
                ySpeed = 0.0f;
            }
            else
            {
                xSpeed = 0.0f;
                ySpeed = 0.0f;
            }
            rb.velocity = new Vector2(xSpeed, ySpeed);
        }

        if (Hayaoshi.ATK2 == 1)
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                xSpeed = 0.0f;
                ySpeed = -speed;
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                xSpeed = 0.0f;
                ySpeed = speed;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                xSpeed = -speed;
                ySpeed = 0.0f;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                xSpeed = speed;
                ySpeed = 0.0f;
            }
            else
            {
                xSpeed = 0.0f;
                ySpeed = 0.0f;
            }
            rb.velocity = new Vector2(xSpeed, ySpeed);
        }
    }
}
