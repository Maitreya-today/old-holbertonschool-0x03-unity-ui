using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float speed;
	private int score = 0;
	public int health = 5;

	// Start is called before the first frame update
	void Start()
	{

	}
	void Update()
	{
		if (health == 0)
		{
			Debug.Log("Game Over!");
			SceneManager.LoadScene("maze");
		}
	}
	// Update is called once per frame
	void FixedUpdate()
	{
		if (Input.GetKey("up") || Input.GetKey("w"))
		{
			GetComponent<Rigidbody>().velocity = new Vector3(0, 0, speed);
		}
		if (Input.GetKey("right") || Input.GetKey("d"))
		{
			GetComponent<Rigidbody>().velocity = new Vector3(speed, 0, 0);
		}
		if (Input.GetKey("down") || Input.GetKey("s"))
		{
			GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -speed);
		}
		if (Input.GetKey("left") || Input.GetKey("a"))
		{
			GetComponent<Rigidbody>().velocity = new Vector3(-speed, 0, 0);
		}
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.tag == "Pickup")
		{
			score += 1;
			Destroy(other.gameObject);
			Debug.Log("Score: " + score);
		}
		if (other.tag == "Trap")
		{
			health -= 1;
			Debug.Log("Health: " + health);
		}
		if (other.tag == "Goal")
		{
			Debug.Log("You win!");
		}
	}

}