using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int playerScore = 0;

    [SerializeField] TextMeshProUGUI livexText;
    [SerializeField] TextMeshProUGUI scoreText;

    void Awake()
    {
        // todo : Çok basit mantığı vardır. Awake herşeyden önce çalışır.
        // todo : ScenePersist'leri arıyoruz.
        // todo : Eğer bu objeler 1'den büyük ise, birini destroy ediyoruz.
        // todo : Eğer değilse Bunu destryo etme diyip, var olan objelerimiz korumunu sağlıyoruz.
        // todo : Bu şekilde text'lerimiz aynı levelde olsak bile, lives sıfırlanmıyor
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }


    private void Start()
    {
        livexText.text = playerLives.ToString();
        scoreText.text = playerScore.ToString();
    }

    public void AddToScore(int point)
    {
        playerScore += point;
        scoreText.text = playerScore.ToString();
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    private void TakeLife()
    {
        playerLives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        livexText.text = playerLives.ToString();
    }

    private void ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
