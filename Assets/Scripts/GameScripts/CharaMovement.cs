using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class CharaMovement : MonoBehaviour
{
    public Vector2 direction = Vector2.right;
    public float moveDelay = 0.3f;

    private float timer;
    private bool isGameOver = false;

    public float tileSize = 0.5f;

    public SnakeBodyManager bodyManager;

    public Tilemap wallTilemap;
    public TextMeshProUGUI gameOverText;

    void Update()
    {
        if (isGameOver) return;

        HandleInput();

        timer += Time.deltaTime;
        if (timer >= moveDelay)
        {
            timer = 0f;
            Move();
        }
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && direction != Vector2.down)
        {
            direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && direction != Vector2.up)
        {
            direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && direction != Vector2.right)
        {
            direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && direction != Vector2.left)
        {
            direction = Vector2.right;
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
            transform.rotation = Quaternion.Euler(0, 0, 90);
        else if (direction == Vector2.down)
            transform.rotation = Quaternion.Euler(0, 0, -90);
        else if (direction == Vector2.left)
            transform.rotation = Quaternion.Euler(0, 0, 180);
        else if (direction == Vector2.right)
            transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    //public void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.CompareTag("SnakeBody"))
    //    {
    //        GameOver();
    //    }
    //}

    public void GameOver()
    {
        isGameOver = true;
        //Debug.Log("Game Over");

        gameOverText.text = "game over.";
    }
}