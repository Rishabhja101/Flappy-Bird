using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NeuralNetworks;

public class BirdPopulationController : MonoBehaviour
{
    [SerializeField]
    private GameObject bird;
    
    private GameObject gameController;

    [SerializeField]
    private Text generationDisplay;

    private NeuralNetwork[] neuralNetworks;
    private GameObject[] birds;

    private int generation;

    [SerializeField]
    private int populationSize = 100;

    [SerializeField]
    private int retained = 5;

    // Start is called before the first frame update
    private void Start()
    {
        gameController = GameObject.Find("/GameController");
        generation = 0;
        neuralNetworks = new NeuralNetwork[populationSize];
        birds = new GameObject[populationSize];
        NeuralNetwork.rand = new System.Random();
        for (int i = 0; i < neuralNetworks.Length; i++)
        {
            neuralNetworks[i] = new NeuralNetwork();
            neuralNetworks[i].Mutate(100);
        }
        for (int i = 0; i < birds.Length; i++)
        {
            birds[i] = Instantiate(bird, transform.position, Quaternion.identity, transform);
            birds[i].GetComponent<BirdController>().AssignNeuralNetwork(neuralNetworks[i]);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (AllDead())
        {
            generation++;
            GenerateNewGeneration();
            gameController = GameObject.Find("/GameController");
            gameController.GetComponent<GameController>().Reset();
            generationDisplay.text = generation.ToString();
        }
    }

    // generates the next generation
    private void GenerateNewGeneration()
    {
        int[] fitnesses = new int[neuralNetworks.Length];
        for (int i = 0; i < neuralNetworks.Length; i++)
        {
            fitnesses[i] = neuralNetworks[i].GetFitness();
        }
        fitnesses = SortFitnesses(fitnesses);
        BreedNeuralNetworks(fitnesses);
        for (int i = 0; i < birds.Length; i++)
        {
            Destroy(birds[i]);
            birds[i] = Instantiate(bird, transform.position, Quaternion.identity, transform);
            birds[i].GetComponent<BirdController>().AssignNeuralNetwork(neuralNetworks[i]);
        }
    } 

    // breeds the next generation of neural networks
    private void BreedNeuralNetworks(int[] fitnesses)
    {
        NeuralNetwork[] strongNerualNetworks = new NeuralNetwork[neuralNetworks.Length / retained];
        int n = 0;
        for (int i = 0; i < fitnesses.Length; i++)
        {
            if (neuralNetworks[i].GetFitness() >= fitnesses[fitnesses.Length / retained])
            {
                if (n < strongNerualNetworks.Length)
                strongNerualNetworks[n] = neuralNetworks[i].Clone();
                n++;
            }
        }
        List<NeuralNetwork> newNetworks = new List<NeuralNetwork>();
        foreach(NeuralNetwork net in neuralNetworks)
        {
            if(net.GetFitness() == fitnesses[0])
            {
                newNetworks.Add(net.Clone());
            }
        }
        for (int i = 0; i < strongNerualNetworks.Length; i++)
        {
            newNetworks.Add(strongNerualNetworks[i].Clone());
        }
        while (newNetworks.Count < neuralNetworks.Length)
        {
            newNetworks.Add(strongNerualNetworks[NeuralNetwork.rand.Next(0, strongNerualNetworks.Length)].Clone());
        }
        for (int i = 0; i < neuralNetworks.Length; i++)
        {
            neuralNetworks[i] = newNetworks[i].Clone();
        }
        for (int  i = 1; i < neuralNetworks.Length; i++)
        {
            neuralNetworks[i].Mutate(5);
        }
    }

    // Returns the given array sorted from greatest to smallest
    private int[] SortFitnesses(int[] fitnesses)
    {
        int[] newFitnesses = new int[fitnesses.Length];
        List<int> used = new List<int>();
        for (int i = 0; i < fitnesses.Length; i++)
        {
            int max = 0;
            int curr = 0;
            for (int n = 0; n < fitnesses.Length; n++)
            {
                if (!used.Contains(n) && fitnesses[n] > max)
                {
                    max = fitnesses[n];
                    curr = n;
                }
            }
            used.Add(curr);
            newFitnesses[i] = fitnesses[curr];
        }
        return newFitnesses;
    }

    // Returns if all the birds are dead
    private bool AllDead()
    {
        foreach (GameObject b in birds)
        {
            if (b.GetComponent<BirdController>().IsAlive())
            {
                return false;
            }
        }
        return true;
    }
}
