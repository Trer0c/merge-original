using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public int scoreCount;
    public int availableTileIndex;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        scoreCount = 0;
        availableTileIndex = 0;
    }
}
