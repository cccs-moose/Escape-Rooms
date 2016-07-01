using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CountdownText : MonoBehaviour {
	public Text CdText;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		CdText.text = GameManager.instance.Countdown + "";
	}
}
