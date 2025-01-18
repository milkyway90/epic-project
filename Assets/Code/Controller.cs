using UnityEngine;

public class Controller : MonoBehaviour
{
    // variables
    public Rigidbody rb;
    float mouse_x, mouse_y, keyb_x, keyb_y;
    bool on_ground;

    // collect input
    void InputCollect(){
        // mouse input and stuff
        mouse_x = Input.GetAxis("Mouse X");
        mouse_y = Input.GetAxis("Mouse Y");

        // keyboard input
        keyb_x = Input.GetAxis("Horizontal");
        keyb_y = Input.GetAxis("Vertical");
    }

    [Header("Camera")]
    public Transform camera;

    void LookAround(float x, float y){
        Quaternion body_rotation = Quaternion.Euler(0, x, 0);
        Quaternion camera_rotation = Quaternion.Euler(y, 0, 0);

        //rotating the bean and the camera
        rb.MoveRotation(rb. rotation * body_rotation);
        camera.Rotate(y * -1, 0, 0);
    }

    [Header ("Movement")]
    public float speed, drag;
    
    void Move(float x, float y){
        Vector3 direction = transform.forward * y + transform.right * x;
        rb.AddForce(direction * speed * 10f, ForceMode.Acceleration);
    }

    [Header ("Ground Check")]
    public GameObject ground_check;
    bool grounded;
    void GroundCheck(){
        if(ground_check.GetComponent<GroundCheck>().grounded){
            grounded = true;
            speed = 10f;
            rb.linearDamping = drag;
        }  else {
            grounded = false;
            speed = 1f;
            rb.linearDamping = 0f; 
        }     
    }

    public float height;
    void Jump(){
        if(Input.GetKey(KeyCode.Space) && grounded)
            rb.AddForce(Vector3.up * height, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update(){
        InputCollect();
        LookAround(mouse_x, mouse_y);
    }

    void FixedUpdate(){
        GroundCheck();
        Move(keyb_x, keyb_y);
        Jump();
    }
}
