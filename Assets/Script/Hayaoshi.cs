using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hayaoshi : MonoBehaviour
{
    float rnd1 = 0.0f;
    int ready1 = 0;
    int ready2 = 0;
    int ready = 0;
    int fight = 0;
    int pena1 = 0;
    int pena2 = 0;
    int DEF1 = 0;
    int DEF2 = 0;
    int ATK1 = 0;
    int ATK2 = 0;
    int reset = 0;

    // Start is called before the first frame update
    void Start()
    {
        fight = 0;
        pena1 = 0;
        pena2 = 0;
        ATK1 = 0;
        ATK2 = 0;
        Debug.Log("準備完了したら　PL1:S  PL2:↓");
    }

    // Update is called once per frame
    void Update()
    {
        float rnd = Random.Range(4.0f, 10.0f);
        // ※ 4.0～10.0の範囲でランダムな小数点数値が返る
        //ここの数値の変更→見切りの秒数変更
        

        if(Input.GetKey(KeyCode.S) && ready1 == 0)
        {
            ready1 = 1;
            //PL1準備
            Debug.Log("PL1 準備完了");
        }

        if (Input.GetKey(KeyCode.DownArrow) && ready2 == 0)
        {
            ready2 = 1;
            //PL2準備
            Debug.Log("PL2 準備完了");
        }

        if (reset >= 1)
        {
            CancelInvoke();
            Debug.Log("Invokeが止まる");
        }

        if ((ready1 == 1 && ready2 == 1 && ready == 0) || reset == 1)
        {
            reset = 0;
            ready = 1;
            rnd1 = rnd;
            Debug.Log("双方の準備完了");
            Invoke("Exclamation", rnd1);
            //両者準備完了からrnd秒後にExclamation関数が実行
            /*Debug.Log("秒数は" + rnd1);*/
        }
            
            if (Input.GetKey(KeyCode.S) && ready == 1 && reset == 0)
        {
            if(fight == 0)
            {
                if (pena1 < 999)//ペナルティはここのn+1回目
                    　　　　　//今は入力受付に間がないため多め
                {
                    pena1 = pena1 + 1;
                    //PL1に罰則値を追加
                    reset = 1;
                    //見切り前へ
                    Debug.Log("PL1 フライング" + pena1 + "回目");
                }
                else//フライング回数でのペナルティ設定
                {
                    reset = 2;
                    Debug.Log("PL1 フライング1000回目　負け");
                }
            }
            else
            {
                //ここに入力タイム関係の記述？

                if (ATK2 == 0)//PL2がまだ押してないとき
                {
                    ATK1 = 1;
                    DEF2 = DEF2 + 1;
                    //攻防シーンでのPL2の守備回数を記録
                    Debug.Log("PL1 勝ち");
                }
            }
        }

        if (Input.GetKey(KeyCode.DownArrow) && ready == 1 && reset == 0)
        {
            if(fight == 0)
            {
                if (pena2 < 999)//ペナルティはここのn+1回目
                              //今は入力受付に間がないため多め
                {
                    pena2 = pena2 + 1;
                    //PL2に罰則値を追加
                    reset = 1;
                    //見切り前へ
                    Debug.Log("PL2 フライング" + pena2 + "回目");
                }
                else//フライング回数でのペナルティ設定
                {
                    reset = 2;
                    Debug.Log("PL2 フライング1000回目　負け");
                }
            }
            else
            {
                //ここに入力タイム関係の記述？

                if (ATK1 == 0)//PL1がまだ押してないとき
                {
                    ATK2 = 1;
                    DEF1 = DEF1 + 1;
                    //PL1の防御回数を記録
                    Debug.Log("PL2 勝ち");
                }
                else
                {

                }
            }
        }
    }

    private void Exclamation()
    {
        fight = 1;
        //ここに「！」画像表示の記述
        Debug.Log("！");
    }
}
