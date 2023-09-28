using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnakeHead : MonoBehaviour
{
    [SerializeField] private List<Transform> _segments = new List<Transform>();
    [SerializeField] private Transform segmentPrefab;

    [SerializeField] private float speed = 10f;
    [SerializeField] private float maxSpeed = 100;
    [SerializeField] private float speedScalePersent = 1.10f; 

    [SerializeField] private int startSize = 3;

    [SerializeField] private Vector2 direction = Vector2.right;
    private float moveSpeedCounter;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        _segments.Add(transform); //add head to _segments
        for (int i = 0; i < startSize; i++)
        {
            Grow();
        }  
    }

    private void FixedUpdate()
    {
        Vector2 directionInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (directionInput == direction * -1)// selfEating protect
        {
            Move();
        }
        else if (directionInput.x == 0 || directionInput.y == 0) // code stile normal?
        {
            if (directionInput.x != 0 || directionInput.y != 0)
            {
                direction = directionInput;
                Move();
            }
        }
    }

    private void Move()
    {
        moveSpeedCounter += speed;
        if (moveSpeedCounter >= maxSpeed)
        {
            for (int i = _segments.Count - 1; i > 0; i--)
            {
                Vector3 position = _segments[i - 1].position;
                _segments[i].position = _segments[i - 1].position;
            }

            rb.position += direction;
            moveSpeedCounter = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Food"))
        {
            Grow();
            speed *= speedScalePersent;
            Destroy(collision.gameObject);
        }

        else if (collision.CompareTag("Obstacle") && Time.timeSinceLevelLoad > 1)
        {                                               // selfEating protect
            Die();
        }
    }

    private void Grow()
    {
        Transform segment = Instantiate(segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment);
    }

    private void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
