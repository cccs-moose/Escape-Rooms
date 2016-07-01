using UnityEngine;
using System.Collections;

public class FallingFloorLogic : MonoBehaviour {

    public float FallDelay;

    private bool shouldFall;
    private float timer;

	// Use this for initialization
	void Start () {
    	ResetFloor();
	}

    // Update is called once per frame
    void Update()
    {
        if (shouldFall)
        {
            timer += Time.deltaTime;

            if (timer > FallDelay)
            {
                
                    FallPlatform();
                
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Player" && !shouldFall)
        {
            shouldFall = true;
            this.gameObject.GetComponents<AudioSource>()[0].Play();
        }
    }

    public void ResetFloor()
    {
        timer = 0;
        shouldFall = false;

     
            this.gameObject.GetComponent<Rigidbody>().useGravity = false;
            this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        
    }

    private void FallPlatform()
    {

        
            this.gameObject.GetComponent<Rigidbody>().useGravity = true;
            this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            //this.gameObject.GetComponents<AudioSource>()[1].Play();
        
       
    }




}
