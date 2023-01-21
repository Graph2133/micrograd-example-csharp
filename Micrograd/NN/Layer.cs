using System.Linq;

namespace Micrograd.NN
{
    public class Layer : Module
    {
        public Neuron[] Neurons { get; private set; }

        public Layer(int inputSize, int neuronCount)
        {
            Neurons = new Neuron[neuronCount];
            for (int i = 0; i < neuronCount; i++)
            {
                Neurons[i] = new Neuron(inputSize);
            }
        }

        public Value[] Forward(Value[] x)
        {
            var result = new Value[Neurons.Length];
            for (int i = 0; i < Neurons.Length; i++)
            {
                result[i] = Neurons[i].Forward(x);
            }

            return result;
        }

        public override Value[] GetParameters() => Neurons.SelectMany(n => n.GetParameters()).ToArray();
    }
}
