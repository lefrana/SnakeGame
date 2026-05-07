using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;
using UnityEngine.SceneManagement;

public class CharaMovement : MonoBehaviour
{
    public Vector2 direction = Vector2.right;
    public float moveDelay = 0.3f;
    int lastScore = 0; //for managing score to increase speed

    private float timer;
    private bool isGameOver = false;

    //to prevent overlapping input bug
    private bool canInput = true;

    public float tileSize = 0.5f;

    public SnakeBodyManager bodyManager;
    public ScoreManager scoreManager;

    public Tilemap wallTilemap;
    public TextMeshProUGUI gameOverText;

    public AudioSource gameOverAudio;
    public AudioClip gameOverSfx;

    void Start()
    {
        scoreManager = FindFirstObjectByType<ScoreManager>();
    }

    void Update()
    {
        if (isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("TitleScene");
            }

            return;
        }

        HandleInput();

        timer += Time.deltaTime;
        if (timer >= moveDelay)
        {
            timer = 0f;
            Move();

            canInput = true;
        }

        if (scoreManager.score != lastScore)
        {
            lastScore = scoreManager.score;
            IncreaseSpeed();
        }

    }

    void HandleInput()
    {
        if(!canInput)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && direction != Vector2.down)
        {
            direction = Vector2.up;
            canInput = false;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && direction != Vector2.up)
        {
            direction = Vector2.down;
            canInput = false;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && direction != Vector2.right)
        {
            direction = Vector2.left;
            canInput = false;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && direction != Vector2.left)
        {
            direction = Vector2.right;
            canInput = false;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void Move()
    {
        Vector3 prevPos = transform.position;
        Vector3 newPos = prevPos + new Vector3(direction.x, direction.y, 0) * tileSize;

        Vector3Int cellPos = wallTilemap.WorldToCell(newPos);

        if (wallTilemap.HasTile(cellPos))
        {
            GameOver();
            return;
        }

        transform.position = newPos;

        UpdateRotation();
        bodyManager.MoveBody(prevPos);
    }

    void UpdateRotation()
    {
        if (direction == Vector2.up)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (direction == Vector2.down)
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else if (direction == Vector2.left)
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else if (direction == Vector2.right)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void IncreaseSpeed()
    {
        if (scoreManager.score % 1 == 0)
        {
            moveDelay -= 0.03f;

            if (moveDelay < 0.03f)
            {
                moveDelay = 0.03f;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (bodyManager.bodyParts.Count > 0 &&
            collision.transform == bodyManager.bodyParts[0])
        {
            return;
        }

        if (collision.CompareTag("SnakeBody"))
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        isGameOver = true;
        //Debug.Log("Game Over");

        gameOverText.text = "game over.";
        gameOverAudio.PlayOneShot(gameOverSfx);
    }
}