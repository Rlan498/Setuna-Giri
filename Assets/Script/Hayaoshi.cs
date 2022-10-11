using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

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
    public static int go = 0;

    GameObject Mark;
    GameObject PUSH;
    GameObject image1;
    GameObject image2;
    GameObject image2_2;
    GameObject pena_obj;
    GameObject back;
    GameObject pena_se;
    GameObject pena_fini;
    GameObject conte;
    public TextMeshProUGUI Mr_text;
    public TextMeshProUGUI PL1_p;
    public TextMeshProUGUI PL2_p;

    //リザルト用変数
    public static string[] fight_array = new string[5];//見切りの勝利者格納用配列
    public static float[] time_array = new float[5];//入力差の格納用配列
    public static int i = 0;//試合の度に+1(配列への勝敗入力用)

    public static int count = 0;//ただの確認のための変数　後々消してヨシ
    public static bool pena = false;
    public static float timer = 0;//入力差を格納する変数
    public static int time_start = 0;//1の時に入力タイミングの差を計測 2で終了する処理
    public static int push = 0;//pushの表示関係
    public static int drawsub = 0;


    // Start is called before the first frame update
    void Start()
    {
        Mark = GameObject.Find("Mark");
        PUSH = GameObject.Find("PUSH");
        image1 = GameObject.Find("image1");
        image2 = GameObject.Find("image2");
        image2_2 = GameObject.Find("image2_2");
        pena_obj = GameObject.Find("pena");
        back = GameObject.Find("back");
        pena_se = GameObject.Find("pena_se");
        pena_fini = GameObject.Find("pena_fini");
        conte = GameObject.Find("conte");
    }

    // Update is called once per frame
    void Update()
    {
        if (fight == 0 && rnd1 ==0)
        {
            stop = 0;//下のifの暴発阻止
        }

        if(fight == 0 && (DEF1 != 0 || DEF2 != 0) && stop == 0 && drawsub != 1)
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
            ATK1 = 0;
            ATK2 = 0;
            reset = 0;
            //count = 0;
            pena = false;
            timer = 0;
            time_start = 0;
            drawsub = 0;
            BGM.count = 0;
        }

        if (fight < 2)
        {
            if (fight == 0)
            {
                if (pena1 == 0)
                {
                    PL1_p.text = "PL1  ○○○";
                }
                else if (pena1 == 1)
                {
                    PL1_p.text = "PL1  ×○○";
                }
                else if (pena1 >= 2)
                {
                    PL1_p.text = "PL1  ××○";
                }

                if (pena2 == 0)
                {
                    PL2_p.text = "PL2  ○○○";
                }
                else if (pena2 == 1)
                {
                    PL2_p.text = "PL2  ×○○";
                }
                else if (pena2 >= 2)
                {
                    PL2_p.text = "PL2  ××○";
                }
            }

            float rnd = UnityEngine.Random.Range(4.0f, 10.0f);
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
                    StartCoroutine("wait_only");
                }
                if (go == 1)
                {
                    if (pena == false)
                    {
                        pena = true;
                        StartCoroutine(wait());//本家で言う所のカットインの役割
                    }
                
                    Invoke("Exclamation", rnd1);
                    //両者準備完了からrnd秒後にExclamation関数が実行
                    /*Debug.Log("秒数は" + rnd1);*/
                }
            }

            if (Input.GetKeyDown(KeyCode.S) && ready == 1 && reset == 0)
            {
                if (fight == 0)
                {
                    if (pena1 < 2)//ペナルティはここのn+1回目
                    {
                        StopAllCoroutines();
                        //reset = 1;//罰則値追加前にresetに1を代入 = 連続でペナ対策  見切り前へ
                        pena1 = pena1 + 1;//PL1に罰則値を追加
                        pena = false;//見合いの連続対策のため
                        Debug.Log(i + "　PL1 フライング" + pena1 + "回目");
                        StartCoroutine("pena_dis");
                    }
                    else//フライング回数でのペナルティ設定
                    {
                        reset = 2;
                        pena1 = pena1 + 1;
                        fight_array[i] = "PL1  ペナルティ負け";
                        Debug.Log("PL1 フライング3回目　負け");
                        i++;
                        StartCoroutine("pena_dis");
                    }
                }
                else
                {
                    //ここに入力タイム関係の記述？

                    if (ATK2 == 0 && ATK1 == 0)//PL2がまだ押してないとき かつ　PL1が押していなかったとき
                    {
                        ATK1 = 1;
                        time_start += 1;
                        
                    }
                    else if (ATK2 == 1)//PL2がすでに押しているとき　かつ　PL1が丁度押したとき
                    {
                        dif_calc();
                        if (drawsub != 1)
                        {
                            fight_array[i] = "PL2";
                            i += 1;
                            DEF1 += 1;
                            //PL1の防御回数を記録
                            Debug.Log(i + "　PL2 勝ち");
                            PL1_p.text = " ";//追いかけっこに移るためpenaを一時的に消去
                            PL2_p.text = " ";
                            //image2.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 255);
                            rnd1 = 0;
                            this.GetComponent<AudioSource>().Play();
                        }
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
                        //reset = 1;//見切り前へ
                        pena2 = pena2 + 1;//PL2に罰則値を追加
                        pena = false;
                        Debug.Log(i + "　PL2 フライング" + pena2 + "回目");
                        StartCoroutine("pena_dis");
                    }
                    else//フライング回数でのペナルティ設定
                    {
                        reset = 2;
                        pena2 = pena2 + 1;//ここが元々はpena1 = pena1 + 1;だった
                        fight_array[i] = "PL2  ペナルティ負け";
                        Debug.Log("PL2 フライング3回目　負け");
                        i++;
                        StartCoroutine("pena_dis");
                    }
                }
                else
                {
                    //ここに入力タイム関係の記述？

                    if (ATK1 == 0 && ATK2 == 0)//PL1がまだ押してないとき かつ　PL2が押していなかったとき
                    {
                        ATK2 = 1;
                        time_start += 1;
                        
                    }
                    else if (ATK1 == 1)//PL1がすでに押しているとき　かつ　PL2が丁度押したとき
                    {
                        dif_calc();
                        if (drawsub != 1)
                        {
                            fight_array[i] = "PL1";
                            i += 1;
                            DEF2 += 1;
                            //攻防シーンでのPL2の守備回数を記録
                            Debug.Log(i + "　PL1 勝ち");
                            PL1_p.text = " ";//追いかけっこに移るためpenaを一時的に消去
                            PL2_p.text = " ";

                            //image2_2.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 255);
                            rnd1 = 0;
                            this.GetComponent<AudioSource>().Play();
                        }
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
            drawsub = 0;
            //ここに「！」画像表示の記述
            Mark.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 255);
            Mark.GetComponent<AudioSource>().Play();
            Debug.Log("！");
        }
    }

    private void dif_calc()
    {
        time_start -= 1;
        
        timer = (float)Math.Round(timer, 3, MidpointRounding.AwayFromZero);
        
        Debug.Log("入力タイミングの差は" + timer + "でした");

        Mark.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 255);

        if (timer <= 0.016)
        {
            drawsub = 1;
            StartCoroutine("draw");
        }

        if (drawsub != 1)
        {
            time_array[i] = timer;
            fight += 1;//この処理でfightは2のはず
            image1.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 255);//image1 消去
        }
    }

    private IEnumerator wait_only()
    {
        yield return new WaitForSeconds(3.5f);
        go = 1;
    }
    private IEnumerator wait()
    {
        //お見合いの処理的な場所
        yield return new WaitForSeconds(0.1f);
        count += 1;
        Debug.Log(i + "　" + count + "回目、構え！！！");
        Mr_text.text = count + "合目";//TextMeshProで何回目の攻撃か表示
        pena_obj.GetComponent<AudioSource>().Play();
        ready = 1;
        reset = 0;
        yield return new WaitForSeconds(2.0f);
        Mr_text.text = " ";
    }

    private IEnumerator wait_result()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Result");//リザルト画面へ
    }

    private IEnumerator pena_dis()
    {
        reset = -1;//処理中に連続ペナが引き起るのを防ぐための処置
        CancelInvoke();//何故か！が処理中に出たので、それを防ぐための処理
        if(pena1 >= 3 || pena2 >= 3)
        {
            BGM.count = -1;
            Mr_text.text = " ";
            pena_se.GetComponent<AudioSource>().Play();
            if(pena1 >= 3)
            {
                PL1_p.text = "PL1  ×××";
            }
            if(pena2 >= 3)
            {
                PL2_p.text = "PL1  ×××";
            }
            pena_fini.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 255);//お手付きを表示
            yield return new WaitForSeconds(3);
            StartCoroutine("wait_result");
        }
        else
        {
            BGM.count = 0;
            back.GetComponent<AudioSource>().Play();
            Mr_text.text = " ";//n goume が表示されていた場合、それを消去するためのもの

            //penaが発生した場合に、その数を表示する処理        
            //PL1_p.text = "PL1 pena = " + pena1;
            if (pena1 == 1)
            {
                PL1_p.text = "PL1  ×○○";
            }
            if (pena1 >= 2)
            {
                PL1_p.text = "PL1  ××○";
            }

            //PL2_p.text = "PL2 pena = " + pena2;
            if (pena2 == 1)
            {
                PL2_p.text = "PL2  ×○○";
            }
            if (pena2 >= 2)
            {
                PL2_p.text = "PL2  ××○";
            }

            pena_obj.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 255);//お手付きを表示
            yield return new WaitForSeconds(3);
            pena_obj.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 255);//お手付きを消去
            reset = 1;
        }
    }

    private IEnumerator draw()
    {
        CancelInvoke();
        reset = -1;//何故か仕切りなおしてくれないのでその対策として
        //引き分けの表示
        conte.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 255);

        yield return new WaitForSeconds(3);
        conte.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 255);
        fight = 0;
        ATK1 = 0;
        ATK2 = 0;
        reset = 1;
        timer = 0;
        pena = false;
    }
}
