using Spectre.Console;
using System.Text;

namespace Micrograd.Console.Extensions
{
    /// <summary>
    /// The <see cref="Value"/> extensions.
    /// </summary>
    public static class ValueExtensions
    {
        /// <summary>
        /// Prints tree view of the Value.
        /// </summary>
        /// <param name="value">The input node.</param>
        public static void PrintAsTree(this Value value)
        {
            var root = new Tree("Tree View:");

            // Create root node because tree is not inherited from Node type ^_^
            var rootNode = root.AddNode(GetFormattedTreeLabel(value));

            PopulateTree(rootNode, value);

            AnsiConsole.Write(root);

            static void PopulateTree(TreeNode node, Value value)
            {
                var nextNode = string.IsNullOrEmpty(value.Operation) ? node : node.AddNode($"[red]({value.Operation})[/]");
                foreach (var child in value.Children)
                {
                    var formattedValue = GetFormattedTreeLabel(child);
                    var childNode = nextNode.AddNode(formattedValue);
                    PopulateTree(childNode, child);
                }
            }
        }

        /// <summary>
        /// Gets the formatted label containing value, gradient and label.
        /// </summary>
        /// <param name="value">The input node.</param>
        /// <returns>The formatted label.</returns>
        private static string GetFormattedTreeLabel(Value value)
        {
            var sb = new StringBuilder($"{value} || [yellow]grad={value.Gradient:0.#######}[/]");
            if (value.Label != null)
            {
                sb.Append($" || [green]{value.Label}[/]");
            }

            return sb.ToString();
        }
    }
}
