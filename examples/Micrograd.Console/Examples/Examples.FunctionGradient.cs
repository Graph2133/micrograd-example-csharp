using Micrograd.Console.Extensions;

namespace Micrograd.Console.Examples
{
    public partial class Examples
    {
        /// <summary>
        /// Calculation of the gradient for the 'y = x1w1 + x2w2 + b' expression.
        /// </summary>
        public static void FunctionGradient()
        {
            var x1 = new Value(2.0) { Label = "x1" };
            var x2 = new Value(0.0) { Label = "x2" };
            var w1 = new Value(-3.0) { Label = "w1" };
            var w2 = new Value(1.0) { Label = "w2" };

            var b = new Value(6.88137358) { Label = "b" };


            var x1w1 = x1 * w1; x1w1.Label = "x1w1";
            var x2w2 = x2 * w2; x2w2.Label = "x2w2";

            var x1w1x2w2 = x1w1 + x2w2; x1w1x2w2.Label = "x1w1x2w2";
            var n = x1w1x2w2 + b; n.Label = "n";
            var o = n.Tanh(); o.Label = "o";

            o.Backward();

            o.PrintAsTree();
        }
    }
}
