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
    private bool isController;
    private bool isKeyboard;
    
    // Use this for initialization
    void Start() 
    {
        isJumping = false;
        prevJumpVelocity = 0;
        jumpForce = new Vector3(0, jumpStrength, 0);
        prevHorizontalVelocity = 0;
        isController = false;
        isKeyboard = false;
        findInputDevice();
    }

    // Update is called once per frame
    void Update()
     {
        
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        
        Vector3 movement = new Vector3(0.0f, 0.0f, 0.0f);
        

        //Debug.Log("Vertical : " + moveVertical);
        //Debug.Log("Horizontal : " + moveHorizontal);
       
        if(Input.GetButtonDown("Jump") || isJumping)
        {
            if (isJumping == false)
            {
                Debug.Log("jumping");
                 Jump();
            }
            else if (GetComponent<Rigidbody>().velocity.y == 0f)
            {
                Debug.Log("can jump");

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


        if (isController)
        {

            //Debug.Log("Controller");
            if (moveHorizontal >= 0.03 || moveHorizontal <= -0.03)
            {

                this.transform.Rotate(new Vector3(0.0f, rotateSpeed * Mathf.Sign(moveHorizontal), 0.0f));


            }
        }
        else if (isKeyboard)
        {
           // Debug.Log("Keyboard");
            if (moveHorizontal != 0)
            {

                this.transform.Rotate(new Vector3(0.0f, rotateSpeed * Mathf.Sign(moveHorizontal), 0.0f));


            }
        }

      

    }

    void findInputDevice()
    {
        if (Input.anyKeyDown)
        {
            isKeyboard = true;
            isController = false;
        }
        else
        {
            isKeyboard = false;
            isController = true;
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
