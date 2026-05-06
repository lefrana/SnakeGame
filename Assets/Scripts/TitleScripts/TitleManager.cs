using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public GameObject instructionText;
    public TextMeshProUGUI titleText;

    float instructionTimer = 0.0f;

    string fullTitle = "<snake>";
    float textInterval = 0.3f;
    float textTimer = 0.0f;
    int currentIndex = 0;

    public AudioSource titleAudio;
    public AudioSource startAudio;
    public AudioClip titleSfx;
    public AudioClip startSfx;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            startAudio.PlayOneShot(startSfx);
            Invoke("LoadGame", 0.3f);
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        ShowTitle();
        ShowInstructions();

    }

    void LoadGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    void SetAlpha(GameObject obj, float alpha)
    {
        TextMeshProUGUI text = obj.GetComponent<TextMeshProUGUI>();
        Color color = text.color;
        color.a = alpha;
        text.color = color;
    }

    void ShowInstructions()
    {
        instructionTimer += Time.deltaTime;

        if (instructionTimer >= 0.5f)
        {
            SetAlpha(instructionText, 1.0f);

            if (instructionTimer >= 1.0f)
            {
                instructionTimer = 0f;
            }
        }
        else
        {
            SetAlpha(instructionText, 0.0f);
        }
    }

    void ShowTitle()
    {
        textTimer += Time.deltaTime;

        if (textTimer >= textInterval && currentIndex < fullTitle.Length)
        {
            currentIndex++;

            titleText.text = fullTitle.Substring(0, currentIndex);

            titleAudio.PlayOneShot(titleSfx);

            textTimer = 0f;
        }
    }
}
