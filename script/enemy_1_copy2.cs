using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_1_copy2 : MonoBehaviour
{

    public int maxHealth = 150;
    int currentHealth;


    public Animator animator;


    public Transform player;

    public bool isFlipped = false;


    public int attackDamage = 10;

    public Vector3 attackOffset;
    public float attackRange = 1f;
    public LayerMask attackMask;


    public float range;
    private float distToPlayer;


    public float speed;
    public float distance;

    private bool movingRight = true;

    public Transform groundDetection;
    public LayerMask groundLayer;





    [SerializeField] private AudioSource die_audio;



    [SerializeField]
    private Transform collectable_spawn;

    [SerializeField]
    private GameObject[] collectables;
    private GameObject spawnedcollectable;
    private int randomIndex;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        // mustPatrol = true;
    }

    void Update()
    {
        distToPlayer = Vector2.Distance(transform.position, player.position);

        if (distToPlayer <= range)
        {
            animator.SetBool("inRange", true);
        }
        else
        {
            animator.SetBool("inRange", false);
            Petrol();
        }
    }


    void Petrol()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance, groundLayer);
        if (groundInfo.collider == false)
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
                // isFlipped = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
                //  isFlipped = true;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died!");

        die_audio.Play();
        animator.SetBool("isDead", true);
        die_audio.Play();
        StartCoroutine(SpawnMonsters());
        GetComponent<Collider2D>().enabled = false;

        this.enabled = false;
    }

    public void LookAtPlayer()
    {
        /*
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;
        */
        if (transform.position.x > player.position.x && movingRight)
        {   /*
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
            */
            transform.eulerAngles = new Vector3(0, -180, 0);
            movingRight = false;
        }

        else if (transform.position.x < player.position.x && !movingRight)
        {   /*
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
            */
            transform.eulerAngles = new Vector3(0, 0, 0);
            movingRight = true;

        }



    }

    public void Attack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
            colInfo.GetComponent<Playerscript>().Player_TakeDamage(attackDamage);
        }
    }





    IEnumerator SpawnMonsters()
    {
        for (int i = 0; i < 1; i++)
        {
            yield return new WaitForSeconds(Random.Range(0, 1));

            randomIndex = 0;
            //  randomSide = Random.Range(0, 2);

            spawnedcollectable = Instantiate(collectables[randomIndex]);

            spawnedcollectable.transform.position = collectable_spawn.position;

            //left side
            /* if (randomSide == 0)
               {
                   spawnedMonster.transform.position = leftPos.position;
                   spawnedMonster.GetComponent<monster>().speed = 0.6f;
                   // spawnedMonster.transform.localScale = new Vector3(-0.169278f, 0.17538f, 1f);
              } */
            /*  else
              {
                  //right side
                  spawnedMonster.transform.position = rightPos.position;
                  spawnedMonster.GetComponent<monster>().speed = -Random.Range(4, 10);

              }*/

        }
    }



}
