using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BirdState { Alive, Dead}

public class BirdController : MonoBehaviour
{
    [SerializeField]
    private float thrust;

    [SerializeField]
    private float fallSpeed;

    [SerializeField]
    private float acceleration;

    [SerializeField]
    private float speed;

    [SerializeField]
    private Text pointsDisplay;

    private float velocity;

    private BirdState state;

    private int points;

    // Start is called before the first frame update
    void Start()
    {
        velocity = -fallSpeed;
        state = BirdState.Alive;
        points = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == BirdState.Alive)
        {
            if (Input.anyKeyDown)
            {
                velocity += thrust;
                if (velocity > fallSpeed)
                {
                    velocity = fallSpeed;
                }
            }
            gameObject.transform.position = new Vector3(0, transform.position.y + velocity, 0);
            if (velocity >= -fallSpeed)
            {
                velocity -= acceleration;
            }
            else
            {
                velocity = -fallSpeed;
            }
        }
        else if(state == BirdState.Dead && transform.position.x > -3.7)
        {
            transform.position = new Vector3(transform.position.x - speed, transform.position.y, 0);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Pipe")
        {
            state = BirdState.Dead;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Goal" && state == BirdState.Alive)
        {
            points++;
            pointsDisplay.text = points.ToString();
        }
    }
}
