using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    private AudioSource sound;

    void Awake()
    {
        sound = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            sound.Play();
            collision.GetComponent<PlayerController>().life++;
            Destroy(this.gameObject, 0.1f);
        }
    }
}