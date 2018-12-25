using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NeuralNetworks;

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

    [SerializeField]
    private GameObject pipeManager;

    private double velocity;
    private double formattedVelocity;
    private double distanceToPipe;
    private double distanceToTop;
    private double distanceToBottom;

    private BirdState state;
    private int points;

    private NeuralNetwork neuralNetwork;
    
    // Start is called before the first frame update
    private void Start()
    {
        velocity = -fallSpeed;
        state = BirdState.Alive;
        points = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        GetNeuralNetworkInputs();
        FormatNerualNetworkInputs();
        if (state == BirdState.Alive)
        {
            //if (Input.anyKeyDown)     // commented out to restrit user from controlling bird
            double[] inputs = new double[] { formattedVelocity, distanceToPipe, distanceToTop, distanceToBottom};
            if (neuralNetwork.CalculateNextMove(inputs))
            {
                velocity += thrust;
                if (velocity > fallSpeed)
                {
                    velocity = fallSpeed;
                }
            }
            gameObject.transform.position = new Vector3(0, transform.position.y + (float)velocity, 0);
            if (velocity >= -fallSpeed)
            {
                velocity -= acceleration;
            }
            if (velocity < -fallSpeed)
            {
                velocity = -fallSpeed;
            }
        }
        else if(state == BirdState.Dead && transform.position.x > -3.7)
        {
            transform.position = new Vector3(transform.position.x - speed, transform.position.y, 0);
        }
    }
    
    // Called when a collider is triggered
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Pipe")
        {
            state = BirdState.Dead;
        }
    }

    // Called when a collider has stopped being triggered
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Goal" && state == BirdState.Alive)
        {
            points++;
            pointsDisplay.text = points.ToString();
        }
    }

    // Gets the Neural Network Inputs for the bird
    private void GetNeuralNetworkInputs()
    {
        Transform[] pipes = pipeManager.GetComponentsInChildren<Transform>();
        Transform closest = pipes[0];
        for (int i = 0; i < pipes.Length; i++)
        {
            if (pipes[i].position.x > transform.position.x && pipes[i].position.x - transform.position.x < closest.position.x - transform.position.x)
            {
                closest = pipes[i];
            }
        }
        try
        {
            distanceToPipe = closest.position.x - transform.position.x;
            distanceToTop = GameObject.Find("/PipeManager/Pipe/Pipe_Top/Top").transform.position.y - transform.position.y;
            distanceToBottom = GameObject.Find("/PipeManager/Pipe/Pipe_Bottom/Bottom").transform.position.y - transform.position.y;
        }
        catch
        {

        }
    }

    // Formats the Neural Network Inputs
    private void FormatNerualNetworkInputs()
    {
        // min velocity = -fallspeed
        // max velocity = fallspeed
        formattedVelocity = (velocity - -fallSpeed) / (2 * fallSpeed);
        // min distance to pipe = 0
        // max distance to pipe = 3.25
        distanceToPipe = distanceToPipe / 3.25;
        // min distance to top = -0.246 - 5
        // max distance to top = 3.48 + 2.75
        distanceToTop = (distanceToTop - (-0.246 - 5)) / ((3.48 + 2.75) - (-0.246 - 5));
        // min distance to bottom = -1.905 - 5
        // max distance to bottom = 1.822 + 2.75
        distanceToBottom = (distanceToBottom - (-1.905 - 5)) / ((1.822 + 2.75) - (-1.905 - 5));
    }
}
