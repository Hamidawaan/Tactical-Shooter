using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapSelection : MonoBehaviour
{
    public Button map1Button;
    public Button map2Button;

    void Start()
    {
        // Attach methods to button click events
        map1Button.onClick.AddListener(Map1ButtonOnClick);
        map2Button.onClick.AddListener(Map2ButtonOnClick);
    }

    void Map1ButtonOnClick()
    {
        // Load the "TDM Room" scene when Map1 button is clicked
        SceneManager.LoadScene("TDM Room");
    }

    void Map2ButtonOnClick()
    {
        // Load the "Map_v1" scene when Map2 button is clicked
        SceneManager.LoadScene("Map_v1");
    }
}
