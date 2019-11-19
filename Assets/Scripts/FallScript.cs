using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallScript : MonoBehaviour
{
    public GameObject attachPoint, fallObj, fallTo;
    Vector3 fallPosition;
    
    void Start()
    {
        fallPosition = fallObj.transform.position;
        fallPosition.y = fallTo.transform.position.y;
    }

    void Update()
    {
        if (!attachPoint)
            if (fallObj.transform.position.y > fallPosition.y)
                fallObj.transform.Translate(Vector3.down * 30f * Time.deltaTime);
    }

    
}
