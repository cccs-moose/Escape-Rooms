using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    static public GameManager instance = null;

    public int NumberOfRooms, CurrentRoom;
    public GameObject playerPrefab;
    public string[] Rooms;
    public AudioClip[] RandomAudio;
    public float MinTimeBetweenAudio, MaxTimeBetweenAudio;
    public GameObject demonPrefab;
    public int Countdown = 0;
    public bool isPaused;
    public float sensitivity = 2;

    private GameObject demon;
    private GameObject player;
    private float timer,TimeBetweenAudio;
    private AudioSource audioSource;
    private bool demonAppear;

	// Use this for initialization
	void Start () 
    {
        // singleton logic
        if(instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
        TimeBetweenAudio = Random.Range(MinTimeBetweenAudio, MaxTimeBetweenAudio);
        audioSource = (gameObject.AddComponent<AudioSource>() as AudioSource);
        demonAppear = false;
        Pause();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if(!isPaused)
        {
            timer += Time.deltaTime;

            // Generate a sound every random time
            TimeBetweenAudio -= Time.deltaTime;
           // Debug.Log(TimeBetweenAudio);
            if(TimeBetweenAudio <= 0){
                TimeBetweenAudio = Random.Range(MinTimeBetweenAudio, MaxTimeBetweenAudio);
                audioSource.clip = RandomAudio[Random.Range(0, RandomAudio.Length - 1)];
                
                audioSource.Play();

                if(!demonAppear && audioSource.clip.name == "I_will_kill_you-Grandpa-13673816")
                {
                    demon = GetDemon();
                    demon.transform.position = GameObject.Find("Exit").transform.position + new Vector3(0.0f, -1.0f, -2.0f);
                    demon.SetActive(true);
                    Destroy(demon,0.5f);
                }

                if (demon == null) demonAppear = true;
                
            }
        }
	}

    public void StartNewGame()
    {
        LoadNextLevel();
        isPaused = false;
        PlayerPrefs.DeleteAll();
        //Time.timeScale = 0f;
        timer = 0f;
        CurrentRoom = 0;
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        if(isPaused)
        {
            Pause();
        }
        else
        {
            UnPause();
        }
    }

    public void Pause()
    {
        isPaused = true;
        //Time.timeScale = 0f;
    }

    public void UnPause()
    {
        isPaused = false;
        //Time.timeScale = 1f;
    }

    public GameObject GetDemon()
    {
        return Instantiate(demonPrefab) as GameObject;
    }

    public GameObject GetPlayer()
    {
        GameObject player = Instantiate(playerPrefab) as GameObject;
        player.GetComponent<PlayerController>().rotateSpeed = sensitivity;
        return player;
    }

    public void EndLevel(bool isSuccess)
    {
        if(isSuccess)
        {
            int successes = PlayerPrefs.GetInt("nSuccess");
            PlayerPrefs.SetInt("nSuccess", successes+1);
            this.gameObject.GetComponents<AudioSource>()[2].Play();
            CurrentRoom++;
            if(NumberOfRooms == CurrentRoom)
            {
                // Ending screen
                LoadStats();
            }
            else
            {
                // Display the interlude screen for 5 seconds
                DisplayInterludeScreen();
            }
        }
        else
        {
            // premature quit?
            LoadMenu();
        }
    }

    public void RedoLevel()
    {
        int totalFails = PlayerPrefs.GetInt("nFail");
        int currentRoomFails = PlayerPrefs.GetInt("nFailRoom" + CurrentRoom);
        PlayerPrefs.SetInt("nFail", totalFails+1);
        PlayerPrefs.SetInt("nFailRoom" + CurrentRoom, currentRoomFails+1);
        GameObject.Find("Room").GetComponent<roomMaker>().reset();
    }

    public void LoadMenu()
    {
        this.CurrentRoom = 0;
        Pause();
        SceneManager.LoadScene("StartMenu");
    }

    private void DisplayInterludeScreen()
    {
        Countdown = 6;
        Application.LoadLevelAdditive("Interlude");
        GameObject.Find("Room").GetComponent<roomMaker>().reset();
        Pause();
        ChangeCountdownText();
    }

    private void ChangeCountdownText()
    {
        Countdown--;
        if(Countdown > 0){Invoke("ChangeCountdownText", 1f);}
        else{UnPause();LoadNextLevel();}
    }

    private void LoadNextLevel()
    {   
        if(Rooms.Length <= 0) return; //abord!
        
        SceneManager.LoadScene(Rooms[CurrentRoom]);
    }

    private void LoadStats(){
        Pause();
        SceneManager.LoadScene("EndingScreen");
    }

    public void LoadSettings()
    {
        Pause();
        SceneManager.LoadScene("Setting");
    }
}
