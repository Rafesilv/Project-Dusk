using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoKitten : MonoBehaviour
{
    public Transform gizmo;

       private void OnTriggerEnter2D(Collider2D colission)
    {
        if(colission.CompareTag("Player"))
        {
            gizmo.GetComponent<GizmoController>().enabled = true;
            gizmo.GetComponent<Animator>().SetBool("IsRun", true);
        }
    }  

           private void OnTriggerExit2D(Collider2D colission)
    {
        if(colission.CompareTag("Player"))
        {
            gizmo.GetComponent<GizmoController>().enabled = false;
            gizmo.GetComponent<Animator>().SetBool("IsRun", false);

        }
    }  
    
}
