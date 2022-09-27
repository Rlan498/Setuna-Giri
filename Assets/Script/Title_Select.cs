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
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))//選択肢　下に
        {
            place += 1;
        } else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))//選択肢　上に
        {
            place -= 1;
        } else if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter)){//決定が押された時の処理
            if(place == 0)
            {
                this.GetComponent<AudioSource>().Play();
                StartCoroutine("wait");
            }
            else if(place == 1)
            {
                this.GetComponent<AudioSource>().Play();

            }
            else if(place == 2)
            {
                UnityEditor.EditorApplication.isPlaying = false;
                Application.Quit();
            }
        }

        if (place > 2)//下から上に　上から下に行くための処理
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
        SceneManager.LoadScene("SampleScene");
    }
}
