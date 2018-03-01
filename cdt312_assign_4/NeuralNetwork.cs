namespace Cdt312_assign_4
{
    //ANN made on assumptiton of a hidden layer.
    class NeuralNetwork
    {
        public Neuron[] Input;
        public Neuron[] Hidden;
        public Neuron[] Output;
        public double[,] weightMatrix;
        public double[] weightVector;
        public NeuralNetwork(int noInput, int noHidden, int noOutput)
        {
            Input = new Neuron[noInput];
            Hidden = new Neuron[noHidden];
            Output = new Neuron[noOutput];
        }
    }
}
