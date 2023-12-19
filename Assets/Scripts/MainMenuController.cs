using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Button playButton;
    public GameObject mapSelectionPanel;
    public GameObject mainMenuPanel;

    public Text winCountText;
    public Text loseCountText;

    private int winCount = 0;
    private int loseCount = 0;

    void Start()
    {
        mapSelectionPanel.SetActive(false);
        playButton.onClick.AddListener(PlayButtonOnClick);

        // Load counts from permanent storage (e.g., PlayerPrefs or file)
        LoadCounts();
        UpdateCountText();
    }

    void PlayButtonOnClick()
    {
        mapSelectionPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void PlayerWins()
    {
        winCount++;
        UpdateCountText();
        SaveCounts();
    }

    public void PlayerLoses()
    {
        loseCount++;
        UpdateCountText();
        SaveCounts();
    }

    void UpdateCountText()
    {
        winCountText.text = "Wins: " + winCount.ToString();
        loseCountText.text = "Loses: " + loseCount.ToString();
    }

    // Save counts to permanent storage
    void SaveCounts()
    {
        // For simplicity, using PlayerPrefs, but consider other methods for permanent storage
        PlayerPrefs.SetInt("WinCount", winCount);
        PlayerPrefs.SetInt("LoseCount", loseCount);
        PlayerPrefs.Save();
    }

    // Load counts from permanent storage
    void LoadCounts()
    {
        // For simplicity, using PlayerPrefs, but consider other methods for permanent storage
        winCount = PlayerPrefs.GetInt("WinCount", 0);
        loseCount = PlayerPrefs.GetInt("LoseCount", 0);
    }
}
