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
            for (int j = 0; j < Hayaoshi.i; j++)
            {
                if (Hayaoshi.fight_array[j] == "PL1 �y�i���e�B����" || Hayaoshi.fight_array[j] == "PL2 �y�i���e�B����")
                {
                    Debug.Log(j + 1 + "��ځF" + Hayaoshi.fight_array[j]);
                }
                else
                {
                    Debug.Log(j + 1 + "��ځF�U�����@" + Hayaoshi.fight_array[j] + "�@���͍��@" + Hayaoshi.time_array[j] + "�@" + Chase.attack_array[j]);
                }
            }

            StartCoroutine("wait");
        }
    }

    private IEnumerator wait()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Title");//�^�C�g���ɖ߂�
    }
}