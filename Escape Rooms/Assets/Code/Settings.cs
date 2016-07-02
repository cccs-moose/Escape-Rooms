using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setVolume()
    {
        
        AudioListener.volume = GameObject.Find("Canvas/Volume Bar").GetComponent<Slider>().value;

    }

    public void setSensitivity()
    {
        GameManager.instance.sensitivity = GameObject.Find("Canvas/SensitivityBar").GetComponent<Slider>().value;
    }

    public void backToMainMenu()
    {
        GameManager.instance.LoadMenu();
    }

    public void loadSettingsScene()
    {
        GameManager.instance.LoadSettings();
    }
}
