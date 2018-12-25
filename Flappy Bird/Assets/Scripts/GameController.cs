using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject Pipe;

    [SerializeField]
    private float speed;

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
        print("sadfasd");
        // max height = 4.496
        // min height = 0.77
        float y = Random.Range((float)0.77, (float)4.496);
        GameObject newPipe = Instantiate(Pipe, new Vector3((float)3.25, y, 0), Quaternion.identity);

        Invoke("PlacePipe", speed * 40);
    }
}
