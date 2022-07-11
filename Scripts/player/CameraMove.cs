using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject _player;

    public int _rangeMinX = 0;
    public int _rangeMaxX = 0;

    public int _rangeMinY = 0;
    public int _rangeMaxY = 0;

    public bool _holdX = false;
    public bool _holdY = false;

    private void Awake()
    {
        _player = GameObject.Find("shero1");
    }

    private void Update()
    {
        if (_player.GetComponent<playerCon>()._lookatme == true)
        {
            if (_holdX == false)
            {
                ChaseX();
            }

            if (_holdY == false)
            {
                ChaseY();
            }
        }
    }

    void ChaseX()
    {
        if (_rangeMinX < _player.transform.position.x && _player.transform.position.x < _rangeMaxX)
        {
            transform.position = new Vector3(_player.transform.position.x, transform.position.y, transform.position.z);
        }

        if (_rangeMinX > _player.transform.position.x)
        {
            transform.position = new Vector3(_rangeMinX, transform.position.y, transform.position.z);
        }

        if (_player.transform.position.x > _rangeMaxX)
        {
            transform.position = new Vector3(_rangeMaxX, transform.position.y, transform.position.z);
        }
    }

    void ChaseY()
    {
        if (_rangeMinY < _player.transform.position.y && _player.transform.position.y < _rangeMaxY)
        {
            transform.position = new Vector3(transform.position.x, _player.transform.position.y, transform.position.z);
        }

        if (_rangeMinY > _player.transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, _rangeMinY, transform.position.z);
        }

        if (_player.transform.position.y > _rangeMaxY)
        {
            transform.position = new Vector3(transform.position.x, _rangeMaxY, transform.position.z);
        }
    }
}
