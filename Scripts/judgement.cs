using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class judgement : MonoBehaviour
{
    public GameObject _jb1;
    public GameObject _jb2;
    public GameObject player;
    public GameObject bb;
    public GameObject judgemode;
    public GameObject dead;

    public Transform _pt;
    public Transform _tt;

    public IEnumerator _cor;

    public int patton = 1;
    public int phase = 1;
    public int combo = 0;

    public float life = 2500;
    public float speed = 1;
    public float rsp = 1;
    public float att_cool = 0f;
    public float crash_cool = 0f;
    public float dash_cool = 0f;
    public float big_cool = 0f;
    public float mov_spd = 1f;

    public Rigidbody2D _rb;

    public bool _delay = true;
    public bool dash_att = false;
    public bool _flip = false;
    public bool _judge = false;

    public Slider hpslider;

    public Color sprite;

    void Awake()
    {
        player = GameObject.Find("shero1");
        //hpslider = GameObject.Find("Bslider").GetComponent<Slider>();
        hpslider.maxValue = life;
        _pt = player.GetComponent<Transform>();
        _tt = GetComponent<Transform>();
        _rb = GetComponent<Rigidbody2D>();
        //patton = Random.Range(1, 3);
    }

    private void Update()
    {
        if (life <= 0)
        {
            dead.SetActive(true);
            dead.transform.position = transform.position;
            StopCoroutine(_cor);
            Time.timeScale = 1;
            Destroy(gameObject);
        }

        if (_delay == false || _flip == true)
        {
            if (_pt.position.x > _tt.position.x)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else

            if (_pt.position.x < _tt.position.x)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            _delay = false;
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            //Warp();
            //Detach();
            //Judge();
            Stomp();
        }

        if (_judge == true && _delay == false && att_cool > 0)
        {
            _delay = true;
            Judge();
        }

        hpslider.value = life;

        if (_delay == false && phase > 0)
        {
            if (life <= 2200 && phase == 1)
            {
                phase = 2;
                mov_spd = 2.5f;
            }

            if (life <= 1500 && phase == 2)
            {
                phase = 3;
                speed = 2;
                rsp = 0.5f;
                att_cool = 5;
                _judge = true;
                _jb1.GetComponent<judgeball>().speed = 2;
                _jb2.GetComponent<judgeball>().speed = 2;
                _jb1.GetComponent<judgeball>().ball_c = 0.5f;
                _jb2.GetComponent<judgeball>().ball_c = 0.5f;
            }

            if (life <= 600 && phase == 3)
            {
                att_cool = 5;
                _judge = true;
                phase = 4;
            }

            if (att_cool > 0 && _judge == false)
            {
                att_cool -= Time.deltaTime;
            }

            if (dash_cool > 0)
            {
                dash_cool -= Time.deltaTime;
            }

            if (crash_cool > 0)
            {
                crash_cool -= Time.deltaTime;
            }

            if (big_cool > 0)
            {
                big_cool -= Time.deltaTime;
            }

            if (Mathf.Abs(_pt.position.x - _tt.position.x) > 6f && _pt.position.x > _tt.position.x)
            {
                transform.Translate(Vector2.right * 2 * Time.deltaTime * mov_spd);

                if (att_cool <= 0f && dash_cool <= 0 && phase >= 2)
                {
                    DashR();
                }
            }
            else

            if (Mathf.Abs(_pt.position.x - _tt.position.x) > 6f && _pt.position.x < _tt.position.x)
            {
                transform.Translate(Vector2.left * 2 * Time.deltaTime * mov_spd);

                if (att_cool <= 0f && dash_cool <= 0 && phase >= 2)
                {
                    DashL();
                }
            }

            if (Mathf.Abs(_pt.position.x - _tt.position.x) > 6f && att_cool <= 0f && phase >= 3 && dash_cool > 0)
            {
                Bigball();
            }


            if (Mathf.Abs(_pt.position.x - _tt.position.x) <= 6f && att_cool <= 0f)
            {
                if(_pt.position.y < 6f)
                {
                    patton = Random.Range(1, 2);

                    if (patton == 1)
                    {
                        if (_pt.position.x < _tt.position.x)
                        {
                            DoubleCrashL();
                        }
                        else

                        if (_pt.position.x > _tt.position.x)
                        {
                            DoubleCrashR();
                        }
                    }
                }
                else

                if(_pt.position.y > 6f)
                {
                    patton = Random.Range(1, 3);

                    if (patton == 1)
                    {
                        Resonance();
                    }
                    else

                    if (patton == 2)
                    {
                        Backstep();
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("SoulAtt"))
        {
            float Att = collision.gameObject.GetComponent<att_dmg>().Damage;// * DataController.instance.GetComponent<DataController>().playerData.soul;
            Debug.Log(Att);
            life = life - Att;
            StartCoroutine(slow());
        }
    }

    void DoubleCrashL()
    {
        _delay = true;
        patton = Random.Range(1, 4);
        _cor = Crash1L(this.gameObject, new Vector2(_pt.position.x + 4f, 5));
        StartCoroutine(_cor);
        att_cool = 2f;
    }

    void DoubleCrashR()
    {
        _delay = true;
        patton = Random.Range(1, 4);
        _cor = Crash1R(this.gameObject, new Vector2(transform.position.x + 4f, 5));
        StartCoroutine(_cor);
        att_cool = 2f;
    }

    void Resonance()
    {
        _delay = true;
        _cor = resonance();
        StartCoroutine(_cor);
        att_cool = 1f;
    }

    void DashL()
    {
        _delay = true;
        att_cool = 1f;
        dash_cool = 5f;
        _cor = dash(gameObject, new Vector2(transform.position.x - 8f, 5));
        StartCoroutine(_cor);
    }

    void DashR()
    {
        _delay = true;
        att_cool = 1f;
        dash_cool = 5f;
        _cor = dash(gameObject, new Vector2(transform.position.x + 8f, 5));
        StartCoroutine(_cor);
    }

    void Backstep()
    {
        _delay = true;
        if (phase == 1)
        {
            att_cool = 0.5f;
        }
        else

        if (phase >= 2)
        {
            att_cool = 2;
        }

        if (_pt.position.x < _tt.position.x)
        {
            _cor = backstep(gameObject, new Vector2(transform.position.x + 4f, 5));
            StartCoroutine(_cor);
        }
        else

        if (_pt.position.x > _tt.position.x)
        {
            _cor = backstep(gameObject, new Vector2(transform.position.x - 4f, 5));
            StartCoroutine(_cor);
        }
    }

    void Bigball()
    {
        _delay = true;
        att_cool = 2;
        _cor = bigball();
        StartCoroutine(_cor);
    }

    public void Recall()
    {
        _cor = recall();
        StartCoroutine(_cor);
    }

    void Warp()
    {
        _delay = true;
        att_cool = 1;
        _cor = warp();
        StartCoroutine(_cor);
    }

    void Detach()
    {
        _delay = true;
        att_cool = 1;
        _cor = detach();
        StartCoroutine(_cor);
    }

    void Stomp()
    {
        _delay = true;
        att_cool = 1;
        _cor = stomp();
        StartCoroutine(_cor);
    }

    void Judge()
    {
        _judge = false;
        _delay = true;
        att_cool = 3;
        _cor = judge();
        StartCoroutine(_cor);
    }

    public void Judge_finish(GameObject spear)
    {
        _cor = judge_finish(spear);
        StartCoroutine(_cor);
    }

    IEnumerator Crash1L(GameObject _this, Vector2 toPos)
    {
        yield return new WaitForSeconds(0.4f * rsp);
        _jb2.GetComponent<judgeball>().Crash1L();

        yield return new WaitForSeconds(0.6f * rsp);

        float count = 0;
        Vector2 wasPos = _this.transform.position;

        if (Mathf.Abs(_pt.position.x - _tt.position.x) <= 4f)
        {
            toPos = new Vector2(_tt.position.x, 5);
        }
        else

        if (Mathf.Abs(_pt.position.x - _tt.position.x) > 4f && Mathf.Abs(_pt.position.x - _tt.position.x) <= 8f)
        {
            toPos = new Vector2(_pt.position.x + 4f, 5);
        }
        else

        if (Mathf.Abs(_pt.position.x - _tt.position.x) > 8f)
        {
            toPos = new Vector2(transform.position.x - 4f, 5);
        }

        if (toPos.x >= 14)
        {
            toPos.x = 14;
        }
        else

        if (toPos.x <= -14)
        {
            toPos.x = -14;
        }

        _flip = true;
        while (true)
        {
            count += Time.deltaTime * 6 * speed;
            _this.transform.position = Vector2.Lerp(wasPos, toPos, count);

            if (count >= 1)
            {
                _this.transform.position = toPos;

                break;
            }
            yield return null;
        }
        _flip = false;

        if (phase == 1)
        {
            if (patton == 1)
            {
                _cor = Crash4L();
                StartCoroutine(_cor);
            }
            else

            if (patton == 2)
            {
                _cor = scrapeL();
                StartCoroutine(_cor);
            }
            else

            if (patton == 3)
            {
                _cor = fall();
                StartCoroutine(_cor);
            }
        }
        else

            if (phase > 1)
        {
            _cor = Crash2L(gameObject, new Vector2(_pt.position.x + 4f, 5));
            StartCoroutine(_cor);
        }

    }

    IEnumerator Crash2L(GameObject _this, Vector2 toPos)
    {
        yield return new WaitForSeconds(0.4f * rsp);
        _jb1.GetComponent<judgeball>().stomp();

        yield return new WaitForSeconds(0.6f * rsp);

        float count = 0;
        Vector2 wasPos = _this.transform.position;

        if (Mathf.Abs(_pt.position.x - _tt.position.x) <= 4f)
        {
            toPos = new Vector2(_tt.position.x, 5);
        }
        else

        if (Mathf.Abs(_pt.position.x - _tt.position.x) > 4f && Mathf.Abs(_pt.position.x - _tt.position.x) <= 8f)
        {
            toPos = new Vector2(_pt.position.x + 4f, 5);
        }
        else

        if (Mathf.Abs(_pt.position.x - _tt.position.x) > 8f)
        {
            toPos = new Vector2(transform.position.x - 4f, 5);
        }

        if (toPos.x >= 14)
        {
            toPos.x = 14;
        }
        else

        if (toPos.x <= -14)
        {
            toPos.x = -14;
        }

        _flip = true;
        while (true)
        {
            count += Time.deltaTime * 6 * speed;
            _this.transform.position = Vector2.Lerp(wasPos, toPos, count);

            if (count >= 1)
            {
                _this.transform.position = toPos;

                break;
            }
            yield return null;
        }
        _flip = false;

        _cor = Crash3L(gameObject, new Vector2(_pt.position.x + 4f, 5));
        StartCoroutine(_cor);
    }

    IEnumerator Crash3L(GameObject _this, Vector2 toPos)
    {
        yield return new WaitForSeconds(0.4f * rsp);
        _jb2.GetComponent<judgeball>().Crash1L();

        yield return new WaitForSeconds(0.6f * rsp);

        float count = 0;
        Vector2 wasPos = _this.transform.position;

        if (Mathf.Abs(_pt.position.x - _tt.position.x) <= 4f)
        {
            toPos = new Vector2(_tt.position.x, 5);
        }
        else

        if (Mathf.Abs(_pt.position.x - _tt.position.x) > 4f && Mathf.Abs(_pt.position.x - _tt.position.x) <= 8f)
        {
            toPos = new Vector2(_pt.position.x + 4f, 5);
        }
        else

        if (Mathf.Abs(_pt.position.x - _tt.position.x) > 8f)
        {
            toPos = new Vector2(transform.position.x - 4f, 5);
        }

        if (toPos.x >= 14)
        {
            toPos.x = 14;
        }
        else

        if (toPos.x <= -14)
        {
            toPos.x = -14;
        }

        _flip = true;
        while (true)
        {
            count += Time.deltaTime * 6 * speed;
            _this.transform.position = Vector2.Lerp(wasPos, toPos, count);

            if (count >= 1)
            {
                _this.transform.position = toPos;

                break;
            }
            yield return null;
        }
        _flip = false;

        if (patton == 1)
        {
            _cor = Crash4L();
            StartCoroutine(_cor);
        }
        else

    if (patton == 2)
        {
            _cor = scrapeL();
            StartCoroutine(_cor);
        }
        else

    if (patton == 3)
        {
            _cor = fall();
            StartCoroutine(_cor);
        }
    }

    IEnumerator Crash4L()
    {
        yield return new WaitForSeconds(0.4f * rsp);
        _jb1.GetComponent<judgeball>().Crash2L();
       
    }

    IEnumerator Crash1R(GameObject _this, Vector2 toPos)
    {
        yield return new WaitForSeconds(0.4f * rsp);
        _jb1.GetComponent<judgeball>().Crash1R();

        yield return new WaitForSeconds(0.6f * rsp);

        float count = 0;
        Vector2 wasPos = _this.transform.position;

        if (Mathf.Abs(_pt.position.x - _tt.position.x) <= 4f)
        {
            toPos = new Vector2(_tt.position.x, 5);
        }
        else

        if (Mathf.Abs(_pt.position.x - _tt.position.x) > 4f && Mathf.Abs(_pt.position.x - _tt.position.x) <= 8f)
        {
            toPos = new Vector2(_pt.position.x - 4f, 5);
        }
        else

        if (Mathf.Abs(_pt.position.x - _tt.position.x) > 8f)
        {
            toPos = new Vector2(transform.position.x + 4f, 5);
        }

        if (toPos.x >= 14)
        {
            toPos.x = 14;
        }
        else

        if (toPos.x <= -14)
        {
            toPos.x = -14;
        }

        _flip = true;
        while (true)
        {
            count += Time.deltaTime * 6 * speed;
            _this.transform.position = Vector2.Lerp(wasPos, toPos, count);

            if (count >= 1)
            {
                _this.transform.position = toPos;

                break;
            }
            yield return null;
        }
        _flip = false;

        if (phase == 1)
        {
            if (patton == 1)
            {
                _cor = Crash4R();
                StartCoroutine(_cor);
            }
            else

            if (patton == 2)
            {
                _cor = scrapeR();
                StartCoroutine(_cor);
            }
            else

            if (patton == 3)
            {
                _cor = fall();
                StartCoroutine(_cor);
            }
        }
        else

        if (phase > 1)
        {
            _cor = Crash2R(gameObject, new Vector2(transform.position.x + 4f, 5));
            StartCoroutine(_cor);
        }
    }

    IEnumerator Crash2R(GameObject _this, Vector2 toPos)
    {
        yield return new WaitForSeconds(0.4f * rsp);
        _jb2.GetComponent<judgeball>().stomp();

        yield return new WaitForSeconds(0.6f * rsp);

        float count = 0;
        Vector2 wasPos = _this.transform.position;

        if (Mathf.Abs(_pt.position.x - _tt.position.x) <= 4f)
        {
            toPos = new Vector2(_tt.position.x, 5);
        }
        else

        if (Mathf.Abs(_pt.position.x - _tt.position.x) > 4f && Mathf.Abs(_pt.position.x - _tt.position.x) <= 8f)
        {
            toPos = new Vector2(_pt.position.x - 4f, 5);
        }
        else

        if (Mathf.Abs(_pt.position.x - _tt.position.x) > 8f)
        {
            toPos = new Vector2(transform.position.x + 4f, 5);
        }

        if (toPos.x >= 14)
        {
            toPos.x = 14;
        }
        else

        if (toPos.x <= -14)
        {
            toPos.x = -14;
        }

        while (true)
        {
            count += Time.deltaTime * 6 * speed;
            _this.transform.position = Vector2.Lerp(wasPos, toPos, count);

            if (count >= 1)
            {
                _this.transform.position = toPos;

                break;
            }
            yield return null;
        }
        _cor = Crash3R(gameObject, new Vector2(transform.position.x + 4f, 5));
        StartCoroutine(_cor);
    }

    IEnumerator Crash3R(GameObject _this, Vector2 toPos)
    {
        yield return new WaitForSeconds(0.4f * rsp);
        _jb1.GetComponent<judgeball>().Crash1R();

        yield return new WaitForSeconds(0.6f * rsp);

        float count = 0;
        Vector2 wasPos = _this.transform.position;

        if (Mathf.Abs(_pt.position.x - _tt.position.x) <= 4f)
        {
            toPos = new Vector2(_tt.position.x, 5);
        }
        else

        if (Mathf.Abs(_pt.position.x - _tt.position.x) > 4f && Mathf.Abs(_pt.position.x - _tt.position.x) <= 8f)
        {
            toPos = new Vector2(_pt.position.x - 4f, 5);
        }
        else

        if (Mathf.Abs(_pt.position.x - _tt.position.x) > 8f)
        {
            toPos = new Vector2(transform.position.x + 4f, 5);
        }

        if (toPos.x >= 14)
        {
            toPos.x = 14;
        }
        else

        if (toPos.x <= -14)
        {
            toPos.x = -14;
        }

        while (true)
        {
            count += Time.deltaTime * 6 * speed;
            _this.transform.position = Vector2.Lerp(wasPos, toPos, count);

            if (count >= 1)
            {
                _this.transform.position = toPos;

                break;
            }
            yield return null;
        }

        if (Mathf.Abs(_pt.position.x - _tt.position.x) < 1)
        {
            Stomp();
        }
        else

        {
            if (patton == 1)
            {
                _cor = Crash4R();
                StartCoroutine(_cor);
            }
            else

        if (patton == 2)
            {
                _cor = scrapeR();
                StartCoroutine(_cor);
            }
            else

        if (patton == 3)
            {
                _cor = fall();
                StartCoroutine(_cor);
            }
        }
    }

    IEnumerator Crash4R()
    {
        yield return new WaitForSeconds(0.4f * rsp);
        _jb2.GetComponent<judgeball>().Crash2R();

    }

    IEnumerator scrapeL()
    {
        yield return new WaitForSeconds(0.4f * rsp);
        _jb1.GetComponent<judgeball>().ScrapeL();
        
    }

    IEnumerator scrapeR()
    {
        yield return new WaitForSeconds(0.4f * rsp);
        _jb2.GetComponent<judgeball>().ScrapeR();

    }

    IEnumerator fall()
    {
        yield return new WaitForSeconds(0.8f * rsp);
        if (_pt.position.x < _tt.position.x)
        {
            _jb1.GetComponent<judgeball>().Fall();
        }
        else

        if (_pt.position.x > _tt.position.x)
        {
            _jb2.GetComponent<judgeball>().Fall();
        }
    }

    IEnumerator resonance()
    {
        yield return new WaitForSeconds(0.4f * rsp);
        _jb1.GetComponent<judgeball>().Resonance();
        _jb2.GetComponent<judgeball>().Resonance();
    }

    IEnumerator dash(GameObject _this, Vector2 toPos)
    {
        yield return new WaitForSeconds(0.4f * rsp);
        dash_att = true;
        float count = 0f;
        Vector2 wasPos = this.transform.position;
        
        if (toPos.x >= 14)
        {
            toPos.x = 14;
        }
        else

        if (toPos.x <= -14)
        {
            toPos.x = -14;
        }

        while (true)
        {
            count += Time.deltaTime * 6 * speed;
            _this.transform.position = Vector2.Lerp(wasPos, toPos, count);

            if (count >= 1)
            {
                _this.transform.position = toPos;

                break;
            }
            yield return null;

            
        }

        dash_att = false;
        yield return new WaitForSeconds(0.4f * rsp);
        _delay = false;
    }

    IEnumerator backstep(GameObject _this, Vector2 toPos)
    {
        float count = 0f;
        Vector2 wasPos = this.transform.position;
        int reso = 1;

        if (toPos.x >= 14)
        {
            toPos.x = 14;
        }
        else

        if (toPos.x <= -14)
        {
            toPos.x = -14;
        }

        while (true)
        {

            count += Time.deltaTime * 4 * speed;
            _this.transform.position = Vector2.Lerp(wasPos, toPos, count);

            if (count >= 0.2 && phase >= 2 && reso == 1)
            {
                if (_pt.position.x < _tt.position.x)
                {
                    reso = 0;
                    _jb1.GetComponent<judgeball>().backdown(wasPos);
                    _jb2.GetComponent<judgeball>().backup(wasPos);
                }
                else

                if (_pt.position.x > _tt.position.x)
                {
                    reso = 0;
                    _jb1.GetComponent<judgeball>().backup(wasPos);
                    _jb2.GetComponent<judgeball>().backdown(wasPos);
                }
            }

            if (count >= 1)
            {
                _this.transform.position = toPos;

                break;
            }
            yield return null;


        }

        if(phase == 1)
        {
            yield return new WaitForSeconds(0.2f * rsp);
            _delay = false;
            reso = 0;
        }
    }

    IEnumerator slow()
    {
        Time.timeScale = 0.1f;
        yield return new WaitForSeconds(0.02f);
        Time.timeScale = 1f;
    }

    IEnumerator bigball()
    {
        _jb1.GetComponent<judgeball>().hide();
        _jb2.GetComponent<judgeball>().hide();

        yield return new WaitForSeconds(1f);
        if (_pt.position.x > _tt.position.x)
        {
            GameObject bigball1 = (GameObject)Instantiate(bb, new Vector2(-17, 5), Quaternion.identity);
        }
        else

        if (_pt.position.x <= _tt.position.x)
        {
            GameObject bigball1 = (GameObject)Instantiate(bb, new Vector2(17, 5), Quaternion.identity);
        }
    }

    IEnumerator stomp()
    {
        yield return new WaitForSeconds(0.5f);
        _jb1.GetComponent<judgeball>().stomp();
        _jb2.GetComponent<judgeball>().stomp();
    }

    IEnumerator recall()
    {
        yield return new WaitForSeconds(0.2f);
        _jb1.SetActive(true);
        _jb2.SetActive(true);
        _jb1.GetComponent<judgeball>().reveal();
        _jb2.GetComponent<judgeball>().reveal();

        yield return new WaitForSeconds(1f);
        _delay = false;
    }

    IEnumerator warp()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        sprite = GetComponent<SpriteRenderer>().color;

        yield return new WaitForSeconds(0.2f);

        while (true)
        {
            sprite.a -= Time.deltaTime * 2;
            GetComponent<SpriteRenderer>().color = sprite;

            if (sprite.a <= 0)
            {
                break;
            }
            yield return null;
        }

        yield return new WaitForSeconds(0.2f);

        if (_pt.position.x <= 0)
        {
            transform.position = new Vector2(_pt.position.x + 2, 5);
        }
        else

        if (_pt.position.x > 0)
        {
            transform.position = new Vector2(_pt.position.x - 4, 5);
        }

        yield return new WaitForSeconds(0.2f);

        while (true)
        {
            sprite.a += Time.deltaTime * 4;
            GetComponent<SpriteRenderer>().color = sprite;

            if (sprite.a >= 1)
            {
                break;
            }
            yield return null;
        }

    }

    IEnumerator detach()
    {
        yield return new WaitForSeconds(0.2f);
        _jb1.GetComponent<judgeball>().detach();
        _jb2.GetComponent<judgeball>().detach();
    }

    IEnumerator judge()
    {
        GetComponent<CapsuleCollider2D>().enabled = false;
        while (true)
        {
            sprite.a -= Time.deltaTime;
            GetComponent<SpriteRenderer>().color = sprite;

            if (sprite.a <= 0)
            {
                break;
            }
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        transform.position = new Vector2(0, 5);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        judgemode.SetActive(true);

        while (true)
        {
            sprite.a += Time.deltaTime;
            judgemode.GetComponent<SpriteRenderer>().color = sprite;

            if (sprite.a >= 1)
            {
                break;
            }
            yield return null;
        }

        yield return new WaitForSeconds(1f);
        GManager.instance.GetComponent<GManager>().Cam_size(13, 0.2f);
        GManager.instance.GetComponent<GManager>().Cam_move(new Vector3(0, 13, -10), 0.2f);
        _jb1.GetComponent<judgeball>().judge1();
        _jb2.GetComponent<judgeball>().judge1();
        float count = 0;

        while (true)
        {
            count += Time.deltaTime;
            transform.position = Vector2.Lerp(new Vector2(0, 4.5f), new Vector2(0, 13f), count);

            if (count >= 1)
            {
                transform.position = new Vector2(0, 13f);

                break;
            }
            yield return null;
        }

    }

    IEnumerator judge_finish(GameObject spear)
    {
        yield return new WaitForSeconds(1f);
        //transform.position = new Vector2(_pt.position.x, 17);

        while (true)
        {
            sprite.a -= Time.deltaTime;
            judgemode.GetComponent<SpriteRenderer>().color = sprite;

            if (sprite.a <= 0)
            {
                break;
            }
            yield return null;
        }
        GManager.instance.GetComponent<GManager>().Cam_return();

        yield return new WaitForSeconds(0.5f);
        transform.position = new Vector2(_tt.position.x, 5.5f);
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<CapsuleCollider2D>().enabled = true;
        judgemode.SetActive(false);

        while (true)
        {
            sprite.a += Time.deltaTime;
            GetComponent<SpriteRenderer>().color = sprite;

            if (sprite.a >= 1)
            {
                Debug.Log("12");
                break;
            }
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        //transform.position = new Vector2(_tt.position.x, 5.5f);
        _jb1.GetComponent<judgeball>().judgeF();
        _jb2.GetComponent<judgeball>().judgeF();
        yield return new WaitForSeconds(1f);
        _delay = false;
    }
}
