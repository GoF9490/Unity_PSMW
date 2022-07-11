using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerPhy : MonoBehaviour
{
    public Cha_Direction CD;

    public Rigidbody2D _rb;
    public Transform _tf;
    public playerCon _pc;
    public playerAnim _anim;

    public GameObject _att1;
    public GameObject _att2;
    public GameObject _att3;
    public GameObject _jatt;
    //public PhysicsMaterial2D _playerPM;

    public bool _jump = false;
    public bool _jDash = false;
    public bool _invulnerable = false;
    public bool _delay = false;
    public bool _attdelay = false;

    //public bool _run = false;
    //public bool _runL = false;
    //public bool _runR = false;
    public bool _move = false;
    public bool _fliphold = false;

    public bool _dodge = false;
    public bool _cancel = false;
    public bool _descent = false;
    public bool _hit = false;
    public bool _overlap = false;

    public bool _fire = false;

    public float _speed = 10f;
    public float _thrust = 20f;
    public float _runcheck = 0.4f;
    public float _dodgetime = 0.2f;

    public int _attcount = 0;
    public int _attcombo = 0;
    public int _jumpcount = 0;

    public GameObject _colbox;
    public GameObject _hitbox;

    public IEnumerator _cor;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _tf = GetComponent<Transform>();
        _anim = GetComponent<playerAnim>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            _anim._Reset();
        }

        _rb.freezeRotation = true;

        if (Input.GetKeyDown(KeyCode.X) && _dodge == false && _hit == false)
        {
            if (_jump == false || _jump == true && Input.GetKey(KeyCode.DownArrow))
            {
                Dodge();
            }
            else

            if (_jump == true &! Input.GetKey(KeyCode.DownArrow) && _jumpcount < 2)
            {
                if (CD == Cha_Direction.Left & !Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    _jumpcount++;
                    _rb.velocity = new Vector2(0, 0);
                    _rb.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                    _rb.AddForce(Vector2.left * 20, ForceMode2D.Impulse);
                }
                else

                if (CD == Cha_Direction.Right & !Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    _jumpcount++;
                    _rb.velocity = new Vector2(0, 0);
                    _rb.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                    _rb.AddForce(Vector2.right * 20, ForceMode2D.Impulse);
                }
            }
        }

        if (_fliphold == false)
        {
            if (CD == Cha_Direction.Left)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else

        if (CD == Cha_Direction.Right)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            _attcount++;
        }

        if (_jumpcount >= 2 && _jump == false)
        {
            _jumpcount = 0;
        }

        if (_delay == false)
        {
            //move
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    CD = Cha_Direction.Left;
                    _move = true;

                    //if (_run == false)
                    {
                        _tf.Translate(Vector3.left * Time.deltaTime * _speed);
                    }
                    //else

                    //if (_run == true)
                    //{
                    //    _tf.Translate(Vector3.left * Time.deltaTime * _speed);
                    //}
                }
                else

                if (Input.GetKey(KeyCode.RightArrow))
                {
                    CD = Cha_Direction.Right;
                    _move = true;

                    //if (_run == false)
                    {
                        _tf.Translate(Vector3.right * Time.deltaTime * _speed);
                    }
                    //else

                    //if (_run == true)
                    //{
                    //    _tf.Translate(Vector3.right * Time.deltaTime * _speed);
                    //}
                }
                else

                {
                    _move = false;
                }
            }

            //jump
            {
                if (Input.GetKeyDown(KeyCode.V) & !Input.GetKey(KeyCode.DownArrow) && _jumpcount < 2)
                {
                    _jumpcount++;
                    _rb.velocity = new Vector2(0, 0);
                    _rb.AddForce(Vector2.up * _thrust, ForceMode2D.Impulse);
                }
                else

                if (Input.GetKeyDown(KeyCode.V) && Input.GetKey(KeyCode.DownArrow) && _descent == true)
                {
                    _jumpcount++;
                    _delay = true;
                    GetComponent<CapsuleCollider2D>().isTrigger = true; //istrig
                    _cor = delay_S();
                    StartCoroutine(_cor);
                }
            }

            //attack
            {
                if (_attcount > 0 && _attdelay == false && _dodge == false)
                {
                    _attcount = 0;
                    _delay = true;
                    Invoke("Attack", 0.05f);
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Ground2"))
        {
            _descent = true;
        }
        else

        {
            _descent = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Ground") || collision.gameObject.tag.Equals("Ground2"))
        {
            _jump = true;
            _descent = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("explosive") && _invulnerable == false)
        {
            _invulnerable = true;
            _hit = true;
            int damage = collision.gameObject.GetComponent<eavalue>().damage;
            float thrx = collision.gameObject.GetComponent<eavalue>().thrx;
            float thry = collision.gameObject.GetComponent<eavalue>().thry;
            Vector2 egicenter = collision.gameObject.GetComponent<Transform>().position;
            Explosive(damage, thrx, thry, egicenter);
        }
        else

        if (collision.gameObject.tag.Equals("throw") && _invulnerable == false)
        {
            _invulnerable = true;
            _hit = true;
            int damage = collision.gameObject.GetComponent<eavalue>().damage;
            float thrx = collision.gameObject.GetComponent<eavalue>().thrx;
            float thry = collision.gameObject.GetComponent<eavalue>().thry;
            Vector2 egicenter = collision.gameObject.GetComponent<Transform>().position;
            Throw(damage, thrx, thry, egicenter);
        }
        else

        if (collision.gameObject.tag.Equals("press") && _invulnerable == false)
        {
            _invulnerable = true;
            _hit = true;
            int damage = collision.gameObject.GetComponent<eavalue>().damage;
            float thrx = collision.gameObject.GetComponent<eavalue>().thrx;
            float thry = collision.gameObject.GetComponent<eavalue>().thry;
            Vector2 egicenter = collision.gameObject.GetComponent<Transform>().position;
            Press(damage, thrx, thry, egicenter);
        }
        else

        if (collision.gameObject.tag.Equals("bash") && _invulnerable == false)
        {
            _invulnerable = true;
            int damage = collision.gameObject.GetComponent<eavalue>().damage;
            float dir = _rb.position.x - collision.GetComponent<Transform>().position.x;
            StartCoroutine(Cha_Hit(damage, dir));
        }
    }

    public void Attack()
    {
        if (_attdelay == false)
        {
            if (_jump == false && _dodge == false)
            {
                if (_attcombo == 0)
                {
                    _attcombo++;
                    _delay = true;
                    _attdelay = true;
                    _cor = att1_st();
                    StartCoroutine(_cor);
                }
                else

                if (_attcombo == 1)
                {
                    _attcombo++;
                    _delay = true;
                    _attdelay = true;
                    _cor = att2_st();
                    StartCoroutine(_cor);
                }
                else

                if (_attcombo == 2)
                {
                    _attcombo++;
                    _delay = true;
                    _attdelay = true;
                    _cor = att3_st();
                    StartCoroutine(_cor);
                }
            }
            else

            if (_jump == true && _dodge == false)
            {
                //if (_jDash == false)
                {
                    _delay = true;
                    _attdelay = true;
                    _cor = jump_att();
                    StartCoroutine(_cor);
                }
            }

            else
            {
                _cor = att_end();
                StartCoroutine(_cor);
            }
        }
    }

    void Dodge()
    {
        _delay = true;
        _invulnerable = true;
        _anim.Dodge();
        if (_cor != null)
        {
            StopCoroutine(_cor);
            _cancel = true;
        }

        if (_jump == false)
        {
            _attdelay = true;
            _cor = dodge();
            StartCoroutine(_cor);
        }
        else

        if (_jump == true && _jumpcount < 2)
        {
            _jumpcount++;
            _dodge = true;
            _cor = dodge();
            StartCoroutine(_cor);
        }
    }

    public void Land()
    {
        if (GetComponent<CapsuleCollider2D>().isTrigger == false)
        {
            if (_cor == null)
            {
                if (_hit == false)
                {
                    _jump = false;
                    _delay = true;
                    _attdelay = true;
                    _dodge = false;
                    _jumpcount = 0;
                    _cor = landing();
                    StartCoroutine(_cor);
                }
                else

            if (_hit == true)
                {
                    StartCoroutine(getup());
                }
            }
        }
    }

    public void Explosive(int damage, float thrx, float thry, Vector2 egicenter)
    {
        _anim.hit1(); //
        _cancel = true;
        if (_cor != null)
        {
            StopCoroutine(_cor);
            _cor = null;
        }
        _invulnerable = true;
        _delay = true;
        _pc._life -= damage;

        if (transform.position.x <= egicenter.x)
        {
            _rb.AddForce(Vector2.left * thrx, ForceMode2D.Impulse);
        }
        else

        if (transform.position.x > egicenter.x)
        {
            _rb.AddForce(Vector2.right * thrx, ForceMode2D.Impulse);
        }

        if (transform.position.y >= egicenter.y)
        {
            _rb.AddForce(Vector2.up * thry, ForceMode2D.Impulse);
        }
        else

        if (transform.position.y < egicenter.y)
        {
            _rb.AddForce(Vector2.down * thry, ForceMode2D.Impulse);
        }
        StartCoroutine(getup2());
    }

    public void Throw(int damage, float thrx, float thry, Vector2 egicenter)
    {
        _anim.hit1(); //
        _cancel = true;
        if (_cor != null)
        {
            StopCoroutine(_cor);
            _cor = null;
        }
        _invulnerable = true;
        _delay = true;
        _pc._life -= damage;

        if (transform.position.x <= egicenter.x)
        {
            _rb.AddForce(Vector2.left * thrx, ForceMode2D.Impulse);
        }
        else

        if (transform.position.x > egicenter.x)
        {
            _rb.AddForce(Vector2.right * thrx, ForceMode2D.Impulse);
        }
        _rb.AddForce(Vector2.up * thry, ForceMode2D.Impulse);
    }

    public void Press(int damage, float thrx, float thry, Vector2 egicenter)
    {
        _anim.hit1(); //
        _cancel = true;
        if (_cor != null)
        {
            StopCoroutine(_cor);
            _cor = null;
        }
        _invulnerable = true;
        _delay = true;
        _pc._life -= damage;

        if (transform.position.x <= egicenter.x)
        {
            _rb.AddForce(Vector2.left * thrx, ForceMode2D.Impulse);
        }
        else

        if (transform.position.x > egicenter.x)
        {
            _rb.AddForce(Vector2.right * thrx, ForceMode2D.Impulse);
        }

        if (_jump == true)
        {
            _rb.AddForce(Vector2.down * thry, ForceMode2D.Impulse);
        }
        StartCoroutine(getup2());
    }

    void delayend()
    {
        _dodge = false;
        _delay = false;
        //_run = false;
        _attdelay = false;
        _attcount = 0;
        _attcombo = 0;
        _cor = null;
    }

    //IEnumerator
    IEnumerator dodge()
    {
        _dodge = true;
        _colbox.GetComponent<CapsuleCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.05f);
        StartCoroutine(dodge_inv());

        if (_jump == false)
        {
            _delay = true;
            if (CD == Cha_Direction.Left & !Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _rb.velocity = new Vector2(0, 0);
                _rb.AddForce(Vector2.left * 20, ForceMode2D.Impulse);
            }
            else

            if (CD == Cha_Direction.Right & !Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                _rb.velocity = new Vector2(0, 0);
                _rb.AddForce(Vector2.right * 20, ForceMode2D.Impulse);
            }

            yield return new WaitForSeconds(0.5f);
            _overlap = true;

            yield return new WaitForSeconds(0.2f);
            _overlap = false;
            _colbox.GetComponent<CapsuleCollider2D>().enabled = true;

            if (_cancel == true)
            {
                _cancel = false;
                yield return new WaitForSeconds(0.3f);
            }
            _anim._stay();
            yield return new WaitForSeconds(0.1f);
            delayend();
        }
        else

        if (_jump == true)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                _delay = true;
                _rb.AddForce(Vector2.down * 20, ForceMode2D.Impulse);
            }
            else

            if (!Input.GetKeyDown(KeyCode.DownArrow))
            {
                
            }
        }
    }

    IEnumerator dodge_inv()
    {
        _invulnerable = true;
        yield return new WaitForSeconds(_dodgetime);

        _invulnerable = false;
        _anim.Dodge_e();
    }

    IEnumerator landing()
    {
        _anim._stay();
        yield return new WaitForSeconds(0.1f);
        _delay = false;
        _fliphold = false;
        _rb.velocity = new Vector2(0, 0);
        _cor = null;
        yield return new WaitForSeconds(0.1f);
        _attdelay = false;
    }

    IEnumerator att1_st()
    {
        yield return null;
        _anim.Att1();
        Vector3 _attpos = new Vector3(0, 0, 0);

        yield return new WaitForSeconds(0.1f);

        if (CD == Cha_Direction.Left)
        {
            _attpos = new Vector3(transform.position.x - 0.5f, transform.position.y, 0);

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                _rb.AddForce(Vector2.left * 10, ForceMode2D.Impulse);
            }
        }
        else

        if (CD == Cha_Direction.Right)
        {
            _attpos = new Vector3(transform.position.x + 0.5f, transform.position.y, 0);

            if (Input.GetKey(KeyCode.RightArrow))
            {
                _rb.AddForce(Vector2.right * 10, ForceMode2D.Impulse);
            }
        }

        GameObject att1 = GameObject.Instantiate(_att1, _attpos, Quaternion.identity);
        att1.transform.parent = transform;

        yield return new WaitForSeconds(0.15f);
        _attdelay = false;
        float _count = 0;

        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            _count += Time.deltaTime;

            if (_attcount > 0)
            {
                _attcount = 0;
                Invoke("Attack", 0.05f);
                break;
            }
            else

            if (_count > 0.1f)
            {
                _cor = att_end();
                StartCoroutine(_cor);
                break;
            }
        }
        Destroy(att1);
    }

    IEnumerator att2_st()
    {
        yield return null;
        _anim.Att2();
        Vector3 _attpos = new Vector3(0, 0, 0);

        yield return new WaitForSeconds(0.2f);

        if (CD == Cha_Direction.Left)
        {
            _attpos = new Vector3(transform.position.x - 1, transform.position.y, 0);

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                _rb.AddForce(Vector2.left * 10, ForceMode2D.Impulse);
            }
        }
        else

        if (CD == Cha_Direction.Right)
        {
            _attpos = new Vector3(transform.position.x + 1, transform.position.y, 0);

            if (Input.GetKey(KeyCode.RightArrow))
            {
                _rb.AddForce(Vector2.right * 10, ForceMode2D.Impulse);
            }
        }

        GameObject att2 = GameObject.Instantiate(_att2, _attpos, Quaternion.identity);
        att2.transform.parent = transform;

        yield return new WaitForSeconds(0.2f);
        _attdelay = false;
        float _count = 0;

        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            _count += Time.deltaTime;

            if (_attcount > 0)
            {
                _attcount = 0;
                Invoke("Attack", 0.05f);
                break;
            }
            else

            if (_count > 0.15f)
            {
                _cor = att_end();
                StartCoroutine(_cor);
                break;
            }
        }
        Destroy(att2);
    }

    IEnumerator att3_st()
    {
        yield return null;
        _anim.Att3();

        yield return new WaitForSeconds(0.1f);

        float _count = 0;
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            _count += Time.deltaTime;

            if (_fire == true)
            {
                GameObject att3 = GameObject.Instantiate(_att3, transform.position, Quaternion.identity);

                if (CD == Cha_Direction.Left)
                {
                    att3.transform.position = new Vector3(transform.position.x - 3.6f, transform.position.y - 0.1f, 0);
                    att3.transform.Rotate(new Vector3(0, 180, 0));

                    _rb.AddForce(Vector2.right * 10, ForceMode2D.Impulse);
                }
                else

                if (CD == Cha_Direction.Right)
                {
                    att3.transform.position = new Vector3(transform.position.x + 3.6f, transform.position.y - 0.1f, 0);
                    //att3.GetComponent<SpriteRenderer>().flipX = false;

                    _rb.AddForce(Vector2.left * 10, ForceMode2D.Impulse);
                }

                break;
            }
            else

            if (_count > 2)
            {
                break;
            }
        }

        yield return new WaitForSeconds(1f);
        _attdelay = false;
        _fire = false;

        _cor = att_end();
        StartCoroutine(_cor);
    }

    IEnumerator jump_att()
    {
        yield return null;
        _rb.velocity = new Vector2(_rb.velocity.x, 0);
        _anim.J_att();
        _fliphold = true;
        _rb.AddForce(Vector2.up * 13, ForceMode2D.Impulse);
        GameObject jatt1 = GameObject.Instantiate(_jatt, transform.position, Quaternion.identity);
        if (CD == Cha_Direction.Left)
        {
            _rb.AddForce(Vector2.left * 2, ForceMode2D.Impulse);
            jatt1.transform.Rotate(new Vector3(0, 180, 0));
        }
        else

        if (CD == Cha_Direction.Right)
        {
            _rb.AddForce(Vector2.right * 2, ForceMode2D.Impulse);
        }

        yield return new WaitForSeconds(0.1f);
        _anim.J_att_end();
        _delay = false;
        _cor = null;
    }

    IEnumerator att_end()
    {
        yield return new WaitForSeconds(0.1f);
        _attcount = 0;
        _attcombo = 0;
        _anim.Att_end();
        _attdelay = false;
        _delay = false;
        _cor = null;
    }

    IEnumerator delay_S()
    {
        yield return new WaitForSeconds(0.1f);
        _delay = false;
    }

    IEnumerator Cha_Hit(int damage, float dir)
    {
        _delay = true;
        _pc._life -= damage;
        _rb.velocity = new Vector2(0, 0);
        _rb.AddForce(Vector2.up * 15.0f, ForceMode2D.Impulse);

        if (dir < 0.0f)
        {
            CD = Cha_Direction.Right;
            _rb.AddForce(Vector2.left * 5.0f, ForceMode2D.Impulse);

        }
        else

        if (dir > 0.0f)
        {
            CD = Cha_Direction.Left;
            _rb.AddForce(Vector2.right * 5.0f, ForceMode2D.Impulse);

        }

        yield return new WaitForSeconds(0.5f);
        _delay = false;
        _invulnerable = false;
    }

    IEnumerator getup()
    {
        _jump = false;
        _delay = true;
        _attdelay = true;
        _dodge = false;
        _jumpcount = 0;
        yield return new WaitForSeconds(0.5f);
        _rb.velocity = new Vector2(0, 0);

        yield return new WaitForSeconds(1f);
        _dodge = false;
        _hit = false;
        _delay = false;;
        _attdelay = false;
        _attcombo = 0;
        _attcount = 0;
        _overlap = false;
        _fliphold = false;
        _colbox.GetComponent<CapsuleCollider2D>().enabled = true;
        _anim._Reset();

        yield return new WaitForSeconds(0.5f);
        _invulnerable = false;
    }

    IEnumerator getup2()
    {
        if (_jump == false)
        {
            yield return new WaitForSeconds(0.5f);
            if (_jump == false && _hit == true)
            {
                _rb.velocity = new Vector2(0, 0);
                _jump = false;
                _delay = true;
                _attdelay = true;
                _dodge = false;
                _jumpcount = 0;

                yield return new WaitForSeconds(0.2f);
                _cancel = false;
                _hit = false;
                _delay = false;
                _dodge = false;
                _attdelay = false;
                _attcombo = 0;
                _attcount = 0;
                _overlap = false;
                _fliphold = false;
                _colbox.GetComponent<CapsuleCollider2D>().enabled = true;
                _anim._Reset();

                yield return new WaitForSeconds(0.5f);
                _invulnerable = false;
            }
        }
    }
}
