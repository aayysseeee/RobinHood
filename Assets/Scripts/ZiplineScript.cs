using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZiplineScript : MonoBehaviour
{
    private Transform[] children;
    public GameObject startPoint, endPoint;

    void InitialisePoints()
    {
        children = gameObject.GetComponentsInChildren<Transform>();
        foreach (Transform i in children)
        {     
            if (i.tag == "StartPoint")
                startPoint = i.GetComponent<GameObject>();
            else if (i.tag == "EndPoint")
                endPoint = i.GetComponent<GameObject>();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            InitialisePoints();
            if (Input.GetKey(KeyCode.Space))
            {
                PlayerCharacterScript.ziplineStart = startPoint;
                PlayerCharacterScript.ziplineEnd = endPoint;
                PlayerCharacterScript.ziplining = true;
            }
        }
    }

}
