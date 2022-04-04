using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    private Transform _transform;
    private Vector3 _offset;

    private void Start()
    {
        _transform = transform;
    }

    private void OnMouseDown()
    {
        if (_transform.name != "0")
        {
            _offset = _transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
            print("начальная");
        }
    }

    private void OnMouseDrag()
    {
        if (_transform.name != "0")
        {
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
    }

    private void OnMouseUp()
    {
        print("конечная");
    } 
}