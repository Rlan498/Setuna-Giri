using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chase : MonoBehaviour
{
    public GameObject ATK;//UŒ‚‘¤
    public GameObject DEF;//ç”õ‘¤1‰ñ–Ú

    
    public static int chaseStart = 0;//1‚É‚È‚é‚ÆU–hŠJn
    int time_start = 1;
    public static int win1 = 0;
    public static int win2 = 0;

    public GameObject Wall;//•Ç
    GameObject conclusion;
    GameObject fa;

    // Start is called before the first frame update
    void Start()
    {
        ATK.SetActive(false);
        DEF.SetActive(false);
        conclusion = GameObject.Find("conclusion");
        fa = GameObject.Find("failed_attack");
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
            DEF.transform.localScale = new Vector2(4.0f, 4.0f);//2‰ñ–Ú‚Ìç”õ‘¤‚Ì‘å‚«‚³
        }

        if (Hayaoshi.ATK1 == 1 && Hayaoshi.DEF2 == 3 || Hayaoshi.ATK2 == 1 && Hayaoshi.DEF1 == 3)
        {
            DEF.transform.localScale = new Vector2(3.0f, 3.0f);//3‰ñ–Ú‚Ìç”õ‘¤‚Ì‘å‚«‚³
        }
        DEF.SetActive(true);

        Debug.Log("ƒXƒ^[ƒg‚©‚ç1•bŒã");

        yield return new WaitForSeconds(2);//‡AŠÔ·2•b‚ÅU–hŠJn
        chaseStart = 1;
        Debug.Log("U–hŠJn@ŠÔ‚Í10•b");

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
                if ((x <= -2.75f || x >= 2.75f) || (y <= -2.75f || y >= 2.75f))
                {
                    win1 = 1;
                }
            }

            if (Hayaoshi.DEF2 == 2)
            {
                if ((x <= -2.25f || x >= 2.25f) || (y <= -2.25f || y >= 2.25f))
                {
                    win1 = 1;
                }
            }

            if (Hayaoshi.DEF2 == 3)
            {
                if ((x <= -1.75f || x >= 1.75f) || (y <= -1.75f || y >= 1.75f))
                {
                    win1 = 1;
                }
            }
        }

        if (Hayaoshi.ATK2 == 1)
        {
            if (Hayaoshi.DEF1 == 1)
            {
                if ((x <= -2.75f || x >= 2.75f) || (y <= -2.75f || y >= 2.75f))
                {
                    win2 = 1;
                }
            }

            if (Hayaoshi.DEF1 == 2)
            {
                if ((x <= -2.25f || x >= 2.25f) || (y <= -2.25f || y >= 2.25f))
                {
                    win2 = 1;
                }
            }

            if (Hayaoshi.DEF1 == 3)
            {
                if ((x <= -1.75f || x >= 1.75f) || (y <= -1.75f || y >= 1.75f))
                {
                    win2 = 1;
                }
            }
        }

        yield return new WaitForSeconds(1);//‡C3•b‚ÌŒ‹‰ÊŠm”FŒã”»’è ¨@1•b‚É’Zk
        ATK.SetActive(false);
        DEF.SetActive(false);

        Wall.SetActive(false);

        //“–‚½‚è”»’èŠm”F¨ğŒ•ªŠò
        if (win1 == 1 || win2 == 1 || Hayaoshi.DEF1 == 3 || Hayaoshi.DEF2 == 3)
        {
            Hayaoshi.fight = 4;
            x = 5;
            y = 5;
            conclusion.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 255);
            Debug.Log("Œˆ’…");
            conclusion.GetComponent<AudioSource>().Play();

            yield return new WaitForSeconds(2);//‡DƒŠƒUƒ‹ƒg‚Ì•\¦(êŠ‚ÍˆÚ‚·‚©‚à)

            if (win1 == 1 || (win2 == 0 && Hayaoshi.DEF1 == 3))
            {
                Debug.Log("PL1‚ÌŸ—˜‚Å‚·");
                SceneManager.LoadScene("Result");
            }
            else if (win2 == 1 || (win1 == 0 && Hayaoshi.DEF2 == 3))
            {
                Debug.Log("PL2‚ÌŸ—˜‚Å‚·");
                SceneManager.LoadScene("Result");
            }
        }
        else
        {
            fa.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 255);
            fa.GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(2);
            fa.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 255);
            Hayaoshi.fight = 0;
            Debug.Log("Œ©Ø‚è‚ÉˆÚs");
        }
    }
}
