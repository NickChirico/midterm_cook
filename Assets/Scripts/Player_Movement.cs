using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    private Rigidbody rb;
    private Camera cam;

    private GameObject holdObject;
    private Vector3 holdObjectLocation;


    public float ClickDistance;
    private float Speed = 4;



    
    private Vector3 InputVector;

    
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        cam = Camera.main;


            
            //..new Vector3(this.transform.position.x + 2f, this.transform.position.y, this.transform.position.z);
    }

    void Update()
    {
        UpdateMouseLook();
        UpdateClick();
        ManageHoldItem();
        
        
        UpdateMovement();
    }

    private void UpdateMouseLook()
    {
        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");

        transform.Rotate(0f, mouseX, 0f);
        cam.transform.Rotate(mouseY * -1, 0f, 0f);
    }

    private void UpdateClick()
    {
        // On click - m1
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit, ClickDistance))
            {
                if (hit.transform.gameObject.CompareTag("Item"))
                {
                    holdObject = hit.transform.gameObject;
                }
            }
        }

        // When you let go, drop the item
        if (Input.GetMouseButtonUp(0))
        {
            holdObject = null;
        }
    }

    private void ManageHoldItem()
    {
        if (holdObject != null)
        {
            Vector3 playerPos = this.transform.position;
            //Vector3 holdPos = new Vector3(transform.forward.x + 0.5f, transform.forward.y, transform.forward.z;
            holdObjectLocation = playerPos + transform.forward + transform.right*0.5f;
            
            //Vector3 spawnPos = new Vector3(playerPos.x+0.5f, playerPos.y, playerPos.z+1);
        
           // holdObjectLocation = spawnPos;
            
            holdObject.transform.position = holdObjectLocation;
            holdObject.transform.rotation = this.transform.rotation;
        }
    }
    
    private void UpdateMovement()
    {
        float hoz = Speed * Input.GetAxis("Horizontal");
        float vert = Speed * Input.GetAxis("Vertical");

        //rb.velocity = new Vector3(hoz, rb.velocity.y, vert);

        InputVector = (transform.forward * vert);
        InputVector += (transform.right * hoz); // forward and strafe
    }
    
    private void FixedUpdate()
    {
        rb.velocity = InputVector;
    }
}