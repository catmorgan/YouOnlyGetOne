using UnityEngine;
using System.Collections;

public class PlayNewParticles : MonoBehaviour
{

	public GameObject m_Target;
	public GameObject m_Target2;
	public GameObject m_Target3;
	public GameObject m_Target4;


	public void PlayNewParticles3()
	{
		if (m_Target == null)
			return;
		m_Target.GetComponent<ParticleSystem>().Play();
	}

	public void StopNewParticles3()
	{
		if (m_Target == null)
			return;
		m_Target.GetComponent<ParticleSystem>().Stop();
	}

	public void PlayNewParticles3andChilds()
	{
		if (m_Target2 == null)
			return;
		m_Target2.GetComponent<ParticleSystem>().Play(true);
	}

	public void StopNewParticles3andChilds()
	{
		if (m_Target2 == null)
			return;
		m_Target2.GetComponent<ParticleSystem>().Stop(true);
	}

	public void PlayNewParticles3andChilds2()
	{
		if (m_Target3 == null)
			return;
		m_Target3.GetComponent<ParticleSystem>().Play(true);
	}

	public void StopNewParticles3andChilds2()
	{
		if (m_Target3 == null)
			return;
		m_Target3.GetComponent<ParticleSystem>().Stop(true);
	}

	public void PlayNewParticles3andChilds3()
	{
		if (m_Target4 == null)
			return;
		m_Target4.GetComponent<ParticleSystem>().Play(true);
	}

	public void StopNewParticles3andChilds3()
	{
		if (m_Target4 == null)
			return;
		m_Target4.GetComponent<ParticleSystem>().Stop(true);
	}
}
