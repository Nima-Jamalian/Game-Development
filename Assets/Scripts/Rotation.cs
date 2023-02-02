using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] Vector3 currentQuaternionRotation;
    [SerializeField] Vector3 currentAngleRotation;
    [SerializeField] Vector3 angleToRoate;
    // Start is called before the first frame update
    void Start()
    {
        currentQuaternionRotation = new Vector3(transform.rotation.x,transform.rotation.y,transform.rotation.z);
        currentAngleRotation = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y,transform.eulerAngles.z);

        transform.localEulerAngles = angleToRoate;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
