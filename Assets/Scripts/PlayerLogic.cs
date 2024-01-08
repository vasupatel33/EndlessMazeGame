using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerLogic : MonoBehaviour
{
    public float initialSpeed = 0.15f;
    private float speed = 0.15f;
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

    //public float minSwipeDistance = 20f;
    public static PlayerLogic instance;

    public List<GameObject> AllGeneteratedPlayer;
    public List<Rigidbody> AllRigidbodies;
    [SerializeField] private GameObject parent;
   
    //Swipe and left and right move variables
    private Vector2 startPos;
    private float swipeThreshold = 50f;

    private bool isTouchingLeft = false;
    private bool isTouchingRight = false;

    private bool isSwipeUp = false;
    private bool isTouching = false;

    private float timer = 0f;
    private float executionInterval = 0.15f;
    [SerializeField] GameObject BulletPref;
    [SerializeField] GameObject PlayerPref;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        PlayerPref = GameObject.Find("PlayerCube");
        parent = GameObject.Find("AllPlayer");
        speed = initialSpeed;
        cameraFollow = GameObject.Find("Main Camera").GetComponent<CameraSmoothFollow>();
        //scoreSound = GameObject.Find("ScoreSound").GetComponent<AudioSource>();
        BulletPref = GameObject.Find("Bullet");
        cameraFollow.enabled = true;
        //score = GameObject.Find("ScoreText").GetComponent<Text>();
        combo = GameObject.Find("ComboText").GetComponent<Text>();
    }
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= executionInterval)
        {
            // Your code to be executed
            GameObject pref = Instantiate(BulletPref,this.transform.position,Quaternion.identity);
            pref.transform.DOMoveZ(transform.position.z + 180, 3);
            //pref.GetComponent<Rigidbody>().AddForce(Vector3.back * 10, ForceMode.VelocityChange);
            // Reset the timer
            Destroy(pref,2.5f);
            timer = 0f;
        }
        if(this.transform.gameObject.transform.position.y <= -3)
        {
            //SceneManager.LoadScene(0);
            Destroy(this.gameObject);
            AllGeneteratedPlayer.RemoveAt(AllGeneteratedPlayer.Count - 1);
            AllRigidbodies.RemoveAt(AllRigidbodies.Count - 1);
            //cameraFollow.enabled = false;
            explosion.SetActive(true);

            if (AllGeneteratedPlayer.Count == 0)
            {
                Debug.Log("Collision");
                explosion.transform.parent = null;
                GameObject.Find("GameManager").GetComponent<Menus>().ShowGameOverMenu();
                GameObject.Find("ExplosionSound").GetComponent<AudioSource>().Play();
            }
        }
        CheckTouchInput();
    }

    void FixedUpdate()
    {
        rb.transform.position = new Vector3(rb.transform.position.x, rb.transform.position.y, rb.transform.position.z + speed);
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput < 0)
        {
            Debug.Log("Left");
            this.transform.position = new Vector3(rb.transform.position.x - (speed + 0.1f), rb.transform.position.y, rb.transform.position.z);
        }
        else if (horizontalInput > 0)
        {
            Debug.Log("Right");
            this.transform.position = new Vector3(rb.transform.position.x + (speed + 0.1f), rb.transform.position.y, rb.transform.position.z);
        }
        if (isSwipeUp)
        {
            // Handle swipe up logic
            speed += 0.02f;
            Debug.Log("Swipe up");
        }
        else if (isTouching && !Menus.Instance.IsJump)
        {
            // Handle touch logic
            if (EventSystem.current.IsPointerOverGameObject() || !Input.GetMouseButton(0))
            {
                // The touch is over a UI element, don't execute movement code
                speed = initialSpeed ;
                return;
            }
            // Handle left or right touch logic
            if (isTouchingLeft)
            {
                Debug.Log("Left");
                targetPosition = new Vector3(rb.transform.position.x - (speed + 0.5f), rb.transform.position.y, rb.transform.position.z);
            }
            else if (isTouchingRight)
            {
                Debug.Log("Right");
                targetPosition = new Vector3(rb.transform.position.x + (speed + 0.5f), rb.transform.position.y, rb.transform.position.z);
            }

            rb.transform.position = Vector3.SmoothDamp(rb.transform.position, targetPosition, ref velocity, smoothTime, Mathf.Infinity, Time.deltaTime);
        }
        else
        {
            speed = initialSpeed;
            Vars.combo = 1;
            combo.enabled = false;
        }
    }

    void OnTouchBegan(Vector2 touchPosition)
    {
        // Determine if touch is on the left or right side
        float screenWidth = Screen.width;
        float screenCenter = screenWidth / 2f;
        bool isLeftSide = touchPosition.x < screenCenter;

        if (isLeftSide)
        {
            isTouchingLeft = true;
            isTouchingRight = false; // Reset right side flag
        }
        else
        {
            isTouchingRight = true;
            isTouchingLeft = false; // Reset left side flag
        }

        if (!Menus.Instance.IsJump)
        {
            isTouching = true;
            Debug.Log("Touch is enabled");
            // Store the initial position for swipe detection
            startPos = touchPosition;
        }
    }

    void OnTouchMoved(Vector2 touchPosition)
    {
        // Check for swipe logic
        float deltaY = touchPosition.y - startPos.y;

        if (deltaY > swipeThreshold)
        {
            isSwipeUp = true;
            isTouching = false; // Disable left/right movement while swiping up
        }
    }

    void OnTouchEnded()
    {
        isTouchingLeft = false;
        isTouchingRight = false;
        isSwipeUp = false;
        isTouching = false;

        // Reset the target position when touch ends
        targetPosition = rb.transform.position;
    }

    void CheckTouchInput()
    {
        foreach (Touch touch in Input.touches)
        {
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    OnTouchBegan(touch.position);
                    break;
                case TouchPhase.Moved:
                    OnTouchMoved(touch.position);

                    break;
                case TouchPhase.Ended:
                    OnTouchEnded();
                    break;
            }
        }
    }

    public void Jump()
    {
        //parent.transform.DOMoveY(transform.position.y + 2,1);

        //foreach (Rigidbody obj in AllRigidbodies)
        //{
        //    Debug.Log("Jumped");
        //    obj.AddForce(Vector3.up * 1000, ForceMode.Force);
        //}
    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Trigger");
        Menus.Instance.ScoreManager();
        
        //scoreSound.pitch = 0.9f + (float)Vars.combo / 10;
        //scoreSound.Play();
        //Vars.score += Vars.combo;
        //score.text = "SCORE: " + Vars.score;
        //combo.text = "+" + Vars.combo;
        //if (Vars.combo > 1)
        //{
        //    combo.enabled = true;
        //}
        //Vars.combo++;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "enemy")
        {
            Destroy(this.gameObject);
            AllGeneteratedPlayer.RemoveAt(AllGeneteratedPlayer.Count - 1);
            AllRigidbodies.RemoveAt(AllRigidbodies.Count - 1);
            //cameraFollow.enabled = false;
            explosion.SetActive(true);
            
            if (AllGeneteratedPlayer.Count == 0)
            {
                Debug.Log("Collision");
                explosion.transform.parent = null;
                GameObject.Find("GameManager").GetComponent<Menus>().ShowGameOverMenu();
                GameObject.Find("ExplosionSound").GetComponent<AudioSource>().Play();
            }
        }
    }

    public void SpawnFollowingPlayer()
    {
        if (AllGeneteratedPlayer.Count < 3)
        {
            Vector3 spawnPosition = AllGeneteratedPlayer[AllGeneteratedPlayer.Count - 1].transform.position - new Vector3(-1.5f, 0f, 0f);
            GameObject newPlayer = Instantiate(PlayerPref, spawnPosition, Quaternion.identity,parent.transform);
            AllGeneteratedPlayer.Add(newPlayer);
            AllRigidbodies.Add(newPlayer.GetComponent<Rigidbody>());
            Debug.Log("Player added");
        }
    }
}