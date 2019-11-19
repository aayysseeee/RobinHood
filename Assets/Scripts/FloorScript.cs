using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MonoBehaviour
{
    public static bool allowFloor;

    private void OnTriggerStay(Collider other)
    {
        if (!allowFloor)
        {
            if (other.transform.tag == "Player")
                PlayerCharacterScript.restart = true;
        }
    }
}
