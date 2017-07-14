using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    //bool called = false;
    void OnCollisionEnter(Collision col)
    {
        col.transform.root.GetComponent<PlayerController>().m_win = true;

        //var createScore = (NetworkTarget) transform.root.GetComponent(typeof(NetworkTarget));
        //if (!called)
        //{
        //    createScore.CmdCreateEndingScore();
        //    called = true;
        //}

        Debug.Log("Player Wins");
    }
}
