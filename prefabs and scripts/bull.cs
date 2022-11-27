using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bull : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;   
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Playerscript player = hitInfo.GetComponent<Playerscript>();
        if(player != null)
        {
            player.Player_TakeDamage(10);
        }
        Destroy(gameObject);
    }
}
