using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Collections.Generic;


public class PlayerController : NetworkBehaviour
{
    public enum State
    {
        Wait,
        Canon,
        Fired,
        Dead,
    }

    public GameObject[] m_canons;

    [SerializeField]
    private float m_turnSpeed = 1.0f;
    [SerializeField]
    private float m_upDownSpeed = 1.0f;
    [SerializeField]
    private float m_twistSpeed = 1.0f;
    [SerializeField]
    private float m_forceScalar = 5000f;
    [SerializeField]
    private float m_bankScalar = 1f;
    [SerializeField]
    private float m_respawnTime = 2.0f;

    // Sounds
    [SerializeField]
    private AudioClip m_deathClip;
    [SerializeField]
    private AudioClip m_shotClip;
    [SerializeField]
    private AudioClip m_ImpactClip;
    [SerializeField]
    private AudioClip m_FlyClip;
    [SerializeField]
    private AudioClip m_victoryClip;

    [SerializeField]
    private GameObject m_camera = null;

    [SerializeField]
    private Transform m_rootJoint = null;

    [SerializeField]
    private GameObject m_vignette = null;

    [SerializeField]
    private GameObject m_deathPrefab = null;
    private GameObject m_deathCrater = null;

    private GameObject m_minimap;
    private Gyroscope m_gyro;
    private bool m_gyroSupported;
    private Quaternion m_rotFix;
    private Rigidbody m_rigidBody;
    private Ragdoll m_ragDolls;

    private State m_state = State.Wait;
    private int m_netId;

    private Vector3 m_bankVector = Vector3.zero;

    private CanonControl m_canonControl;
    private List<Vector3> previousFlightPath = new List<Vector3>();

    private Animator m_animator;

    [SyncVar]
    public bool m_win = false;

