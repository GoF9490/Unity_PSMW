using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject Cursor;
    public GameObject Player;
    public Transform cT;
    public Text soulT;

    public string s;

    private void Start()
    {
        cT = Cursor.GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            cT.localPosition = new Vector2 (-200, 80);
        }
        else

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            cT.localPosition = new Vector2(-200, -80);
        }
        
        s = DataController.instance.GetComponent<DataController>().playerData.soul.ToString();
        soulT.GetComponent<Text>().text = s;
    }
}
