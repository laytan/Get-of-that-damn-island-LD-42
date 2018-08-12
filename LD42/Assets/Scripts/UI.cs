using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour {

    public AudioClip btnClick;
    private SoundManager sm;

    void Start()
    {
        sm = GameObject.Find("SoundManager").GetComponent<SoundManager>();    
    }

    public void ToMainMenu()
    {
        sm.PlaySound(btnClick);
        SceneManager.LoadScene(0);
    }
    public void ToWinScreen()
    {
        SceneManager.LoadScene(3);
    }
    public void RestartLvl()
    {
        sm.PlaySound(btnClick);
        SceneManager.LoadScene(1);
    }
    public void ToLoseScreen()
    {

        SceneManager.LoadScene(4);
    }
    public void ToLevelSelection()
    {
        sm.PlaySound(btnClick);
        SceneManager.LoadScene(2);
    }
    public void LoadLvl(int index)
    {
        sm.PlaySound(btnClick);
        index--;
        GameObject.FindGameObjectWithTag("Lvl").GetComponent<CurrentLevel>().lvl = index;
        SceneManager.LoadScene(1);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
