using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    Renderer body;
    public Material orange, red;
    private void Start()
    {
        body = GetComponentInChildren<Renderer>();
        body.material = orange;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
                transform.LookAt(other.gameObject.transform);
                transform.Translate(Vector3.forward * 4f * Time.deltaTime);
                body.material = red;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            if(this.gameObject.activeInHierarchy)
            body.material = orange;

        }
    }
}
