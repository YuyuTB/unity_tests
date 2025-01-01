using UnityEngine;

public class CharacterMovements : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private bool isGrounded;
    [SerializeField] private Transform cameraTransform;
    
    private Rigidbody _rb;

    public bool isWalking;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        HandleMovements();
        HandleJump();
        HandleRotate();
    }
    
    private void HandleMovements()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        isWalking = horizontal != 0 || vertical != 0;
        
        Vector3 direction = cameraTransform.forward * vertical + cameraTransform.right * horizontal;
        direction.y = 0;
        Vector3 velocity = direction.normalized * speed;
        
        _rb.MovePosition(transform.position + velocity * Time.deltaTime);
    }

    private void HandleRotate()
    {
        float horizontal = Input.GetAxis("Mouse X");
        
        transform.Rotate(Vector3.up, horizontal);
    }

    private void HandleJump()
    {
        if (isGrounded && Input.GetAxis("Jump") > 0)
        {
            _rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = false;
        }
    }
}