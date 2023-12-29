using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Menus : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject soundOff;
    public GameObject gamePlayMenu;
    public GameObject gameOverMenu;
    public GameObject pauseMenu;
    public CameraSmoothFollow cameraFollow;
    public AudioSource buttonClick;

    public Text score;
    public Text gameOverScore;
    public Text gameOverBestScore;
    public static Menus Instance;
    [SerializeField] Button JumpButton;

    private void Awake()
    {
        Instance = this;
    }
    public void Play() {
        buttonClick.Play();
        gameOverMenu.SetActive(false);
        pauseMenu.SetActive(false);
        score.text = "SCORE: 0";

        GameObject playerCube = GameObject.Find("PlayerCube");
        if(playerCube != null) {
            playerCube.transform.position = new Vector3(0, 0.5f, 0);
        }else {
            GameObject player = Instantiate (Resources.Load ("PlayerCube") as GameObject);
            player.transform.position = new Vector3(0, 0.5f, 0);
            player.name = "PlayerCube";
        }

        
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        for(int i = 0; i < obstacles.Length; i++) {
            Destroy(obstacles[i]);
        }
        Vars.obstacle = 0;
        Vars.score = 0;
        Vars.combo = 1;

        for(int i = 0; i < 5; i++) {
            GameObject.Find("GameManager").GetComponent<ObstacleManagment>().CreateObstacle();
        }
        
        mainMenu.SetActive(false);
        gamePlayMenu.SetActive(true);
        Time.timeScale = 1;
        cameraFollow.enabled = true;
        Camera.main.transform.position = new Vector3(0, 5.499999f, -2.9999f);
    }

    public void BackToMainMenu() {
        Time.timeScale = 1;
        buttonClick.Play();
        gameOverMenu.SetActive(false);
        pauseMenu.SetActive(false);
        cameraFollow.enabled = false;
        Camera.main.transform.position = new Vector3(0, 5.499999f, -2.9999f);

        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        for(int i = 0; i < obstacles.Length; i++) {
            Destroy(obstacles[i]);
        }
        Vars.obstacle = 0;
        
        GameObject playerCube = GameObject.Find("PlayerCube");
        if(playerCube != null) Destroy(playerCube);
        gamePlayMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void ShowPauseMenu() {
        Time.timeScale = 0;
        buttonClick.Play();
        pauseMenu.SetActive(true);
    }

    public void HidePauseMenu() {
        Time.timeScale = 1;
        buttonClick.Play();
        pauseMenu.SetActive(false);
    }

    public void ShowGameOverMenu() {
        Invoke("GameOver", 1.5f);
    }

    private void GameOver() {
        gameOverMenu.SetActive(true);
        gameOverScore.text = "YOUR SCORE: " + Vars.score;
         if(Vars.score > PlayerPrefs.GetInt("BestScore")) {
            PlayerPrefs.SetInt("BestScore", Vars.score);
        }
        gameOverBestScore.text = "BEST SCORE: " + PlayerPrefs.GetInt("BestScore");
        Camera.main.transform.position = new Vector3(0, 5.499999f, -2.9999f);
    }

    public void SoundOnOff() {
        buttonClick.Play();
        if(AudioListener.volume == 0f) {
             AudioListener.volume = 1f;
             soundOff.SetActive(false);
        }else {
            soundOff.SetActive(true);
             AudioListener.volume = 0f;
        }
    }
    public bool isButtonPressed;
    public void Quit() {
        buttonClick.Play();
        Application.Quit();
    }
    public bool IsJump;
    public void OnJumpBtnPressed()
    {
        IsJump = true;
        Debug.Log("Jump pressed");
        PlayerLogic.instance.Jump();
        JumpButton.interactable = false;
        StartCoroutine(WaitUntillLandCube());
    }
    IEnumerator WaitUntillLandCube()
    {
        yield return new WaitForSeconds(1f);
        JumpButton.interactable = true;
        IsJump = false;
    }
    public void OnButtonPress()
    {
        Debug.Log("Clicked");
        isButtonPressed = true;
    }

    // Call this method when your button is released
    public void OnButtonRelease()
    {
        Debug.Log("Released");
        isButtonPressed = false;
    }
}
