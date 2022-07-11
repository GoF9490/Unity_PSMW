using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapLoad : MonoBehaviour
{
    private void Update()
    {
        if (DataController.instance.GetComponent<DataController>().playerData.map == 1)
        {
            SceneManager.LoadScene("tutorial");
        }
        else

        if (DataController.instance.GetComponent<DataController>().playerData.map == 2)
        {
            SceneManager.LoadScene("Test");
        }
    }
}