    public IEnumerator RespawnAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        DrawPreviousFlightpath();
        transform.GetComponent<AudioSource>().Stop();
        InitCanon();
    }

    public void ZoomCameraIntoCanon(float distance, float seconds, Action action = null)
    {
        Vector3 originalPosition = m_camera.transform.position;
        Quaternion originalRotation = m_camera.transform.rotation;

        m_camera.transform.position += (-m_camera.transform.forward + m_camera.transform.up) * distance;
        m_camera.transform.LookAt(originalPosition);
        StartCoroutine(Transitions.TransformTo(m_camera.transform, originalPosition, originalRotation, false, seconds, action));
    }

    public void ZoomCameraOutFromDeath(float distance, float seconds, Action action = null)
    {
        Vector3 originalPosition = m_camera.transform.position;
        Quaternion originalRotation = m_camera.transform.rotation;

        Vector3 cameraToCanon = m_canonControl.transform.position - originalPosition;
        Vector3 zoomOutVector = (cameraToCanon.normalized + Vector3.up)  * distance;
        Vector3 newPosition = originalPosition + zoomOutVector;
        
        m_camera.transform.rotation = Quaternion.LookRotation(-zoomOutVector, Vector3.up);        

        StartCoroutine(Transitions.MoveTo(m_camera.transform, originalPosition + (cameraToCanon.normalized + Vector3.up) * 10f, false, 2f, () => 
        {
            StartCoroutine(Transitions.TransformTo(m_camera.transform, newPosition, Quaternion.LookRotation(-zoomOutVector, Vector3.up), false, seconds, action));
        }));
    }

    public void InitCanon()
    {
        m_animator.Play("idle");
        m_minimap.SetActive(true);

        // Put player back into canon
        transform.position = m_canonControl.transform.position;
        m_rootJoint.transform.localPosition = Vector3.zero;
        m_rootJoint.transform.localRotation = Quaternion.identity;
        transform.GetChild(0).transform.localPosition = new Vector3(0f, 0f, -4f);
        transform.GetChild(0).transform.localEulerAngles = new Vector3(90f, 0f, 0f);

        if (!m_gyroSupported)
            transform.rotation = m_canonControl.transform.rotation;

        // Reset camera
        m_camera.transform.localPosition = Vector3.zero;
        m_camera.transform.localRotation = Quaternion.identity;

        // turn canon control again
        m_canonControl.m_followPlayer = true;

        // Zoom transition
        m_state = State.Wait;
        ZoomCameraIntoCanon(30f, 1.5f, () =>
        {
            // turn on vignette
            m_vignette.SetActive(true);
            //m_canonControl.SetAlpha(0.1f);

            m_state = State.Canon;
        });
    }

    public IEnumerator ShowDeathFX()
    {
        yield return new WaitForSeconds(0.2f);
        if (m_deathCrater != null)
            Destroy(m_deathCrater);

        AudioSource.PlayClipAtPoint(m_ImpactClip, transform.position);
        m_deathCrater = Instantiate(m_deathPrefab, transform.position, Quaternion.identity) as GameObject;
    }

    void OnTriggerEnter(Collider col)
    {
        if (!isLocalPlayer)
        {
            return;
        }

        m_minimap.SetActive(true);

        m_animator.Play("idle");

        if (m_state != State.Dead && (col.gameObject.CompareTag("Floor") || col.gameObject.CompareTag("KillBox")))
        {
            AudioSource.PlayClipAtPoint(m_deathClip, transform.position);
            StartCoroutine(ShowDeathFX());

            m_state = State.Dead;
            // Show then respawn
            ZoomCameraOutFromDeath(200f, 5f, () => { StartCoroutine(RespawnAfter(m_respawnTime)); });
        }
        else if (m_state != State.Dead && 
            !(col.gameObject.CompareTag("Floor") || col.gameObject.CompareTag("Target")))
        {
            AudioSource.PlayClipAtPoint(m_ImpactClip, transform.position);
            ScoreController.instance.UpdateTotalDamage(m_netId);
        } else if (col.gameObject.CompareTag("Target"))
        {
            AudioSource.PlayClipAtPoint(m_victoryClip, transform.position);
            ScoreController.instance.UpdateClosestDistance(m_netId, transform.position);
        }
    }

    public void FireCharacter()
    {
        m_animator.Play("crazyarms");
        m_minimap.SetActive(false);

        // Stop canon follow
        m_canonControl.m_followPlayer = false;

        // turn on rigid body and apply canon force
        m_rigidBody.isKinematic = false;
        AudioSource.PlayClipAtPoint(m_shotClip, transform.position);
        m_rigidBody.AddForce(transform.forward * m_forceScalar);

        // Turn off vignette
        m_vignette.SetActive(false);
        //m_canonControl.SetAlpha(1f);

        previousFlightPath = new List<Vector3>();
        ScoreController.instance.UpdateNumberOfAttempts(m_netId);
        StartCoroutine("AddPositionToPath");
    }

    // Use this for initialization
    void Start()
    {
        m_animator = GetComponentInChildren<Animator>();
        m_minimap = GameObject.FindGameObjectWithTag("MiniMap");
        m_netId = (int)GetComponent<NetworkIdentity>().netId.Value;
        
        m_rigidBody = GetComponent<Rigidbody>();
        m_ragDolls = GetComponent<Ragdoll>();

        // Create a canon for this player
        Debug.LogFormat("My connection id is {0}", m_netId.ToString());
        //GameObject go = Instantiate(m_canons[m_netId % m_canons.Length], transform.position, transform.rotation) as GameObject;
        GameObject go = Instantiate(m_canons[0], transform.position, transform.rotation) as GameObject;
        m_canonControl = go.GetComponent<CanonControl>();
        if (m_canonControl != null)
        {
            m_canonControl.m_player = transform;
            m_canonControl.m_followPlayer = true;
        }

        if (!isLocalPlayer)
        {
            // Destroy the camera
            Destroy(m_camera);
            return;
        }
        
        // Calculate rotation offset for gyro
        m_gyroSupported = SystemInfo.supportsGyroscope;

        if (m_gyroSupported)
        {
            GameObject camParent = new GameObject("camParent");
            camParent.transform.position = transform.position;
            transform.SetParent(camParent.transform);

            m_gyro = Input.gyro;
            m_gyro.enabled = true;

            camParent.transform.rotation = Quaternion.Euler(90f, 180f, 0f);
            m_rotFix = new Quaternion(0f, 0f, 1f, 0f);
        }

        CmdInitOnServer();
        InitCanon();
    }

    [Command]
    public void CmdInitOnServer()
    {
        //TargetSpawner.instance.CreateTarget();
        ScoreController.instance.OnPlayerConnect(m_netId, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if ((m_state == State.Canon) && (Input.GetKeyDown(KeyCode.Space) || Input.GetButton("Fire1")))
        {
            m_state = State.Fired;
            FireCharacter();
            transform.GetComponent<AudioSource>().clip = m_FlyClip;
            transform.GetComponent<AudioSource>().Play();
        }

        if (m_state == State.Dead || m_state == State.Wait)
            return;
        
        var rotateUpDown = Input.GetAxis("Vertical") * m_upDownSpeed;
        var rotateLeftRight = Input.GetAxis("Horizontal") * m_turnSpeed;
        float rotateTwist = 0f;
        if (Input.GetKey(KeyCode.Q))
        {
            rotateTwist = m_twistSpeed;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            rotateTwist = -m_twistSpeed;
        }

        transform.localEulerAngles += new Vector3(rotateUpDown, rotateLeftRight, rotateTwist);
        
#if UNITY_ANDROID || UNITY_IPHONE

        if ((m_state == State.Canon) && (Input.touchCount > 0))
        {
            m_state = State.Fired;
            FireCharacter();
        }

        // Use Gyro if supported
        if (m_gyroSupported)
        {
            transform.localRotation = m_gyro.attitude * m_rotFix;
        }

#endif // Unity_ANDROID

    }
    
    void FixedUpdate()
    {
        if (m_state == State.Fired)
        {
            float angleScalar = Mathf.Lerp(0f, 1f, Vector3.Angle(m_rigidBody.velocity, transform.forward)/90);
            
            Vector3 velToFor = (transform.forward.normalized * angleScalar);
            velToFor.y = 0;
            m_rigidBody.velocity += velToFor * m_bankScalar;
        }
    }

    IEnumerator AddPositionToPath()
    {
        while (m_state == State.Fired)
        {
            previousFlightPath.Add(transform.position);
            yield return new WaitForSeconds(1);
        }   
    }

    void DrawPreviousFlightpath()
    {
        var line = GetComponent<LineRenderer>();
        line.numPositions = previousFlightPath.Count;
        for (var i = 0; i < previousFlightPath.Count; i++)
        {
            line.SetPosition(i, previousFlightPath[i]);
        }
    }

    void OnDestroy()
    {
        // Destroy the canon too
        Destroy(m_canonControl.gameObject);
    }

    public void PlayerWon()
    {
    }
}
