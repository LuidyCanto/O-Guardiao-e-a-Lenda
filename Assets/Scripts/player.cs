using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    private Rigidbody2D rig;

    public bool isJumping;
    public bool doubleJumping;

    private Animator anim;

    public GameObject bullet, spawnerBulletPos;
    public int health;
    public Image healthBar;
    float currentHealth;
    private float maxHealth = 100;

    public float fireRate = 1f; // Adiciona a taxa de disparo
    private float nextFireTime = 0f; // Controla o tempo do próximo disparo

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        health = 100;

        // Notifica o GameController que o jogador foi inicializado
        if (gameController.instance != null)
        {
            gameController.instance.SetPlayer(this.gameObject);
        }
    }

    void Update()
    {
        currentHealth = health;
        healthBar.fillAmount = currentHealth / maxHealth;
        Move();
        Jump();
        if (Input.GetMouseButtonDown(0) && Time.time >= nextFireTime)
        {
            StartCoroutine(Shoot());
            nextFireTime = Time.time + 1f / fireRate; // Atualiza o tempo do próximo disparo
        }

        if (Input.GetKeyDown("e"))
        {
            health += 30;
        }
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    

    void Move()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * Speed;
        if (Input.GetAxis("Horizontal") > 0f) // andando pra direita 
        {
            anim.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            spawnerBulletPos.transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        else if (Input.GetAxis("Horizontal") < 0f) // andando pra esquerda
        {
            anim.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            spawnerBulletPos.transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
        else
        {
            anim.SetBool("walk", false);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (!isJumping)
            {
                rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                doubleJumping = true;
                anim.SetBool("jump", true);
            }
            else if (doubleJumping)
            {
                rig.AddForce(new Vector2(0f, JumpForce * 1.5f), ForceMode2D.Impulse);
                doubleJumping = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            isJumping = false;
            anim.SetBool("jump", false);
        }
        if (collision.gameObject.tag == "fall")
        {
            gameController.instance.ShowGameOver();
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            isJumping = true;
        }
    }

    private IEnumerator Shoot()
    {
        anim.SetBool("atack", true);

        // Aguarda o fim da primeira metade da animação
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length / 2);

        // Instancia o projétil
        GameObject projectile = Instantiate(bullet, spawnerBulletPos.transform.position, Quaternion.identity);

        anim.SetBool("atack", false);

        // Define a direção do projétil com base na orientação do player
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Verifica para qual lado o player está olhando
            Vector2 direction = transform.eulerAngles.y == 0 ? Vector2.right : Vector2.left;
            rb.velocity = direction * 10f; // Ajuste a velocidade conforme necessário

            // Gira o projétil para que a ponta esteja sempre na direção do movimento
            projectile.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y == 0 ? 0 : 180, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            health -= 10;
        }
    }

    public int GetHealth()
    {
        return health;
    }
}