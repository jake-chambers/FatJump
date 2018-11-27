using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class playerScript : MonoBehaviour
{

    public Rigidbody2D rb;
    GUIStyle style = new GUIStyle();
    public float moveSpeed = 4f;
    public bool grounded = true;
    bool isWalking = false;
    public Vector2 jumpHeight;
    public Transform groundCheck;
    bool isJumping = false;

    public GameObject background;

    private Animator animator;
    public float groundRadius = 0.2f;

    public LayerMask ground;
    public int score = 0;

    int heightCheck;


    public Canvas loseCanvas;
    public Canvas startCanvas;
    bool gameStarted = true;

    public TextMeshProUGUI first;

    public InputField input;
    public static string username;
    private int firstTime;


    // Use this for initialization
    void Start()
    {
        Time.timeScale = 0;
        animator = GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        loseCanvas = GameObject.Find("LoseCanvas").GetComponent<Canvas>();
        startCanvas = GameObject.Find("StartCanvas").GetComponent<Canvas>();


        if (PlayerPrefs.GetInt("firstTime") == 1)
        {
            PlayerPrefs.SetInt("First", 0);
            Debug.Log("First time");
            PlayerPrefs.Save();

            PlayerPrefs.SetInt("firstTime", 0);
        }


        first.text = "Highscore: " + PlayerPrefs.GetInt("First").ToString();


    }

    // Update is called once per frame
    void Update()
    {

        background.transform.position = new Vector2(0, transform.position.y);
        score = (int)transform.position.y;

        isWalking = Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow);
        animator.SetBool("Walk_01", isWalking);


        isJumping = Input.GetKeyDown(KeyCode.UpArrow);
        animator.SetBool("Jump_01", isJumping);

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, ground);

        if (Input.GetKeyDown(KeyCode.UpArrow) && grounded)  //makes player jump
        {
            GetComponent<Rigidbody2D>().AddForce(jumpHeight, ForceMode2D.Impulse);
            heightCheck = (int)transform.position.y - 1;

        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {

            transform.position += transform.right * -moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += transform.right * moveSpeed * Time.deltaTime;
        }

        if (transform.position.y < heightCheck)
        {
            Died();
            Time.timeScale = 0;
        }

    }

    private void SubmitName(string arg0)
    {
        username = arg0;
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        loseCanvas.enabled = false;
        startCanvas.enabled = false;

    }

    public void Back()
    {

        loseCanvas.enabled = false;

        startCanvas.enabled = true;
    }

    public void Died()
    {
        if (firstTime == 1)
        {
            PlayerPrefs.SetInt("First", score);
        }

        else if (score > PlayerPrefs.GetInt("First") )
        {

            PlayerPrefs.SetInt("First", score);
            PlayerPrefs.Save();

            Debug.Log("First has changed to: " + PlayerPrefs.GetInt("First"));

        }


        startCanvas.enabled = false;
        loseCanvas.enabled = true;

    }

    public void showLeaderboard()
    {

        startCanvas.enabled = false;
        loseCanvas.enabled = false;

    }

    public void Restart()
    {

        //Get current scene name
        string scene = SceneManager.GetActiveScene().name;
        //Load it
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    void OnGUI()
    {

        style.normal.textColor = Color.black;
        style.fontSize = 40;
        GUI.Label(new Rect(Screen.width * 0.05f, 10, 100, 30), "Score: " + (int)(score), style);




    }
}

