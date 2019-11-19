using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKillScript : MonoBehaviour
{ 

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            PlayerCharacterScript.dead = true;
    }
}
