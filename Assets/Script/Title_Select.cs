using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title_Select : MonoBehaviour
{
    public static int place = 0;

    GameObject Back;
    GameObject Title;
    GameObject start;
    GameObject Exp;
    GameObject Exit;
    GameObject Arrow;
    GameObject bgm_2;
    public GameObject exp1_1;
    public GameObject exp1_2;
    public GameObject exp1_3;
    public GameObject exp2_1;
    public GameObject exp2_2;
    public GameObject exp2_3;
    public GameObject exp2_4;
    public GameObject pointer;
    public GameObject exp_dis;

    int push = 0;
    int place2 = 0;
    bool exp_open = false;

    // Start is called before the first frame update
    void Start()
    {
        place = 0;
        Back = GameObject.Find("Back");
        Title = GameObject.Find("Title");
        start = GameObject.Find("Start");
        Exp = GameObject.Find("Exp");
        Exit = GameObject.Find("Exit");
        Arrow = GameObject.Find("Arrow");
        bgm_2 = GameObject.Find("BGM_2");
    }

    // Update is called once per frame
    void Update()
    {
        if(exp_open == false)
        {
            if (push != 1)
            {
                if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))//選択肢　下に
                {
                    place += 1;
                }
                else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))//選択肢　上に
                {
                    place -= 1;
                }
                else if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
                {//決定が押された時の処理
                    if (place == 0)//始めるを選択
                    {
                        push = 1;
                        this.GetComponent<AudioSource>().Play();
                        StartCoroutine("wait");
                    }
                    else if (place == 1)//操作説明を選択
                    {
                        push = 1;
                        exp_open = true;
                        this.GetComponent<AudioSource>().Play();
                        exp1_1.SetActive(true);
                        exp1_2.SetActive(false);
                        exp1_3.SetActive(false);
                        exp2_1.SetActive(false);
                        exp2_2.SetActive(false);
                        exp2_3.SetActive(false);
                        exp2_4.SetActive(false);
                        exp_dis.SetActive(true);
                    }
                    else if (place == 2)//ゲーム終了を選択
                    {
                        //UnityEditor.EditorApplication.isPlaying = false;
                        Application.Quit();
                    }
                }

                if (place > 2)//下から上に　上から下に行くための処理
                {
                    place = 0;
                }
                else if (place < 0)
                {
                    place = 2;
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))//選択肢　右に
            {
                place2 += 1;
                StartCoroutine("exp_disp");
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))//選択肢　左に
            {
                place2 -= 1;
                StartCoroutine("exp_disp");
            }
            else if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
            {
                exp_dis.SetActive(false);
                exp1_1.SetActive(true);
                exp1_2.SetActive(false);
                exp1_3.SetActive(false);
                exp2_1.SetActive(false);
                exp2_2.SetActive(false);
                exp2_3.SetActive(false);
                exp2_4.SetActive(false);
                pointer.transform.position = new Vector2(-3, -4.5f);
                place2 = 0;
                push = 0;
                exp_open = false;
            }  
        }
        
    }

    private IEnumerator wait()
    {
        yield return new WaitForSeconds(1);

        for (float i = 0; i < 1; i += 0.1f)//Titleをフェードアウト
        {
            Debug.Log("タイトル消す奴通過");
            Title.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 0.1f);
            start.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 0.1f);
            Exp.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 0.1f);
            Exit.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 0.1f);
            Arrow.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 0.1f);
            yield return new WaitForSeconds(0.1f);
        }

        for(float i = 0.3f; i >= 0; i -= 0.1f)
        {
            bgm_2.GetComponent<AudioSource>().volume = i;
            yield return new WaitForSeconds(0.1f);
        }
        bgm_2.GetComponent<AudioSource>().volume = 0;

        Back.transform.localPosition = new Vector2(0, 0);

        for(float i = 10;i>=0.8f;i -= 0.1f)
        {
            Debug.Log("縮小通過");
            Back.transform.localScale = new Vector2(i, i);
            yield return new WaitForSeconds(0.001f);
        }

        Back.transform.localScale = new Vector2(0.8f, 0.8f);

        yield return new WaitForSeconds(0.1f);
        //ここからSampleSceneのための処理
        Hayaoshi.fight = 0;
        Hayaoshi.ready1 = 0;
        Hayaoshi.ready2 = 0;
        Hayaoshi.ready = 0;
        Hayaoshi.pena1 = 0;
        Hayaoshi.pena2 = 0;
        Hayaoshi.reset = 0;
        Hayaoshi.DEF1 = 0;
        Hayaoshi.DEF2 = 0;
        Hayaoshi.ATK1 = 0;
        Hayaoshi.ATK2 = 0;
        Hayaoshi.stop = 0;
        Hayaoshi.pena = false;
        Hayaoshi.push = 0;
        Hayaoshi.time_start = 0;
        Hayaoshi.count = 0;
        Hayaoshi.go = 0;
        Hayaoshi.drawsub = 0;

        Chase.time_start = 1;
        Chase.win1 = 0;
        Chase.win2 = 0;
        
        Array.Clear(Hayaoshi.fight_array, 0, Hayaoshi.fight_array.Length);
        Array.Clear(Hayaoshi.time_array, 0, Hayaoshi.time_array.Length);
        Array.Clear(Chase.attack_array, 0, Chase.attack_array.Length);
        Hayaoshi.i = 0;

        Result.stop = 0;

        SceneManager.LoadScene("SampleScene");
    }

    private IEnumerator exp_disp()
    {
        exp1_1.SetActive(false);
        exp1_2.SetActive(false);
        exp1_3.SetActive(false);
        exp2_1.SetActive(false);
        exp2_2.SetActive(false);
        exp2_3.SetActive(false);
        exp2_4.SetActive(false);

        if (place2 > 6)//右から左に　左から右に行くための処理
        {
            place2 = 0;
        }
        else if (place2 < 0)
        {
            place2 = 6;
        }

        if (place2 == 0)
        {
            yield return new WaitForSeconds(0.001f);
            exp1_1.SetActive(true);//1-1表示

            pointer.transform.position = new Vector2(-3, -4.5f);
            Debug.Log("1番目表示");
        }
        else if (place2 == 1)
        {
            yield return new WaitForSeconds(0.001f);
            exp1_2.SetActive(true);//1-2表示

            pointer.transform.position = new Vector2(-2, -4.5f);
            Debug.Log("2番目表示");
        }
        else if (place2 == 2)
        {
            yield return new WaitForSeconds(0.001f);
            exp1_3.SetActive(true);//1-3表示

            pointer.transform.position = new Vector2(-1, -4.5f);
            Debug.Log("3番目表示");
        }
        else if (place2 == 3)
        {
            yield return new WaitForSeconds(0.001f);
            exp2_1.SetActive(true);//2-1表示

            pointer.transform.position = new Vector2(0, -4.5f);
            Debug.Log("4番目表示");
        }
        else if (place2 == 4)
        {
            yield return new WaitForSeconds(0.001f);
            exp2_2.SetActive(true);//2-2表示

            pointer.transform.position = new Vector2(1, -4.5f);
            Debug.Log("5番目表示");
        }
        else if (place2 == 5)
        {
            yield return new WaitForSeconds(0.001f);
            exp2_3.SetActive(true);//2-3表示

            pointer.transform.position = new Vector2(2, -4.5f);
            Debug.Log("6番目表示");
        }
        else if (place2 == 6)
        {
            yield return new WaitForSeconds(0.001f);
            exp2_4.SetActive(true);//2-4表示

            pointer.transform.position = new Vector2(3, -4.5f);
            Debug.Log("7番目表示");
        }
    }
}
