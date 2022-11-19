using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [Header("CHARACTER CONTROLLER")]
    public CharacterController characterController;

    [Header("MOVE AND ROTATE SPEED")]
    public float moveSpeed;
    public float rotateSpeed;

    [Header("ANIMATOR")]
    public Animator characterAnimator;

    private Vector3 InputDirection;
    private Quaternion lastRotation;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        characterAnimator = GetComponent<Animator>();

        lastRotation = transform.rotation;
    }
    private void Update()
    {        
        if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0 || Input.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical") < 0)
        {
            InputDirection.x = Input.GetAxis("Horizontal");
            InputDirection.z = Input.GetAxis("Vertical");

            characterController.Move(moveSpeed * Time.deltaTime * GetCameraRelativeDirection(InputDirection));

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(GetCameraRelativeDirection(InputDirection)), rotateSpeed * Time.deltaTime);
            lastRotation= transform.rotation;
            characterAnimator.ResetTrigger("Idle");
            characterAnimator.SetTrigger("Walk");
        }
        else
        {
            transform.rotation = lastRotation;
            characterAnimator.ResetTrigger("Walk");
            characterAnimator.SetTrigger("Idle");
        }
    }
    private Vector3 GetCameraRelativeDirection(Vector3 dir)
    {
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        forward *= dir.z;
        right *= dir.x;

        return forward + right;
    }
}
