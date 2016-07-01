using UnityEngine;
using System.Collections;

public class roomMaker : MonoBehaviour {
	public Transform startingPoint, endingPoint;
	public GameObject player;

	// Use this for initialization
	void Start () {
		player = GameManager.instance.GetPlayer();
		reset();
	}
	
	public virtual void reset(){
		player.transform.position = startingPoint.position;
		player.transform.rotation = Quaternion.identity;
	}
}
