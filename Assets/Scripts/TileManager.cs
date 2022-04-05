using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileManager : MonoBehaviour
{
    [SerializeField] private Sprite[] _spriteTile;
    private Transform _transform;
    private Image _image;
    private Vector3 _offset;
    private Vector3 _startPos;
    private GameObject go;
    private float distance;

    private void Start()
    {
        _transform = transform;
        _image = GetComponent<Image>();
    }

    private void OnMouseDown()
    {
        if (_transform.name == "8" || _transform.name == "null")
        {
            return;
        }
        else if (_transform.name == "9")
        {
            foreach (GameObject item in Board.instance.boardTile)
            {
                if (item.name == "null")
                {
                    item.GetComponent<Image>().sprite = _spriteTile[0];
                    item.GetComponent<Image>().color = new Color(255, 255, 255, 255);
                    item.name = "0";
                    return;
                }
            }
        }
        else if (_transform.name == "10")
        {
            _transform.GetComponent<Image>().sprite = null;
            _transform.GetComponent<Image>().color = new Color(255, 255, 255, 0);
            _transform.name = "null";
            DataManager.instance.scoreCount++;
            UIManager.instance.UpdateTextScore();
            return;
        }
        _offset = _transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
        _startPos = _transform.localPosition;
    }

    private void OnMouseDrag()
    {
        if (_transform.name == "8" || _transform.name == "9" || _transform.name == "10" || _transform.name == "null")
        {
            return;
        }
        Vector3 newPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f);
        _transform.position = Camera.main.ScreenToWorldPoint(newPosition) + _offset;
        if (_transform.localPosition.x >= 420)
        {
            _transform.localPosition = new Vector3(420, _transform.localPosition.y, _transform.localPosition.z);
        }
        else if (_transform.localPosition.x <= -420)
        {
            _transform.localPosition = new Vector3(-420, _transform.localPosition.y, _transform.localPosition.z);
        }
        if (_transform.localPosition.y >= 560)
        {
            _transform.localPosition = new Vector3(_transform.localPosition.x, 560, _transform.localPosition.z);
        }
        else if (_transform.localPosition.y <= -560)
        {
            _transform.localPosition = new Vector3(_transform.localPosition.x, -560, _transform.localPosition.z);
        }
    }

    private void OnMouseUp()
    {
        if (_transform.name == "8" || _transform.name == "9" || _transform.name == "10" || _transform.name == "null")
        {
            return;
        }
        go = null;
        distance = 1000;
        Vector3 pos = _transform.localPosition;
        foreach (GameObject tile in Board.instance.boardTile)
        {
            if (_transform.name == tile.name || tile.name == "null")
            {
                if (_transform.gameObject != tile)
                {
                    float curDistance = Vector3.Distance(pos, tile.transform.localPosition);
                    if (curDistance < distance)
                    {
                        distance = curDistance;
                        go = tile;
                    }
                }
            }
        }
        if (distance > 55)
        {
            _transform.localPosition = _startPos;
        }
        else
        {
            _transform.localPosition = _startPos;
            if (_transform.name == go.name)
            {
                for (int i = 0; i < 8; i++)
                {
                    _transform.GetComponent<Image>().sprite = null;
                    _transform.GetComponent<Image>().color = new Color(255, 255, 255, 0);
                    _transform.name = "null";
                    if (go.name == i.ToString())
                    {
                        if (i != 7)
                        {
                            go.name = (i + 1).ToString();
                            go.GetComponent<Image>().sprite = _spriteTile[i + 1];
                            return;
                        }
                        else if (i == 7)
                        {
                            go.name = "10";
                            go.GetComponent<Image>().sprite = _spriteTile[8];
                        }
                    }
                }
            }
            else if (go.name == "null")
            {
                _transform.GetComponent<Image>().sprite = null;
                _transform.GetComponent<Image>().color = new Color(255, 255, 255, 0);
                for (int i = 0; i < 8; i++)
                {
                    if (_transform.name == i.ToString())
                    {
                        go.name = i.ToString();
                        go.GetComponent<Image>().sprite = _spriteTile[i];
                        go.GetComponent<Image>().color = new Color(255, 255, 255, 255);
                        _transform.name = "null";
                        return;
                    }
                }
            }
        }
    }
}