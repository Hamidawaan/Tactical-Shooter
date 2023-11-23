using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Button playButton; // Reference to the play button in the inspector
    public GameObject mapSelectionPanel;
    public GameObject mainMenuPanel;

    void Start()
    {
        mapSelectionPanel.SetActive(false);
        // Attach the method to the button's click event
        playButton.onClick.AddListener(PlayButtonOnClick);
    }

    void PlayButtonOnClick()
    {
        // Load Scene1 when the play button is clicked
        mapSelectionPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }
}
