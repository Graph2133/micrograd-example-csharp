using Micrograd.Console.Examples;
using Spectre.Console;

var example = AnsiConsole.Ask<int>(
    "[yellow]Which example do you want to run ?" +
        "\n 1 - Calculation of the gradient for the 'y = x1w1 + x2w2 + b' expression " +
        "\n 2 - Binary classification " +
        "\n 3 - MNIST dataset image recognition [/] \n");

Action runExample = example switch
{
    1 => () => Examples.RunSimpleExpression(),
    2 => () => Examples.RunBinaryClassificationExample(),
    3 => () => Examples.RunImageRecognition(),
    _ => () => AnsiConsole.MarkupLine("[red]Invalid choice[/]")
};

runExample();