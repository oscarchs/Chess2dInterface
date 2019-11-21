using Proyecto26;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MovesBehaviour : MonoBehaviour
{
    public GameObject item;
    public string source;
    public void makeMove()
    {
        Debug.Log("Make move");
        Debug.Log(source);
        RestClient.Request(new RequestHelper
        {
            Uri = "https://ihc-chess-server.herokuapp.com/make_move",
            Method = "POST",
            Timeout = 10,
            Params = new Dictionary<string, string> { { "game_id", "5dbf88ddb67efa00144cbf3d" }, { "player_id", "2" }, { "source_position", GameObject.Find("sourcetext").GetComponent<Text>().text },
                                                                                                               { "target_position", GameObject.Find("targettext").GetComponent<Text>().text} }
        });
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("on start");
        RestClient.GetArray<Move>("https://ihc-chess-server.herokuapp.com/list-moves/5dbf88ddb67efa00144cbf3d").Then(response => {
            for (int i = 0; i < response.Length; i++)
            {
                GameObject newitem = (GameObject)Instantiate(item);
                var panel = GameObject.Find("ContentMoves");
                newitem.transform.parent = panel.transform.parent;
                newitem.GetComponent<RectTransform>().SetParent(panel.transform);
                newitem.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, 200);
                newitem.GetComponent<RectTransform>().GetComponentInChildren<Text>().text = string.Format("{0} // {1}", response[i].source_position, response[i].target_position);

            }
        });
        InvokeRepeating("update_moves", 2f, 2f);


    }
    // Update is called once per frame

    void update_moves()
    {
        RestClient.GetArray<Move>("https://ihc-chess-server.herokuapp.com/list-moves/5dbf88ddb67efa00144cbf3d").Then(response => {
            var panel1 = GameObject.Find("ContentMoves");
            if (response.Length > panel1.transform.childCount)
            {
                int last_index = response.Length - 1;
                GameObject newitem = (GameObject)Instantiate(item);
                var panel = GameObject.Find("ContentMoves");
                newitem.transform.parent = panel.transform.parent;
                newitem.GetComponent<RectTransform>().SetParent(panel.transform);
                newitem.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, 200);
                newitem.GetComponent<RectTransform>().GetComponentInChildren<Text>().text = string.Format(
                    "{0} // {1}", response[last_index].source_position, response[last_index].target_position);
            }


        });
    }
    void Update()
    {
        /*
        Debug.Log("on update");
        RestClient.GetArray<Move>("https://ihc-chess-server.herokuapp.com/list-moves/5dbf88ddb67efa00144cbf3d").Then(response => {
            var panel1 = GameObject.Find("ContentMoves");
            if( response.Length > panel1.transform.childCount)
            {
                for (int i = panel1.transform.childCount - 1; i > 0; i--)
                {
                    Object.Destroy(panel1.transform.GetChild(i).gameObject);
                }

                for (int i = 0; i < response.Length; i++)
                {
                    GameObject newitem = (GameObject)Instantiate(item);
                    var panel = GameObject.Find("ContentMoves");
                    newitem.transform.parent = panel.transform.parent;
                    newitem.GetComponent<RectTransform>().SetParent(panel.transform);
                    newitem.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, 200);
                    newitem.GetComponent<RectTransform>().GetComponentInChildren<Text>().text = string.Format("{0} // {1}", response[i].source_position, response[i].target_position);
                }
            }
            

        });
        */




    }

 
}
