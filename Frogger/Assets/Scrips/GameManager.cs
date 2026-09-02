using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Game References")]
    [SerializeField] private Home[] homes;
    [SerializeField] private Frogger frogger;

    [Header("UI")]
    [SerializeField] private Text scoreText;
    [SerializeField] private Text livesText;
    [SerializeField] private Text timeText;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject startMenu;

    [Header("Game Settings")]
    private int score;
    private int lives;
    private int time;

    public int Score => score;
    public int Lives => lives;
    public float Time => time;


    private void Awake()
    {
        
    }
    private void NewGame()
    {
        frogger.gameObject.SetActive(true);
        gameOverMenu.SetActive(false);
        startMenu.SetActive(false);

        SetScore(0);
        SetLives(3);
        NewLevel();

    }

    private void StartGameMenu()
    {
        frogger.gameObject.SetActive(false);
        gameOverMenu.SetActive(false);
        startMenu.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(PlayAgain()); 
    }
    private void Start()
    {
        StartGameMenu();
    }

    private void NewLevel()
    {
        frogger.gameObject.SetActive(true);
        for (int i = 0; i < homes.Length; i++) {
            homes[i].enabled = false;
        }
        Respawn();
    }


    private void Respawn()
    {
        frogger.Respawn();

        StopAllCoroutines();    
        StartCoroutine(Timer(30));
    }

    private IEnumerator Timer(int duration)
    {
        time = duration;
        timeText.text = time.ToString();

        while (time > 0) { 
            yield return new WaitForSeconds(1);

            time--;
            timeText.text = time.ToString();   
        }

        frogger.Death();
    }

    private void GameOver()
    {
        frogger.gameObject.SetActive(false);
        gameOverMenu.SetActive(true);

        StopAllCoroutines();
        StartCoroutine(PlayAgain());
    }

    private IEnumerator PlayAgain()
    {
        bool playAgain = false;

        while (!playAgain)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                playAgain = true;
            }

            yield return null;
        }
        NewGame();
    }

    private void RemoveLife()
    {
        SetLives(lives - 1);

        if (lives > 0)
        {
            Invoke(nameof(Respawn), 1f);
        }
        else
        {
            Invoke(nameof(GameOver), 1f);
        }
    }
    public void Died()
    {
      
        RemoveLife();
    }
    public void AdvancedRow()
    {
        SetScore(score + 10);
    }
    public void HomeOccupied()
    {
        frogger.gameObject.SetActive(false);

        int bonusPoints = time * 20;
        SetScore(score + bonusPoints + 50);

        if (Cleared())
        {
            SetScore(score + 1000);
            Invoke(nameof(NewLevel), 1f);
        } 
        else {
            Invoke(nameof(Respawn), 1f);
        }
    }

    private bool Cleared()
    {
        for (int i = 0; i < homes.Length; i++)
        {
            if (!homes[i].enabled)
            {
                return false;
            }
        }

        return true;
    }
    private void SetScore(int score)
    {
        this.score = score; 
        scoreText.text= score.ToString();
    }
    private void SetLives(int lives)
    {
        this.lives = lives; 
        livesText.text= lives.ToString();
    }
}
