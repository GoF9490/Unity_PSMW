using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool isPause = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SetPause();
        }
    }

    public void SetPause()
    {
        if (isPause == false)
        {
            isPause = true;
            Time.timeScale = 0;
            //pauseMenu.gameObject.SetActive(true);
        }
        else

        if (isPause == true)
        {
            isPause = false;
            Time.timeScale = 1;
            pauseMenu.gameObject.SetActive(false);
        }
    }

    public void Continue()
    {
        
    }
}
