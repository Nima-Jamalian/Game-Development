using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float health = 10f;
    float horizontalInput;
    float verticalInput;
    float mouseX;
    float mouseY;
    Vector3 direction;
    Vector3 velocity;
    CharacterController characterController;
    float speed = 1f;
    float gravity = 9.8f;
    [SerializeField] float mouseSensivity = 1f;
    [SerializeField] float walkingSpeed = 1f;
    [SerializeField] float runningSpeed = 1f;
    [SerializeField] Weapon weapon;
    AudioSource audioSource;
    [SerializeField] AudioSource damageAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        characterController = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
        //weapon = GameObject.Find("Player").GetComponent<Weapon>();
    }

    // Update is called once per frame
    void Update()
    {
        CharacterMovement();
        MouseMovement();

        if (Input.GetMouseButton(0))
        {
            weapon.Shooting();
        }
    }

    float nextActionTime = 0f;
    float playRate = 0.8f;
    void CharacterMovement() {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        if(verticalInput > 0 || horizontalInput > 0)
        {
            if(Time.time >= nextActionTime)
            {
                nextActionTime = Time.time + playRate;
                audioSource.Play();
            }
      
        }
        direction = new Vector3(horizontalInput, 0, verticalInput);
        velocity = direction * speed;
        velocity.y -= gravity; //velocity.y = velocity.y - gravity
        velocity = transform.TransformDirection(velocity);
        characterController.Move(velocity * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runningSpeed;
            playRate = 0.4f;
        }
        else
        {
            speed = walkingSpeed;
            playRate = 0.8f;
        }
    }

    void MouseMovement() {
        mouseX = Input.GetAxis("Mouse X");
        Vector3 newRotationX = transform.localEulerAngles;
        newRotationX.y += mouseX * mouseSensivity;
        transform.localEulerAngles = newRotationX;


        mouseY = Input.GetAxis("Mouse Y");
        Vector3 newRotationY = Camera.main.transform.localEulerAngles;
        newRotationY.x -= mouseY * mouseSensivity;
        Camera.main.transform.localEulerAngles = newRotationY;
    }

    public void TakeDamage()
    {
        damageAudioSource.Play();
        health--;
        if(health == 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Debug.Log("Game Over!");
    }
}
