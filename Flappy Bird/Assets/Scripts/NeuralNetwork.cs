using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworks
{
    public class NeuralNetwork
    {
        public static Random rand;

        private double[,] inputLayer;
        private double[] hiddenLayer;

        private int fitness;

        // Creates a new Neural Network object with randomazied weights
        public NeuralNetwork()
        {
            fitness = 0;
            NeuralNetwork.rand = new Random();
            this.inputLayer = new double[4,3];
            this.hiddenLayer = new double[3];
            for (int i = 0; i < this.inputLayer.GetLength(0); i++)
            {
                for (int n = 0; n < this.inputLayer.GetLength(1); n++)
                {
                    this.inputLayer[i, n] = NeuralNetwork.rand.NextDouble();
                }
            }
            for (int i = 0; i < this.hiddenLayer.Length; i++)
            {
                this.hiddenLayer[i] = NeuralNetwork.rand.NextDouble();
            }
        }

        // calculates the output of the Neural Network given the inputs
        private double SumLayers(double[] input)
        {
            double[] hiddenLayerSums = new double[this.hiddenLayer.Length];
            for(int i = 0; i < hiddenLayerSums.Length; i++)
            {
                hiddenLayerSums[i] = 0;
                for (int n = 0; n < this.inputLayer.GetLength(0); n++)
                {
                    hiddenLayerSums[i] += this.inputLayer[n, i] * input[n];
                }
                hiddenLayerSums[i] = Sigmoid(hiddenLayerSums[i]);
            }
            double outputSum = 0;
            for (int i = 0; i < this.hiddenLayer.Length; i++)
            {
                outputSum += this.hiddenLayer[i] * hiddenLayerSums[i];
            }
            return Sigmoid(outputSum);
        }
        
        private double Sigmoid(double raw)
        {
            return 1 / (1 + Math.Pow(Math.E, -raw));
        }

        public bool CalculateNextMove(double[] input)
        {
            double output = SumLayers(input);
            return output > 0.5;
        }

        public NeuralNetwork Clone()
        {
            fitness = 0;
            NeuralNetwork clone = new NeuralNetwork();
            for (int i = 0; i < clone.inputLayer.GetLength(0); i++)
            {
                for (int n = 0; n < clone.inputLayer.GetLength(1); n++)
                {
                    clone.inputLayer[i, n] = this.inputLayer[i, n];
                }
            }
            for (int i = 0; i < clone.hiddenLayer.Length; i++)
            {
                clone.hiddenLayer[i] = this.hiddenLayer[i];
            }
            return clone;
        }

        // Has a given probability of mutating each connection in the Nerual Network
        public void Mutate(int chance)
        {
            Random rand = new Random();
            for (int i = 0; i < this.inputLayer.GetLength(0); i++)
            {
                for (int n = 0; n < this.inputLayer.GetLength(1); n++)
                {
                    if (NeuralNetwork.rand.Next(100) < chance)
                    {
                        this.inputLayer[i, n] = NeuralNetwork.rand.NextDouble();
                    }
                }
            }
            for (int i = 0; i < this.hiddenLayer.Length; i++)
            {
                if (NeuralNetwork.rand.Next(100) < chance)
                {
                    this.hiddenLayer[i] = NeuralNetwork.rand.NextDouble();
                }
            }
        }

        // Returns the fitness value
        public int GetFitness()
        {
            return this.fitness;
        }

        // Sets the fitness value
        public void SetFitness(int fitness)
        {
            this.fitness = fitness;
        }
    }
}
