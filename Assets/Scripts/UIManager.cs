using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private Text _scoreCount;

    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        UpdateTextScore();
    }

    public void UpdateTextScore()
    {
        _scoreCount.text = "Score: " + DataManager.instance.scoreCount.ToString();
    }
    
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
