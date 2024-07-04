using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D colission)
    {
        if(colission.CompareTag("Keeper"))
        {
            colission.GetComponent<KeeperController>().life--;
        }
        if(colission.CompareTag("Gizmo"))
        {
            colission.GetComponent<GizmoController>().life--;
        }
    }  
}
