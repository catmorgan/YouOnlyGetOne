using UnityEngine;
using UnityEngine.Networking;

public class TargetSpawner : NetworkBehaviour 
{
    public static TargetSpawner instance = null;
    
    public GameObject m_targetPrefab;
    private Vector3 m_targetPosition = new Vector3(1542f, -7, 10);
    public Transform m_targetTransform;

    private float m_minRadius;
    private float m_maxRadius;
    
    public override void OnStartAuthority()
    {
        // Default
        Vector3 raySource = Random.insideUnitSphere.normalized * Random.Range(m_minRadius, m_maxRadius);
        raySource.y = 20;
        m_targetPosition = raySource;

        // Raycast to the floor
        RaycastHit hit;
        raySource.y = 3000;
        Ray ray = new Ray(raySource, Vector3.down);
        Physics.Raycast(ray, out hit, 10000);
        
        // Keep raycasting until we hit the floor
        while (!hit.collider.gameObject.CompareTag("Floor"))
        {
            ray = new Ray(raySource, Vector3.down);
            raySource = Random.insideUnitSphere.normalized * Random.Range(m_minRadius, m_maxRadius);
            raySource.y = 3000;
            Physics.Raycast(ray, out hit, 10000);
            m_targetPosition = hit.point;
        }

        //m_targetTransform.transform.position = m_targetPosition;
        //var target = (GameObject)Instantiate(Resources.Load("TargetSpawn"), m_targetPosition, Quaternion.identity);
        var target = (GameObject)Instantiate(m_targetPrefab, m_targetPosition, Quaternion.identity);
        NetworkServer.Spawn(target);
        ScoreController.instance.targetPosition = target.transform.position;
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = new TargetSpawner();
        }
        else
        {
            instance = this;
        }
        SphereCollider[] col = GetComponents<SphereCollider>();

        m_minRadius = col[0].radius;
        m_maxRadius = col[1].radius;

        // Destroy the collider since we're only using it to calculate radius
        Destroy(col[1]);
        Destroy(col[0]);
    }
}