using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public enum Cha_Move
{
    Stand,
    Move,
    Run
}




public class N_Cha_Controller : MonoBehaviour
{
    public Cha_Attack CA;
    public Cha_Direction CD;
    public Cha_Move CM;

    public bool Cha_Jump = false;
    public bool Cha_Invulnerable = false;
    public bool Att_Delay = false;
    public bool Cha_Delay = false;
    public bool Run = false;
    public bool RunR = false;
    public bool RunL = false;
    public bool CamR = false;
    public bool CamL = false;
    public bool Archer_Ready = true;
    public bool Archer_Count = false;
    public bool Dash = false;
    public bool C_Dash = false;
    public bool g2 = false;
    public bool touch = false;
    public bool lookatme = true;
    public bool Hit = false;
    public bool dot = false;

    public bool assassin_ready = true;

    public float Speed = 20.0f;
    public float Dir_Check = 0.0f; // 없앨것
    public float thrust = 20.0f;
    public float Jum_Count = 0.0f;
    public float Att_Count = 0.0f;
    public float Run_Check = 0.4f;
    public float Archer_Cool = 0.0f;
    public float assassin_cool = 0f;

    public int Cha_Life = 100;

    public Rigidbody2D rb;
    public Transform tf;

    public Animator animator;

    public GameObject img;
    public GameObject Att1;
    public GameObject Att2;
    public GameObject Att3;
    
    public GameObject Camera;

    public GameObject mobcol;

    public GameObject ArcherL;
    public GameObject ArcherR;

    public GameObject assassin;

    public Slider hp_Slider;

    public IEnumerator _coroutine;




    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();

