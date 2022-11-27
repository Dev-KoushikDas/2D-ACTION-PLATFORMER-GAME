using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Playerscript : MonoBehaviour
{   
    [SerializeField]
    private float moveForce = 1f;
    [SerializeField]
    private float jumpForce = 3f;
    [SerializeField]
    private float movementX;
    [SerializeField]
    private Rigidbody2D myBody;
    [SerializeField]
    private SpriteRenderer sr; 
    [SerializeField]
    private Animator anim;

 
    private bool at_right = true;


    [SerializeField]
    private string Walk_anim = "Walk";

    //private string ENEMY_TAG = "enemy";

    private string HOME_TAG = "Finish";

    private string GROUND_TAG = "ground";

    private bool isGrounded;

    public Animator animator;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    public int attackDamage = 30;


    public int maxHealth = 200;
    int player_currentHealth;


    public healthbar healthBar;

    public Transform Balloon;

    //  public Text count;


    private int collectables = 0;
    [SerializeField] private Text coll;


    [SerializeField] private AudioSource attack_audio;
    [SerializeField] private AudioSource attack_hit_audio;
    [SerializeField] private AudioSource walk_audio;
    [SerializeField] private AudioSource collect_audio;


    public float attackRate = 2f;
    float nextAttackTime = 0f; 

    // Start is called before the first frame update

    void Start()
    {
        player_currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMoveKeyboard();
        AnimatePlayer();
        PlayerJump();
        PlayerAttack();
        

           
  }

    private void FixedUpdate()//checks input after a fixed intrval of time determined by settings but not evey frame
    {

    }



    void PlayerMoveKeyboard()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        
        transform.position += new Vector3(movementX, 0f, 0f) * moveForce * Time.deltaTime;

        
        
    }
    void AnimatePlayer()
    {
        if (movementX > 0 )
        {
            anim.SetBool(Walk_anim, true);
        //  walk_audio.Play();

            if (at_right == false)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                at_right = true;
                


            }
            /*sr.flipX = false;
            if(at_right == false)
            {
                point.position.x *= -1;
                at_right = true;
            }*/

        }
        else if(movementX < 0 )
        {
            anim.SetBool(Walk_anim, true);
          //walk_audio.Play();


            if (at_right == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                at_right = false;
                
            }

            /*
            sr.flipX = true;
        
            if (at_right == true)
            {
                point.position.x *= -1;
                at_right = false;
            }
            */
        }
        else
        {
            anim.SetBool(Walk_anim, false);
        }
    }

    void PlayerJump()
    {   //Jump is predefined -> pc space 
        //console x 
        //mobile touch
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isGrounded = false;
            myBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        
        }
    }

    void PlayerAttack()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                animator.SetTrigger("Attack");

                attack_audio.Play();

                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

                foreach (Collider2D enemy in hitEnemies)
                {
                    Debug.Log("we hit" + enemy.name);
                    if (enemy.GetComponent<enemy_1>())
                    {
                        enemy.GetComponent<enemy_1>().TakeDamage(attackDamage);
                    }
                    else if (enemy.GetComponent<petrol_script>())
                    {

                        enemy.GetComponent<petrol_script>().TakeDamage(attackDamage);
                    }
                    else if (enemy.GetComponent<take_damage>())
                    {

                        enemy.GetComponent<take_damage>().TakeDamage(attackDamage);
                    }
                    else if (enemy.GetComponent<boss_primary_sr>())
                    {
                        enemy.GetComponent<boss_primary_sr>().TakeDamage(attackDamage);
                    }

                    attack_hit_audio.Play();
                    nextAttackTime = Time.time + 1f / attackRate;
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*if (collision.gameObject.CompareTag(ENEMY_TAG)){
            Destroy(gameObject);
        }*/

        if (collision.gameObject.CompareTag(GROUND_TAG)){
            isGrounded = true;
        }

        else if (collision.gameObject.CompareTag(HOME_TAG))
        {
            Destroy(gameObject);
            SceneManager.LoadScene("main_menu");
        }
      else if (collision.gameObject.CompareTag("collect"))
        {
            collect_audio.Play();
            health_increase();
            collectables++;
            coll.text = "" + collectables;

            //    Destroy(collision.gameObject);
        }

        
        //  else if (collision.gameObject.CompareTag("balloon"))
        // {
        //Vector3 pos = transform.position;
        //transform.position = Balloon.position;

        //  collision.transform.SetParent(Balloon.transform); 
        //  }

    }
    /*
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("balloon")) {
            collision.transform.SetParent(null);
        }
    }*/
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("collect"))
        {
            collect_audio.Play();
            health_increase();

       //     Destroy(gameObject);
        }
    }

   */ 

    public void Player_TakeDamage(int damage)
    {
        player_currentHealth -= damage;
        //Debug.Log("Player hit !" + player_currentHealth);
        healthBar.SetHealth(player_currentHealth);
        animator.SetTrigger("isHurt");

        if (player_currentHealth <= 0)
        {
            
            Die();
        }
        else
        {
            animator.SetBool("dead", false);
        }
    }

    void Die()
    {

        Debug.Log("Player died!");
        animator.SetBool("dead", true);


        // GetComponent<Collider2D>().enabled = false;

        this.enabled = false;

        SceneManager.LoadScene("main_menu");
    }

    void Walk_sound()
    {

        if (isGrounded)
        {
            walk_audio.Play();
        }
    }


    public void health_increase()
    {
        if(player_currentHealth + 7 > maxHealth)
        {
            player_currentHealth = maxHealth;
            healthBar.SetHealth(player_currentHealth);
        }
        else
        {
            player_currentHealth += 5;
            healthBar.SetHealth(player_currentHealth);
        }
    }
}
