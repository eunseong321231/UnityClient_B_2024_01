using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameData gameData;
    // Start is called before the first frame update
    void Start()
    {
        //시작시 GameData의 내역을 Debug.Log로 보여준다.

        Debug.Log("Game Name : " + gameData.gameName);
        Debug.Log("Game Score : " + gameData.gameScore);
        Debug.Log("is Game Active : " + gameData.isGameActive);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
