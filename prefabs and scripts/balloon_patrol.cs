using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balloon_patrol : MonoBehaviour
{
    // Start is called before the first frame update
    //void Start()
    //{

    //}
    public float speed;
    public float distance;

    private bool movingRight = true;

    public Transform groundDetection;
    public LayerMask groundLayer;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance, groundLayer);
        if (groundInfo.collider == false)
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }

    

    private void OnCollisionEnter2D(Collision2D collision) { 
        if (collision.gameObject.CompareTag("reaper")) { 
        //Vector3 pos = transform.position;
        // pos.x = Balloon.position.x;

        collision.gameObject.transform.SetParent(transform);
        }

    }


    private void OnCollisionExit2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("reaper"))
    {
            collision.gameObject.transform.SetParent(null);
        }
}

}
