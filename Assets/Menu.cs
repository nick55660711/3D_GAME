using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    bool SoundSwitch;
    public void StartButton()
    {
        SceneManager.LoadScene("Game");
    } 
    
    public void QuitButton()
    {
        Application.Quit();
    }
        public void SoundButton()
    {
        SoundSwitch = !SoundSwitch;
        AudioListener.pause = SoundSwitch;

    }


}
