//using System.Collections;
//using System.Collections.Generic;
//using TMPro;
//using UnityEngine;
//using UnityEngine.EventSystems;
//using UnityEngine.UI;

//public class PlayerLogic : MonoBehaviour
//{
//    public float initialSpeed = 0.2f;
//    private float speed = 0.2f;
//    public Rigidbody rb;
//    public GameObject explosion;
//    public CameraSmoothFollow cameraFollow;
//    public GameObject gameOverMenu;

//    public Text score;
//    public Text combo;

//    private AudioSource scoreSound;

//    void Start()
//    {
//        speed = initialSpeed;
//        cameraFollow = GameObject.Find("Main Camera").GetComponent<CameraSmoothFollow>();
//        cameraFollow.enabled = true;
//        score = GameObject.Find("ScoreText").GetComponent<Text>();
//        combo = GameObject.Find("ComboText").GetComponent<Text>();
//        scoreSound = GameObject.Find("ScoreSound").GetComponent<AudioSource>();
//    }

//    public float smoothTime = 0.1f; // Smoothing time for position interpolation
//    private Vector3 velocity = Vector3.zero; // Velocity for smoothing

//     Vector3 targetPosition;

//    void Jump()
//    {

//        Debug.Log("Jumped");
//        // Add your jump logic here, for example, applying force to the rigidbody
//        rb.AddForce(Vector3.up * 300);
//    }

//    void FixedUpdate()
//    {
//        if (Menus.instance.shouldJump)
//        {
//            Jump();
//            // Perform jump action
//            Menus.instance.shouldJump = false; // Reset the flag
//        }

//            Debug.Log("LEft and right");
//        if (Input.GetMouseButton(0))
//        {
//            Vector3 mousePosition = Input.mousePosition;

//            float screenWidth = Screen.width;
//            float screenCenter = screenWidth / 2f;
//            bool isLeftSide = mousePosition.x < screenCenter;

//            if (isLeftSide)
//            {
//                Debug.Log("Left");
//                // Left side of the screen
//                targetPosition = new Vector3(rb.transform.position.x - (speed + 0.5f), rb.transform.position.y, rb.transform.position.z);
//            }
//            else
//            {
//                Debug.Log("Right");
//                // Right side of the screen
//                targetPosition = new Vector3(rb.transform.position.x + (speed + 0.5f), rb.transform.position.y, rb.transform.position.z);
//                //if (speed < 0.7f)
//                //{
//                //    speed += 0.05f;
//                //}
//            }
//            rb.transform.position = Vector3.SmoothDamp(rb.transform.position, targetPosition, ref velocity, smoothTime);
//        }
//        else
//        {
//            speed = initialSpeed;
//            Vars.combo = 1;
//            combo.enabled = false;
//        }

//        rb.transform.position = new Vector3(rb.transform.position.x, rb.transform.position.y, rb.transform.position.z + speed);
//    }
//    void IncreaseSpeed()
//    {
//        if (speed < 0.7f)
//        {
//            speed += 0.05f;
//        }
//    }

//    // Call this method when your button is pressed

//    void OnTriggerEnter(Collider col)
//    {
//        scoreSound.pitch = 0.9f + (float)Vars.combo / 10;
//        scoreSound.Play();
//        Vars.score += Vars.combo;
//        score.text = "SCORE: " + Vars.score;
//        combo.text = "+" + Vars.combo;
//        if (Vars.combo > 1)
//        {
//            combo.enabled = true;
//        }
//        Vars.combo++;
//    }

//    void OnCollisionEnter(Collision col)
//    {
//        if (col.gameObject.tag == "enemy")
//        {
//            explosion.transform.parent = null;
//            explosion.SetActive(true);
//            cameraFollow.enabled = false;
//            GameObject.Find("GameManager").GetComponent<Menus>().ShowGameOverMenu();
//            GameObject.Find("ExplosionSound").GetComponent<AudioSource>().Play();
//            Destroy(this.gameObject);
//        }
//    }

