using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public float runSpeed = 40f;
    public Animator animator;
    public GameManager gameManager;
    float horizontalMove = 0;
    bool jump = false;
    ProjectV controls;

    void Awake()
    {
        controls = gameManager.controls;

        controls.Player.Jump.performed += ctx => jump = true;
        controls.Player.Move.performed += ctx =>
        {
            horizontalMove = ctx.ReadValue<Vector2>().x * runSpeed;
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        };

        controls.Player.Move.canceled += ctx =>
        {
            horizontalMove = 0;
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        };
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        //Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
