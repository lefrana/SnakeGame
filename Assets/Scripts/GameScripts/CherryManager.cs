using UnityEngine;

public class CherryManager : MonoBehaviour
{
    public CherryGenerator cherryGenerator;
    public SnakeBodyManager snakeBodyManager;
    public ScoreManager scoreManager;

    public AudioSource cherryAudio;
    public AudioClip cherrySfx;

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
            cherryAudio.PlayOneShot(cherrySfx);
            scoreManager.AddScore();

            //grow snake body
            snakeBodyManager.Grow(transform.position - new Vector3(1000.0f, 1000.0f, 0));

            cherryGenerator.SpawnCherry();
            Destroy(gameObject, 0.2f);
        }
    }
}
