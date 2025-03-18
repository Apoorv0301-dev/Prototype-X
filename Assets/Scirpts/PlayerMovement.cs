using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController player;
    public LayerMask groundLayer;

    public string doorLayerName = "Door";
    public string takeKeyLayerName = "TakeKey";
    public string MainDoorLayerName = "MainDoor";

    public float groundDistance = 0.4f;
    float speed = 10f;
    float gravity = -19.6f;
    float sprintLimit = 20f;

    public Transform groundCheck;

    Vector3 velocity;
    Vector3 move;

    public GameObject door;
    GameObject key;
    public Image keyPad;
    public GameObject takeKey;
    public GameObject enterCode;
    public GameObject findKey;
    public GameObject openDoor;
    public GameObject footsteps;
    public GameObject fastFootsteps;
    public GameObject keySound;
    public GameObject pauseMenu;

    bool isGrounded;
    bool canDoor;
    bool doorOpen;
    bool canTakeKey;
    [SerializeField] 
    bool keyObtained;
    bool mainDoorFront; 
    bool enteringCode;  
    public bool isPaused = false;
    void Start()
    {
        key = GameObject.Find("Key");
        keyObtained = false;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        if(canDoor && Input.GetMouseButtonDown(0) && !doorOpen && keyObtained){
            door.SetActive(false);
            doorOpen = true;
        }
        else if(canDoor && Input.GetMouseButtonDown(0) && doorOpen){
            door.SetActive(true);
            doorOpen = false;
        }
        if(canTakeKey){
            if(Input.GetKeyDown(KeyCode.Q)){
                key.SetActive(false);
                takeKey.SetActive(false);
                keySound.SetActive(true);
                Debug.Log("Key Obtained!");
                keyObtained = true;
            }
        }
        if(Input.GetKey(KeyCode.LeftShift) && (move.x != 0 || move.z != 0)){
            speed = sprintLimit;
            footsteps.SetActive(false);
            fastFootsteps.SetActive(true);
        }
        else{
            speed = 10f;
            fastFootsteps.SetActive(false);
            footsteps.SetActive(true);
        }
        if(mainDoorFront)
        {
            if(Input.GetKeyDown(KeyCode.E)){
                keyPad.gameObject.SetActive(true);
                enterCode.SetActive(false);
                Cursor.lockState = CursorLockMode.Confined;
                enteringCode = true;
            }
            if(enteringCode){
                if(Input.GetKeyDown(KeyCode.Escape)){
                    keyPad.gameObject.SetActive(false);
                    Cursor.lockState = CursorLockMode.Locked;
                    enteringCode = false;
                }
            }
        }
        else{
            keyPad.gameObject.SetActive(false);
            if(!isPaused){
                Cursor.lockState = CursorLockMode.Locked;
            }
            else{
                Cursor.lockState = CursorLockMode.None;
            }
            
        }
        if(Input.GetKeyDown(KeyCode.Escape) && !enteringCode)
        {
            if(!isPaused)
            {
                Pause();
            }
            else{
                Resume();
            }
        }
        
        if(!isPaused){
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            move = transform.right * x + transform.forward * z;
            velocity.y += 0.5f * gravity * Time.deltaTime;

            player.Move(move * speed * Time.deltaTime);
            player.Move(velocity * Time.deltaTime);
            if(move.x != 0 || move.z != 0)
            {
                footsteps.SetActive(true);
            }
            else if(move.x == 0 && move.z == 0){
                footsteps.SetActive(false);
            }
        }
        else{
            footsteps.SetActive(false);
        }
        
    }
    private void OnTriggerEnter(Collider other) {
         // Check if the collided object is in the "Player" layer
        if (other.gameObject.layer == LayerMask.NameToLayer(doorLayerName))
        {
            if(keyObtained){
                Debug.Log("Can Open");
                openDoor.SetActive(true);
                canDoor = true;
                door = other.gameObject;
                StartCoroutine("RemoveClickPrompt");
            }
            else
            {
                findKey.SetActive(true);
            }
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer(takeKeyLayerName))
        {
            Debug.Log("Can Take Key");
            takeKey.SetActive(true);
            canTakeKey = true;
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer(MainDoorLayerName))
        {
            enterCode.SetActive(true);
            mainDoorFront = true;
        }
    }
    private void OnTriggerExit(Collider other){
         if (other.gameObject.layer == LayerMask.NameToLayer(doorLayerName))
        {
            canDoor = false;
            if(!keyObtained){
                Debug.Log("Can't Open");
                findKey.SetActive(false);
            }
            else{
                openDoor.SetActive(false);
            }
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer(MainDoorLayerName))
        {
            enterCode.SetActive(false);
            mainDoorFront = false;
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer(takeKeyLayerName))
        {
            Debug.Log("Can't Take Key");
            takeKey.SetActive(false);
            canTakeKey = false;
        }
    }
    IEnumerator RemoveClickPrompt(){
        yield return new WaitForSeconds(3);
        openDoor.SetActive(false);
    }
    void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        EnablePlayerInput(true);
    }
    void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        EnablePlayerInput(false);
    }
    void EnablePlayerInput(bool isEnabled)
    {
        if(player != null)
        {
            player.enabled = isEnabled;
        }
    }
}
