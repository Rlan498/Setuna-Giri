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
    GameObject fa;
    GameObject y_d;
    GameObject y_a;
    GameObject black_back;
    GameObject white_back;
    GameObject f_b;
    GameObject conclusion;
    GameObject main_camera;

    public static string[] attack_array = new string[5];

    // Start is called before the first frame update
    void Start()
    {
        ATK.SetActive(false);
        DEF.SetActive(false);
        fa = GameObject.Find("failed_attack");
        y_d = GameObject.Find("you_def");
        y_a = GameObject.Find("you_atk");
        black_back = GameObject.Find("Black_back");
        f_b = GameObject.Find("finish_black");
        conclusion = GameObject.Find("conclusion");
        white_back = GameObject.Find("White_back");
        main_camera = GameObject.Find("Main Camera");
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
                Debug.Log("steream起動");
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
            y_d.transform.position = new Vector2(7, 0);
            y_a.transform.position = new Vector2(-7, 0);
            y_d.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 255);
            y_a.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 255);
        }

        if (Hayaoshi.ATK2 == 1)
        {
            ATK.transform.position = new Vector2(2, 0);
            DEF.transform.position = new Vector2(-2, 0);
            y_d.transform.position = new Vector2(-7, 0);
            y_a.transform.position = new Vector2(7, 0);
            y_d.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 255);
            y_a.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 255);
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
        y_d.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 255);
        y_a.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 255);

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

        if (win1 == 1 || win2 == 1)
        {

            attack_array[Hayaoshi.i-1] = "攻撃成功";
        }
        else
        {

            attack_array[Hayaoshi.i-1] = "攻撃失敗";
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
            yield return Settlement();//決着演出からシーン移行まで
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

    IEnumerator Settlement()//決着の演出を処理する部分
    {
        for (float i = 0; i < 20; i++) 
        {
            black_back.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 0.05f);
            Debug.Log("暗転通過");
            yield return new WaitForSeconds(0.017f);
        }
        yield return new WaitForSeconds(2);
        if (win1 == 1 || (win2 == 0 && Hayaoshi.DEF1 == 3))//PL1勝利
        {
            f_b.transform.Rotate(new Vector3(0, 180, 0));
            f_b.transform.position = new Vector2(6, -2.5f);
            f_b.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 1);
        }
        else if (win2 == 1 || (win1 == 0 && Hayaoshi.DEF2 == 3))//PL2勝利
        {
            f_b.transform.Rotate(new Vector3(0, 0, 0));
            f_b.transform.position = new Vector2(-6, -2.5f);
            f_b.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 1);
        }

        conclusion.GetComponent<AudioSource>().Play();
        white_back.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 1);
        yield return new WaitForSeconds(0.025f);
        white_back.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 1);

        conclusion.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 1);
        Debug.Log("決着");

        yield return new WaitForSeconds(2);//�Dリザルトの表示(場所は移すかも)

        for (float i = 0; i < 20; i++)
        {
            conclusion.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 0.05f);
            f_b.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 0.05f);
            Debug.Log("文字とか暗転");
            yield return new WaitForSeconds(0.034f);
        }
        black_back.GetComponent<AudioSource>().Play();


        yield return new WaitForSeconds(1);

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
}
