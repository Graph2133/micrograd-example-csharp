using System.Linq;

namespace Micrograd.NN
{
    public class MLP : Module
    {
        public Layer[] Layers { get; private set; }

        public MLP(int inputSize, int[] layerSizes)
        {
            Layers = new Layer[layerSizes.Length];
            for (int i = 0; i < layerSizes.Length; i++)
            {
                Layers[i] = new Layer(i == 0 ? inputSize : layerSizes[i - 1], layerSizes[i]);
            }
        }

        public Value[] Forward(Value[] x)
        {
            var result = x;
            for (int i = 0; i < Layers.Length; i++)
            {
                result = Layers[i].Forward(result);
            }

            return result;
        }

        public override Value[] GetParameters() => Layers.SelectMany(l => l.GetParameters()).ToArray();
    }
}
