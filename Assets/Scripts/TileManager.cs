using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileManager : MonoBehaviour
{
    [SerializeField] private Sprite[] _spriteTile;
    public bool moving;
    private Transform _transform;
    private Image _image;
    private Vector3 _offset;
    private Vector3 _startPos;
    private GameObject go;
    private int goIndex;
    private float distance;

    private void Start()
    {
        moving = true;
        _transform = transform;
        _image = GetComponent<Image>();
    }

    private void OnMouseDown()
    {
        if(!moving)
        {
            return;
        }
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
        if(!moving)
        {
            return;
        }
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
        if(!moving)
        {
            return;
        }
        if (_transform.name == "8" || _transform.name == "9" || _transform.name == "10" || _transform.name == "null")
        {
            return;
        }
        go = null;
        goIndex = 0;
        distance = 1000;
        Vector3 pos = _transform.localPosition;
        int i = 0;
        foreach (GameObject tile in Board.instance.boardTile)
        {
            i++;
            if (_transform.name == tile.name || tile.name == "null")
            {
                if (_transform.gameObject != tile)
                {
                    float curDistance = Vector3.Distance(pos, tile.transform.localPosition);
                    if (curDistance < distance)
                    {
                        distance = curDistance;
                        go = tile;
                        goIndex = i;
                    }
                }
            }
        }
        if (distance > 70)
        {
            _transform.localPosition = _startPos;
        }
        else
        {
            _transform.localPosition = _startPos;
            if (_transform.name == go.name)
            {
                CheckBox();
                for (int j = 0; j < 8; j++)
                {
                    _transform.GetComponent<Image>().sprite = null;
                    _transform.GetComponent<Image>().color = new Color(255, 255, 255, 0);
                    _transform.name = "null";
                    if (go.name == j.ToString())
                    {
                        if (j != 7)
                        {
                            go.name = (j + 1).ToString();
                            go.GetComponent<Image>().sprite = _spriteTile[j + 1];
                            go.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                            go.GetComponent<TileManager>().moving = true;
                            if (DataManager.instance.availableTileIndex < j)
                            {
                                DataManager.instance.availableTileIndex = j;
                                if (DataManager.instance.availableTileIndex >= 3)
                                {
                                    DataManager.instance.availableTileIndex = 3;
                                }
                            }
                            return;
                        }
                        else if (j == 7)
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
                for (int j = 0; j < 8; j++)
                {
                    if (_transform.name == j.ToString())
                    {
                        go.name = j.ToString();
                        go.GetComponent<Image>().sprite = _spriteTile[j];
                        go.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                        _transform.name = "null";
                        return;
                    }
                }
            }
        }
    }

    private void CheckBox()
    {
        foreach (GameObject item in Board.instance.boardTile)
        {
            if (item.name == "8")
            {
                if (item.transform.localPosition.x == (go.transform.localPosition.x + 140) && item.transform.localPosition.y == go.transform.localPosition.y)
                {
                    item.GetComponent<TileManager>().moving = false;
                    item.GetComponent<Image>().sprite = _spriteTile[DataManager.instance.availableTileIndex + 3];
                    item.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    item.name = (DataManager.instance.availableTileIndex + 3).ToString();
                }
                else if (item.transform.localPosition.x == (go.transform.localPosition.x - 140) && item.transform.localPosition.y == go.transform.localPosition.y)
                {
                    item.GetComponent<TileManager>().moving = false;
                    item.GetComponent<Image>().sprite = _spriteTile[DataManager.instance.availableTileIndex + 3];
                    item.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    item.name = (DataManager.instance.availableTileIndex + 3).ToString();
                }
                else if (item.transform.localPosition.x == go.transform.localPosition.x && item.transform.localPosition.y == (go.transform.localPosition.y + 140))
                {
                    item.GetComponent<TileManager>().moving = false;
                    item.GetComponent<Image>().sprite = _spriteTile[DataManager.instance.availableTileIndex + 3];
                    item.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    item.name = (DataManager.instance.availableTileIndex + 3).ToString();
                }
                else if (item.transform.localPosition.x == go.transform.localPosition.x && item.transform.localPosition.y == (go.transform.localPosition.y - 140))
                {
                    item.GetComponent<TileManager>().moving = false;
                    item.GetComponent<Image>().sprite = _spriteTile[DataManager.instance.availableTileIndex + 3];
                    item.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    item.name = (DataManager.instance.availableTileIndex + 3).ToString();
                }
            }
        }
    }
}