using Micrograd.NN;
using Spectre.Console;

namespace Micrograd.Console.Examples
{
    public partial class Examples
    {
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

            // Draw the loss function
            if (AnsiConsole.Confirm("Draw loss function ?"))
            {
                var plt = new ScottPlot.Plot(800, 600);
                plt.XLabel("Iteration");
                plt.YLabel("Loss");
                var xValues = Enumerable.Range(0, numberOfIteration).Select(v => (double)v).ToArray();
                plt.AddScatter(xValues, losses);

                var path = plt.SaveFig("loss.png");

                AnsiConsole.MarkupLine($"[green]Loss function saved to {path}[/]");
            }
        }
    }
}
