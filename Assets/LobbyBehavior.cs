using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Proyecto26;
using UnityEditor;
using System;
using UnityEngine.UI;
using System.Net.Http;

[Serializable]
public class Move
{
    public string game_id;
    public int id;
    public int player_id;
    public string source_position;
    public string target_position;
}

[Serializable]
public class Player
{
    public int id;
    public string name;
    public string game_id;

}

[Serializable]
public class Game
{
    public string id;
    public Move[] moves;
    public Player[] players;
}


public class LobbyBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject itemRoom;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void enterGame(Text gameId)
    {
        Debug.Log(gameId.text);
        Debug.Log(GlobalVars.player_name);
        RestClient.Request(new RequestHelper
        {
            Uri = "https://ihc-chess-server.herokuapp.com/enter_game",
            Method = "POST",
            Timeout = 10,
            Params = new Dictionary<string, string> { {"game_id",gameId.text }, {"player_id", GlobalUser.Instance.player_id} }

        }).Then( response => SceneManager.LoadScene("game"));
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("mainmenu");
    }
}
