using Micrograd.Console.Extensions;
using Micrograd.Network;
using Spectre.Console;

namespace Micrograd.Console
{
    public static class Test
    {
        /// <summary>
        /// Calculation of the gradient in the 'y = x1w1 + x2w2 + b' expression
        /// </summary>
        public static void RunSimpleExpression()
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

        /// <summary>
        /// Binary classification example.
        /// </summary>
        public static void RunBinaryClassificationExample()
        {
            // Define learning parameters : 3 inputs, 2 hidden layers with 4 neurons and single output
            var mlp = new MLP(3, new[] { 4, 4, 1 });
            
            // Create training dataset
            var matrix = new Value[4][]
            {
                new Value[3] { 2.0, 3.0, -1.0 },
                new Value[3] { 3.0, -1.0, 0.5 },
                new Value[3] { 0.5, 1.0, 1.0 },
                new Value[3] { 1.0, 1.0, -1.0 }
            };

            // Desired targets
            var ys = new double[4] { 1.0, -1.0, -1.0, 1.0 };

            var numberOfIteration = AnsiConsole.Ask<int>("[yellow]Number of iterations ?[/]");
            var losses = new double[numberOfIteration];
            
            // Train the network anc calculate the loss function
            for (int it = 0; it < numberOfIteration; it++)
            {
                var loss = new Value(0.0);
                for (int j = 0; j < ys.Length; j++)
                {
                    var pred = mlp.Forward(matrix[j])[0];
                    loss += (pred - ys[j]).Pow(2);
                }

                // Reset gradient
                mlp.ZeroGrad();

                // Backpropagation
                loss.Backward();

                // Update the parameters (weights and biases)
                foreach (var p in mlp.GetParameters())
                {
                    p.Data -= 0.01 * p.Gradient;
                }

                losses[it] = loss.Data;

                AnsiConsole.WriteLine($"Loss: {loss.Data}");
            }

            // Draw the loss (mean squared error)
            var drawLossFunction = AnsiConsole.Prompt(
                new TextPrompt<string>("[grey][[Optional]][/] [yellow] Draw loss function (y/n) [/]?")
                    .AllowEmpty());


            if (drawLossFunction?.Equals("y", StringComparison.OrdinalIgnoreCase) == true)
            {
                var plt = new ScottPlot.Plot(800, 600);
                var xValues = Enumerable.Range(0, numberOfIteration).Select(v => (double)v).ToArray();
                plt.AddScatter(xValues, losses);

                plt.SaveFig("loss.png");
            }
        }
    }
}
