using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{
    public GameObject ATK;//UŒ‚‘¤
    public GameObject DEF;//Žç”õ‘¤1‰ñ–Ú

    
    public static int chaseStart = 0;//1‚É‚È‚é‚ÆU–hŠJŽn
    int time_start = 1;
    public static int win1 = 0;
    public static int win2 = 0;

    public GameObject Wall;//•Ç

    // Start is called before the first frame update
    void Start()
    {
        ATK.SetActive(false);
        DEF.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Hayaoshi.fight == 2)
        {
            Hayaoshi.fight = 3;
            time_start = 1;
            if(time_start == 1)
            {
                StartCoroutine("stream");
            }
        }
    }

    private IEnumerator stream()
    {
        Wall.SetActive(true);//•Ç‚Ì‹N“®

        if (time_start == 0)
        {
            yield break;
        }
        time_start = 0;
        yield return new WaitForSeconds(1);//‡@‘€ìƒIƒuƒWƒFƒNƒgoŒ»

        if (Hayaoshi.ATK1 == 1)
        {
            ATK.transform.position = new Vector2(-2, 0);
            DEF.transform.position = new Vector2(2, 0);
        }

        if (Hayaoshi.ATK2 == 1)
        {
            ATK.transform.position = new Vector2(2, 0);
            DEF.transform.position = new Vector2(-2, 0);
        }

        ATK.SetActive(true);

        if(Hayaoshi.ATK1 == 1 && Hayaoshi.DEF2 == 2 || Hayaoshi.ATK2 == 1 && Hayaoshi.DEF1 == 2)
        {
            DEF.transform.localScale = new Vector2(2.0f, 2.0f);//2‰ñ–Ú‚ÌŽç”õ‘¤‚Ì‘å‚«‚³
        }

        if (Hayaoshi.ATK1 == 1 && Hayaoshi.DEF2 == 3 || Hayaoshi.ATK2 == 1 && Hayaoshi.DEF1 == 3)
        {
            DEF.transform.localScale = new Vector2(1.5f, 1.5f);//3‰ñ–Ú‚ÌŽç”õ‘¤‚Ì‘å‚«‚³
        }
        DEF.SetActive(true);

        Debug.Log("ƒXƒ^[ƒg‚©‚ç1•bŒã");

        yield return new WaitForSeconds(2);//‡AŽžŠÔ·2•b‚ÅU–hŠJŽn
        chaseStart = 1;
        Debug.Log("U–hŠJŽn@ŽžŠÔ‚Í10•b");

        yield return new WaitForSeconds(10);//‡B10•b‚ÅU–hI—¹
        chaseStart = 0;
        Debug.Log("U–hI—¹");

        Vector2 ATK_position = GameObject.Find("attacker").transform.position;
        Vector2 DEF_position = GameObject.Find("defender").transform.position;

        float x = ATK_position.x - DEF_position.x;
        float y = ATK_position.y - DEF_position.y;

        Debug.Log("x = " + x + ", y = " + y);

        if (Hayaoshi.ATK1 == 1)
        {
            if (Hayaoshi.DEF2 == 1)
            {
                if ((x > -1.5f && x < 1.5f) && (y > -1.5f && y < 1.5f))
                {
                    win1 = 1;
                }
            }

            if (Hayaoshi.DEF2 == 2)
            {
                if ((x > -1.25f && x < 1.25f) && (y > -1.25f && y < 1.25f))
                {
                    win1 = 1;
                }
            }

            if (Hayaoshi.DEF2 == 3)
            {
                if ((x > -1.0f && x < 1.0f) && (y > -1.0f && y < 1.0f))
                {
                    win1 = 1;
                }
            }
        }

        if (Hayaoshi.ATK2 == 1)
        {
            if (Hayaoshi.DEF1 == 1)
            {
                if ((x > -1.5f && x < 1.5f) && (y > -1.5f && y < 1.5f))
                {
                    win2 = 1;
                }
            }

            if (Hayaoshi.DEF1 == 2)
            {
                if ((x > -1.25f && x < 1.25f) && (y > -1.25f && y < 1.25f))
                {
                    win2 = 1;
                }
            }

            if (Hayaoshi.DEF1 == 3)
            {
                if ((x > -1.0f && x < 1.0f) && (y > -1.0f && y < 1.0f))
                {
                    win2 = 1;
                }
            }
        }

        yield return new WaitForSeconds(3);//‡C3•b‚ÌŒ‹‰ÊŠm”FŒã”»’è
        ATK.SetActive(false);
        DEF.SetActive(false);

        Wall.SetActive(false);

        //“–‚½‚è”»’èŠm”F¨ðŒ•ªŠò
        if (win1 == 1 || win2 == 1 || Hayaoshi.DEF1 == 3 || Hayaoshi.DEF2 == 3)
        {
            Hayaoshi.fight = 4;
            x = 5;
            y = 5;
            Debug.Log("Œˆ’…");

            yield return new WaitForSeconds(1);//‡DƒŠƒUƒ‹ƒg‚Ì•\Ž¦(êŠ‚ÍˆÚ‚·‚©‚à)

            if (win1 == 1 || (win2 == 0 && Hayaoshi.DEF1 == 3))
            {
                Debug.Log("PL1‚ÌŸ—˜‚Å‚·");
            }
            else if (win2 == 1 || (win1 == 0 && Hayaoshi.DEF2 == 3))
            {
                Debug.Log("PL2‚ÌŸ—˜‚Å‚·");
            }
        }
        else
        {
            Hayaoshi.fight = 0;
            Debug.Log("Œ©Ø‚è‚ÉˆÚs");
        }
    }
}
