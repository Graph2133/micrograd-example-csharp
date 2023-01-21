using System.Linq;
using Micrograd.Utilities;

namespace Micrograd.NN
{
    public class Neuron : Module
    {
        public Value[] Weights { get; private set; }

        public Value Bias { get; private set; }

        public Neuron(int inputSize)
        {
            Weights = new Value[inputSize];
            for (int i = 0; i < inputSize; i++)
            {
                Weights[i] = new Value(Randomizer.GetRandomValue());
            }

            Bias = new Value(Randomizer.GetRandomValue());
        }

        public Value Forward(Value[] x)
        {
            // Iterate over input values and multiply them by weights y = w1*x1 + w2*x2 + ... + wn*xn + b
            var activation = x.Zip(Weights, (input, weight) => input * weight).Aggregate((a, b) => a + b) + Bias;
            
            return activation.Tanh();
        }

        public override Value[] GetParameters() => Weights.Concat(new[] { Bias }).ToArray(); 
    }
}
