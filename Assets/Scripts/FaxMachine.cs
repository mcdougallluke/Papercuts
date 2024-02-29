using System.Collections;
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
    private bool showEnemyWarning = false;

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
        if (isPlayerClose && playerScript.paperCount >= paperThreshold && !isFaxing)
        {
            countdown -= Time.deltaTime;

            // Only update countdownText with countdown if showEnemyWarning is false
            if (!showEnemyWarning)
            {
                countdownText.text = "Defend the Fax Machine!\n" + countdown.ToString("F2");
            }

            if (!enemiesSpawned)
            {
                enemiesSpawned = true; // Prevents coroutine from being called more than once
                StartCoroutine(PrepareAndSpawnEnemies());
            }

            if (countdown <= 0)
            {
                Debug.Log("Faxing complete!");
                isFaxing = true;
                SceneManager.LoadScene("Main Menu");
            }
        }
        else
        {
            // Reset text and flag when conditions are not met
            countdownText.text = "";
            showEnemyWarning = false;
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

    IEnumerator PrepareAndSpawnEnemies()
    {
        // Temporarily prevent the normal countdown message from showing
        showEnemyWarning = true;
        countdownText.text = "Enemies are coming, Defend the Fax Machine!";
        yield return new WaitForSeconds(3); // Show the message for 3 seconds

        SpawnEnemyAroundFaxMachine();

        // After spawning enemies, allow the countdown message to resume
        showEnemyWarning = false;
    }

    void SpawnEnemyAroundFaxMachine()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}
