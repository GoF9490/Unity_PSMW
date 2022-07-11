using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum startMenu
{
    start,
    exit
}

public class StartMenu : MonoBehaviour
{
    public GameObject _Start;
    public GameObject _End;
    public startMenu sm;


    private void Start()
    {
        sm = startMenu.start;
    }

    private void Update()
    {
        if (sm == startMenu.start)
        {
            transform.position = new Vector2(transform.position.x, _Start.GetComponent<RectTransform>().position.y);

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                sm = startMenu.exit;
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                SceneManager.LoadScene("LoadMenu");
            }
        }
        else

        if (sm == startMenu.exit)
        {
            transform.position = new Vector2(transform.position.x, _End.GetComponent<RectTransform>().position.y);
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                sm = startMenu.start;
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                Application.Quit();
                UnityEditor.EditorApplication.isPlaying = false;
            }
        }
    }
}
