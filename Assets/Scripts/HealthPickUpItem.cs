using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUpItem : MonoBehaviour
{
    public float healthValue = 1f;
    [SerializeField] float speed = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 1 * speed * Time.deltaTime, 0);
    }
}
