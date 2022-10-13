using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Result : MonoBehaviour
{
    public static int stop = 0;
    GameObject PL1;
    GameObject PL2;
    int push;
    public TextMeshProUGUI result;
    public TextMeshProUGUI result_cause;

    // Start is called before the first frame update
    void Start()
    {
        push = 0;
        PL1 = GameObject.Find("PL1_WIN");//PL1�ɃQ�[���I�u�W�F�N�gPL1_WIN�����ѕt��
        PL2 = GameObject.Find("PL2_WIN");
        PL1.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 255);//WIN�𓧖���
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
                PL1.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 255);//WIN��\��
            }
            else if (Chase.win2 == 1 || (Chase.win1 == 0 && Hayaoshi.DEF2 == 3) || Hayaoshi.pena1 == 3)
            {
                PL2.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 255);
            }
            else
            {
                Debug.Log("?????");//����������ʂ����Ƃ��p
            }

            result_cause.text = "�����̕������";

            if (Chase.win1 == 1 || Chase.win2 == 1)
            {
                result_cause.text = result_cause.text + "�@�s���ꑾ��";
            }
            else if (Hayaoshi.DEF1 == 3 || Hayaoshi.DEF2 == 3)
            {
                result_cause.text = result_cause.text + "�@�Ԃ��a��";
            }
            else if (Hayaoshi.pena1 == 3 || Hayaoshi.pena2 == 3)
            {
                result_cause.text = result_cause.text + "�@�W���s��";
            }
            else
            {
                result_cause.text = result_cause.text + "�@?????";
            }

            for (int j = 0; j < Hayaoshi.i; j++)
            {
                if (Hayaoshi.fight_array[j] == "PL1  �y�i���e�B����" || Hayaoshi.fight_array[j] == "PL2  �y�i���e�B����")
                {
                    result.text = result.text + (j + 1) + "��ځF" + Hayaoshi.fight_array[j] + "\n";
                }
                else
                {
                    result.text = result.text + (j + 1) + "��ځF�U�����@" + Hayaoshi.fight_array[j] + "�@���͍��@" + Hayaoshi.time_array[j] + "�b�@" + Chase.attack_array[j] + "\n";
                }
            }

            StartCoroutine("wait");
        }
        if(push == 1)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("Title");
            }
        }
    }

    private IEnumerator wait()
    {
        yield return new WaitForSeconds(1);
        push = 1;
    }
}