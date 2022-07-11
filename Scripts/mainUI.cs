using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mainUI : MonoBehaviour
{
    public Text HPT;

    public GameObject Player;

    private void Awake()
    {
        Player = GameObject.Find("shero1");
    }

    private void Update()
    {
        HPT.GetComponent<Text>().text = Player.GetComponent<playerCon>()._life.ToString() + " / " + DataController.instance.GetComponent<DataController>().playerData.HP.ToString();
    }
}
