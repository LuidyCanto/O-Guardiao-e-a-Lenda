using System.Collections;
using UnityEngine;

public class BossFinal : MonoBehaviour
{
    public int enemie_health = 100;
    public GameObject projectilePrefab; // Prefab do projétil
    public Transform firePoint; // Ponto de onde o projétil é disparado
    public float fireRate = 1f; // Taxa de disparo (um tiro por segundo, por exemplo)
    private float nextFireTime = 0f;
    private Animator anim;
    public float detectionRange = 10f; // Alcance de detecção do player
    private Transform player;
    private Player playerController;

    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (enemie_health <= 0)
        {
            Destroy(gameObject);

        }

        DetectPlayerAndShoot();
    }

    void DetectPlayerAndShoot()
    {
        if (Vector2.Distance(transform.position, player.position) <= detectionRange)
        {
            // Verificar se o inimigo pode atirar (com base na taxa de disparo)
            if (Time.time >= nextFireTime)
            {
                StartCoroutine(Shoot());
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    private IEnumerator Shoot()
    {
        anim.SetBool("attack", true);

        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length / 2);

        // Instancia o projétil
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        anim.SetBool("attack", false);

        // Define a direção do projétil para o player
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 direction = (player.position - firePoint.position).normalized;
            rb.velocity = direction * 10f; // Ajuste a velocidade conforme necessário
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "projetil")
        {
            enemie_health -= 50;
        }
    }
}