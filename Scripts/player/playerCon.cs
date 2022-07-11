using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum Cha_Direction
{
    Right,
    Left
}

public enum Cha_Attack
{
    None,
    Att1,
    Att2,
    Att3
}

public class playerCon : MonoBehaviour
{

    public playerPhy _pp;

    public Rigidbody2D _rb;
    public Transform _tf;

    public bool _moveL = false;
    public bool _moveR = false;
    public bool _assassinready = true;
    public bool _archerready = true;
    public bool _archercount = false;
    public bool _lookatme = false;
    public bool _dot = false;

    public float _archercool = 0f;
    public float _assassincool = 0f;

    public int _life = 100;
    public int _soul = 32;

    public Animator animator;

    public GameObject _img;
    public GameObject _camera;

    public GameObject _archer;
    public GameObject _assassin;

    public Slider _hpslider;

    public IEnumerator _cor;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _tf = GetComponent<Transform>();

        if (_hpslider == null)
        {
            _hpslider = GameObject.Find("playerHP").GetComponent<Slider>();
        }
        _camera = GameObject.Find("Main Camera");
        //animator = _img.GetComponent<Animator>();
    }

    private void Update()
    {
        _hpslider.value = _life;

        if (_life < 0)
        {
            _life = 0;
        }

        if (_pp._delay == false)
        {
            //surport
            {
                //archer
                if (Input.GetKeyDown(KeyCode.A))
                {
                    if (_archerready == true && _archercount == false)
                    {
                        _archerready = false;
                        _archercount = true;
                        _archercool = 30;
                        //invoke()
                    }
                }

                if (_archercount == false && _archerready == false)
                {
                    _archercool -= Time.deltaTime;
                }

                if (_archercool <= 0 && _archerready == false)
                {
                    _archerready = true;
                }

                //assassin
                if (Input.GetKeyDown(KeyCode.S))
                {
                    if (_assassinready == true)
                    {
                        _assassinready = false;
                        _assassincool = 15f;
                        //Invoke()
                    }
                }

                if (_assassincool > 0f)
                {
                    _assassincool -= Time.deltaTime;
                }

                if (_assassincool <= 0 && _assassinready == false)
                {
                    _assassinready = true;
                }
            }

        }
    }

    void Archer()
    {
        if (_pp.CD == Cha_Direction.Left)
        {

        }
        else

        if (_pp.CD == Cha_Direction.Right)
        {

        }
    }

    void Assassin()
    {
        if (_pp._jump == true)
        {
            GameObject assassin = (GameObject)Instantiate(_assassin, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        }
        else

        {
            GameObject assassin = (GameObject)Instantiate(_assassin, new Vector2(transform.position.x, transform.position.y + 5f), Quaternion.identity);
        }

    }

    public void Dot(int damage, float interval)
    {
        StartCoroutine(Dotdmg(damage, interval));
    }

    IEnumerator Dotdmg(int damage, float interval)
    {
        while (true)
        {
            if (_pp._invulnerable == false)
            {
                _life -= damage;
                yield return new WaitForSeconds(interval);
            }

            if (_dot == false)
            {
                break;
            }
            yield return null;
        }
    }

}
