using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.UI;

public class Player_Controller : MonoBehaviour
{
    // This script is attached to the player. 
    // This class contains the majority of the code in this game. 
    // It manages the player's movement/look, clicking interactivity, and holding/carrying objects around.

    public float ClickDistance;
    public float HozSensitivity;
    public float VertSensitivity;
    public Text Label; 
    public Image LabelBackground;
    public GameObject RecipePanel;
    public GameObject Crosshair;
    public GameObject Crosshair_knife;
    public GameObject Knife_label;

    public bool CanMove = false;

    private Rigidbody rb;
    private Camera cam;
    private GameObject holdObject;
    private Vector3 holdObjectLocation;
    private float WalkSpeed = 4;
    private Vector3 InputVector;
    private float verticalLook;

    private Pot pot;
    private AudioSource chopSound;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        cam = Camera.main;
 
        pot = FindObjectOfType<Pot>();

        Knife_label.SetActive(false);

        chopSound = this.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (CanMove)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            UpdateMouseLook();
            UpdateClick();
            ManageHoldItem();

            UpdateMovement();
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
     }

    // This method manages mouse look and rotation. It is called from Update each frame.
    private void UpdateMouseLook()
    {

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        verticalLook += -mouseY;
        verticalLook = Mathf.Clamp(verticalLook, -80f, 80f);

        transform.Rotate(0f, mouseX, 0f);
        cam.transform.localEulerAngles = new Vector3(verticalLook,0f,0f);


        //transform.LookAt(cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane)), Vector3.up);
    }

    // This method manages what happens when you click on objects. It is called from Update each frame.
    private void UpdateClick()
    {
        RaycastHit hit;
        var ray = new Ray(cam.transform.position, cam.transform.forward);

        // LEFT CLICK
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, ClickDistance))
            {
                if (holdObject == null)
                {
                    // If you click on an object ("Item" or "Tool") and you are not currently holding an object, pick it up. 
                    if ((hit.transform.gameObject.CompareTag("Item") || hit.transform.gameObject.CompareTag("Tool") ||
                         hit.transform.gameObject.CompareTag("ChoppedFood")) && holdObject == null)
                    {
                        holdObject = hit.transform.gameObject;
                    }

                    if (hit.transform.gameObject.CompareTag("Button"))
                    {
                        //Debug.Log("Clicked BUTTON");
                        pot.Cook();
                    }
                }
                else if (holdObject != null)
                {
                    holdObject = null;
                }

            }
        }

        // RIGHT CLICK
        // If you're holding a TOOL (knife) right click to drop
        if (Input.GetMouseButtonDown(1) && holdObject != null && holdObject.CompareTag("Tool"))
        {
            if (Physics.Raycast(ray, out hit, ClickDistance))
            {
                // If you click an "Item" while holding a "Tool"
                if (hit.transform.gameObject.CompareTag("Item"))
                {
                    // Call the "Item" (the food's) CHOP method
                    hit.transform.gameObject.SendMessageUpwards("Chop");
                    chopSound.Play();
                }
            }
        }       
    }

    // This method determines and manages what object you are currently holding, if any. Called from Update each frame. 
    private void ManageHoldItem()
    {
        if (holdObject != null)
        {
            Vector3 camPos = new Vector3(cam.transform.position.x, cam.transform.position.y - 0.4f,
               cam.transform.position.z);
            //Vector3 holdPos = new Vector3(transform.forward.x + 0.5f, transform.forward.y, transform.forward.z;
            holdObjectLocation = camPos + transform.forward * 1f + transform.right * 0.4f;

            //Vector3 spawnPos = new Vector3(playerPos.x+0.5f, playerPos.y, playerPos.z+1);

            // holdObjectLocation = spawnPos;

            holdObject.transform.position = holdObjectLocation;
            holdObject.transform.rotation = this.transform.rotation;

            // Updates the UI title text with the Name of the object you are currently holding
            Label.text = holdObject.GetComponent<Item_all>().GetName();
            LabelBackground.enabled = true;
        }
        else
        {
            Label.text = "";
            //labelBack.enabled = false;
        }

        // Updates the crosshair (if you're holding the knife)
        if (holdObject != null && holdObject.CompareTag("Tool"))
        {
            Crosshair.SetActive(false);
            Crosshair_knife.SetActive(true);
            Knife_label.SetActive(true);
        }
        else
        {
            Crosshair_knife.SetActive(false);
            Crosshair.SetActive(true);
            Knife_label.SetActive(false);
        }
    }

    // This method manages player movement. It is called from Update each frame. 
    private void UpdateMovement()
    {
        float hoz = WalkSpeed * Input.GetAxis("Horizontal");
        float vert = WalkSpeed * Input.GetAxis("Vertical");

        InputVector = (transform.forward * vert);
        InputVector += (transform.right * hoz); // forward and strafe
    }

    // FixedUpdate for physics player movement
    private void FixedUpdate()
    {
        rb.velocity = InputVector;
    }
}