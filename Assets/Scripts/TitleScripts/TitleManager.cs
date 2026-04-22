using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public GameObject titleText;
    float timer = 0.0f;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("GameScene");
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        //title text effect
        timer += Time.deltaTime;
        if (timer % 2.0f == 0.0f)
        {
            SetAlpha(titleText, 3.0f);
        }
        else
        {
            SetAlpha(titleText, 0.0f);
        }
    }

    void SetAlpha(GameObject obj, float alpha)
    {
        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
        Color color = text.color;
        color.a = alpha;
        text.color = color;
    }
}
