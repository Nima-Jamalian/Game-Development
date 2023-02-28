using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] Color color0;
    [SerializeField] Color color1;
    [SerializeField] Color colorOutput;
    [Range(0,1)]
    [SerializeField] float t = 0;
    [SerializeField] float duration;
    [SerializeField] float currentTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

            if(currentTime <= duration)
            {
                currentTime += Time.deltaTime;
                colorOutput = Color.Lerp(color0, color1, currentTime/duration);
            }

    }
}
 