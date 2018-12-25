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

    private bool reset;

    // Start is called before the first frame update
    void Start()
    {
        reset = false;
        PlacePipe();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Resets the pipes
    public void Reset()
    {
        float y = Random.Range((float)0.77, (float)4.496);
        GameObject newPipe = Instantiate(pipe, new Vector3((float)3.25, y, 0), Quaternion.identity, pipeManager.transform);
        newPipe.name = pipe.name + "reset";

        Transform[] pipes = pipeManager.GetComponentsInChildren<Transform>();
        for (int i = 0; i < pipes.Length; i++)
        {
            if (pipes[i].name == pipe.name)   
            {
                Destroy(pipes[i].gameObject);
            }
        }
        newPipe.name = pipe.name;
        reset = true;


        //Invoke("PlacePipe", speed * 40);
    }

    // places the pipes
    private void PlacePipe()
    {
        // max height = 4.496
        // min height = 0.77
        float y = Random.Range((float)0.77, (float)4.496);
        GameObject newPipe = Instantiate(pipe, new Vector3((float)3.25, y, 0), Quaternion.identity, pipeManager.transform);
        newPipe.name = pipe.name;

        print("reset: " + reset.ToString());
        if (!reset)
        {
            Invoke("PlacePipe", speed * 40);
        }
        reset = false;
    }
}
