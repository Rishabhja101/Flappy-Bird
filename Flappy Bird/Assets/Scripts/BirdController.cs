using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BirdState { Alive, Dead}

public class BirdController : MonoBehaviour
{
    [SerializeField]
    private float thrust;

    [SerializeField]
    private float fallSpeed;

    [SerializeField]
    private float acceleration;

    private float velocity;

    private BirdState state;

    // Start is called before the first frame update
    void Start()
    {
        velocity = -fallSpeed;
        state = BirdState.Alive;
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Pipe")
        {
            state = BirdState.Dead;
        }
    }
}
