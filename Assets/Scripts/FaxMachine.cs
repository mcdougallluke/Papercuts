using UnityEngine;
using UnityEngine.SceneManagement;

public class FaxMachine : MonoBehaviour
{
    public GameObject player; // Assign this in the Inspector
    private AutoProximityPickUp playerScript;
    private bool isPlayerClose = false;
    private float countdown = 12f; // 12 second countdown
    private float resetCountDown = 12f;
    private bool isFaxing = false;

    [SerializeField]
    public int paperThreshold = 45;

    void Start()
    {
        playerScript = player.GetComponent<AutoProximityPickUp>(); // Get script that has the paper count
    }

    void Update()
    {
        //Debug.Log($"Paper Count: {playerScript.paperCount}, Threshold: {paperThreshold}");
        //Debug.Log("Time Remaining: " + countdown);

        // Check if player is close enough and has more than x papers
        if (isPlayerClose && playerScript.paperCount > paperThreshold && !isFaxing)
        {
            // Start the countdown
            countdown -= Time.deltaTime;
            Debug.Log("Time Remaining: " + countdown);
            if (countdown <= 0)
            {
                // Complete the faxing process
                Debug.Log("Faxing complete!");
                isFaxing = true; // Prevent the faxing process from repeating
                
                // Load next scene
                SceneManager.LoadScene("EndLevelOne");
            }
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
            countdown = resetCountDown; // Reset the countdown when the player leaves
            isFaxing = false; // Allow faxing again if the player comes back
        }
    }
}

