using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class textManager : MonoBehaviour
{
    public GameObject canvas;

    public Text chattext;

    public bool txt_openX = false;
    public bool txt_openY = false;
    public bool txt_close = false;
    public bool _play = false;

    public bool _tut1 = false;
    public bool _tut2 = false;

    private void Awake()
    {
        transform.position = new Vector3(0, -42, -7);
        canvas = GameObject.Find("Canvas");
        GetComponent<Transform>().parent = canvas.GetComponent<Transform>();
    }

    private void Update()
    {
        if (txt_openX == false)
        {
            if(GetComponent<SpriteRenderer>().size.y < 5)
            {
                GetComponent<SpriteRenderer>().size += new Vector2(0, 0.05f);
            }
            else

            if(GetComponent<SpriteRenderer>().size.y >= 5)
            {
                txt_openX = true;
                GetComponent<SpriteRenderer>().size = new Vector2(0.3f, 5);
            }
        }
        else

        if (txt_openX == true && txt_openY == false)
        {
            if(GetComponent<SpriteRenderer>().size.x < 24)
            {
                GetComponent<SpriteRenderer>().size += new Vector2(0.6f, 0);
            }
            else

            if (GetComponent<SpriteRenderer>().size.x >= 24)
            {
                txt_openY = true;
                GetComponent<SpriteRenderer>().size = new Vector2(24, 5);
            }
        }

        if (txt_openX == true && txt_openY == true && _play == false)
        {
            if (_tut1 == true)
            {
                StartCoroutine(tut1());
                _play = true;
            }
            
            if (_tut2 == true)
            {
                StartCoroutine(tut2());
                _play = true;
            }
        }

        if (txt_close == true)
        {
            chattext.text = "";
            GetComponent<SpriteRenderer>().size -= new Vector2(0, 0.05f);

            if (GetComponent<SpriteRenderer>().size.y <= 0.1)
            {
                Destroy(gameObject);
            }
        }
    }

    IEnumerator tut1()
    {
        yield return StartCoroutine(chat("아….\n아아아……."));
        yield return StartCoroutine(chat("어째서….이런일이……."));
        yield return StartCoroutine(chat("……."));
        yield return StartCoroutine(chat("아마드여,\n……목소리가 들리시오?"));
        yield return StartCoroutine(chat("당신의 세계가 무너지고 있소!"));
        yield return StartCoroutine(chat("……."));
        yield return StartCoroutine(chat("…….\n……."));
        yield return StartCoroutine(chat("…….\n…………어째서."));
        yield return StartCoroutine(chat("……누구라도 좋아…."));
        yield return StartCoroutine(chat("누구라도 좋으니"));
        yield return StartCoroutine(chat("부디……!\n부디 나의 세계를 구해다오!"));
        yield return new WaitForSeconds(0.5f);
        txt_close = true;
        GameObject.Find("tutorialP2").GetComponent<tutorialP2>()._Down();
    }

    IEnumerator tut2()
    {
        yield return StartCoroutine(chat("……."));
        yield return StartCoroutine(chat("…안녕하신가, 이방인이여."));
        yield return StartCoroutine(chat("나의 이름은 아마드.\n이 세계의 신 같은 존재라고 할 수 있지."));
        yield return StartCoroutine(chat("일단, 나의 부름에 답한것에 감사를 표하도록 하겠네."));
        yield return StartCoroutine(chat("이곳은 자네가 살던 세계와는 다른 세계일세."));
        yield return StartCoroutine(chat("자네는 나의 부름을 받아 영혼만이 이 세계로 옮겨졌네.\n그리고 급한대로 눈 앞의 고철덩어리에\n자네의 영혼을 깃들였지."));
        yield return StartCoroutine(chat("이 세계에 대한 설명은 생략하지.\n자네는 결국 원래 세계로 돌아가야 할 몸이니까."));
        yield return StartCoroutine(chat("원래 세계로 돌아가기 위해서는\n내가 자네를 부른 목적을 달성해야만 하네."));
        yield return StartCoroutine(chat("그 목적이란, 각 방위의 끝에 있는\n성물에 깃든 마물을 4체 쓰러트리는 것일세."));
        yield return StartCoroutine(chat("지금의 자네는 스스로 움직이는 것조차 못하겠지.\n정신뿐이니까."));
        yield return new WaitForSeconds(0.5f);
        GameObject.Find("amad_aurora").GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(chat("내가 힘을 주지. 이 힘을 가지고 목적을 완수하길 바라네."));
        txt_close = true;
        GameObject.Find("tutorialP2").GetComponent<tutorialP2>().Burning();
    }

    public IEnumerator chat(string narration)
    {
        int word = 0;
        chattext.text = "";
        string writeText = "";

        yield return new WaitForSeconds(1f);

        for (word = 0; word < narration.Length; word++)
        {
            writeText += narration[word];
            chattext.text = writeText;

            if (Input.GetKey(KeyCode.C))
            {
                chattext.text = narration;
                break;
            }
            yield return new WaitForSeconds(0.1f);
        }

        while (true)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                break;
            }
            yield return null;
        }
    }
}
