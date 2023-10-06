using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class SnakeHead : MonoBehaviour
{
    [SerializeField] private List<Transform> _segments = new List<Transform>(); //all Snake segments
    [SerializeField] private Transform segmentPrefab;

    [SerializeField] private float speed = 10f; 
    [SerializeField] private float maxSpeed = 100;
    [SerializeField] private float speedScalePersent = 1.10f; //speed = log(speed, speedScalePersent) 

    [SerializeField] private int startSize = 3;
    [SerializeField] private int growSize = 2; //Segments count added in Grow()

    [SerializeField] private Vector2 direction = Vector2.right;
    private float moveSpeedCounter;

    private bool startMove; //if == false, wait Input.anyKeyDown

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
        Vector2 directionInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); //wasd and arrows

        if ((directionInput.x == 0 || directionInput.y == 0) && //you cant move on 
           (directionInput.x != 0 || directionInput.y != 0) &&  //diagonal
           (directionInput != direction * -1)) //cant move into yor self
        {
            direction = directionInput;   
        }

        if (Input.anyKeyDown || startMove)
        {
            Move();

            startMove = true;
        }       
    }


    private void Move() 
    {
        moveSpeedCounter += speed;
        if (moveSpeedCounter >= maxSpeed) //mb rerite this
        {
            for (int i = _segments.Count - 1; i > 0; i--)
            {
                Vector3 position = _segments[i - 1].position; //last segment move on next one
                _segments[i].position = _segments[i - 1].position; //
            }

            rb.position += direction; //head move to direction
            moveSpeedCounter = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Food"))
        {
            ScoreSystem.Instance.AddScore();
            ScoreSystem.Instance.SpawnPopUp(transform.position);

            for (int i = 0; i < growSize; i++)
            {
                Grow();
            }
        
            speed = Mathf.Log(speed, speedScalePersent);

            Destroy(collision.gameObject);
        }

        else if (collision.CompareTag("Obstacle") && Time.timeSinceLevelLoad > 1)
        {                                               // selfEating protect (no detect this collisin first sec)
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
        YandexGame.NewLeaderboardScores("BestScoreLeaderBoard", ScoreSystem.Instance.GetBestScore());
        YandexGame.SaveProgress();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
