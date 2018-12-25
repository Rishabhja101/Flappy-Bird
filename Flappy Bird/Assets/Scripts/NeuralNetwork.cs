using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworks
{
    class NeuralNetwork
    {
        private double[,] inputLayer;
        private double[] hiddenLayer;
        private double outputLayer;

        public NeuralNetwork()
        {
            
        }

        // calculates the output of the Neural Network given the inputs
        public double SumLayers(double[] input)
        {
            double[] hiddenLayerSums = new double[hiddenLayer.Length];
            for(int i = 0; i < hiddenLayerSums.Length; i++)
            {
                hiddenLayerSums[i] = 0;
                for (int n = 0; n < inputLayer.GetLength(0); n++)
                {
                    hiddenLayerSums[i] += inputLayer[n, i] * input[n];
                }
                hiddenLayerSums[i] = Sigmoid(hiddenLayerSums[i]);
            }
            double outputSum = 0;
            for (int i = 0; i < hiddenLayer.Length; i++)
            {
                outputSum += hiddenLayer[i] * hiddenLayerSums[i];
            }
            return Sigmoid(outputSum);
        }

        // returns a value between 0 and 1 for the given input using a sigmoid function
        public double Sigmoid(double raw)
        {
            return 1 / (1 + Math.Pow(Math.E, -raw));
        }

        // determines and returns if the bird should jump
        public bool CalculateNextMove(double[] input)
        {
            double output = SumLayers(input);
            return output > 0.5;
        }
    }
}
