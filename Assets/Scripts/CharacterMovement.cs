using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    // Variables
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float jumpHeight = 2.0f;
    [SerializeField] private bool isGrounded;

    public bool isMoving;

    private CharacterController _controller;
    private Vector3 _velocity;
    private float _gravity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        // Gets the global gravity value
        _gravity = Physics.gravity.y;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = _controller.isGrounded;

        if (isGrounded && _velocity.y < 0)
        {
            _velocity.y = 0f;
        }
        
        MoveSideways();
        MoveForwardOrBack();
        Jump();
        ApplyGravity();
    }

    private void Move(string axis)
    {
        float moveInput = Input.GetAxis(axis);
        
        Vector3 direction = new Vector3(0, 0, 0);
        
        if (axis == "Horizontal")
        {
            direction = new Vector3(moveInput, 0, 0);
        }
        else if (axis == "Vertical")
        {
            direction = new Vector3(0, 0, moveInput);
        }
        
        SetIsMoving(moveInput);
        
        Vector3 velocity = direction * speed;

        _controller.Move(velocity * Time.deltaTime);
    }
    
    private void MoveSideways()
    {
        Move("Horizontal");
    }

    private void MoveForwardOrBack()
    {
        Move("Vertical");
    }

    private void Jump()
    {
        if (isGrounded && Input.GetAxis("Jump") > 0)
        {
            // Apply an upward movement vector
            _velocity.y += _velocity.y = Mathf.Sqrt(jumpHeight * -2f * _gravity);
        }
    }

    private void ApplyGravity()
    {
        _velocity.y += _gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }

    private void SetIsMoving(float moveInput)
    {
        isMoving = moveInput != 0;
    }

    private void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up * mouseX);
    }
}