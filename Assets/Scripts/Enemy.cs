using System.Collections;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public int enemie_health = 100;
    public GameObject projectilePrefab; // Prefab do projétil
    public Transform firePoint; // Ponto de onde o projétil é disparado
    public float fireRate = 1f; // Taxa de disparo (um tiro por segundo, por exemplo)
    private float nextFireTime = 0f;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (enemie_health <= 0)
        {
            Destroy(gameObject);
        }

        // Verificar se o inimigo pode atirar (com base na taxa de disparo)
        if (Time.time >= nextFireTime)
        {
            StartCoroutine(Shoot());
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    private IEnumerator Shoot()
    {
        anim.SetBool("attack", true);

        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length / 2);

        // Instancia o projétil
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        anim.SetBool("attack", false);

        // Define a direção do projétil para frente
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = firePoint.right * 10f; // Ajuste a velocidade conforme necessário
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