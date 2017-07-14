using UnityEngine;
using System.Collections;

public class Ragdoll : MonoBehaviour
{
    private Rigidbody m_currentRB;
    private Rigidbody[] m_rigRBs;
    private Vector3 m_inheritVelocity;
    [SerializeField]
    private Rigidbody m_rootJoint = null;

    // Turns on ragdoll state temporarily
    public IEnumerator TurnRagdollOnForTime(float timeInSeconds)
    {
        m_currentRB.isKinematic = true;
        SetRagdollState(true);

        // inherit velocity
        m_rootJoint.velocity = m_inheritVelocity * 0.3f;
        yield return new WaitForSeconds(timeInSeconds);
        SetRagdollState(false);
    }
    
    // Use this for initialization
    private void SetRagdollState(bool state = false)
    {
        foreach (var rb in m_rigRBs)
            if (rb != m_currentRB)
                rb.isKinematic = !state;
    }

    void Awake()
    {
        m_currentRB = GetComponent<Rigidbody>();

        m_rigRBs = GetComponentsInChildren<Rigidbody>();
        //SetRagdollState(false);
        //m_currentRB.isKinematic = true;
    }

    // Turn off all other rigidbodies when we've landed
    void OnTriggerEnter(Collider col)
    {
        
        if (col.gameObject.CompareTag("Floor") || col.gameObject.CompareTag("Target"))
        {
            m_inheritVelocity = m_currentRB.velocity;
        
            StartCoroutine(TurnRagdollOnForTime(2f));
        }
    }
}
