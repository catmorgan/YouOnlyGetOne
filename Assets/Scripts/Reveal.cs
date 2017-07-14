using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reveal : MonoBehaviour
{
    private SphereCollider m_reveal;
    [SerializeField]
    private GameObject[] m_objects;

    // Use this for initialization
    void Start()
    {
        m_reveal = GetComponent<SphereCollider>();
        SetVisibility(false);
    }

    void SetVisibility(bool state)
    {
        foreach (var obj in m_objects)
            obj.SetActive(state);
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Player"))
        { 
            Destroy(m_reveal);
            SetVisibility(true);
        }
    }
}
