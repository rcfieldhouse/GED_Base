using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerAction inputAction;
    Vector2 move;
    Vector2 rotate;
    Rigidbody rb;
    public static PlayerController instance;

    private float distanceToGround;
    bool isGrounded = true;
    public float jump = 15f;
    public float walkSpeed = 5f;
    public Camera playerCamera;
    Vector3 cameraRotation;

    private Animator animator;
    private bool isWalking = false,isRunning = false;

    public GameObject projectile;
    public Transform projectilePos;



    private void Start() {

        if (instance == null)
        {
            instance = this;
        }

        //  inputAction
        inputAction = PlayerInputController.controller.inputAction;

        inputAction.Player.Jump.performed += cntxt => Jump();

        inputAction.Player.Move.performed += cntxt => move = cntxt.ReadValue<Vector2>();
        inputAction.Player.Move.canceled += cntxt => move = Vector2.zero;

        inputAction.Player.Look.performed += cntxt => rotate = cntxt.ReadValue<Vector2>();
        inputAction.Player.Look.canceled += cntxt => rotate = Vector2.zero;

        inputAction.Player.Shoot.performed += cntxt => Shoot();

        inputAction.Player.Sprint.canceled += cntxt => Sprint(false);
        inputAction.Player.Sprint.performed += cntxt => Sprint(true);
     
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        distanceToGround = GetComponent<Collider>().bounds.extents.y;
        cameraRotation = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);
        
    }
    private void Sprint(bool TF)
    {
        isRunning = TF;
    }

    
    private void Jump()
    {
        if(isGrounded)
        {
            animator.SetBool("Jumped", true);
            rb.velocity = new Vector2(rb.velocity.x, jump);
            isGrounded = false;
        }
    }

    private void Shoot()
    {
        Rigidbody bulletRb = Instantiate(projectile, projectilePos.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        bulletRb.AddForce(transform.forward * 32f, ForceMode.Impulse);
        bulletRb.AddForce(transform.up * 3f, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Jumped", !isGrounded);
        animator.SetBool("Landed", isGrounded);
        cameraRotation = new Vector3(cameraRotation.x + rotate.y, cameraRotation.y + rotate.x, cameraRotation.z);
        
        playerCamera.transform.rotation = Quaternion.Euler(cameraRotation);
        transform.eulerAngles = new Vector3(transform.rotation.x, cameraRotation.y, transform.rotation.z);

        transform.Translate(Vector3.right * Time.deltaTime * move.x * walkSpeed, Space.Self);
        transform.Translate(Vector3.forward * Time.deltaTime * move.y * walkSpeed, Space.Self);

        isGrounded = Physics.Raycast(transform.position, -Vector3.up, distanceToGround);

        Vector3 m = new Vector3(move.x, 0, move.y);
        AnimateRun(m);
    }

    void AnimateRun(Vector3 m)
    {
        isWalking = (m.x > 0.1f || m.x < -0.1f) || (m.z > 0.1f || m.z < -0.1f) ? true : false;
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("IsRunning", isRunning);
    }
}
