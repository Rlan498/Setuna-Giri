using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{
    public GameObject ATK;//攻撃側
    public GameObject DEF;//守備側1回目

    
    public static int chaseStart = 0;//1になると攻防開始
    int time_start = 1;
    int win1 = 0;
    int win2 = 0;

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
        if (time_start == 0)
        {
            yield break;
        }
        time_start = 0;
        yield return new WaitForSeconds(1);//①操作オブジェクト出現

        if (Hayaoshi.ATK1 == 1)
        {
            ATK.transform.position = new Vector3(-3, 0, 0);
            DEF.transform.position = new Vector3(3, 0, 0);
        }

        if (Hayaoshi.ATK2 == 1)
        {
            ATK.transform.position = new Vector3(3, 0, 0);
            DEF.transform.position = new Vector3(-3, 0, 0);
        }

        ATK.SetActive(true);

        if(Hayaoshi.ATK1 == 1 && Hayaoshi.DEF2 == 2 || Hayaoshi.ATK2 == 1 && Hayaoshi.DEF1 == 2)
        {
            DEF.transform.localScale = new Vector3(2.0f, 2.0f, 1);//2回目の守備側の大きさ
        }

        if (Hayaoshi.ATK1 == 1 && Hayaoshi.DEF2 == 3 || Hayaoshi.ATK2 == 1 && Hayaoshi.DEF1 == 3)
        {
            DEF.transform.localScale = new Vector3(1.5f, 1.5f, 1);//3回目の守備側の大きさ
        }
        DEF.SetActive(true);

        Debug.Log("スタートから1秒後");

        yield return new WaitForSeconds(1 + 2);//②時間差2秒で攻防開始
        chaseStart = 1;
        Debug.Log("攻防開始　時間は10秒");

        yield return new WaitForSeconds(1 + 2 + 10);//③10秒で攻防終了
        chaseStart = 0;
        Debug.Log("スタートから13秒後");

        yield return new WaitForSeconds(1 + 2 + 10 + 1);//④1秒の結果確認後判定
        ATK.SetActive(false);
        DEF.SetActive(false);

        //当たり判定確認→条件分岐
        if (win1 != 0 && win2 != 0 || Hayaoshi.DEF1 == 3 || Hayaoshi.DEF2 == 3)
        {
            Hayaoshi.fight = 4;
            Debug.Log("決着");
        }
        else
        {
            Hayaoshi.fight = 0;
            Debug.Log("見切りに移行");
        }
    }
}
