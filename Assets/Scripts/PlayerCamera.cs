using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private CharacterMovements character;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (character.isWalking)
        {
            FollowPlayer();
        }
        else
        {
            RotateAroundCharacter();
        }
    }
    
    private void FollowPlayer()
    {
        transform.position = target.position + offset;
        Quaternion.LookRotation(character.transform.forward);
    }
    
    private void RotateAroundCharacter()
    {
        float horizontal = Input.GetAxis("Mouse X");
        float vertical = Input.GetAxis("Mouse Y");
        
        transform.RotateAround(target.position, Vector3.up, horizontal);
        transform.RotateAround(target.position, transform.right, -vertical);
    }
}
