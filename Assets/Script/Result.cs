using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour
{
    GameObject PL1;
    GameObject PL2;

    // Start is called before the first frame update
    void Start()
    {
        PL1 = GameObject.Find("PL1_WIN");//PL1にゲームオブジェクトPL1_WINを結び付け
        PL2 = GameObject.Find("PL2_WIN");
        PL1.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 255);//WINを透明化
        PL2.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 255);
        this.GetComponent<AudioSource>().Play();
        if (Chase.win1 == 1)
        {
            PL1.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 255);//WINを表示
        }
        else if (Chase.win2 == 1)
        {
            PL2.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 255);
        }
        else
        {
            Debug.Log("?????");//何かここを通ったとき用
        }
        StartCoroutine("wait");
    }

    // Update is called once per frame
    void Update()
    {

    }
    private IEnumerator wait()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Title");//タイトルに戻る
    }
}