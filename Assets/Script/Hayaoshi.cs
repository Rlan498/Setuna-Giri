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

    public GameObject Mark;
    int count = 0;//�����̊m�F�̂��߂̕ϐ��@��X�����ă��V
    bool pena = false;

    // Start is called before the first frame update
    void Start()
    {
        fight = 0;
        pena1 = 0;
        pena2 = 0;
        ATK1 = 0;
        ATK2 = 0;

        Mark = GameObject.Find("Mark");
        Debug.Log("��������������@PL1:S  PL2:��");
    }

    // Update is called once per frame
    void Update()
    {
        float rnd = Random.Range(4.0f, 10.0f);
        // �� 4.0�`10.0�͈̔͂Ń����_���ȏ����_���l���Ԃ�
        //�����̐��l�̕ύX�����؂�̕b���ύX
        

        if(Input.GetKey(KeyCode.S) && ready1 == 0)
        {
            ready1 = 1;
            //PL1����
            Debug.Log("PL1 ��������");
        }

        if (Input.GetKey(KeyCode.DownArrow) && ready2 == 0)
        {
            ready2 = 1;
            //PL2����
            Debug.Log("PL2 ��������");
        }

        if (reset >= 1)
        {
            CancelInvoke();
            Debug.Log("Invoke���~�܂�");
        }

        //�������猩�؂�̏���
        if ((ready1 == 1 && ready2 == 1 && ready == 0) || reset == 1)
        {
            if(pena == false)
            {
                pena = true;
                StartCoroutine(wait());//�{�ƂŌ������̃J�b�g�C���̖���
            }

            rnd1 = rnd;
            Debug.Log("�o���̏�������");
            Invoke("Exclamation", rnd1);
            //���ҏ�����������rnd�b���Exclamation�֐������s
            /*Debug.Log("�b����" + rnd1);*/
        }

        if (Input.GetKey(KeyCode.S) && ready == 1 && reset == 0)
        {
            if (fight == 0)
            {
                if (pena1 < 999)//�y�i���e�B�͂�����n+1���
                                //���͓��͎�t�ɊԂ��Ȃ����ߑ���
                {
                    StopAllCoroutines();
                    reset = 1;//�����l�ǉ��O��reset��1���� = �A���Ńy�i�΍�  ���؂�O��
                    pena1 = pena1 + 1;//PL1�ɔ����l��ǉ�
                    pena = false;//�������̘A���΍�̂���
                    Debug.Log("PL1 �t���C���O" + pena1 + "���");
                }
                else//�t���C���O�񐔂ł̃y�i���e�B�ݒ�
                {
                    reset = 2;
                    Debug.Log("PL1 �t���C���O1000��ځ@����");
                }
            }
            else
            {
                //�����ɓ��̓^�C���֌W�̋L�q�H

                if (ATK2 == 0)//PL2���܂������ĂȂ��Ƃ�
                {
                    ATK1 = 1;
                    DEF2 = DEF2 + 1;
                    //�U�h�V�[���ł�PL2�̎���񐔂��L�^
                    Debug.Log("PL1 ����");
                }
            }
        }

        if (Input.GetKey(KeyCode.DownArrow) && ready == 1 && reset == 0)
        {
            if (fight == 0)
            {
                if (pena2 < 999)//�y�i���e�B�͂�����n+1���
                                //���͓��͎�t�ɊԂ��Ȃ����ߑ���
                {
                    StopAllCoroutines();
                    reset = 1;//���؂�O��
                    pena2 = pena2 + 1;//PL2�ɔ����l��ǉ�
                    pena = false;
                    Debug.Log("PL2 �t���C���O" + pena2 + "���");
                }
                else//�t���C���O�񐔂ł̃y�i���e�B�ݒ�
                {
                    reset = 2;
                    Debug.Log("PL2 �t���C���O1000��ځ@����");
                }
            }
            else
            {
                //�����ɓ��̓^�C���֌W�̋L�q�H

                if (ATK1 == 0)//PL1���܂������ĂȂ��Ƃ�
                {
                    ATK2 = 1;
                    DEF1 = DEF1 + 1;
                    //PL1�̖h��񐔂��L�^
                    Debug.Log("PL2 ����");
                }
            }
        }
    }        

    private void Exclamation()
    {
        reset = 0;
        fight = 1;
        //�����Ɂu�I�v�摜�\���̋L�q
        Mark.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 255);
        Debug.Log("�I");
    }

    IEnumerator wait()
    {
        //���������̏����I�ȏꏊ
        yield return new WaitForSeconds(1.0f);
        count += 1;
        Debug.Log(count + "��ځA�\���I�I�I");
        ready = 1;
        reset = 0;
    }
}
