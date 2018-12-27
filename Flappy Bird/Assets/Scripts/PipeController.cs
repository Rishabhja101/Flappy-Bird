using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeController : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private bool isActive = true;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            transform.position = new Vector3(transform.position.x - speed, transform.position.y, 0);
        }
        if (transform.position.x <= -3.7)
        {
            Destroy(gameObject);
        }
    }

    // Sets the pipe movement to true or false
    public void SetIsActive(bool move)
    {
        isActive = move;
    }
}
