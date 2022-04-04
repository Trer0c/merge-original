using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    [SerializeField] private Sprite[] _spriteTile;
    [SerializeField] private List<GameObject> _boardTile = new List<GameObject>();
    [SerializeField] private GameObject _tile;
    [SerializeField] private Transform _board;
    private Vector3 _direction;
    private void Start()
    {
        Spawn();
        _direction = new Vector3(-420, -560, 0);
    }

    private void Spawn()
    {
        foreach (Transform item in _board)
        {
            _boardTile.Add(item.gameObject);
            int random = Random.Range(0, _spriteTile.Length);
            item.name = random.ToString();
            item.GetComponent<Image>().sprite = _spriteTile[random];
        }
    }

    private void Update()
    {

    }
}