        Camera = GameObject.Find("Main Camera");
        animator = img.GetComponent<Animator>();
    }

    private void Update()
    {
        if (Cha_Jump == false && Run == false && Cha_Delay == false)
        {
            animator.SetBool("Att_end", false);
            animator.SetBool("run", false);
            animator.SetBool("stay", true);

        }
        else

        if (Cha_Jump == false && Run == true && Cha_Delay == false)
        {
            animator.SetBool("stay", false);
            animator.SetBool("run", true);
        }
        else

        {
            animator.SetBool("stay", false);
            animator.SetBool("run", false);
        }

        //캔슬대쉬
        if (Input.GetKeyDown(KeyCode.LeftShift) && C_Dash == false && Cha_Jump == false)
        {
            Cha_Delay = true;
            Invoke("C_dash", 0.1f);
        }
        else

        if (Input.GetKeyDown(KeyCode.LeftShift) && C_Dash == false && Cha_Jump == true)
        {
            Cha_Delay = true;
            C_dash();
        }

        if (C_Dash == true && mobcol.GetComponent<mobcol>().touch == true && touch == true)
        {
            if (mobcol.GetComponent<mobcol>().left == true)
            {
                tf.Translate(Vector2.left * Time.deltaTime * 30);
            }
            else

            if (mobcol.GetComponent<mobcol>().right == true)
            {
                tf.Translate(Vector2.right * Time.deltaTime * 30);
            }
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            StopCoroutine(_coroutine);
        }

        if (Cha_Delay == false)
        {
            

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                RunL = false;

            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                RunR = false;

            }

            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                RunR = true;
                RunL = false;

            } else

            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                RunL = true;
                RunR = false;

            }
            //런 체크
            if (RunR == true)
            {
                Run_Check -= Time.deltaTime;

                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    Run = true;

                }


            } else

            if (RunL == true)
            {
                Run_Check -= Time.deltaTime;

                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    Run = true;

                }

            }

            //런체크 초기화
            if (Run_Check <= 0 & !Input.GetKey(KeyCode.RightArrow) & !Input.GetKey(KeyCode.LeftArrow))
            {
                Run = false;
                RunL = false;
                RunR = false;
                Run_Check = 0.4f;
            }


        }

        //공격 키 입력
        if (Input.GetKeyDown(KeyCode.C))
        {
            Att_Count = Att_Count + 1f;

        }

        if (Att_Count > 0 && Cha_Delay == false && Att_Delay == false && Dash == false )
        {
            Att_Count = 0f;
            Cha_Delay = true;
            Invoke("Attack", 0.03f);
        }
        else

        if (Att_Count > 0 && Att_Delay == false && Dash == true && Cha_Jump == false)
        {
            StopCoroutine(_coroutine);
            Att_Count = 0f;
            Cha_Delay = true;
            rb.velocity = new Vector2(0, 0);
            Invoke("Attack", 0.03f);
        }
        else

        if (Att_Count > 0 && Att_Delay == false && Dash == true && Cha_Jump == true)
        {;
            Att_Count = 0f;
            rb.velocity = new Vector2(0, 0);
            Invoke("Attack", 0.03f);
        }

        //궁수

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (Archer_Ready == true && Archer_Count == false)
            {
                Archer_Ready = false;
                Archer_Count = true;
                Archer_Cool = 30.0f;
                Invoke("Archer", 0.05f);

            }

        }

        if (Archer_Count == false && Archer_Ready == false)
        {
            Archer_Cool -= Time.deltaTime;

        }

        if (Archer_Cool <= 0.0f && Archer_Ready == false)
        {
            Archer_Ready = true;

        }

        //도적

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (assassin_ready == true)
            {
                assassin_ready = false;
                assassin_cool = 15f;
                Invoke("Assassin", 0.05f);

            }
        }

        if (assassin_cool > 0f)
        {
            assassin_cool -= Time.deltaTime;
        }

        if (assassin_cool <= 0f && assassin_ready == false)
        {
            assassin_ready = true;
        }

        //hp슬라이더
        hp_Slider.value = Cha_Life;

        //점프
        if (Input.GetKeyDown(KeyCode.V) & !Input.GetKey(KeyCode.DownArrow) && Jum_Count < 2.0f && Cha_Delay == false)
        {
            Jum_Count = Jum_Count + 1.0f;
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(Vector2.up * thrust, ForceMode2D.Impulse);

        }
        else
        if (Input.GetKeyDown(KeyCode.V) && Input.GetKey(KeyCode.DownArrow) && Jum_Count < 2.0f && Cha_Delay == false && g2 == true)
        {
            Jum_Count = Jum_Count + 1f;
           GetComponent<CapsuleCollider2D>().isTrigger = true;
        }

        if (Input.GetKeyDown(KeyCode.X) && CD == Cha_Direction.Right && Cha_Jump == true && Cha_Delay == false && Jum_Count < 2.0f)
        {
            Jum_Count = Jum_Count + 1.0f;
            Dash = true;
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
            rb.AddForce(Vector2.right * 15, ForceMode2D.Impulse);

        } else

        if (Input.GetKeyDown(KeyCode.X) && CD == Cha_Direction.Left && Cha_Jump == true && Cha_Delay == false && Jum_Count < 2.0f)
        {
            Jum_Count = Jum_Count + 1.0f;
            Dash = true;
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
            rb.AddForce(-Vector2.right * 15, ForceMode2D.Impulse);

        }


        //대쉬
        if (Input.GetKeyDown(KeyCode.X) && Cha_Jump == false && Cha_Delay == false && Dash == false)
        {
            Cha_Delay = true;
            _coroutine = Dash_cool();
            StartCoroutine(_coroutine);

        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            DataController.instance.GetComponent<DataController>().SaveGameData();
        }
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            DataController.instance.GetComponent<DataController>().playerData.soul = 10;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            DataController.instance.GetComponent<DataController>().LoadGameData();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Test 1");
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            SceneManager.LoadScene("Test");
        }



    }

    private void FixedUpdate()
    {
        //회전 고정
        rb.freezeRotation = true;

        //이동
        if (Cha_Delay == false)
        {

            float h = Input.GetAxis("Horizontal"); // 좌 우 방향키 입력
            float v = Input.GetAxis("Vertical"); // 위 아래 방향키 입력

            h = h * Time.deltaTime;
            v = v * Time.deltaTime;

            //좌우 방향
            if (h < 0.0f)
            {
                CD = Cha_Direction.Left;

            }
            else

            if (h > 0.0f)
            {
                CD = Cha_Direction.Right;

            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                
                RunL = false;

                if (Run == false)
                {
                    transform.Translate(Vector2.right * Speed * Time.deltaTime * 0.5f);

                }
                else

                if (Run == true)
                {
                    transform.Translate(Vector2.right * Speed * Time.deltaTime);

                }

            } else

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                
                RunR = false;

                if (Run == false)
                {
                    transform.Translate(-Vector2.right * Speed * Time.deltaTime * 0.5f);

                } else

                if (Run == true)
                {
                    transform.Translate(-Vector2.right * Speed * Time.deltaTime);

                }


            }

            


        }

        // 카메라 
        if (CD == Cha_Direction.Left)
        {
            CamR = false;

        } else

        if (CD == Cha_Direction.Right)
        {
            CamL = false;

        }

        if (lookatme == true)
        {
            if (Camera.GetComponent<Transform>().position.x - tf.position.x < -4.5f)
            {
                //Camera.transform.Translate(Vector2.right * 25f * Time.deltaTime);
                Camera.transform.position = new Vector3(rb.position.x - 4.5f, Camera.transform.position.y, -10);

            }
            else

            if (Camera.GetComponent<Transform>().position.x - tf.position.x > 4.5f)
            {
                //Camera.transform.Translate(-Vector2.right * 25f * Time.deltaTime);
                Camera.transform.position = new Vector3(rb.position.x + 4.5f, Camera.transform.position.y, -10);
            }

            if (Camera.GetComponent<Transform>().position.y - tf.position.y < -1.5f)
            {
                //Camera.transform.Translate(Vector2.up * 25f * Time.deltaTime);
                Camera.transform.position = new Vector3(Camera.transform.position.x, rb.position.y - 1.5f, -10);

            }
            else

            if (Camera.GetComponent<Transform>().position.y - tf.position.y > 4.5f)
            {
                //Camera.transform.Translate(-Vector2.up * 25f * Time.deltaTime);
                Camera.transform.position = new Vector3(Camera.transform.position.x, rb.position.y + 4.5f, -10);
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //점프 상태
        if (collision.gameObject.tag.Equals("Ground") || collision.gameObject.tag.Equals("Ground2"))
        {
            Cha_Jump = true;
            g2 = false;

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Ground2"))
        {
            g2 = true;
        }
        else
        {
            g2 = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        //피격
        if (collision.gameObject.tag.Equals("Enemy_Att"))
        {
            if (Cha_Invulnerable == false)
            {


                Cha_Delay = true;
                Cha_Invulnerable = true;
                Dir_Check = rb.position.x - collision.GetComponent<Transform>().position.x;
                //Cha_Life = Cha_Life - 10f; // 임시 데미지
                rb.velocity = new Vector2(0, 0);
                rb.AddForce(Vector2.up * 15.0f, ForceMode2D.Impulse);

                if (Dir_Check < 0.0f)
                {
                    CD = Cha_Direction.Right;
                    rb.AddForce(Vector2.left * 5.0f, ForceMode2D.Impulse);

                }
                else

                if (Dir_Check > 0.0f)
                {
                    CD = Cha_Direction.Left;
                    rb.AddForce(Vector2.right * 5.0f, ForceMode2D.Impulse);

                }

                //StartCoroutine(Cha_Hit());

            }

        }

        if (collision.gameObject.tag.Equals("explosive") && Cha_Invulnerable == false)
        {
            Cha_Invulnerable = true;
            Hit = true;
            int damage = collision.gameObject.GetComponent<eavalue>().damage;
            float thrx = collision.gameObject.GetComponent<eavalue>().thrx;
            float thry = collision.gameObject.GetComponent<eavalue>().thry;
            Vector2 egicenter = collision.gameObject.GetComponent<Transform>().position;
            _explosive(damage, thrx,thry, egicenter);
        }
        else

        if (collision.gameObject.tag.Equals("throw") && Cha_Invulnerable == false)
        {
            Cha_Invulnerable = true;
            Hit = true;
            int damage = collision.gameObject.GetComponent<eavalue>().damage;
            float thrx = collision.gameObject.GetComponent<eavalue>().thrx;
            float thry = collision.gameObject.GetComponent<eavalue>().thry;
            Vector2 egicenter = collision.gameObject.GetComponent<Transform>().position;
            _throw(damage, thrx, thry, egicenter);
        }
        else

        if (collision.gameObject.tag.Equals("press") && Cha_Invulnerable == false)
        {
            Cha_Invulnerable = true;
            Hit = true;
            int damage = collision.gameObject.GetComponent<eavalue>().damage;
            float thrx = collision.gameObject.GetComponent<eavalue>().thrx;
            float thry = collision.gameObject.GetComponent<eavalue>().thry;
            Vector2 egicenter = collision.gameObject.GetComponent<Transform>().position;
            _press(damage, thrx, thry, egicenter);
        }
        else

        if (collision.gameObject.tag.Equals("bash") && Cha_Invulnerable == false)
        {
            Cha_Invulnerable = true;
            int damage = collision.gameObject.GetComponent<eavalue>().damage;
            float dir = rb.position.x - collision.GetComponent<Transform>().position.x;
            StartCoroutine(Cha_Hit(damage, dir));
        }
    }

    void Attack()
    {

        //오른쪽
        if (CD == Cha_Direction.Right && Att_Delay == false)
        {
            if (Cha_Jump == false && Run == false && Dash == false)
            {
                if (CA == Cha_Attack.None)
                {
                    CA = Cha_Attack.Att1;
                    Cha_Delay = true;
                    Att_Delay = true;
                    animator.SetTrigger("Att1");
                    GameObject att1 = (GameObject)Instantiate(Att1, new Vector3(this.transform.position.x + 2f, this.transform.position.y + 0.5f, -5), Quaternion.identity);
                    att1.transform.eulerAngles = new Vector3(0, 0, 30);
                    _coroutine = Att_Chain();
                    StartCoroutine(_coroutine);

                    if (Input.GetKey(KeyCode.RightArrow))
                    {
                        rb.AddForce(Vector2.right * 8f, ForceMode2D.Impulse);

                    }

                } else

                if (CA == Cha_Attack.Att1)
                {
                    CA = Cha_Attack.Att2;
                    Cha_Delay = true;
                    Att_Delay = true;
                    animator.SetTrigger("Att2");
                    GameObject att1 = (GameObject)Instantiate(Att1, new Vector3(this.transform.position.x + 2f, this.transform.position.y + 0.5f, -5), Quaternion.identity);
                    att1.transform.eulerAngles = new Vector3(0, 0, -30);
                    _coroutine = Att_Chain();
                    StartCoroutine(_coroutine);

                    if (Input.GetKey(KeyCode.RightArrow))
                    {
                        rb.AddForce(Vector2.right * 8f, ForceMode2D.Impulse);

                    }

                } else

                if (CA == Cha_Attack.Att2)
                {
                    CA = Cha_Attack.Att3;
                    Cha_Delay = true;
                    Att_Delay = true;
                    animator.SetTrigger("Att3");
                    GameObject att1 = (GameObject)Instantiate(Att2, new Vector3(this.transform.position.x + 2.5f, this.transform.position.y, -5), Quaternion.identity);
                    _coroutine = Att_Final();
                    StartCoroutine(_coroutine);

                    if (Input.GetKey(KeyCode.RightArrow))
                    {
                        rb.AddForce(Vector2.right * 8f, ForceMode2D.Impulse);

                    }

                }


            } else

            if (Cha_Jump == false && Run == true && CA == Cha_Attack.None && Dash == false)
            {
                CA = Cha_Attack.Att1;
                Cha_Delay = true;
                Att_Delay = true;
                animator.SetTrigger("Att1");
                GameObject att1 = (GameObject)Instantiate(Att3, new Vector3(this.transform.position.x + 2.5f, this.transform.position.y - 0.2f, -5), Quaternion.identity);
                _coroutine = Att_Final();
                StartCoroutine(_coroutine);
                Run = false;
                rb.AddForce(Vector2.right * 15f, ForceMode2D.Impulse);

            } else

            if (Cha_Jump == true && CA == Cha_Attack.None && Dash == false)
            {
                CA = Cha_Attack.Att1;
                Cha_Delay = false;
                Att_Delay = true;
                Run = false;
                Jum_Count = Jum_Count + 1;
                animator.SetTrigger("Att1");
                GameObject att1 = (GameObject)Instantiate(Att1, new Vector3(this.transform.position.x + 1.5f, this.transform.position.y - 0.5f, -5), Quaternion.identity);
                //StartCoroutine(J_Att_Chain());
                //rb.constraints = RigidbodyConstraints2D.FreezePositionY;
                rb.velocity = new Vector2(rb.velocity.x, 10);
                //rb.simulated = false;
                //rb.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
                //rb.AddForce(Vector2.right * 1f, ForceMode2D.Impulse);


            }
            else

            if (Cha_Jump == true && CA == Cha_Attack.None && Dash == true)
            {
                CA = Cha_Attack.Att1;
                Cha_Delay = true;
                Att_Delay = true;
                Dash = false;
                Jum_Count = Jum_Count + 1;
                animator.SetTrigger("Att1");
                GameObject att1 = (GameObject)Instantiate(Att1, new Vector3(transform.position.x + 1.5f, transform.position.y - 0.5f, -5), Quaternion.identity);
                _coroutine = J_Att_Chain();
                StartCoroutine(_coroutine);
                rb.velocity = new Vector2(-8, 15);
            }
            else

            if (Cha_Jump == false && Run == false && CA == Cha_Attack.None && Dash == true)
            {
                CA = Cha_Attack.Att1;
                Cha_Delay = true;
                Att_Delay = true;
                animator.SetTrigger("Att1");
                GameObject att1 = (GameObject)Instantiate(Att1, new Vector3(this.transform.position.x + 2.5f, this.transform.position.y + 0.5f, -5), Quaternion.identity);
                StopCoroutine(_coroutine);
                _coroutine = Att_Final();
                StartCoroutine(_coroutine);
                rb.AddForce(Vector2.right * 0.8f, ForceMode2D.Impulse);

            }
            

        } else

        //왼쪽
        if (CD == Cha_Direction.Left && Att_Delay == false)
        {
            if (Cha_Jump == false && Run == false && Dash == false)
            {
                if (CA == Cha_Attack.None)
                {
                    CA = Cha_Attack.Att1;
                    Cha_Delay = true;
                    Att_Delay = true;
                    animator.SetTrigger("Att1");
                    GameObject att1 = (GameObject)Instantiate(Att1, new Vector3(this.transform.position.x - 2f, this.transform.position.y + 0.5f, -5), Quaternion.identity);
                    att1.transform.eulerAngles = new Vector3(0, 180, 30);
                    _coroutine = Att_Chain();
                    StartCoroutine(_coroutine);

                    if (Input.GetKey(KeyCode.LeftArrow))
                    {
                        rb.AddForce(Vector2.right * -8f, ForceMode2D.Impulse);

                    }

                }
                else

                if (CA == Cha_Attack.Att1)
                {
                    CA = Cha_Attack.Att2;
                    Cha_Delay = true;
                    Att_Delay = true;
                    animator.SetTrigger("Att2");
                    GameObject att1 = (GameObject)Instantiate(Att1, new Vector3(this.transform.position.x - 2f, this.transform.position.y + 0.5f, -5), Quaternion.identity);
                    att1.transform.eulerAngles = new Vector3(0, 180, -30);
                    _coroutine = Att_Chain();
                    StartCoroutine(_coroutine);

                    if (Input.GetKey(KeyCode.LeftArrow))
                    {
                        rb.AddForce(Vector2.right * -8f, ForceMode2D.Impulse);

                    }

                }
                else

                if (CA == Cha_Attack.Att2)
                {
                    CA = Cha_Attack.Att3;
                    Cha_Delay = true;
                    Att_Delay = true;
                    animator.SetTrigger("Att3");
                    GameObject att1 = (GameObject)Instantiate(Att2, new Vector3(this.transform.position.x - 2.5f, this.transform.position.y, -5), Quaternion.identity);
                    att1.transform.eulerAngles = new Vector3(0, 180, 0);
                    _coroutine = Att_Final();
                    StartCoroutine(_coroutine);

                    if (Input.GetKey(KeyCode.LeftArrow))
                    {
                        rb.AddForce(Vector2.right * -8f, ForceMode2D.Impulse);

                    }

                }


            }
            else

            if (Cha_Jump == false && Run == true && CA == Cha_Attack.None && Dash == false)
            {
                CA = Cha_Attack.Att1;
                Cha_Delay = true;
                Att_Delay = true;
                animator.SetTrigger("Att1");
                GameObject att1 = (GameObject)Instantiate(Att3, new Vector3(this.transform.position.x - 2.5f, this.transform.position.y - 0.2f , -5), Quaternion.identity);
                att1.transform.eulerAngles = new Vector3(0, 180, 0);
                _coroutine = Att_Final();
                StartCoroutine(_coroutine);
                Run = false;
                rb.AddForce(Vector2.left * 15f, ForceMode2D.Impulse);

            }
            else

            if (Cha_Jump == true && CA == Cha_Attack.None && Dash == false)
            {
                CA = Cha_Attack.Att1;
                Cha_Delay = false;
                Att_Delay = true;
                Run = false;
                Jum_Count = Jum_Count + 1;
                animator.SetTrigger("Att1");
                GameObject att1 = (GameObject)Instantiate(Att1, new Vector3(this.transform.position.x - 1.5f, this.transform.position.y - 0.5f, -5), Quaternion.identity);
                att1.transform.eulerAngles = new Vector3(0, 180, 0);
                StartCoroutine(J_Att_Chain());
                //rb.constraints = RigidbodyConstraints2D.FreezePositionY;
                rb.velocity = new Vector2(rb.velocity.x, 10);
                //rb.simulated = false;
                //rb.AddForce(Vector2.up * 5f, ForceMode2D.Impulse);
                //rb.AddForce(-Vector2.right * 1f, ForceMode2D.Impulse);


            }
            else

            if (Cha_Jump == true && CA == Cha_Attack.None && Dash == true)
            {
                CA = Cha_Attack.Att1;
                Cha_Delay = true;
                Att_Delay = true;
                Dash = false;
                Jum_Count = Jum_Count + 1;
                animator.SetTrigger("Att1");
                GameObject att1 = (GameObject)Instantiate(Att1, new Vector3(transform.position.x - 1.5f, transform.position.y - 0.5f, -5), Quaternion.identity);
                att1.transform.eulerAngles = new Vector3(0, 180, 0);
                _coroutine = J_Att_Chain();
                StartCoroutine(_coroutine);
                rb.velocity = new Vector2(8, 15);
            }
            else

            if (Cha_Jump == false && Run == false && CA == Cha_Attack.None && Dash == true)
            {
                CA = Cha_Attack.Att1;
                Cha_Delay = true;
                Att_Delay = true;
                animator.SetTrigger("Att1");
                GameObject att1 = (GameObject)Instantiate(Att1, new Vector3(this.transform.position.x - 2.5f, this.transform.position.y + 0.5f, -5), Quaternion.identity);
                att1.transform.eulerAngles = new Vector3(0, 180, 0);
                StopCoroutine(_coroutine);
                _coroutine = Att_Final();
                StartCoroutine(_coroutine);
                rb.AddForce(Vector2.left * 0.8f, ForceMode2D.Impulse);

            }


        }

    }

    void Archer()
    {
        if (CD == Cha_Direction.Right)
        {
            GameObject archer = (GameObject)Instantiate(ArcherR, new Vector2(transform.position.x, transform.position.y + 1f), Quaternion.identity);
        }
        else

        if (CD == Cha_Direction.Left)
        {
            GameObject archer = (GameObject)Instantiate(ArcherL, new Vector2(transform.position.x, transform.position.y + 1f), Quaternion.identity);
        }

    }

    void Assassin()
    {
        if (Cha_Jump == true)
        {
            GameObject _assassin = (GameObject)Instantiate(assassin, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        }
        else
        {
            GameObject _assassin = (GameObject)Instantiate(assassin, new Vector2(transform.position.x, transform.position.y + 5f), Quaternion.identity);
        }
    }

    void C_dash()
    {
        Cha_Delay = true;
        Att_Delay = true;
        Cha_Invulnerable = true;
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        if (Cha_Jump == false)
        {
            StartCoroutine(c_dash());
        }
        else

        if (Cha_Jump == true)
        {
            StartCoroutine(j_c_dash());
        }
    }

    public void land()
    {
        //점프 상태 초기화
        if (GetComponent<CapsuleCollider2D>().isTrigger == false)
        {
            if (Hit == false)
            {
                Cha_Jump = false;
                Cha_Delay = true;
                Att_Delay = true;
                Dash = false;
                Jum_Count = 0.0f;
                rb.velocity = new Vector2(0, 0);
                //animator.SetTrigger("Att_end");
                _coroutine = landing();
                StartCoroutine(_coroutine);
            }
            else

            if (Hit == true)
            {
                StartCoroutine(getup());
            }
        }
    }

    public void _explosive(int damage, float thrx, float thry, Vector2 egicenter)
    {
        //StopCoroutine(_coroutine);
        Cha_Invulnerable = true;
        Cha_Delay = true;
        Cha_Life -= damage;
        if (transform.position.x <= egicenter.x)
        {
            rb.AddForce(Vector2.left * thrx, ForceMode2D.Impulse);
        }
        else

        if (transform.position.x > egicenter.x)
        {
            rb.AddForce(Vector2.right * thrx, ForceMode2D.Impulse);
        }

        if (transform.position.y >= egicenter.y)
        {
            rb.AddForce(Vector2.up * thry, ForceMode2D.Impulse);
        }
        else

        if (transform.position.y < egicenter.y && Cha_Jump == true)
        {
            rb.AddForce(Vector2.down * thry, ForceMode2D.Impulse);
        }
        StartCoroutine(getup2());
    }

    public void _throw(int damage, float thrx, float thry, Vector2 egicenter)
    {
        //StopCoroutine(_coroutine);
        Cha_Invulnerable = true;
        Cha_Delay = true;
        Cha_Life -= damage;
        if (transform.position.x <= egicenter.x)
        {
            rb.AddForce(Vector2.left * thrx, ForceMode2D.Impulse);
        }
        else

        if (transform.position.x > egicenter.x)
        {
            rb.AddForce(Vector2.right * thrx, ForceMode2D.Impulse);
        }

        rb.AddForce(Vector2.up * thry, ForceMode2D.Impulse);
    }

    public void _press(int damage, float thrx, float thry, Vector2 egicenter)
    {
        //StopCoroutine(_coroutine);
        Cha_Invulnerable = true;
        Cha_Delay = true;
        Cha_Life -= damage;
        if (transform.position.x <= egicenter.x)
        {
            rb.AddForce(Vector2.left * thrx, ForceMode2D.Impulse);
        }
        else

        if (transform.position.x > egicenter.x)
        {
            rb.AddForce(Vector2.right * thrx, ForceMode2D.Impulse);
        }

        if (Cha_Jump == true)
        {
            rb.AddForce(Vector2.down * thry, ForceMode2D.Impulse);
        }
        StartCoroutine(getup2());
    }

    public void Dot(int damage, float interval)
    {
        StartCoroutine(_dot(damage, interval));
    }

    //IEnumerater


    IEnumerator Att_Chain()
    {
        yield return new WaitForSeconds(0.2f);
        Att_Delay = false;
        if (Att_Count > 0)
        {
            Att_Count = 0.0f;
            Invoke("Attack", 0.05f);

        } else

        if(Att_Count == 0)
        {
            _coroutine = Att_End();
            StartCoroutine(_coroutine);

        }

    }

    IEnumerator Att_Final()
    {
        yield return new WaitForSeconds(0.5f);
        Att_Count = 0f;
        Cha_Delay = false;
        Dash = false;
        Att_Delay = false;
        animator.SetBool("Att_end", true);
        Cha_Delay = false;
        CA = Cha_Attack.None;

    }

    IEnumerator Att_End()
    {
        yield return new WaitForSeconds(0.2f);
        Att_Count = 0f;
        Cha_Delay = false;
        animator.SetBool("Att_end", true);
        Cha_Delay = false;
        CA = Cha_Attack.None;

    }

    IEnumerator J_Att_Chain()
    {
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("Att_end", true);
        Cha_Delay = false;
    }

    IEnumerator J_Att_End()
    {
        yield return new WaitForSeconds(0.3f);

        //rb.constraints = RigidbodyConstraints2D.None;
        Att_Count = 0f;
        //Cha_Delay = false;

    }

    IEnumerator J_Att_Final()
    {
        yield return new WaitForSeconds(0.2f);
        Att_Delay = false;
        StartCoroutine(J_Att_End());

    }

    IEnumerator Dmg_End()
    {
        yield return new WaitForSeconds(0.5f);
        Cha_Delay = false;

    }

    IEnumerator Cha_Hit(int damage, float dir)
    {
        Cha_Delay = true;
        Cha_Life -= damage;
        rb.velocity = new Vector2(0, 0);
        rb.AddForce(Vector2.up * 15.0f, ForceMode2D.Impulse);

        if (dir < 0.0f)
        {
            CD = Cha_Direction.Right;
            rb.AddForce(Vector2.left * 5.0f, ForceMode2D.Impulse);

        }
        else

        if (dir > 0.0f)
        {
            CD = Cha_Direction.Left;
            rb.AddForce(Vector2.right * 5.0f, ForceMode2D.Impulse);

        }

        yield return new WaitForSeconds(0.5f);
        Cha_Delay = false;
        Cha_Invulnerable = false;
    }

    IEnumerator Dash_cool()
    {
        if (CD == Cha_Direction.Right)
        {
            Dash = true;
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(Vector2.right * 17, ForceMode2D.Impulse);

        }
        else

        if (CD == Cha_Direction.Left)
        {
            Dash = true;
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(-Vector2.right * 17, ForceMode2D.Impulse);

        }

        yield return new WaitForSeconds(0.5f);
        Dash = false;
        Cha_Delay = false;
        

    }

    IEnumerator landing()
    {
        if (C_Dash == false)
        {
            if (true)//jatt
            {
                CA = Cha_Attack.None;

                Att_Count = 0f;
                animator.SetBool("Att_end", true);
            }
            yield return new WaitForSeconds(0.02f);
            Cha_Delay = false;
            yield return new WaitForSeconds(0.1f);
            Att_Delay = false;
        }
        else

        if (C_Dash == true)
        {
            yield return new WaitForSeconds(1f);
            C_Dash = false;
            Cha_Delay = false;
            Dash = false;
            Att_Delay = false;
            CA = Cha_Attack.None;
            Att_Count = 0f;
            touch = false;
            mobcol.GetComponent<CapsuleCollider2D>().enabled = true;
            animator.SetBool("Att_end", true);
        }
        
    }

    IEnumerator c_dash()
    {
        Cha_Delay = true;
        mobcol.GetComponent<CapsuleCollider2D>().enabled = false;
        if (CD == Cha_Direction.Right &! Input.GetKey(KeyCode.LeftArrow) && Cha_Jump == false)
        {
            C_Dash = true;
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(Vector2.right * 15, ForceMode2D.Impulse);

        }
        else

        if (CD == Cha_Direction.Right && Input.GetKey(KeyCode.LeftArrow) && Cha_Jump == false)
        {
            C_Dash = true;
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(-Vector2.right * 15, ForceMode2D.Impulse);

        }
        else

        if (CD == Cha_Direction.Left &! Input.GetKey(KeyCode.RightArrow) && Cha_Jump == false)
        {
            C_Dash = true;
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(-Vector2.right * 15, ForceMode2D.Impulse);

        }
        else

        if (CD == Cha_Direction.Left &&Input.GetKey(KeyCode.RightArrow) && Cha_Jump == false)
        {
            C_Dash = true;
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(Vector2.right * 15, ForceMode2D.Impulse);

        }

        yield return new WaitForSeconds(0.2f);
        Cha_Invulnerable = false;

        yield return new WaitForSeconds(0.2f);
        touch = true;

        yield return new WaitForSeconds(0.3f);
        mobcol.GetComponent<CapsuleCollider2D>().enabled = true;
        touch = false;
        C_Dash = false;
        Cha_Delay = false;
        Dash = false;
        Att_Delay = false;
        CA = Cha_Attack.None;
        Att_Count = 0f;
        animator.SetBool("Att_end", true);
    }

    IEnumerator j_c_dash()
    {
        Cha_Delay = true;
        C_Dash = true;
        mobcol.GetComponent<CapsuleCollider2D>().enabled = false;
        rb.velocity = new Vector2(0, 0);
        rb.AddForce(Vector2.down * 55, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.1f);
        Cha_Invulnerable = false;

        yield return new WaitForSeconds(0.2f);
        touch = true;
    }

    IEnumerator jcend()
    {
        yield return new WaitForSeconds(1f);
        C_Dash = false;
        Cha_Delay = false;
        Dash = false;
        Att_Delay = false;
        CA = Cha_Attack.None;
        Att_Count = 0f;
        mobcol.GetComponent<CapsuleCollider2D>().enabled = true;
        animator.SetBool("Att_end", true);
    }

    IEnumerator getup()
    {
        Cha_Jump = false;
        Cha_Delay = true;
        Att_Delay = true;
        Dash = false;
        Jum_Count = 0.0f;
        yield return new WaitForSeconds(0.5f);
        rb.velocity = new Vector2(0, 0);

        yield return new WaitForSeconds(1f);
        C_Dash = false;
        Hit = false;
        Cha_Delay = false;
        Dash = false;
        Att_Delay = false;
        CA = Cha_Attack.None;
        Att_Count = 0f;
        touch = false;
        mobcol.GetComponent<CapsuleCollider2D>().enabled = true;
        animator.SetBool("Att_end", true);

        yield return new WaitForSeconds(0.5f);
        Cha_Invulnerable = false;
    }

    IEnumerator getup2()
    {
        if (Cha_Jump == false)
        {
            yield return new WaitForSeconds(1f);
            if (Cha_Jump == false && Hit == true)
            {
                rb.velocity = new Vector2(0, 0);
                Cha_Jump = false;
                Cha_Delay = true;
                Att_Delay = true;
                Dash = false;
                Jum_Count = 0.0f;

                yield return new WaitForSeconds(1f);
                C_Dash = false;
                Hit = false;
                Cha_Delay = false;
                Dash = false;
                Att_Delay = false;
                CA = Cha_Attack.None;
                Att_Count = 0f;
                touch = false;
                mobcol.GetComponent<CapsuleCollider2D>().enabled = true;
                animator.SetBool("Att_end", true);

                yield return new WaitForSeconds(0.5f);
                Cha_Invulnerable = false;
            }
        }
        
    }

    IEnumerator _dot(int damage, float interval)
    {
        while (true)
        {
            if (Cha_Invulnerable == false)
            {
                Cha_Life -= damage;
                yield return new WaitForSeconds(interval);
            }

            if (dot == false)
            {
                break;
            }
            yield return null;
        }
    }
}
