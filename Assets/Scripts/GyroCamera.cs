using UnityEngine;

public class GyroCamera : MonoBehaviour
{
    private Gyroscope gyro;
    private bool gyroSupported;
    private Quaternion rotFix;

    //[SerializeField]
    //private Transform zoomObj;

	// Use this for initialization
	void Start ()
    {
        gyroSupported = SystemInfo.supportsGyroscope;

        GameObject camParent = new GameObject("camParent");
        camParent.transform.position = transform.position;
        transform.SetParent(camParent.transform);

        if (gyroSupported)
        {
            gyro = Input.gyro;
            gyro.enabled = true;

            camParent.transform.rotation = Quaternion.Euler(90f, 180f, 0f);
            rotFix = new Quaternion(0f, 0f, 1f, 0f);
        }
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.localRotation = gyro.attitude * rotFix;
	}
    
}


