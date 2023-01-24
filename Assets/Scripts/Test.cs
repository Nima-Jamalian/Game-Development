using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    //Data Type
    /*
     * private/public typeOfData GiveItName = Value
     * int: 1,2,3,4
     * float: 1.2, 2.4, 5.3
     * bool: true/false
     * String: "value"
    */
    [SerializeField] private int health = 100;
    private float damage = 1.4f;
    private string playerName = "Hello World!";
    [SerializeField] private bool isPlayerAlive = true;
    [SerializeField] GameObject cube;
    [SerializeField] List<GameObject> cubeList = new List<GameObject>();
    private bool isCubeActive = true;
    [SerializeField] int a = 1;
    [SerializeField] int b = 5;
    [SerializeField] int day = 1;

    //Array
    [SerializeField] int[] arrayValues = new int[2];

    //List
    [SerializeField] List<int> listValues = new List<int>();
    /*
     * a = 2
     * b = 5
     * 
     * a+b=7
     * 
     * return
     */
    // Start is called before the first frame update
    void Start()
    {
        cube.GetComponent<MeshRenderer>().material.color = Color.red;
        Debug.Log(arrayValues[0]);
        Debug.Log(arrayValues[1]);
        Debug.Log(listValues[0]);
        Debug.Log(listValues[1]);

        for (int i = 0; i < cubeList.Count; i++)
        {
            if (i == 0)
            {
                cubeList[i].GetComponent<MeshRenderer>().material.color = Color.red;
            }
            else if (i == 1)
            {
                cubeList[i].GetComponent<MeshRenderer>().material.color = Color.yellow;
            }
            else
            {
                cubeList[i].GetComponent<MeshRenderer>().material.color = Color.blue;
            }
        }

        foreach (GameObject myCube in cubeList)
        {
            myCube.GetComponent<MeshRenderer>().material.color = Color.red;
        }

        for(int i=0; i<listValues.Count; i++)
        {
            Debug.Log(listValues[i]);

        }
        //var age = 29;
        //Debug.Log(age);
        //Plus1(a);
        //Debug.Log(Addition(a,b));

        //switch (day)
        //{
        //    case 1:
        //        Debug.Log("Monday");
        //        break;
        //    case 2:
        //        Debug.Log("Tuesday");
        //        break;
        //    case 3:
        //        Debug.Log("Wednseday");
        //        break;
        //    default:
        //        Debug.Log("Error");
        //        break;
        //}

        //if(day == 1)
        //{
        //    Debug.Log("Monday");
        //} else if (day == 2)
        //{
        //    Debug.Log("Tuesday");
        //} else if (day == 3)
        //{
        //    Debug.Log("Wednseday");
        //}
    }

    // Update is called once per frame
    void Update()
    {
        CubeStatus();
  
        if (isPlayerAlive == true)
        {
            health--;
        }
        //print(health);
        if(health <= 0)
        {
            isPlayerAlive = false;
        }
    }

    private int Addition(int value1, int value2)
    {
        return value1 + value2;
    }

    private void Plus1(int value)
    {
        value = value + 1;
        print(value);
    }
    

    private void CubeStatus()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isCubeActive == true)
            {
                cube.SetActive(false);
                isCubeActive = false;
            }
            else
            {
                cube.SetActive(true);
                isCubeActive = true;
            }
        }
    }


    private int CheckPlayerHealth()
    {
        return health;
    }
}
