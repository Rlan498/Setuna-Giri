using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hayaoshi : MonoBehaviour
{
    float rnd1 = 0.0f;
    public static int ready1 = 0;
    public static int ready2 = 0;
    public static int ready = 0;
    public static int fight = 0;
    public static int pena1 = 0;
    public static int pena2 = 0;
    public static int DEF1 = 0;
    public static int DEF2 = 0;
    public static int ATK1 = 0;
    public static int ATK2 = 0;
    public static int reset = 0;
    public static int stop = 0;

    GameObject Mark;
    GameObject PUSH;
    GameObject image1;
    GameObject image2;
    GameObject image2_2;

    //リザルト用変数
    public static string[] fight_array = new string[5];//見切りの勝利者格納用配列
    public static float[] time_array = new float[5];//入力差の格納用配列
    public static int i = 0;//試合の度に+1(配列への勝敗入力用)
    
    int count = 0;//ただの確認のための変数　後々消してヨシ
    public static bool pena = false;
    public static float timer = 0;//入力差を格納する変数
    public static int time_start = 0;//1の時に入力タイミングの差を計測 2で終了する処理
    public static int push = 0;//pushの表示関係



    // Start is called before the first frame update
    void Start()
    {
        Mark = GameObject.Find("Mark");
        PUSH = GameObject.Find("PUSH");
        image1 = GameObject.Find("image1");
        image2 = GameObject.Find("image2");
        image2_2 = GameObject.Find("image2_2");
    }

    // Update is called once per frame
    void Update()
    {
        if (fight == 0 && rnd1 ==0)
        {
            stop = 0;//下のifの暴発阻止
        }

        if(fight == 0 && (DEF1 != 0 || DEF2 != 0) && stop == 0)
        {
            stop = 1;

            if (ATK1 == 1)
            {
                image2.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 255);
            }
            if (ATK2 == 1)
            {
                image2_2.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 255);
            }
            image1.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 255);

            //見切り開始時にリセットすべき変数
            ready1 = 1;
            ready2 = 1;
            ready = 0;
            fight = 0;
            pena1 = 0;//ペナルティはリセットすべきか要相談？
            pena2 = 0;//毎回3回まで or 通算5回(仮)まで？
            ATK1 = 0;
            ATK2 = 0;
            reset = 0;
            count = 0;
            pena = false;
            timer = 0;
            time_start = 0;
        }

        if (fight < 2)
        {
            float rnd = Random.Range(4.0f, 10.0f);
            // ※ 4.0～10.0の範囲でランダムな小数点数値が返る
            //ここの数値の変更→見切りの秒数変更

            rnd1 = rnd;
            if (Input.GetKeyDown(KeyCode.S) && ready1 == 0)
            {
                ready1 = 1;
                //PL1準備
                Debug.Log("PL1 準備完了");
            }

            if (Input.GetKeyDown(KeyCode.DownArrow) && ready2 == 0)
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

            //ここから見切りの処理
            if ((ready1 == 1 && ready2 == 1 && ready == 0) || reset == 1)
            {
                if(push == 0)
                {
                    PUSH.GetComponent<AudioSource>().Play();
                    PUSH.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 255);
                    push = 1;
                }
                if (pena == false)
                {
                    pena = true;
                    StartCoroutine(wait());//本家で言う所のカットインの役割
                }

                Debug.Log("双方の準備完了");
                Invoke("Exclamation", rnd1);
                //両者準備完了からrnd秒後にExclamation関数が実行
                /*Debug.Log("秒数は" + rnd1);*/
            }

            if (Input.GetKeyDown(KeyCode.S) && ready == 1 && reset == 0)
            {
                if (fight == 0)
                {
                    if (pena1 < 2)//ペナルティはここのn+1回目
                    {
                        StopAllCoroutines();
                        reset = 1;//罰則値追加前にresetに1を代入 = 連続でペナ対策  見切り前へ
                        pena1 = pena1 + 1;//PL1に罰則値を追加
                        pena = false;//見合いの連続対策のため
                        Debug.Log(i + "　PL1 フライング" + pena1 + "回目");
                    }
                    else//フライング回数でのペナルティ設定
                    {
                        reset = 2;
                        pena1 = pena1 + 1;
                        fight_array[i] = "PL1 ペナルティ負け";
                        Debug.Log("PL1 フライング3回目　負け");
                        i++;
                        StartCoroutine("wait_result");
                    }
                }
                else
                {
                    //ここに入力タイム関係の記述？

                    if (ATK2 == 0 && ATK1 == 0)//PL2がまだ押してないとき かつ　PL1が押していなかったとき
                    {
                        ATK1 = 1;
                        time_start += 1;
                        DEF2 += 1;
                        //攻防シーンでのPL2の守備回数を記録
                        fight_array[i] = "PL1";
                        Debug.Log(i + "　PL1 勝ち");
                    }
                    else if (ATK2 == 1)//PL2がすでに押しているとき　かつ　PL1が丁度押したとき
                    {
                        dif_calc();
                        //image2.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 255);
                        rnd1 = 0;
                        this.GetComponent<AudioSource>().Play();
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.DownArrow) && ready == 1 && reset == 0)
            {
                if (fight == 0)
                {
                    if (pena2 < 2)//ペナルティはここのn+1回目
                    {
                        StopAllCoroutines();
                        reset = 1;//見切り前へ
                        pena2 = pena2 + 1;//PL2に罰則値を追加
                        pena = false;
                        Debug.Log(i + "　PL2 フライング" + pena2 + "回目");
                    }
                    else//フライング回数でのペナルティ設定
                    {
                        reset = 2;
                        pena2 = pena2 + 1;//ここが元々はpena1 = pena1 + 1;だった
                        fight_array[i] = "PL2 ペナルティ負け";
                        Debug.Log("PL2 フライング3回目　負け");
                        i++;
                        StartCoroutine("wait_result");
                    }
                }
                else
                {
                    //ここに入力タイム関係の記述？

                    if (ATK1 == 0 && ATK2 == 0)//PL1がまだ押してないとき かつ　PL2が押していなかったとき
                    {
                        ATK2 = 1;
                        time_start += 1;
                        DEF1 += 1;
                        //PL1の防御回数を記録

                        fight_array[i] = "PL2";
                        Debug.Log(i + "　PL2 勝ち");
                    }
                    else if (ATK1 == 1)//PL1がすでに押しているとき　かつ　PL2が丁度押したとき
                    {
                        dif_calc();
                        //image2_2.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 255);
                        rnd1 = 0;
                        this.GetComponent<AudioSource>().Play();
                    }
                }
            }

            if (time_start == 1)//差の計算
            {
                timer += Time.deltaTime;
            }
        }
    }        

    private void Exclamation()
    {
        if(fight == 0)
        {
            reset = 0;
            fight = 1;
            //ここに「！」画像表示の記述
            Mark.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 255);
            Mark.GetComponent<AudioSource>().Play();
            Debug.Log("！");
        }
    }

    private void dif_calc()
    {
        time_start += 1;
        fight += 1;//この処理でfightは2のはず

        time_array[i] = timer;
        i += 1;
        Debug.Log("入力タイミングの差は" + timer + "でした");
        Mark.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 255);
        image1.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 255);//image1 消去
    }

    IEnumerator wait()
    {
        //お見合いの処理的な場所
        yield return new WaitForSeconds(1.0f);
        count += 1;
        Debug.Log(i + "　" + count + "回目、構え！！！");
        ready = 1;
        reset = 0;
    }

    private IEnumerator wait_result()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Result");//リザルト画面へ
    }
}
