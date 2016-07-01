using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float rotateSpeed;
    public float jumpStrength;

    private bool isJumping;
    private float prevJumpVelocity;
    private Vector3 jumpForce;
    private float prevHorizontalVelocity;

    
    // Use this for initialization
    void Start() 
    {
        isJumping = false;
        prevJumpVelocity = 0;
        jumpForce = new Vector3(0, jumpStrength, 0);
        prevHorizontalVelocity = 0;

  
    }

    // Update is called once per frame
    void Update()
     {
        if(GameManager.instance.isPaused){ return; }
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        
        Vector3 movement = new Vector3(0.0f, 0.0f, 0.0f);
        

        //Debug.Log("Vertical : " + moveVertical);
        //Debug.Log("Horizontal : " + moveHorizontal);
       
        if(Input.GetButtonDown("Jump") || isJumping)
        {
            if (isJumping == false)
            {
                 Jump();
            }
            else if (GetComponent<Rigidbody>().velocity.y == 0f)
            {
                isJumping = false;
            }
          
            
        }

       

       // Debug.Log("Vertical : " + moveVertical);
       // Debug.Log("Horizontal : " + moveHorizontal);
       
         if (moveVertical != 0.0)
            {
            movement.z = moveVertical * (-1);
            transform.Translate(movement * (speed * Time.deltaTime));
            if(!this.gameObject.GetComponents<AudioSource>()[0].isPlaying)
            {
                this.gameObject.GetComponents<AudioSource>()[0].Play();
            }
            }
         else
         {
            this.gameObject.GetComponents<AudioSource>()[0].Stop();
         }



        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            this.transform.Rotate(new Vector3(0.0f, 0.0f, 0.0f));
        
        else if (moveHorizontal >= 0.2 || moveHorizontal <= -0.2)
            {

                this.transform.Rotate(new Vector3(0.0f, rotateSpeed * Mathf.Sign(moveHorizontal), 0.0f));


            }
           
    }


    void FixedUpdate()
    {
       // float changeForce = 
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Trigger enter with "+other.gameObject.name);
        if(other.gameObject.tag == "Exit")
        {
            GameManager.instance.EndLevel(true);
        }
        if(other.gameObject.tag == "Trap")
        {
            this.gameObject.GetComponents<AudioSource>()[1].Play();
            GameManager.instance.RedoLevel();
        }
    }
    
    void Jump()
    {
        isJumping = true;
        
        
        GetComponent<Rigidbody>().AddForce(jumpForce);
        prevJumpVelocity = GetComponent<Rigidbody>().velocity.y;

    }

}
