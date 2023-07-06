using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private int score = 0;
    public int health = 5;
    public Text scoreText;
    public Text healthText;
    public Text winLoseText;
    public Image winLoseBG;

    void Start()
    {
        // Check if the necessary references are assigned
        if (winLoseBG != null)
            winLoseBG.enabled = false;
        else
            Debug.LogError("winLoseBG reference not assigned in the inspector!");

        if (scoreText != null)
            SetScoreText();
        else
            Debug.LogError("scoreText reference not assigned in the inspector!");
    }

    void Update()
    {
        if (health == 0)
        {
            if (winLoseBG != null)
            {
                winLoseBG.enabled = true;
                winLoseText.text = "Game Over!";
                winLoseText.color = Color.white;
                winLoseBG.color = Color.red;
            }
            StartCoroutine(LoadScene(3));
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("menu");
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        GetComponent<Rigidbody>().velocity = movement * speed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            score += 1;
            SetScoreText();
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Trap"))
        {
            health -= 1;
            SetHealthText();
        }
        if (other.CompareTag("Goal"))
        {
            if (winLoseBG != null)
            {
                winLoseBG.enabled = true;
                winLoseText.text = "You Win!";
                winLoseText.color = Color.black;
                winLoseBG.color = Color.green;
            }
            StartCoroutine(LoadScene(3));
        }
    }

    void SetScoreText()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
        else
            Debug.LogError("scoreText reference not assigned in the inspector!");
    }

    void SetHealthText()
    {
        if (healthText != null)
            healthText.text = "Health: " + health;
        else
            Debug.LogError("healthText reference not assigned in the inspector!");
    }

    IEnumerator LoadScene(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
