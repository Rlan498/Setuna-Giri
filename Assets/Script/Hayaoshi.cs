using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    GameObject Mark;
    GameObject PUSH;
    GameObject image1;
    GameObject image2;
    GameObject image2_2;

    //���U���g�p�ϐ�
    public static string[] fight_array = new string[5];//���؂�̏����Ҋi�[�p�z��
    public static float[] time_array = new float[5];//���͍��̊i�[�p�z��
    public static int i = 0;//�����̓x��+1(�z��ւ̏��s���͗p)
    
    int count = 0;//�����̊m�F�̂��߂̕ϐ��@��X�����ă��V
    public static bool pena = false;
    public static float timer = 0;//���͍����i�[����ϐ�
    public static int time_start = 0;//1�̎��ɓ��̓^�C�~���O�̍����v�� 2�ŏI�����鏈��
    public static int push = 0;//push�̕\���֌W



    // Start is called before the first frame update
    void Start()
    {
        Mark = GameObject.Find("Mark");
        PUSH = GameObject.Find("PUSH");
        image1 = GameObject.Find("image1");
        image2 = GameObject.Find("image2");
        image2_2 = GameObject.Find("image2_2");
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
            if (Input.GetKeyDown(KeyCode.S) && ready1 == 0)
            {
                ready1 = 1;
                //PL1����
                Debug.Log("PL1 ��������");
            }

            if (Input.GetKeyDown(KeyCode.DownArrow) && ready2 == 0)
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
                if(push == 0)
                {
                    PUSH.GetComponent<AudioSource>().Play();
                    PUSH.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 255);
                    push = 1;
                }
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

            if (Input.GetKeyDown(KeyCode.S) && ready == 1 && reset == 0)
            {
                if (fight == 0)
                {
                    if (pena1 < 2)//�y�i���e�B�͂�����n+1���
                    {
                        StopAllCoroutines();
                        reset = 1;//�����l�ǉ��O��reset��1���� = �A���Ńy�i�΍�  ���؂�O��
                        pena1 = pena1 + 1;//PL1�ɔ����l��ǉ�
                        pena = false;//�������̘A���΍�̂���
                        Debug.Log(i + "�@PL1 �t���C���O" + pena1 + "���");
                    }
                    else//�t���C���O�񐔂ł̃y�i���e�B�ݒ�
                    {
                        reset = 2;
                        pena1 = pena1 + 1;
                        fight_array[i] = "PL1 �y�i���e�B����";
                        Debug.Log("PL1 �t���C���O3��ځ@����");
                        i++;
                        StartCoroutine("wait_result");
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
                        fight_array[i] = "PL1";
                        Debug.Log(i + "�@PL1 ����");
                    }
                    else if (ATK2 == 1)//PL2�����łɉ����Ă���Ƃ��@���@PL1�����x�������Ƃ�
                    {
                        dif_calc();
                        //image2.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 255);
                        rnd1 = 0;
                        this.GetComponent<AudioSource>().Play();
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.DownArrow) && ready == 1 && reset == 0)
            {
                if (fight == 0)
                {
                    if (pena2 < 2)//�y�i���e�B�͂�����n+1���
                    {
                        StopAllCoroutines();
                        reset = 1;//���؂�O��
                        pena2 = pena2 + 1;//PL2�ɔ����l��ǉ�
                        pena = false;
                        Debug.Log(i + "�@PL2 �t���C���O" + pena2 + "���");
                    }
                    else//�t���C���O�񐔂ł̃y�i���e�B�ݒ�
                    {
                        reset = 2;
                        pena2 = pena2 + 1;//���������X��pena1 = pena1 + 1;������
                        fight_array[i] = "PL2 �y�i���e�B����";
                        Debug.Log("PL2 �t���C���O3��ځ@����");
                        i++;
                        StartCoroutine("wait_result");
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

                        fight_array[i] = "PL2";
                        Debug.Log(i + "�@PL2 ����");
                    }
                    else if (ATK1 == 1)//PL1�����łɉ����Ă���Ƃ��@���@PL2�����x�������Ƃ�
                    {
                        dif_calc();
                        //image2_2.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 255);
                        rnd1 = 0;
                        this.GetComponent<AudioSource>().Play();
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
            Mark.GetComponent<AudioSource>().Play();
            Debug.Log("�I");
        }
    }

    private void dif_calc()
    {
        time_start += 1;
        fight += 1;//���̏�����fight��2�̂͂�

        time_array[i] = timer;
        i += 1;
        Debug.Log("���̓^�C�~���O�̍���" + timer + "�ł���");
        Mark.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 255);
        image1.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 255);//image1 ����
    }

    IEnumerator wait()
    {
        //���������̏����I�ȏꏊ
        yield return new WaitForSeconds(1.0f);
        count += 1;
        Debug.Log(i + "�@" + count + "��ځA�\���I�I�I");
        ready = 1;
        reset = 0;
    }

    private IEnumerator wait_result()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Result");//���U���g��ʂ�
    }
}
