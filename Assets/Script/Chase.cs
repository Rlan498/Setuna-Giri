using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{
    public GameObject ATK;//�U����
    public GameObject DEF;//�����1���

    
    public static int chaseStart = 0;//1�ɂȂ�ƍU�h�J�n
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
        yield return new WaitForSeconds(1);//�@����I�u�W�F�N�g�o��

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
            DEF.transform.localScale = new Vector3(2.0f, 2.0f, 1);//2��ڂ̎�����̑傫��
        }

        if (Hayaoshi.ATK1 == 1 && Hayaoshi.DEF2 == 3 || Hayaoshi.ATK2 == 1 && Hayaoshi.DEF1 == 3)
        {
            DEF.transform.localScale = new Vector3(1.5f, 1.5f, 1);//3��ڂ̎�����̑傫��
        }
        DEF.SetActive(true);

        Debug.Log("�X�^�[�g����1�b��");

        yield return new WaitForSeconds(1 + 2);//�A���ԍ�2�b�ōU�h�J�n
        chaseStart = 1;
        Debug.Log("�U�h�J�n�@���Ԃ�10�b");

        yield return new WaitForSeconds(1 + 2 + 10);//�B10�b�ōU�h�I��
        chaseStart = 0;
        Debug.Log("�X�^�[�g����13�b��");

        yield return new WaitForSeconds(1 + 2 + 10 + 1);//�C1�b�̌��ʊm�F�㔻��
        ATK.SetActive(false);
        DEF.SetActive(false);

        //�����蔻��m�F����������
        if (win1 != 0 && win2 != 0 || Hayaoshi.DEF1 == 3 || Hayaoshi.DEF2 == 3)
        {
            Hayaoshi.fight = 4;
            Debug.Log("����");
        }
        else
        {
            Hayaoshi.fight = 0;
            Debug.Log("���؂�Ɉڍs");
        }
    }
}
