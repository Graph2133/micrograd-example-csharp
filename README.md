# micrograd-example-csharp
A port of [karpathy/micrograd](https://github.com/karpathy/micrograd) from Python to C#. All the credits go to [Andrej Karpathy](https://github.com/karpathy).
The project itself is a tiny scalar-valued autograd engine and basic neural network implementation on top of it.

# Examples
The examples folder contains a few examples of how to use the library.
The examples are:
* `Function gradient` - calculation of the gradient for the simple function.
* `Binary classification` - binary classification example using a neural network.
* `Image recognition` - recognition of handwritten digits from the MNIST dataset using MLP.

### Micrograd.Console (example project) dependencies
* [ScottPlot](https://github.com/ScottPlot/ScottPlot)
* [Spectre.Console](https://github.com/spectreconsole/spectre.console)