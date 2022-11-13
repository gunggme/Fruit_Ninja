using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    public Image fadeImage;
    public Button openButton;
    public Button openButton2;
    public Text BestScore;
    public Text Gameover;

    private Blade blade;
    private Spawner spawner;
    

    public string Best = "BestScore";
    public float BESTScore_ = 0f;


    public Text time_text;
    public float time = 61f;

    public int score;

    
    private void Update()
    {
        time -= Time.deltaTime;
        time_text.text = "Time : " + ((int)time % 60).ToString();

        if(time < 0)
        {
            GameOver();
        }
    }
    private void Awake()
    {
        blade = FindObjectOfType<Blade>();
        spawner = FindObjectOfType<Spawner>();

        

        BESTScore_ = PlayerPrefs.GetFloat(Best);
        BestScore.text = "Best Score : " + BESTScore_.ToString();
    }

    private void Start()
    {
        NewGame();
    }

    public void NewGame()
    {
        Time.timeScale = 1f;

        ClearScene();

        time = 61f;

        blade.enabled = true;
        spawner.enabled = true;
        GetComponent<Spawner>().bombChance = 0.005f;

        score = 0;
        scoreText.text = "Score : " + score.ToString();
    }

    private void ClearScene()
    {
        Fruit[] fruits = FindObjectsOfType<Fruit>();

        foreach (Fruit fruit in fruits)
        {
            Destroy(fruit.gameObject);
        }

        Bomb[] bombs = FindObjectsOfType<Bomb>();

        foreach (Bomb bomb in bombs)
        {
            Destroy(bomb.gameObject);
        }
    }

    public void IncreaseScore(int points)
    {
        score += points;    
        scoreText.text = "Score : " + score.ToString();

        
    }

    public void Explode()
    {
        blade.enabled = false;
        spawner.enabled = false;

        StartCoroutine(ExplodeSequence());
    }

    public void GameOver()
    {
        Time.timeScale = 0f;

        openButton.gameObject.SetActive(true);
        openButton2.gameObject.SetActive(true);
        Gameover.gameObject.SetActive(true);
        
    }


    private IEnumerator ExplodeSequence()
    {
        float elapsed = 0f;
        float duration = 0.5f;

        // Fade to white
        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            fadeImage.color = Color.Lerp(Color.clear, Color.white, t);

            Time.timeScale = 1f - t;
            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }

        yield return new WaitForSecondsRealtime(1f);

        GameOver();

        elapsed = 0f;

        // Fade back in
        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            fadeImage.color = Color.Lerp(Color.white, Color.clear, t);

            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }
    }

}
