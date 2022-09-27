using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chase : MonoBehaviour
{
    public GameObject ATK;//攻撃側
    public GameObject DEF;//守備側1回目

    
    public static int chaseStart = 0;//1になると攻防開始
    public static int time_start = 1;
    public static int win1 = 0;
    public static int win2 = 0;

    public GameObject Wall;//壁
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
        Wall.SetActive(true);//壁の起動

        if (time_start == 0)
        {
            yield break;
        }
        time_start = 0;
        yield return new WaitForSeconds(1);//�@操作オブジェクト出現

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

        if (Hayaoshi.ATK1 == 1 && Hayaoshi.DEF2 == 1 || Hayaoshi.ATK2 == 1 && Hayaoshi.DEF1 == 1)
        {
            DEF.transform.localScale = new Vector2(5.0f, 5.0f);//1回目の守備側の大きさ
        }
        if (Hayaoshi.ATK1 == 1 && Hayaoshi.DEF2 == 2 || Hayaoshi.ATK2 == 1 && Hayaoshi.DEF1 == 2)
        {
            DEF.transform.localScale = new Vector2(4.0f, 4.0f);//2回目の守備側の大きさ
        }

        if (Hayaoshi.ATK1 == 1 && Hayaoshi.DEF2 == 3 || Hayaoshi.ATK2 == 1 && Hayaoshi.DEF1 == 3)
        {
            DEF.transform.localScale = new Vector2(3.0f, 3.0f);//3回目の守備側の大きさ
        }
        DEF.SetActive(true);

        Debug.Log("スタートから1秒後");

        yield return new WaitForSeconds(2);//�A時間差2秒で攻防開始
        chaseStart = 1;
        Debug.Log("攻防開始　時間は10秒");

        yield return new WaitForSeconds(10);//�B10秒で攻防終了
        chaseStart = 0;
        Debug.Log("攻防終了");

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

        yield return new WaitForSeconds(1);//�C3秒の結果確認後判定 →　1秒に短縮
        ATK.SetActive(false);
        DEF.SetActive(false);

        Wall.SetActive(false);

        //当たり判定確認→条件分岐
        if (win1 == 1 || win2 == 1 || Hayaoshi.DEF1 == 3 || Hayaoshi.DEF2 == 3)
        {
            Hayaoshi.fight = 4;
            x = 5;
            y = 5;
            conclusion.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 255);
            Debug.Log("決着");
            conclusion.GetComponent<AudioSource>().Play();

            yield return new WaitForSeconds(2);//�Dリザルトの表示(場所は移すかも)

            if (win1 == 1 || (win2 == 0 && Hayaoshi.DEF1 == 3))
            {
                Debug.Log("PL1の勝利です");
                SceneManager.LoadScene("Result");
            }
            else if (win2 == 1 || (win1 == 0 && Hayaoshi.DEF2 == 3))
            {
                Debug.Log("PL2の勝利です");
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
            Debug.Log("見切りに移行");
        }
    }
}
