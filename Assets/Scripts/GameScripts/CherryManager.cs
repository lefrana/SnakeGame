using UnityEngine;

public class CherryManager : MonoBehaviour
{
    public CherryGenerator      cherryGenerator;
    public SnakeBodyManager     snakeBodyManager;
    public ScoreManager         scoreManager;

    void Start()
    {
        cherryGenerator     = FindFirstObjectByType<CherryGenerator>();
        snakeBodyManager    = FindFirstObjectByType<SnakeBodyManager>();
        scoreManager        = FindFirstObjectByType<ScoreManager>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            scoreManager.AddScore();

            //grow snake body
            snakeBodyManager.Grow(transform.position - new Vector3(-10.0f, 0, 0));

            cherryGenerator.SpawnCherry();
            Destroy(gameObject);
        }
    }
}
