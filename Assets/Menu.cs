using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    bool SoundSwitch = true;
    public Image Sound_S;
    public Sprite sound_ON; 
    public Sprite sound_OFF; 
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
        AudioListener.pause = !SoundSwitch;
        if(SoundSwitch)
        Sound_S.sprite = sound_ON;
        else Sound_S.sprite = sound_OFF;

    }
    

}
