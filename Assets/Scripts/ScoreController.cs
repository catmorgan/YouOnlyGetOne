using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Events;

public class ScoreController : NetworkBehaviour
{
    public static ScoreController instance = null;
    public static Dictionary<int, Score> clientScores = new Dictionary<int, Score>();
    public Vector3 targetPosition;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = new ScoreController();
            //_targetPosition = 
        } else
        {
            instance = this;
            Invoke("RpcDisplayScore", 30f);
        }
    }
	
    public void OnPlayerConnect(int netId, Vector3 position)
    {
        if (!clientScores.ContainsKey(netId))
        {
            clientScores.Add(netId, new Score() { NetworkId = netId, ClosestDistance = (int) Vector3.Distance(position, targetPosition)});
            Debug.LogFormat("Added {0} to client score dictionary", netId.ToString());
        }
    }

    public void UpdateNumberOfAttempts(int netId)
    {
        if (clientScores.ContainsKey(netId))
        {
            var numAttempts = clientScores[netId].NumberOfAttempts + 1;
            clientScores[netId].NumberOfAttempts++;
            Debug.LogFormat("Client {0} number of attemps is {1}", netId.ToString(), numAttempts);
        }
    }

    public void UpdateTotalDamage(int netId)
    {
        if (clientScores.ContainsKey(netId))
        {
            var totalDamage = clientScores[netId].TotalDamage + 100;
            clientScores[netId].TotalDamage+=100;
            Debug.LogFormat("Client {0} total damage is {1}", netId.ToString(), totalDamage);
        }
    }

    public void UpdateClosestDistance(int netId, Vector3 position)
    {
        if (clientScores.ContainsKey(netId))
        {
            var totalScore = (int) Vector3.Distance(position, targetPosition);
            clientScores[netId].ClosestDistance = clientScores[netId].ClosestDistance <= totalScore ?
                clientScores[netId].ClosestDistance : totalScore;
            Debug.LogFormat("Client {0} total score is {1}", netId.ToString(), clientScores[netId].ClosestDistance);
        }
    }
    
    public void Update()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var player in players)
        { 
            if (player.GetComponent<PlayerController>().m_win)
            {
                GameObject go = GameObject.FindGameObjectWithTag("score");
                ClientScore clientScore = go.GetComponent<ClientScore>();
                clientScore.RpcDoOnClient(clientScores.ToString());
            }
        }
                

        //GameObject instance = Instantiate(Resources.Load("EndingScore", typeof(GameObject))) as GameObject;
        //GameObject instance = GameObject.FindGameObjectWithTag("score");
        //var index = 1;
        //foreach(var player in clientScores)
        //{
        //    var go = new GameObject();
        //    go.AddComponent<RectTransform>();
        //    go.AddComponent<CanvasRenderer>();

        //    go.transform.SetParent(instance.transform, false);
        //    go.GetComponent<RectTransform>().localPosition = new Vector3(0, index * 120, 0);

        //    var scoreProperty1 = Instantiate(Resources.Load("Text", typeof(GameObject))) as GameObject;

        //    scoreProperty1.GetComponent<Text>().text = String.Format("Player: {0}", player.Value.NetworkId);
        //    scoreProperty1.transform.SetParent(go.transform, false);
        //    scoreProperty1.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);

        //    var scoreProperty2 = Instantiate(Resources.Load("Text", typeof(GameObject))) as GameObject;

        //    scoreProperty2.GetComponent<Text>().text = String.Format("Closest Distance From Target: {0}", player.Value.ClosestDistance);
        //    scoreProperty2.transform.SetParent(go.transform, false);
        //    scoreProperty2.GetComponent<RectTransform>().localPosition = new Vector3(0, -30, 0);

        //    var scoreProperty3 = Instantiate(Resources.Load("Text", typeof(GameObject))) as GameObject;

        //    scoreProperty3.GetComponent<Text>().text = String.Format("Total Damage: {0}", player.Value.TotalDamage);
        //    scoreProperty3.transform.SetParent(go.transform, false);
        //    scoreProperty3.GetComponent<RectTransform>().localPosition = new Vector3(0, -60, 0);

        //    var scoreProperty4 = Instantiate(Resources.Load("Text", typeof(GameObject))) as GameObject;

        //    scoreProperty4.GetComponent<Text>().text = String.Format("Number of Attempts: {0}", player.Value.NumberOfAttempts);
        //    scoreProperty4.transform.SetParent(go.transform, false);
        //    scoreProperty4.GetComponent<RectTransform>().localPosition = new Vector3(0, -90, 0);

        //    index++;
        //}
    } 
}

public class Score {
    public int NetworkId { get; set; }
    public int ClosestDistance { get; set; }
    public int NumberOfAttempts { get; set; }
    public int TotalDamage { get; set; }
}