//}
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class PlayerLogic : MonoBehaviour
{
    public float initialSpeed = 0.1f;
    private float speed = 0.1f;
    public Rigidbody rb;
    public GameObject explosion;
    public CameraSmoothFollow cameraFollow;
    public GameObject gameOverMenu;

    public Text score;
    public Text combo;

    private AudioSource scoreSound;

    public float smoothTime = 0.1f; // Smoothing time for position interpolation
    private Vector3 velocity = Vector3.zero; // Velocity for smoothing
    private Vector3 targetPosition;

    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;
    public float minSwipeDistance = 20f;
    public SwipeManager swipeControls;
    public static PlayerLogic instance;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        speed = initialSpeed;
        cameraFollow = GameObject.Find("Main Camera").GetComponent<CameraSmoothFollow>();
        cameraFollow.enabled = true;
        score = GameObject.Find("ScoreText").GetComponent<Text>();
        combo = GameObject.Find("ComboText").GetComponent<Text>();
        scoreSound = GameObject.Find("ScoreSound").GetComponent<AudioSource>();
        swipeControls = GameObject.Find("GameManager").GetComponent<SwipeManager>();
    }

    private Vector2 startPos;
    private bool isSwiping = false;
    private float swipeThreshold = 50f;

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Record the starting position when the touch (or mouse button) begins
            startPos = Input.mousePosition;
            isSwiping = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isSwiping = false;
        }
        rb.transform.position = new Vector3(rb.transform.position.x, rb.transform.position.y, rb.transform.position.z + speed);
        // Check if the mouse is over a UI element
        if (EventSystem.current.IsPointerOverGameObject())
        {
            // The mouse is over a UI element, don't execute movement code
            speed = initialSpeed;
            return;
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            float screenWidth = Screen.width;
            float screenCenter = screenWidth / 2f;
            bool isLeftSide = mousePosition.x < screenCenter;

            float deltaY = Input.mousePosition.y - startPos.y;
            if (isSwiping && deltaY > swipeThreshold)
            {
                //Debug.Log("22");
                // Calculate the distance moved in the y-axis

                // Check if the swipe is in the up direction
                //if (deltaY > swipeThreshold)
                //{
                speed += 0.03f;
                Debug.Log("Swipe up");
                //}
            }
            else if (isLeftSide)
            {
                // Left side of the screen
                Debug.Log("Left");
                targetPosition = new Vector3(rb.transform.position.x - (speed + 0.5f), rb.transform.position.y, rb.transform.position.z);
            }
            else if (!isLeftSide)
            {
                Debug.Log("Right");
                // Right side of the screen
                targetPosition = new Vector3(rb.transform.position.x + (speed + 0.5f), rb.transform.position.y, rb.transform.position.z);
            }
            else
            {
                Debug.Log("C");
            }

            // Smoothly move towards the targetPosition
            rb.transform.position = Vector3.SmoothDamp(rb.transform.position, targetPosition, ref velocity, smoothTime, Mathf.Infinity, Time.deltaTime);
        }
        else
        {
            speed = initialSpeed;
            Vars.combo = 1;
            combo.enabled = false;
        }
    }
    public void Jump()
    {
        Debug.Log("Jumped");
        rb.AddForce(Vector3.up * 1000);
    }

    //void OnTriggerEnter(Collider col)
    //{
    //    Debug.Log("Trigger");
    //    scoreSound.pitch = 0.9f + (float)Vars.combo / 10;
    //    scoreSound.Play();
    //    Vars.score += Vars.combo;
    //    score.text = "SCORE: " + Vars.score;
    //    combo.text = "+" + Vars.combo;
    //    if (Vars.combo > 1)
    //    {
    //        combo.enabled = true;
    //    }
    //    Vars.combo++;
    //}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "enemy")
        {
            Debug.Log("Collision");
            explosion.transform.parent = null;
            explosion.SetActive(true);
            cameraFollow.enabled = false;
            GameObject.Find("GameManager").GetComponent<Menus>().ShowGameOverMenu();
            GameObject.Find("ExplosionSound").GetComponent<AudioSource>().Play();
            Destroy(this.gameObject);
        }
    }

}