using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hayaoshi : MonoBehaviour
{
    float rnd1 = 0.0f;
    int ready1 = 0;
    int ready2 = 0;
    int ready = 0;
    public static int fight = 0;
    int pena1 = 0;
    int pena2 = 0;
    public static int DEF1 = 0;
    public static int DEF2 = 0;
    public static int ATK1 = 0;
    public static int ATK2 = 0;
    int reset = 0;
    int stop = 0;

    GameObject Mark;
    GameObject image1;
    GameObject image2;
    GameObject image2_2;
    
    int count = 0;//�����̊m�F�̂��߂̕ϐ��@��X�����ă��V
    bool pena = false;
    public static float timer = 0;//���͍����i�[����ϐ�
    int time_start = 0;//1�̎��ɓ��̓^�C�~���O�̍����v�� 2�ŏI�����鏈��

    // Start is called before the first frame update
    void Start()
    {
        Mark = GameObject.Find("Mark");
        image1 = GameObject.Find("image1");
        image2 = GameObject.Find("image2");
        image2_2 = GameObject.Find("image2_2");
        Debug.Log("��������������@PL1:S  PL2:��");
    }

    // Update is called once per frame
    void Update()
    {
        if (fight == 0 && rnd1 ==0)
        {
            stop = 0;//����if�̖\���j�~
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

            //���؂�J�n���Ƀ��Z�b�g���ׂ��ϐ�
            ready1 = 1;
            ready2 = 1;
            ready = 0;
            fight = 0;
            pena1 = 0;//�y�i���e�B�̓��Z�b�g���ׂ����v���k�H
            pena2 = 0;//����3��܂� or �ʎZ5��(��)�܂ŁH
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
            // �� 4.0�`10.0�͈̔͂Ń����_���ȏ����_���l���Ԃ�
            //�����̐��l�̕ύX�����؂�̕b���ύX

            rnd1 = rnd;

            if (Input.GetKey(KeyCode.S) && ready1 == 0)
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
                if (pena == false)
                {
                    pena = true;
                    StartCoroutine(wait());//�{�ƂŌ������̃J�b�g�C���̖���
                }

                Debug.Log("�o���̏�������");
                Invoke("Exclamation", rnd1);
                //���ҏ�����������rnd�b���Exclamation�֐������s
                /*Debug.Log("�b����" + rnd1);*/
            }

            if (Input.GetKey(KeyCode.S) && ready == 1 && reset == 0)
            {
                if (fight == 0)
                {
                    if (pena1 < 2)//�y�i���e�B�͂�����n+1���
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
                        Debug.Log("PL1 �t���C���O3��ځ@����");
                    }
                }
                else
                {
                    //�����ɓ��̓^�C���֌W�̋L�q�H

                    if (ATK2 == 0 && ATK1 == 0)//PL2���܂������ĂȂ��Ƃ� ���@PL1�������Ă��Ȃ������Ƃ�
                    {
                        ATK1 = 1;
                        time_start += 1;
                        DEF2 += 1;
                        //�U�h�V�[���ł�PL2�̎���񐔂��L�^
                        Debug.Log("PL1 ����");
                    }
                    else if (ATK2 == 1)//PL2�����łɉ����Ă���Ƃ��@���@PL1�����x�������Ƃ�
                    {
                        dif_calc();
                        //image2.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 255);
                        rnd1 = 0;
                    }
                }
            }

            if (Input.GetKey(KeyCode.DownArrow) && ready == 1 && reset == 0)
            {
                if (fight == 0)
                {
                    if (pena2 < 2)//�y�i���e�B�͂�����n+1���
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
                        Debug.Log("PL2 �t���C���O3��ځ@����");
                    }
                }
                else
                {
                    //�����ɓ��̓^�C���֌W�̋L�q�H

                    if (ATK1 == 0 && ATK2 == 0)//PL1���܂������ĂȂ��Ƃ� ���@PL2�������Ă��Ȃ������Ƃ�
                    {
                        ATK2 = 1;
                        time_start += 1;
                        DEF1 += 1;
                        //PL1�̖h��񐔂��L�^
                        Debug.Log("PL2 ����");
                    }
                    else if (ATK1 == 1)//PL1�����łɉ����Ă���Ƃ��@���@PL2�����x�������Ƃ�
                    {
                        dif_calc();
                        //image2_2.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 255);
                        rnd1 = 0;
                    }
                }
            }

            if (time_start == 1)//���̌v�Z
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
            //�����Ɂu�I�v�摜�\���̋L�q
            Mark.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 255);
            Debug.Log("�I");
        }
    }

    private void dif_calc()
    {
        time_start += 1;
        fight += 1;//���̏�����fight��2�̂͂�
        Debug.Log("���̓^�C�~���O�̍���" + timer + "�ł���");
        Mark.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 255);
        image1.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 255);//image1 ����
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