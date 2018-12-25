using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject pipe;

    [SerializeField]
    private float speed;

    [SerializeField]
    private GameObject pipeManager;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("PlacePipe", 5);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // places the pipes
    private void PlacePipe()
    {
        // max height = 4.496
        // min height = 0.77
        float y = Random.Range((float)0.77, (float)4.496);
        GameObject newPipe = Instantiate(pipe, new Vector3((float)3.25, y, 0), Quaternion.identity, pipeManager.transform);
        newPipe.name = pipe.name;

        Invoke("PlacePipe", speed * 40);
    }
}
