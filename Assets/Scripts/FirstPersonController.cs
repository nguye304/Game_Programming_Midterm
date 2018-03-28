using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float speed;
    public float jumpHeight;
    public float gravity;
    public LayerMask ground;
    public Transform feet;
    private AudioSource audio;
    private Vector3 fallingVelocity;
    private Vector3 walkingVelocity;
    private Vector3 direction;

    //private AudioSource audio;
    private float rotationSpeed = 1f;
    private float minY = -60f;
    private float maxY = 60f;

    private float rotationY = 0f;
    private float rotationX = 0f;
    private CharacterController controller;


    // Use this for initialization
    void Start()
    {
        controller = GetComponent<CharacterController>();
        direction = Vector3.zero;
        fallingVelocity = Vector3.zero;
        audio = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        direction = Vector3.zero;
        MoveIt();

    }
    void MoveIt()
    {
        //--------------Handles getting the input from user---------------
        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");
        //---handles moving from direction of the first person camera----- 
        direction = Camera.main.transform.TransformDirection(direction);
        direction = direction.normalized;
        //-------------handles moving the player--------------------------
        walkingVelocity = direction * speed;
        Debug.Log(direction.y);
        controller.Move(walkingVelocity * Time.deltaTime);
        
        //-------------Handles First Person Camera-------------------------      
        rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * rotationSpeed;
        rotationY += Input.GetAxis("Mouse Y") * rotationSpeed;
        rotationY = Mathf.Clamp(rotationY, minY, maxY);
        transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        //------------------------------------------------------------
        bool isGrounded = Physics.CheckSphere(feet.position, 0.25f, ground, QueryTriggerInteraction.Ignore);
        if (isGrounded)//if we are on the ground gravity will not fall
        { fallingVelocity.y = 0f; }
        else//else if we are not on the ground go from position down by gravity 
        { fallingVelocity.y -= gravity * Time.deltaTime; }
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            
            fallingVelocity.y = Mathf.Sqrt(gravity * jumpHeight);
        }
        controller.Move(fallingVelocity * Time.deltaTime);//move the player by the falling velocity 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Safe")
        {
            SceneManager.LoadScene("gameOver");
        }
    }
}