using Micrograd.Console.Examples;
using Spectre.Console;

var exampleNumber = AnsiConsole.Prompt(
    new SelectionPrompt<int>()
        .Title("Select example:")
        .UseConverter(value =>
        {
            return value switch
            {
                1 => "Calculation of the gradient for the 'y = x1w1 + x2w2 + b' function",
                2 => "Binary classification",
                3 => "MNIST dataset image recognition",
                _ => "Unknown"
            };
        })
        .AddChoices(new[] { 1, 2, 3 }));

Action runExample = exampleNumber switch
{
    1 => () => Examples.RunFunctionGradient(),
    2 => () => Examples.RunBinaryClassificationExample(),
    3 => () => Examples.RunImageRecognition(),
    _ => () => AnsiConsole.MarkupLine("Invalid choice")
};

runExample();