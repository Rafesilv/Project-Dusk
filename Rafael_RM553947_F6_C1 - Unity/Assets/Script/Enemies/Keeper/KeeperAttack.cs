using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeeperAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D colission)
    {
        if(colission.CompareTag("Player"))
        {
            colission.GetComponent<PlayerController>().life--;
        }
    }  
}
