using System.Collections;
using System.Collections.Generic;
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
    public Text Maintext;

    private void Update()
    {
        StartCoroutine(WinOrLose());
    }

    IEnumerator WinOrLose()
    
    {
        playerKillCounter.text = "" + Kills;
        enemyKillCounter.text = "" + enemyKills;

        if(Kills >= 10)
        {
            Maintext.text = "Blue Team Win";
            PlayerPrefs.SetInt("Kills", Kills);
            Time.timeScale = 0f;
            yield return new WaitForSeconds(5f);
            SceneManager.LoadScene("TDMRoom");

        }

        else if(enemyKills >= 10)
        {
            Maintext.text = "Red Team Win";
            PlayerPrefs.SetInt("enemyKills", enemyKills);
            Time.timeScale = 0f;
            yield return new WaitForSeconds(5f);
            SceneManager.LoadScene("Test_Map");
        }



    }


}
