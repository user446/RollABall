using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    public GameObject loseMessage;
    public static GameObject player;

    void Awake()
    {
        _instance = this;
        player = GameObject.Find("Player");
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene("Game");
    }

    void CheckIfFellOff()
    {
        if(player.transform.position.y < EnvironmentManager.GetCurrentEnvironment().transform.position.y)
        {
            loseMessage.SetActive(true);
        }
    }

    void Update()
    {
        CheckIfFellOff();
    }
}
