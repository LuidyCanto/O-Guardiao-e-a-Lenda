using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f; // Velocidade do projétil
    private Vector2 direction; // Direção do movimento do projétil
    public float lifetime = 5f; // Tempo de vida    do projétil

    // Start is called before the first frame update
    void Start()
    {
        // Destruir o projétil após o tempo de vida
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        // Mover o projétil na direção definida
        transform.Translate(direction * speed * Time.deltaTime);
    }

    // Método para definir a direção do projétil
    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized;
    }

    // Método chamado ao colidir com outro objeto
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Adicione lógica para causar dano ao jogador aqui
            Destroy(gameObject); // Destruir o projétil ao colidir com o jogador
        }
    }

    // void Shoot()
    // {
    //     // Instanciar o projétil no firePoint
    //     GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

    //     // Definir a direção do projétil em direção ao jogador
    //     Bullet bulletScript = projectile.GetComponent<Bullet>();
    //     if (bulletScript != null)
    //     {
    //         Vector2 direction = (player.position - firePoint.position).normalized;
    //         bulletScript.SetDirection(direction);
    //     }
    // }

}