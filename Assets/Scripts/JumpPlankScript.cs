using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlankScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
            PlayerCharacterScript.onRamp = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            PlayerCharacterScript.onRamp = false;
    }
}
