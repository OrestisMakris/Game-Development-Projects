using UnityEngine;
using UnityEngine.SceneManagement;

public class WinFlag : MonoBehaviour
{
    public GameObject winMessage; // Reference to the win message UI

    void Start()
    {
        // Ensure the win message is hidden at the start of the game
        if (winMessage != null)
        {
            winMessage.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding object has the PlatformerPlayer script
        PlatformerPlayer player = collision.GetComponent<PlatformerPlayer>();
        if (player != null)
        {
            Debug.Log("Congratulations! You Win!");
            ShowWinMessage();
        }
    }

    void ShowWinMessage()
    {
        // Activate the win message UI
        if (winMessage != null)
        {
            winMessage.SetActive(true);

            // Center the win message in the camera view
            Vector3 cameraCenter = Camera.main.transform.position;
            winMessage.transform.position = new Vector3(cameraCenter.x, cameraCenter.y, 0);
        }

        // Restart the game after 2 seconds
        Invoke(nameof(RestartGame), 2f);
    }

    void RestartGame()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
