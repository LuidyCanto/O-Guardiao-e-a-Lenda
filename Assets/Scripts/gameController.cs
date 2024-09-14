using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameController : MonoBehaviour
{
    public int totalScore;
    public Text scoreText;

    public static gameController instance;
    public GameObject gameOver;

    private Player player; // Agora é do tipo Player, não GameObject
    public GameObject playerObject;  // Referência ao GameObject do Player


    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        // Se o playerObject for nulo, encontra o player dinamicamente
        if (playerObject == null)
        {
            playerObject = GameObject.FindWithTag("Player");
        }

        // Acessa o componente Player associado ao GameObject
        if (playerObject != null)
        {
            player = playerObject.GetComponent<Player>();
        }
    }

    public void SetPlayer(GameObject playerObj)
    {
        player = playerObj.GetComponent<Player>(); // Obtém o componente Player do GameObject
    }

    void Update()
    {
        if (player != null && player.health <= 0)
        {
            ShowGameOver();
        }
    }

    public void UpdateScoreText()
    {
        scoreText.text = totalScore.ToString();
    }

    public void ShowGameOver()
    {
        gameOver.SetActive(true);
        Time.timeScale = 0f;
        StartCoroutine(WaitAndLoadScene());
    }

    private IEnumerator WaitAndLoadScene()
    {
        yield return new WaitForSecondsRealtime(3f); // Espera 3 segundos em tempo real
        Time.timeScale = 1f; // Restaura o tempo antes de carregar a cena
        SceneManager.LoadScene("0"); // Substitua "NomeDaCena" pelo nome da cena que você deseja carregar
    }
}
