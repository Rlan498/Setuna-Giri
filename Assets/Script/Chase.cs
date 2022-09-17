using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{
    public GameObject ATK;//�U����
    public GameObject DEF;//�����1���

    
    public static int chaseStart = 0;//1�ɂȂ�ƍU�h�J�n
    int time_start = 1;
    public static int win1 = 0;
    public static int win2 = 0;

    public GameObject Wall;//��

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
        Wall.SetActive(true);//�ǂ̋N��

        if (time_start == 0)
        {
            yield break;
        }
        time_start = 0;
        yield return new WaitForSeconds(1);//�@����I�u�W�F�N�g�o��

        if (Hayaoshi.ATK1 == 1)
        {
            ATK.transform.position = new Vector2(-2, 0);
            DEF.transform.position = new Vector2(2, 0);
        }

        if (Hayaoshi.ATK2 == 1)
        {
            ATK.transform.position = new Vector2(2, 0);
            DEF.transform.position = new Vector2(-2, 0);
        }

        ATK.SetActive(true);

        if(Hayaoshi.ATK1 == 1 && Hayaoshi.DEF2 == 2 || Hayaoshi.ATK2 == 1 && Hayaoshi.DEF1 == 2)
        {
            DEF.transform.localScale = new Vector2(4.0f, 4.0f);//2��ڂ̎�����̑傫��
        }

        if (Hayaoshi.ATK1 == 1 && Hayaoshi.DEF2 == 3 || Hayaoshi.ATK2 == 1 && Hayaoshi.DEF1 == 3)
        {
            DEF.transform.localScale = new Vector2(3.0f, 3.0f);//3��ڂ̎�����̑傫��
        }
        DEF.SetActive(true);

        Debug.Log("�X�^�[�g����1�b��");

        yield return new WaitForSeconds(2);//�A���ԍ�2�b�ōU�h�J�n
        chaseStart = 1;
        Debug.Log("�U�h�J�n�@���Ԃ�10�b");

        yield return new WaitForSeconds(10);//�B10�b�ōU�h�I��
        chaseStart = 0;
        Debug.Log("�U�h�I��");

        Vector2 ATK_position = GameObject.Find("attacker").transform.position;
        Vector2 DEF_position = GameObject.Find("defender").transform.position;

        float x = ATK_position.x - DEF_position.x;
        float y = ATK_position.y - DEF_position.y;

        Debug.Log("x = " + x + ", y = " + y);

        if (Hayaoshi.ATK1 == 1)
        {
            if (Hayaoshi.DEF2 == 1)
            {
                if ((x <= -2.75f || x >= 2.75f) || (y <= -2.75f || y >= 2.75f))
                {
                    win1 = 1;
                }
            }

            if (Hayaoshi.DEF2 == 2)
            {
                if ((x <= -2.25f || x >= 2.25f) || (y <= -2.25f || y >= 2.25f))
                {
                    win1 = 1;
                }
            }

            if (Hayaoshi.DEF2 == 3)
            {
                if ((x <= -1.75f || x >= 1.75f) || (y <= -1.75f || y >= 1.75f))
                {
                    win1 = 1;
                }
            }
        }

        if (Hayaoshi.ATK2 == 1)
        {
            if (Hayaoshi.DEF1 == 1)
            {
                if ((x <= -2.75f || x >= 2.75f) || (y <= -2.75f || y >= 2.75f))
                {
                    win2 = 1;
                }
            }

            if (Hayaoshi.DEF1 == 2)
            {
                if ((x <= -2.25f || x >= 2.25f) || (y <= -2.25f || y >= 2.25f))
                {
                    win2 = 1;
                }
            }

            if (Hayaoshi.DEF1 == 3)
            {
                if ((x <= -1.75f || x >= 1.75f) || (y <= -1.75f || y >= 1.75f))
                {
                    win2 = 1;
                }
            }
        }

        yield return new WaitForSeconds(3);//�C3�b�̌��ʊm�F�㔻��
        ATK.SetActive(false);
        DEF.SetActive(false);

        Wall.SetActive(false);

        //�����蔻��m�F����������
        if (win1 == 1 || win2 == 1 || Hayaoshi.DEF1 == 3 || Hayaoshi.DEF2 == 3)
        {
            Hayaoshi.fight = 4;
            x = 5;
            y = 5;
            Debug.Log("����");

            yield return new WaitForSeconds(1);//�D���U���g�̕\��(�ꏊ�͈ڂ�����)

            if (win1 == 1 || (win2 == 0 && Hayaoshi.DEF1 == 3))
            {
                Debug.Log("PL1�̏����ł�");
            }
            else if (win2 == 1 || (win1 == 0 && Hayaoshi.DEF2 == 3))
            {
                Debug.Log("PL2�̏����ł�");
            }
        }
        else
        {
            Hayaoshi.fight = 0;
            Debug.Log("���؂�Ɉڍs");
        }
    }
}
