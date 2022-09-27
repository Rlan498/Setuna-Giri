using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour
{
    public static int stop = 0;
    GameObject PL1;
    GameObject PL2;

    // Start is called before the first frame update
    void Start()
    {
        PL1 = GameObject.Find("PL1_WIN");//PL1にゲームオブジェクトPL1_WINを結び付け
        PL2 = GameObject.Find("PL2_WIN");
        PL1.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 255);//WINを透明化
        PL2.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 255);
        this.GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (stop == 0)
        {
            stop = 1;

            if (Chase.win1 == 1 || (Chase.win2 == 0 && Hayaoshi.DEF1 == 3) || Hayaoshi.pena2 == 3)
            {
                PL1.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 255);//WINを表示
            }
            else if (Chase.win2 == 1 || (Chase.win1 == 0 && Hayaoshi.DEF2 == 3) || Hayaoshi.pena1 == 3)
            {
                PL2.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 255);
            }
            else
            {
                Debug.Log("?????");//何かここを通ったとき用
            }
            for (int j = 0; j < Hayaoshi.i; j++)
            {
                if (Hayaoshi.fight_array[j] == "PL1 ペナルティ負け" || Hayaoshi.fight_array[j] == "PL2 ペナルティ負け")
                {
                    Debug.Log(j + 1 + "回目：" + Hayaoshi.fight_array[j]);
                }
                else
                {
                    Debug.Log(j + 1 + "回目：攻撃側　" + Hayaoshi.fight_array[j] + "　入力差　" + Hayaoshi.time_array[j] + "　" + Chase.attack_array[j]);
                }
            }

            StartCoroutine("wait");
        }
    }

    private IEnumerator wait()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Title");//タイトルに戻る
    }
}