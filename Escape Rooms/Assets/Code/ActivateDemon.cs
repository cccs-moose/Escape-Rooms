using UnityEngine;
using System.Collections;

public class ActivateDemon : MonoBehaviour {
    private bool hasCrossed;

    void start()
    {
        hasCrossed = false;
    }
    void OnTriggerExit(Collider other)
    {
        if (!hasCrossed)
        {
            GameManager.instance.showDemon();
            GameManager.instance.audioSource.clip = GameManager.instance.RandomAudio[4];
            GameManager.instance.audioSource.Play();
            hasCrossed = true;
        }
    }
	

}
