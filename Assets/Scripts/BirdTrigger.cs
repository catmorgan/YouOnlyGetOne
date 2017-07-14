using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdTrigger : MonoBehaviour {

	private ParticleSystem m_pSystem;
	private AudioSource m_Audio;
	//private Renderer m_rend;

	void Start ()
	{
		m_pSystem = GetComponent<ParticleSystem> ();
		m_Audio = GetComponent<AudioSource> ();

	}

	void OnTriggerEnter (Collider other)
	{
		if (other.CompareTag ("Player")) 
		{
			m_pSystem.Play ();
			m_Audio.Play ();
			Renderer[] rs = GetComponentsInChildren<Renderer> ();
			foreach (Renderer r in rs)
				r.enabled = false;
			Renderer prs = m_pSystem.GetComponent<Renderer> ();
			prs.enabled = true;
		}
	}
}
