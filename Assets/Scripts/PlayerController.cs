using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    CharacterController character;
    Vector3 moveVector;
    public float speed = 10f;
    int score = 0;
    int health = 5;
    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        score = 0;
        health = 5;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        character.Move(moveVector * speed * Time.fixedDeltaTime);
    }

    public void OnMovementChanged(InputAction.CallbackContext context)
	{
        Vector2 direction = context.ReadValue<Vector2>();
        moveVector = new Vector3(direction.x, 0, direction.y);
	}

    void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.CompareTag("Pickup"))
		{
            other.gameObject.SetActive(false);
            score++;
            Debug.Log("Score: " + score);
		}

        if (other.gameObject.CompareTag("Trap"))
		{
            health--;
            Debug.Log("Health: " + health);
            if (health == 0)
			{
                Debug.Log("Game Over!");
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        if (other.gameObject.CompareTag("Goal"))
        {
            Debug.Log("You win!");
        }
    }
}
