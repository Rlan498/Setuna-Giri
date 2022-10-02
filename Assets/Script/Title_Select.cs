using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title_Select : MonoBehaviour
{
    public static int place = 0;

    // Start is called before the first frame update
    void Start()
    {
        place = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))//�I�����@����
        {
            place += 1;
        } else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))//�I�����@���
        {
            place -= 1;
        } else if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return)){//���肪�����ꂽ���̏���
            if(place == 0)//�n�߂��I��
            {
                this.GetComponent<AudioSource>().Play();
                StartCoroutine("wait");
            }
            else if(place == 1)//���������I��
            {
                this.GetComponent<AudioSource>().Play();

            }
            else if(place == 2)//�Q�[���I����I��
            {
                //UnityEditor.EditorApplication.isPlaying = false;
                Application.Quit();
            }
        }

        if (place > 2)//�������Ɂ@�ォ�牺�ɍs�����߂̏���
        {
            place = 0;
        }else if(place < 0)
        {
            place = 2;
        }
    }

    private IEnumerator wait()
    {
        yield return new WaitForSeconds(1);

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
