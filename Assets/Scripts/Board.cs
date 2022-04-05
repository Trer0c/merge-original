using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    public static Board instance;
    public List<GameObject> boardTile = new List<GameObject>();
    [SerializeField] private Sprite[] _spriteTile;
    [SerializeField] private Transform _board;


    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        Spawn();
    }

    private void Spawn()
    {
        foreach (Transform item in _board)
        {
            boardTile.Add(item.gameObject);
            // int random = Random.Range(0, _spriteTile.Length);
            // item.name = random.ToString();
            // item.GetComponent<Image>().sprite = _spriteTile[random];
        }
    }
}
