using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemie"))
        {
            // Adicione lógica para causar dano ao jogador aqui
            Destroy(gameObject); // Destruir o projétil ao colidir com o jogador
        }
    }
}

