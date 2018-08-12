using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour {
    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void ToWinScreen()
    {
        SceneManager.LoadScene(3);
    }
    public void RestartLvl()
    {
        SceneManager.LoadScene(1);
    }
    public void ToLoseScreen()
    {
        SceneManager.LoadScene(4);
    }
    public void ToLevelSelection()
    {
        SceneManager.LoadScene(2);
    }
    public void LoadLvl(int index)
    {
        index--;
        GameObject.FindGameObjectWithTag("Lvl").GetComponent<CurrentLevel>().lvl = index;
        SceneManager.LoadScene(1);
    }
}
