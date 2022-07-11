using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMenu : MonoBehaviour
{
    public GameObject slot1;
    public GameObject slot2;
    public GameObject slot3;
    public GameObject slot4;
    public GameObject slot5;
    public saveSlot ss;


    private void Start()
    {
        ss = saveSlot.save1;
    }

    private void Update()
    {
        if (ss == saveSlot.save1)
        {
            transform.position = new Vector2(transform.position.x, slot1.GetComponent<RectTransform>().position.y);

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                ss = saveSlot.save2;
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                DataController.instance.GetComponent<DataController>().ss = saveSlot.save1;
                DataController.instance.GetComponent<DataController>().LoadGameData();
                SceneManager.LoadScene("Load");
            }
        }
        else

        if (ss == saveSlot.save2)
        {
            transform.position = new Vector2(transform.position.x, slot2.GetComponent<RectTransform>().position.y);

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                ss = saveSlot.save1;
            }
            else

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                ss = saveSlot.save3;
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                DataController.instance.GetComponent<DataController>().ss = saveSlot.save2;
                DataController.instance.GetComponent<DataController>().LoadGameData();
                SceneManager.LoadScene("Load");
            }
        }
        else

        if (ss == saveSlot.save3)
        {
            transform.position = new Vector2(transform.position.x, slot3.GetComponent<RectTransform>().position.y);

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                ss = saveSlot.save2;
            }
            else

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                ss = saveSlot.save4;
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                DataController.instance.GetComponent<DataController>().ss = saveSlot.save3;
                DataController.instance.GetComponent<DataController>().LoadGameData();
                SceneManager.LoadScene("Load");
            }
        }
        else

        if (ss == saveSlot.save4)
        {
            transform.position = new Vector2(transform.position.x, slot4.GetComponent<RectTransform>().position.y);

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                ss = saveSlot.save3;
            }
            else

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                ss = saveSlot.save5;
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                DataController.instance.GetComponent<DataController>().ss = saveSlot.save4;
                DataController.instance.GetComponent<DataController>().LoadGameData();
                SceneManager.LoadScene("Load");
                }
        }
        else

        if (ss == saveSlot.save5)
        {
            transform.position = new Vector2(transform.position.x, slot5.GetComponent<RectTransform>().position.y);

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                ss = saveSlot.save4;
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                DataController.instance.GetComponent<DataController>().ss = saveSlot.save5;
                DataController.instance.GetComponent<DataController>().LoadGameData();
                SceneManager.LoadScene("Load");
            }
        }
    }
}

