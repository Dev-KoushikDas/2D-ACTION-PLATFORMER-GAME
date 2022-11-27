using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class collect_point : MonoBehaviour
{
    [SerializeField] private AudioSource collect_audio;

//    private int collectables = 0;
//    [SerializeField] private Text coll;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("reaper"))
        {

            // collect_audio.Play();
          //  collectables++;
          //  coll.text = ""+ collectables;
            Destroy(gameObject);
       
        }
    }
}

