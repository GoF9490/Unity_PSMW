using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class judgeball : MonoBehaviour
{
    public GameObject player;
    public GameObject judgement;
    public GameObject boom;
    public GameObject zzz;
    public GameObject bullet;
    //public GameObject _bullet;
    public GameObject web;
    public eavalue eav;

    public float ball_c = 1f;
    public float interval;
    public int patton = 0;
    public int speed = 1;
    public IEnumerator _cor;
    public int energy = 0;
    //public Rigidbody2D rb;

    public bool moveR = false;
    public bool moveL = false;
    public bool moveU = false;
    public bool moveD = false;
    public bool finish = false;
    public bool phase4 = false;
    public bool pattonJ = false;

    public Vector2 tp;

    public Transform pt;

    public List<string> bulletL = new List<string>();
    public List<string> bulletR = new List<string>();

    private void Start()
    {
        player = GameObject.Find("shero1");
        pt = player.GetComponent<Transform>();
        //eav = GetComponent<eavalue>();
        //rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (phase4 == true)
        {
            if (transform.position.x < -17.5f)
            {
                moveL = false;
                moveR = true;
            }
            else

            if (transform.position.x > 17.5f)
            {
                moveR = false;
                moveL = true;
            }

            if (moveR == true)
            {
                transform.Translate(Vector2.right * Time.deltaTime * 5f * speed);
            }
            else

        if (moveL == true)
            {
                transform.Translate(Vector2.right * Time.deltaTime * -5f * speed);
            }

        }
        else

        if (pattonJ == true)
        {
            if (moveR == true)
            {
                transform.Translate(Vector2.right * Time.deltaTime * 20f * speed);
            }
            else
            
            if (moveL == true)
            {
                transform.Translate(Vector2.right * Time.deltaTime * -20f * speed);
            }

            if (Mathf.Abs(transform.position.x) <= 2)
            {
                if (interval < 1)
                {
                    transform.position = new Vector2(-2, 7f);
                }
                else

                if (interval > 1)
                {
                    transform.position = new Vector2(2, 7f);
                }
                moveL = false;
                moveR = false;
                pattonJ = false;
                judge2();
            }
        }
        else

        if (moveR == true)
        {
            transform.Translate(Vector2.right * Time.deltaTime * 50f * speed);
        } else

        if (moveL == true)
        {
            transform.Translate(Vector2.right * Time.deltaTime * -50f * speed);
        }
        else

        if (moveD == true)
        {
            transform.Translate(Vector2.down * Time.deltaTime * 75f * speed);
        }

        if (phase4 == false && pattonJ == false)
        {
            zzz.SetActive(false);
        }
        
        tp = new Vector2(judgement.transform.position.x + interval, 7f);
    }

    public void Crash1L()
    {
        gameObject.tag = "press";
        eav.thrx = 20;
        eav.thry = 20;
        eav.damage = 10;
        _cor = MoveTo(this.gameObject, new Vector2(judgement.transform.position.x - 5f, 2f));
        StartCoroutine(_cor);
    }

    public void Crash2L()
    {
        gameObject.tag = "press";
        eav.thrx = 30;
        eav.thry = 20;
        eav.damage = 15;
        finish = true;
        _cor = MoveTo(this.gameObject, new Vector2(judgement.transform.position.x - 7f, 2f));
        StartCoroutine(_cor);
    }

    public void Crash1R()
    {
        gameObject.tag = "press";
        eav.thrx = 20;
        eav.thry = 20;
        eav.damage = 10;
        _cor = MoveTo(this.gameObject, new Vector2(judgement.transform.position.x + 5f, 2f));
        StartCoroutine(_cor);
    }

    public void Crash2R()
    {
        gameObject.tag = "press";
        eav.thrx = 30;
        eav.thry = 20;
        eav.damage = 15;
        finish = true; 
        _cor = MoveTo(this.gameObject, new Vector2(judgement.transform.position.x + 7f, 2f));
        StartCoroutine(_cor);
    }

    public void ScrapeL()
    {
        gameObject.tag = "press";
        eav.thrx = 30;
        eav.thry = 20;
        eav.damage = 15;
        _cor = scrapeL(this.gameObject, new Vector2(judgement.transform.position.x - 7f, 2f));
        StartCoroutine(_cor);
    }

    public void ScrapeR()
    {
        gameObject.tag = "press";
        eav.thrx = 30;
        eav.thry = 20;
        eav.damage = 15;
        _cor = scrapeR(this.gameObject, new Vector2(judgement.transform.position.x + 7f, 2f));
        StartCoroutine(_cor);
    }

    public void Fall()
    {
        GetComponent<Transform>().parent = null;
        Vector2 wasPos = new Vector2(pt.position.x, 13f);
        transform.position = wasPos;

        _cor = fall(this.gameObject, wasPos);
        StartCoroutine(_cor);
    }

    public void Resonance()
    {
        _cor = resonance();
        StartCoroutine(_cor);
    }

    public void backup(Vector2 toPos)
    {
        _cor = back_up(gameObject, new Vector2(toPos.x, 8f));
        StartCoroutine(_cor);
    }

    public void backdown(Vector2 toPos)
    {
        _cor = back_up(gameObject, new Vector2(toPos.x, 4f));
        StartCoroutine(_cor);
    }

    public void stomp()
    {
        _cor = _stomp();
        StartCoroutine(_cor);
    }

    public void hide()
    {
        _cor = _hide();
        StartCoroutine(_cor);
    }

    public void reveal()
    {
        _cor = _reveal();
        StartCoroutine(_cor);
    }

    public void detach()
    {
        _cor = _detach();
        StartCoroutine(_cor);
    }

    public void judge1()
    {
        _cor = _judge1();
        StartCoroutine(_cor);
    }

    void judge2()
    {
        _cor = _judge2();
        StartCoroutine(_cor);
    }

    public void judgeF()
    {
        _cor = _judge5();
        StartCoroutine(_cor);
    }

    IEnumerator MoveTo(GameObject ball, Vector2 toPos)
    {
        GetComponent<Transform>().parent = null;

        float count = 0;
        Vector2 wasPos = ball.transform.position;
        while (true)
        {
            count += Time.deltaTime * 6 * speed;
            ball.transform.position = Vector2.Lerp(wasPos, toPos, count);

            if (count >= 1)
            {
                ball.transform.position = toPos;

                break;
            }

            yield return null;

            _cor = MoveBack(this.gameObject, new Vector2(judgement.transform.position.x + interval, 7f));
            StartCoroutine(_cor);
        }
    }

    IEnumerator MoveBack(GameObject ball, Vector2 toPos)
    {
        yield return new WaitForSeconds(0.3f * ball_c);
        StartCoroutine(eend());
        if (finish == false)
        {
            yield return new WaitForSeconds(ball_c * 0.5f);
        }
        else

        if (finish == true)
        {
            yield return new WaitForSeconds(ball_c);
        }

        float count = 0;
        Vector2 wasPos = ball.transform.position;
        while (true)
        {
            count += Time.deltaTime * 3 * speed;
            ball.transform.position = Vector2.Lerp(wasPos, tp, count);

            if (count >= 1)
            {
                ball.transform.position = tp;

                break;
            }

            yield return null;

            GetComponent<Transform>().parent = judgement.GetComponent<Transform>();
            if (finish == true)
            {
                judgement.GetComponent<judgement>()._delay = false;
                finish = false;
            }
        }
    }

    IEnumerator scrapeL(GameObject ball, Vector2 toPos)
    {
        GetComponent<Transform>().parent = null;

        float count = 0;
        Vector2 wasPos = ball.transform.position;
        while (true)
        {
            count += Time.deltaTime * 6 * speed;
            ball.transform.position = Vector2.Lerp(wasPos, toPos, count);

            if (count >= 1)
            {
                ball.transform.position = toPos;

                break;
            }

            _cor = _scrape();
            StartCoroutine(_cor);
            yield return null;
        }
    }

    IEnumerator scrapeR(GameObject ball, Vector2 toPos)
    {
        GetComponent<Transform>().parent = null;

        float count = 0;
        Vector2 wasPos = ball.transform.position;
        while (true)
        {
            count += Time.deltaTime * 6 * speed;
            ball.transform.position = Vector2.Lerp(wasPos, toPos, count);

            if (count >= 1)
            {
                ball.transform.position = toPos;

                break;
            }

            _cor = _scrape();
            StartCoroutine(_cor);
            yield return null;
        }
    }

    IEnumerator _scrape()
    {
        yield return new WaitForSeconds(0.5f * ball_c);
        gameObject.tag = "throw";
        eav.thrx = 20;
        eav.thry = 5;
        eav.damage = 15;
        if (pt.position.x >= transform.position.x && moveL == false && moveR == false)
        {
            moveR = true;
        }
        else

        if (pt.position.x < transform.position.x && moveL == false && moveR == false)
        {
            moveL = true;
        }

        yield return new WaitForSeconds(1f);
        StartCoroutine(eend());
        moveR = false;
        moveL = false;
        transform.position = new Vector2(judgement.transform.position.x + interval, 7f);
        GetComponent<Transform>().parent = judgement.GetComponent<Transform>();
        judgement.GetComponent<judgement>()._delay = false;

    }


    IEnumerator fall(GameObject ball, Vector2 wasPos)
    {
        
        yield return new WaitForSeconds(0.8f);
        gameObject.tag = "press";
        eav.damage = 20;
        eav.thrx = 10;
        eav.thry = 30;
        Vector2 toPos = new Vector2(transform.position.x, 2f);

        float count = 0;
        while (true)
        {
            count += Time.deltaTime * 4;
            ball.transform.position = Vector2.Lerp(transform.position, toPos, count);

            if (count >= 1)
            {
                ball.transform.position = toPos;

                break;
            }
            _cor = fall2();
            StartCoroutine(_cor);

            yield return null;
        }
    }

    IEnumerator fall2()
    {
        yield return new WaitForSeconds(1f * ball_c);
        StartCoroutine(eend());
        transform.position = new Vector2(judgement.transform.position.x + interval, 7f);
        GetComponent<Transform>().parent = judgement.GetComponent<Transform>();
        judgement.GetComponent<judgement>()._delay = false;
    }

    IEnumerator resonance()
    {
        GameObject boom1 = (GameObject)Instantiate(boom, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);
        yield return new WaitForSeconds(1.4f);
        judgement.GetComponent<judgement>()._delay = false;

    }

    IEnumerator back_up(GameObject ball, Vector2 toPos)
    {
        GetComponent<Transform>().parent = null;

        float count = 0;
        Vector2 wasPos = ball.transform.position;
        while (true)
        {
            count += Time.deltaTime * 6 * speed;
            ball.transform.position = Vector2.Lerp(wasPos, toPos, count);

            if (count >= 1)
            {
                ball.transform.position = toPos;

                break;
            }
            yield return null;
        }

        _cor = resonance_back(this.gameObject, new Vector2(judgement.transform.position.x + interval, 7f));
        StartCoroutine(_cor);
    }

    IEnumerator back_down(GameObject ball, Vector2 toPos)
    {
        GetComponent<Transform>().parent = null;

        float count = 0;
        Vector2 wasPos = ball.transform.position;
        while (true)
        {
            count += Time.deltaTime * 6 * speed;
            ball.transform.position = Vector2.Lerp(wasPos, toPos, count);

            if (count >= 1)
            {
                ball.transform.position = toPos;

                break;
            }

            yield return null;
        }

        _cor = resonance_back(this.gameObject, new Vector2(judgement.transform.position.x + interval, 7f));
        StartCoroutine(_cor);
    }

    IEnumerator resonance_back(GameObject ball, Vector2 toPos)
    {
        GameObject boom1 = (GameObject)Instantiate(boom, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);
        yield return new WaitForSeconds(1.4f);

        float count = 0;
        Vector2 wasPos = ball.transform.position;
        while (true)
        {
            count += Time.deltaTime * 3 * speed;
            ball.transform.position = Vector2.Lerp(wasPos, tp, count);

            if (count >= 1)
            {
                ball.transform.position = tp;

                break;
            }

            yield return null;

            GetComponent<Transform>().parent = judgement.GetComponent<Transform>();

            judgement.GetComponent<judgement>()._delay = false;
        }
    }

    IEnumerator _hide()
    {
        yield return new WaitForSeconds(0.4f);
        //gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.SetActive(false);
    }

    IEnumerator _reveal()
    {
        //gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        
    }

    IEnumerator _detach()
    {
        GetComponent<Transform>().parent = null;

        yield return new WaitForSeconds(0.4f);
        phase4 = true;
        if (interval < 1)
        {
            moveL = true;
        }
        else

        if (interval > 1)
        {
            moveR = true;
        }
        zzz.SetActive(true);
    }

    IEnumerator _stomp()
    {
        GetComponent<Transform>().parent = null;

        float count = 0;
        Vector2 wasPos = transform.position;
        Vector2 toPos = new Vector2(transform.position.x, transform.position.y + 2);
        while (true)
        {
            count += Time.deltaTime * 12 * speed;
            transform.position = Vector2.Lerp(wasPos, toPos, count);

            if (count >= 1)
            {
                transform.position = toPos;

                break;
            }

            yield return null;

           //_cor = MoveBack(this.gameObject, new Vector2(judgement.transform.position.x + interval, 7f));
           //StartCoroutine(_cor);
        }

        yield return new WaitForSeconds(0.1f);
        count = 0;
        wasPos = transform.position;
        toPos = new Vector2(transform.position.x, 2);
        while (true)
        {
            count += Time.deltaTime * 6 * speed;
            transform.position = Vector2.Lerp(wasPos, toPos, count);

            if (count >= 1)
            {
                transform.position = toPos;

                break;
            }

            yield return null;
        }
        GameObject resonence1 = GameObject.Instantiate(boom, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(ball_c * 0.5f);
        StartCoroutine(eend());
        count = 0;
        wasPos = transform.position;
        toPos = new Vector2(judgement.transform.position.x + interval, 7f);
        while (true)
        {
            count += Time.deltaTime * 6 * speed;
            transform.position = Vector2.Lerp(wasPos, toPos, count);

            if (count >= 1)
            {
                transform.position = toPos;

                break;
            }

            yield return null;
        }
        GetComponent<Transform>().parent = judgement.GetComponent<Transform>();
    }

    IEnumerator _judge1()
    {
        GetComponent<Transform>().parent = null;
        pattonJ = true;

        yield return new WaitForSeconds(1.5f);
        if (interval < 1)
        {
            transform.position = new Vector2(-18, 7);
        }
        else

        if (interval > 1)
        {
            transform.position = new Vector2(18, 7);
        }

        yield return new WaitForSeconds(0.5f);
        if (interval < 1)
        {
            moveR = true;
        }
        else

        if (interval > 1)
        {
            moveL = true;
        }
        zzz.SetActive(true);
    }

    IEnumerator _judge2()
    {
        zzz.SetActive(false);
        bulletL.Add("1");
        bulletL.Add("3");
        bulletL.Add("5");
        bulletR.Add("2");
        bulletR.Add("4");
        bulletR.Add("6");

        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            transform.Translate(Vector2.up * Time.deltaTime * 30);

            if (transform.position.y > 18)
            {
                transform.position = new Vector2(transform.position.x, 18);

                break;
            }
            yield return null;
        }
        energy = 3;

        _cor = _judge3();
        StartCoroutine(_cor);


    }

    IEnumerator _judge3()
    {
        yield return new WaitForSeconds(0.5f);

        if (energy > 0)
        {
            _cor = _judge4();
            StartCoroutine(_cor);
        }
        else

        if (energy == 0)
        {
            yield return new WaitForSeconds(0.5f);
            Vector2 toPos = new Vector2(0, 0);
            float count = 0;

            if (interval < 1)
            {
                toPos = new Vector2(-18, 2);
            }
            else

            if (interval > 1)
            {
                toPos = new Vector2(18, 2);
            }

            while (true)
            {
                count += Time.deltaTime * 3;

                transform.position = Vector2.Lerp(transform.position, toPos, count);

                if (count >= 1)
                {
                    transform.position = toPos;

                    break;
                }
                yield return null;
            }
            web.SetActive(true);

            yield return new WaitForSeconds(0.5f);

            if (interval < 1)
            {
                GameObject.Find("bul1").GetComponent<bullet>().Shoot();
                yield return new WaitForSeconds(0.5f);
                GameObject.Find("bul2").GetComponent<bullet>().Shoot();
                yield return new WaitForSeconds(0.5f);
                GameObject.Find("bul3").GetComponent<bullet>().Shoot();
                yield return new WaitForSeconds(0.5f);
                GameObject.Find("bul4").GetComponent<bullet>().Shoot();
                yield return new WaitForSeconds(0.5f);
                GameObject.Find("bul5").GetComponent<bullet>().Shoot();
                yield return new WaitForSeconds(0.5f);
                GameObject.Find("bul6").GetComponent<bullet>().Shoot();

                yield return new WaitForSeconds(1f);
                web.SetActive(false);

                yield return new WaitForSeconds(0.5f);
                judgement.GetComponent<judgement>().Judge_finish(judgement);
            }

        }

    }

    IEnumerator _judge4()
    {
        yield return new WaitForSeconds(0.2f);

        float count = 0;
        int ranL = 0;
        int ranR = 0;        

        Vector2 toPos = new Vector2(0, 0);

        if (interval < 1)
        {
            toPos = new Vector2(transform.position.x - 4, transform.position.y + 1);
        }
        else
        
        if (interval > 1)
        {
            toPos = new Vector2(transform.position.x + 4, transform.position.y + 1);
        }

        while (true)
        {
            count += Time.deltaTime * 3;

            transform.position = Vector2.Lerp(transform.position, toPos, count);

            if (count >= 1)
            {
                transform.position = toPos;
                energy = energy - 1;
                if (interval < 1)
                {
                    ranL = Random.Range(0, bulletL.Count);
                    GameObject bullet1 = (GameObject)Instantiate(bullet, new Vector3(this.transform.position.x, this.transform.position.y - 3, 1), Quaternion.identity);
                    bullet1.name = "bul" + bulletL[ranL];
                    bulletL.Remove(bulletL[ranL]);
                }
                else

                if (interval > 1)
                {
                    ranR = Random.Range(0, bulletR.Count);
                    GameObject bullet2 = (GameObject)Instantiate(bullet, new Vector3(this.transform.position.x, this.transform.position.y - 3, 1), Quaternion.identity);
                    bullet2.name = "bul" + bulletR[ranR];
                    bulletR.Remove(bulletR[ranR]);
                }

                break;
            }
            yield return null;
        }

        _cor = _judge3();
        StartCoroutine(_cor);
    }

    IEnumerator _judge5()
    {
        yield return new WaitForSeconds(0.5f);
        transform.position = new Vector2(judgement.transform.position.x + interval, 7f);
        GetComponent<Transform>().parent = judgement.GetComponent<Transform>();
    }

    IEnumerator eend()
    {
        yield return null;
        gameObject.tag = "none";
        eav.damage = 0;
        eav.thrx = 0;
        eav.thry = 0;
    }
}
