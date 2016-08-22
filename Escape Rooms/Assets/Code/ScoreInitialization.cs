using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreInitialization : MonoBehaviour {

	public UnityEngine.UI.Text RoomScore;

	// Use this for initialization
	void Start () {
		RoomScore.text = "";
		int nbRooms = GameManager.instance.NumberOfRooms;
		for(int i = 0; i < nbRooms; i++){
			RoomScore.text += "Room " + i + " Fails : " + PlayerPrefs.GetInt("nFailRoom" + i) + "\n";
		}
	}

	public void returnToMenu(){
		GameManager.instance.LoadMenu();
	}
}
