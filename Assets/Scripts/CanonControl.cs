using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonControl : MonoBehaviour
{
    public Transform m_player;
    public bool m_followPlayer = true;

    private Renderer[] renderers;

    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
    }

    public void SetAlpha(float value)
    {
        foreach (var render in renderers)
        {
            Color color = render.material.color;
            color.a = value;
            render.material.color = color;
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if (m_followPlayer)
        {
            transform.rotation = m_player.rotation;
        }		
	}
}
