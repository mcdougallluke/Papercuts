using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FaxMachine : MonoBehaviour
{
    public GameObject player;
    private AutoProximityPickUp playerScript;
    private bool isPlayerClose = false;
    private float countdown = 15f;
    private float resetCountDown = 15f;
    private bool isFaxing = false;

    [SerializeField]
    public GameObject enemyPrefab;

    public float spawnRadius = 5f;
    private bool enemiesSpawned = false;
    public Transform[] spawnPoints;

    [SerializeField]
    public int paperThreshold = 45;

    [SerializeField]
    public Text countdownText;

    void Start()
    {
        playerScript = player.GetComponent<AutoProximityPickUp>();
    }

    void Update()
    {
        // Check if player is close enough and has more than x papers
        if (isPlayerClose && playerScript.paperCount >= paperThreshold && !isFaxing)
        {
            countdown -= Time.deltaTime;
            countdownText.text = "Defend the Fax Machine!\n" + countdown.ToString("F2"); // Formats the float to show only two decimal places

            if (!enemiesSpawned)
            {
                SpawnEnemyAroundFaxMachine();
            }
            enemiesSpawned = true;

            if (countdown <= 0)
            {
                Debug.Log("Faxing complete!");
                isFaxing = true;

                // Load Win Scene
                SceneManager.LoadScene("Main Menu");
            }
        }
        else
        {
            countdownText.text = ""; // Hide the timer when not in use
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            Debug.Log("Player in Range");
            isPlayerClose = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            Debug.Log("Player NOT in Range");
            isPlayerClose = false;
            countdown = resetCountDown;
            isFaxing = false;
        }
    }

    void SpawnEnemyAroundFaxMachine()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}

