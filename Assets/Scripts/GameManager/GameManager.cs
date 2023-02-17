using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float timeScale = 1f;
    [SerializeField] private List<GameObject> spawnPoints;
    [SerializeField] private Canvas narrativeCanvas;
    [SerializeField] private Canvas imageCanvas;
    [SerializeField] private Canvas endCanvas;
    [SerializeField] private TextMeshProUGUI endCanvasText;
    [SerializeField] private GameObject mainRoot;
    TextMeshProUGUI storyText;
    private GameState gameState;
    private bool started = false;
    private bool storyStarted = false;
    private Coroutine storyCoroutine;
    private Coroutine skipCoroutine;
    int enemyCount;
    private void Awake()
    {
        gameState = GameState.story;
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = timeScale;
        enemyCount = spawnPoints.Count;
        storyText = narrativeCanvas.transform.Find("StoryText").GetComponent<TextMeshProUGUI>(); 
    }
    private void Update()
    {
        switch (gameState)
        {
            case GameState.story:
                if (!storyStarted)
                {
                    storyStarted = true;
                    GameStory();      
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    //skipCoroutine = StartCoroutine(SkipStory());
                    StopCoroutine(storyCoroutine);
                    Debug.Log("Stop Story");
                    narrativeCanvas.gameObject.SetActive(false);
                    gameState = GameState.started;
                }
                break;
            case GameState.started:
                if (!started)
                {
                    started = true;
                    Time.timeScale = 1;
                    mainRoot.SetActive(true);
                    StartCoroutine(CreateMiniRoot());
                }
                break;
            case GameState.paused:
                GamePaused();
                break;
            case GameState.won:
                GameWon();
                break;
            case GameState.lose:
                GameLost();
                break;
            default:
                break;
        }
    }
    private IEnumerator CreateMiniRoot()
    {
        int x = 0;
        int difficulty = PlayerPrefs.GetInt("Difficulty");
        int level1EnemyChance = 1;
        int level2EnemyChance = 1;
        int level3EnemyChance = 1;
        int enemyCountDif = 0;
        if (difficulty == 0)
        {
            Debug.Log("easy");
            level1EnemyChance = 3;
            enemyCountDif = 8;
        }
        else if (difficulty == 1)
        {
            Debug.Log("Normal");
            level1EnemyChance = 2;
            level2EnemyChance = 3;
            enemyCountDif = 10;
        }
        else if (difficulty == 2)
        {
            Debug.Log("Hard");
            level2EnemyChance = 2;
            level3EnemyChance = 4;
            enemyCountDif = 12;
        }
        while (x < enemyCountDif)
        {
            Vector3 pos = spawnPoints[x].transform.position;
            int enemyX = Random.Range(0, level1EnemyChance + level2EnemyChance + level3EnemyChance);

            if (enemyX < level1EnemyChance)
            {
                Instantiate(GameResources.Instance.level1EnemyPrefab, pos, Quaternion.identity);
            }
            else if (enemyX >= level1EnemyChance && (enemyX < level1EnemyChance + level2EnemyChance))
            {
                Instantiate(GameResources.Instance.level2EnemyPrefab, pos, Quaternion.identity);
            }
            else if ((enemyX >= level1EnemyChance + level2EnemyChance))
            {
                Instantiate(GameResources.Instance.level3EnemyPrefab, pos, Quaternion.identity);
            }
            x++;
            yield return new WaitForSeconds(1f);
        }

        
    }
    /*private IEnumerator SkipStory()
    {
        yield return new WaitForSeconds(2f)
    }*/
    public void SetGameState(GameState state)
    {
        gameState = state;
    }
    private void GameStory()
    {
        Time.timeScale = 1;

        storyCoroutine = StartCoroutine(FadeCanvas());

    }
    private IEnumerator FadeCanvas()
    {
        storyText.SetText("One day the calm people of the Bay City were living their life peacefully");
        StartCoroutine(Fade(5f));
        yield return new WaitForSeconds(5f);
        storyText.SetText("Until that one day when the weather get dark suddenly and a asteroid storm started");
        StartCoroutine(Fade(5f));
        yield return new WaitForSeconds(5f);
        storyText.SetText("With the asteroids, some evil creatures also arrived the earth and started refuting it with the everything that lives in it");
        StartCoroutine(Fade(7f));
        yield return new WaitForSeconds(7f);
        storyText.SetText("It was growing every second, slow by slow with the food it get");
        StartCoroutine(Fade(5f));
        yield return new WaitForSeconds(5f);
        storyText.SetText("Arthur, a 12 years old boy scout, is one of the first witnesses of these creatures");
        StartCoroutine(Fade(5f));
        yield return new WaitForSeconds(5f);
        storyText.SetText("and he realized that his mother was at home sleeping and didn't know what was happening");
        StartCoroutine(Fade(5f));
        yield return new WaitForSeconds(5f);
        storyText.SetText("There was one big creature and several smaller creature and Arthur found out that the smaller ones was feeding the big one");
        StartCoroutine(Fade(7f));
        yield return new WaitForSeconds(7f);
        storyText.SetText("He tought that if he can destroy the smaller ones the bigger one will be defeated automatically or at least stop it's growing");
        StartCoroutine(Fade(7f));
        yield return new WaitForSeconds(7f);
        storyText.SetText("He must fight for his mother");
        StartCoroutine(Fade(5f));
        yield return new WaitForSeconds(5f);

        narrativeCanvas.gameObject.SetActive(false);
        gameState = GameState.started;
    }
    private IEnumerator Fade(float wait)
    {
        for (float i = 0; i <= 255; i += Time.deltaTime)
        {
            storyText.alpha = i;
            yield return null;
        }
        yield return new WaitForSeconds(wait - 2);
        for (float i = 255; i >= 0; i -= Time.deltaTime)
        {
            storyText.alpha = i;
            yield return null;
        }
    }
    private void GameWon()
    {
        Time.timeScale = 0;
        endCanvas.gameObject.SetActive(true);
        endCanvasText.SetText("You Won");
    }
    private void GameLost()
    {
        Time.timeScale = 0;
        endCanvas.gameObject.SetActive(true);
        endCanvasText.SetText("You Lost");
    }
    private void GamePaused()
    {
        Time.timeScale = 0;
    }
    public void EnemyKilled()
    {
        enemyCount--;
        if (enemyCount == 0)
        {
            gameState = GameState.won;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
