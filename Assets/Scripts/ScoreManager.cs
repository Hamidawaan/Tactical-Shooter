using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    [Header("Score Manager")]
    public int Kills;
    public int enemyKills;
    public Text playerKillCounter;
    public Text enemyKillCounter;

    public GameObject blueTeamPanel;
    public GameObject redTeamPanel;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("Kills"))
        {
            Kills = PlayerPrefs.GetInt("0");
        }
        else if (PlayerPrefs.HasKey("enemyKills"))
        {
            enemyKills = PlayerPrefs.GetInt("0");
        }
    }

    private void Update()
    {
        StartCoroutine(WinOrLose());
    }

    IEnumerator WinOrLose()
    {
        playerKillCounter.text = "" + Kills;
        enemyKillCounter.text = "" + enemyKills;

        if (Kills >= 3)
        {
            blueTeamPanel.SetActive(true);
            PlayerPrefs.SetInt("Kills", Kills);
            Time.timeScale = 0f;
            yield return new WaitForSeconds(5f);
            SceneManager.LoadScene("Test_Map");
        }
        else if (enemyKills >= 3)
        {
            redTeamPanel.SetActive(true);
            PlayerPrefs.SetInt("enemyKills", enemyKills);
            Time.timeScale = 0f;
            yield return new WaitForSeconds(5f);
            SceneManager.LoadScene("Test_Map");
        }
    }
}
