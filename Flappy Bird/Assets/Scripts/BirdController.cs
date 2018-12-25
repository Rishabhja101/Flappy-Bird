using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    [SerializeField]
    private float thrust;

    [SerializeField]
    private float fallSpeed;

    [SerializeField]
    private float acceleration;

    private float velocity;

    // Start is called before the first frame update
    void Start()
    {
        velocity = -fallSpeed;
    }

    // Update is called once per frame
    void Update()
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
