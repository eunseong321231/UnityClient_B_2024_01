using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExPlayerPrefabsData : MonoBehaviour
{
    public int scorePoint;
    void SaveData(int Score)
    {
        PlayerPrefs.SetInt("Score", Score);
        PlayerPrefs.Save();
    }

    int LoadData()
    {
        return PlayerPrefs.GetInt("Score");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            SaveData(scorePoint);
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Score :" + LoadData());
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            PlayerPrefs.DeleteKey("Score");
        }
    }
}

