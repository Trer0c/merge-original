using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    private Transform _transform;
    private Vector3 offset;

    private void Start()
    {
        _transform = transform;
    }

    private void OnMouseDown()
    {
        offset = _transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
    }

    private void OnMouseDrag()
    {
        Vector3 newPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f);
        _transform.position = Camera.main.ScreenToWorldPoint(newPosition) + offset;
    }
}
