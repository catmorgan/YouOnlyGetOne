using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using System.Collections.Generic;
using System.Collections;

public class NetworkMatchMaker : MonoBehaviour
{
    [SerializeField]
    private string m_defaultMatchName = "YOGO";

    [SerializeField]
    private Text m_existingMatchesText = null;
    [SerializeField]
    private Text m_messageText = null;

    private List<MatchInfoSnapshot> m_allMatches = new List<MatchInfoSnapshot>();

    public IEnumerator FindAllMatches()
    {
        // Always keep looking for matches
        while (true)
        {
            NetworkManager.singleton.matchMaker.ListMatches(0, 10, m_defaultMatchName, true, 0, 0, OnInternetMatchList);
            yield return new WaitForSeconds(0.5f);
        }
    }

    void Start()
    {
        NetworkManager.singleton.StartMatchMaker();
        StartCoroutine(FindAllMatches());
    }

    //call this method to request a match to be created on the server
    public void CreateInternetMatch()
    {
        // Check if game already exists
        NetworkManager.singleton.matchMaker.CreateMatch(m_defaultMatchName, 4, true, "", "", "", 0, 0, OnInternetMatchCreate);
    }

    //this method is called when your request for creating a match is returned
    private void OnInternetMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        if (success)
        {
            //Debug.Log("Create match succeeded");
            MatchInfo hostInfo = matchInfo;
            NetworkServer.Listen(hostInfo, 9000);

            NetworkManager.singleton.StartHost(hostInfo);
        }
        else
        {
            m_messageText.text = "Create match failed. Try again.";
            m_allMatches.Clear();
            Debug.LogError("Create match failed");
        }
    }

    //call this method to find a match through the matchmaker
    public void FindInternetMatch()
    {
        //join the last server (just in case there are two...)
        if (m_allMatches.Count > 0)
        {
            m_messageText.text = "Joining Game";
            foreach (var match in m_allMatches)
                if (match.name == m_defaultMatchName)
                    NetworkManager.singleton.matchMaker.JoinMatch(match.networkId, "", "", "", 0, 0, OnJoinInternetMatch);
        }
        else
            CreateInternetMatch();
    }

    //this method is called when a list of matches is returned
    private void OnInternetMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
    {
        if (success)
        {
            if (matches.Count != 0)
            {
                //Debug.Log("A list of matches was returned");
                m_allMatches = matches;
                List<string> matList = new List<string>();
                foreach (var mat in m_allMatches)
                    matList.Add(mat.name);
                m_existingMatchesText.text = string.Join("\n", matList.ToArray());
            }
        }
    }

    //this method is called when your request to join a match is returned
    private void OnJoinInternetMatch(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        if (success)
        {
            MatchInfo hostInfo = matchInfo;
            NetworkManager.singleton.StartClient(hostInfo);
        }
        else
        {
            m_messageText.text = "Joining match failed";
            Debug.LogError("Join match failed");
            m_allMatches.Clear();
        }
    }
}