using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RubyControllerModified : MonoBehaviour
{
    public float speed = 4;

    public int maxHealth = 5;
    public float timeInvincible = 2.0f;
    public ParticleSystem hitParticle;
    
    public GameObject projectilePrefab;
    public GameObject YouLoseText;
        public GameObject WinText;

    public GameObject Enemy1;
    public GameObject Enemy2;
    public GameObject Enemy3;
    public GameObject Enemy4;

    public AudioClip hitSound;
    public AudioClip shootingSound;
        public AudioClip winmusic;
            public AudioClip losemusic;


    
    public int health
    {
        get { return currentHealth; }
    }
    
    Rigidbody2D rigidbody2d;
    Vector2 currentInput;
    
    int currentHealth;
    float invincibleTimer;
    bool isInvincible;
   
    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);
    
    AudioSource audioSource;
    
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
                
        invincibleTimer = -1.0f;
        currentHealth = maxHealth;
        
        animator = GetComponent<Animator>();
        
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
       if(Enemy1.tag == "Counted" && Enemy2.tag == "Counted" && Enemy3.tag == "Counted" && Enemy4.tag == "Counted" ){
                       YouWin();

       }
        if(Input.GetKeyDown(KeyCode.R))
            {
                            print("r key was pressed");

            YouLose();
                 }
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
                
        Vector2 move = new Vector2(horizontal, vertical);
        
        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        currentInput = move;



        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);


        if (Input.GetKeyDown(KeyCode.C))
            LaunchProjectile();
        
        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, 1 << LayerMask.NameToLayer("NPC"));
            if (hit.collider != null)
            {
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                if (character != null)
                {
                    character.DisplayDialog();
                }  
            }
        }
 
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        
        position = position + currentInput * speed * Time.deltaTime;
        
        rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        { 
            if (isInvincible)
                return;
            
            isInvincible = true;
            invincibleTimer = timeInvincible;
            
            animator.SetTrigger("Hit");
            audioSource.PlayOneShot(hitSound);

            Instantiate(hitParticle, transform.position + Vector3.up * 0.5f, Quaternion.identity);
        }
        
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
                UIHealthBar.Instance.SetValue(currentHealth / (float)maxHealth);

        if(currentHealth == 0){
        speed =0;
           YouLoseText.SetActive(true);
        }
        
        
    }
    
 void YouLose(){
     SceneManager.LoadScene(0);
                 audioSource.PlayOneShot(losemusic);


 } void YouWin(){
    speed =0;
            audioSource.PlayOneShot(winmusic);

           WinText.SetActive(true);
 }
    
    void LaunchProjectile()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);
        
        animator.SetTrigger("Launch");
        audioSource.PlayOneShot(shootingSound);
    }
    
  
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
