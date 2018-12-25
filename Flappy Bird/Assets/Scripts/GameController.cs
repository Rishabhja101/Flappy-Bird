using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject Pipe;

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
        GameObject newPipe = Instantiate(Pipe, transform.position, transform.rotation);

        Invoke("PlacePipe", 5);
    }
}
