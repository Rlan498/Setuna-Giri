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

    int push = 0;

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
        if(push != 1)
        {
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))//�I�����@����
            {
                place += 1;
            }
            else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))//�I�����@���
            {
                place -= 1;
            }
            else if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
            {//���肪�����ꂽ���̏���
                if (place == 0)//�n�߂��I��
                {
                    push = 1;
                    this.GetComponent<AudioSource>().Play();
                    StartCoroutine("wait");
                }
                else if (place == 1)//���������I��
                {
                    push = 1;
                    this.GetComponent<AudioSource>().Play();
                }
                else if (place == 2)//�Q�[���I����I��
                {
                    //UnityEditor.EditorApplication.isPlaying = false;
                    Application.Quit();
                }
            }

            if (place > 2)//�������Ɂ@�ォ�牺�ɍs�����߂̏���
            {
                place = 0;
            }
            else if (place < 0)
            {
                place = 2;
            }
        }
    }

    private IEnumerator wait()
    {
        yield return new WaitForSeconds(1);

        for (float i = 0; i < 1; i += 0.1f)//Title���t�F�[�h�A�E�g
        {
            Debug.Log("�^�C�g�������z�ʉ�");
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
            Debug.Log("�k���ʉ�");
            Back.transform.localScale = new Vector2(i, i);
            yield return new WaitForSeconds(0.001f);
        }

        Back.transform.localScale = new Vector2(0.8f, 0.8f);

        yield return new WaitForSeconds(0.1f);
        //��������SampleScene�̂��߂̏���
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
}
