using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject startPanel;
    public GameObject winPanel;
    public GameObject win2Panel;
    public GameObject losePanel;
    public GameObject neutralPanel;
    public TMP_Text coinText;
    public TMP_Text timerText;
    public TMP_Text win2PanelText;
    public TMP_Text neutralPanelText;
    private Vector3 initialPlayerPosition;


    private int totalCoinsToCollect = 10;
    public int coinsCollected = 0;
    private float timeLeft = 30f;
    private bool gameIsActive = false;
    private bool isFirstRun = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        

        DeactivateAllPanels();
        startPanel.SetActive(true);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        initialPlayerPosition = player.transform.position;
    }
    private void DeactivateAllPanels()
    {
        winPanel.SetActive(false);
        win2Panel.SetActive(false);
        losePanel.SetActive(false);
        neutralPanel.SetActive(false);
    }
    void Start()
    {
        ResetGame();
        
    }

    void Update()
    {
        if (gameIsActive)
        {
            UpdateTimer();
        }
    }

    public void StartGame()
    {
        gameIsActive = true;
        startPanel.SetActive(false);
        coinsCollected = 0;
        timeLeft = 30f;
        UpdateUI();
    }

    public void RejectGameStart()
    {
        losePanel.SetActive(true);
        startPanel.SetActive(false);
    }

    public void CollectCoin()
    {
        coinsCollected++;
        UpdateUI();
        CheckWinCondition();
    }

    private void UpdateTimer()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            timeLeft = 0;
            EndGame(false); 
        }
        UpdateUI();
    }

    private void CheckWinCondition()
    {
        if (coinsCollected >= totalCoinsToCollect)
        {
            EndGame(true); 
        }
    }

    private void EndGame(bool didPlayerWin)
    {
        gameIsActive = false;
        if (didPlayerWin)
        {
            if (isFirstRun)
            {
                winPanel.SetActive(true);
            }
            else
            {
                win2Panel.SetActive(true);
                win2PanelText.text = $"It was worth going in again we managed to collect another 10 coins! You can have the {coinsCollected} coins we gathered";
            }
        }
        else
        {
            if (isFirstRun)
            {
                losePanel.SetActive(true);
            }
            else
            {
                neutralPanel.SetActive(true);
                neutralPanelText.text = $"So Close! We have {coinsCollected} coins this time. Personally I would have liked us to get 10 each but you can have these for helping me out. Maybe be could venture some other time.";
            }
        }
    }

    private void UpdateUI()
    {
        coinText.text = $"Coins: {coinsCollected}/{totalCoinsToCollect}";
        timerText.text = $"Time Left: {Mathf.CeilToInt(timeLeft)}s";
    }

    public void ResetGame()
    {
        startPanel.SetActive(true);
        winPanel.SetActive(false);
        win2Panel.SetActive(false);
        losePanel.SetActive(false);
        neutralPanel.SetActive(false);
        coinsCollected = 0;
        timeLeft = 30f;
        UpdateUI();
        isFirstRun = true;
        gameIsActive = false;
        ResetPlayerPosition();
    }
    
    public void AcceptToEnterAgain()
    {
        coinsCollected = 0;
        timeLeft = 30f;
        gameIsActive = true;
        UpdateUI();
        winPanel.SetActive(false);
        isFirstRun = false;
        StartGame();
        ResetPlayerPosition();
    }
    public void ResetPlayerPosition()
{
    GameObject player = GameObject.FindGameObjectWithTag("Player");
    if (player != null)
    {
        CharacterController charController = player.GetComponent<CharacterController>();
        if (charController != null)
        {
            charController.enabled = false; // Disable the CharacterController
            player.transform.position = initialPlayerPosition;
            charController.enabled = true; // Re-enable the CharacterController
        }
        else
        {
            player.transform.position = initialPlayerPosition;
        }
    }
}

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
