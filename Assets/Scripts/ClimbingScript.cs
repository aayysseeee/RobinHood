using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbingScript : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            PlayerCharacterScript.climbing = true;

     
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (this.gameObject.tag == "Vines")
            {
                PlayerCharacterScript.onVines = true;
                PlayerCharacterScript.vines = this.gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Vector3 temp = other.gameObject.transform.position;

            PlayerCharacterScript.onVines = false;
            PlayerCharacterScript.climbing = false;

            other.transform.parent = null;
            other.gameObject.transform.position = temp;

        }
    }
}
