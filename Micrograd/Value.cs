using System;
using System.Collections.Generic;
using System.Linq;

namespace Micrograd
{
    public class Value : IComparable<Value>
    {
        private Action _backward;

        public Value(double data, IEnumerable<Value> children = null, string op = "")
        {
            this.Data = data;
            this.Operation = op;
            this.Children = children ?? Enumerable.Empty<Value>();
            this.Gradient = 0;
        }

        public double Data { get; set; }

        public string Label { get; set; }

        public double Gradient { get; private set; }

        public string Operation { get; private set; }

        public IEnumerable<Value> Children { get; private set; }

        public static implicit operator Value(int other) => new Value(other);
        public static implicit operator Value(double other) => new Value(other);
        public static implicit operator Value(float other) => new Value(other);

        public static Value operator +(Value left, Value right)
        {
            var result = new Value(left.Data + right.Data, new[] { left, right }, "+");
            result._backward = () =>
            {
                left.Gradient += result.Gradient;
                right.Gradient += result.Gradient;
            };

            return result;
        }

        public static Value operator *(Value left, Value right)
        {
            var result = new Value(left.Data * right.Data, new[] { left, right }, "*");
            result._backward = () =>
            {
                left.Gradient += right.Data * result.Gradient;
                right.Gradient += left.Data * result.Gradient;
            };

            return result;
        }

        public static Value operator -(Value val) => val * (-1);

        public static Value operator -(Value left, Value right) => left + (-right);

        public static Value operator /(Value left, Value right) => left * right.Pow(-1);

        public Value Exp()
        {
            var result = new Value(Math.Exp(this.Data), new[] { this }, "exp");
            result._backward = () =>
            {
                this.Gradient += result.Data * result.Gradient;
            };

            return result;
        }

        public Value Pow(int power)
        {
            var result = new Value(Math.Pow(this.Data, power), new[] { this }, $"pow({power})");
            result._backward = () =>
            {
                this.Gradient += power * Math.Pow(this.Data, power - 1) * result.Gradient;
            };

            return result;
        }

        public Value Tanh()
        {
            var result = new Value(Math.Tanh(this.Data), new[] { this }, "tanh");
            result._backward = () =>
            {
                this.Gradient += (1 - Math.Pow(result.Data, 2)) * result.Gradient;
            };

            return result;
        }

        public Value RelU()
        {
            var result = new Value(Math.Max(0, this.Data), new[] { this }, "relu");
            result._backward = () =>
            {
                this.Gradient += (this.Data > 0 ? 1 : 0) * result.Gradient;
            };

            return result;
        }


        public void Backward()
        {
            var topology = new List<Value>();
            var visited = new HashSet<Value>();

            BuildTopology(this);

            this.Gradient = 1;

            foreach (var val in Enumerable.Reverse(topology))
            {
                val._backward?.Invoke();
            }

            void BuildTopology(Value value)
            {
                if (visited.Contains(value))
                {
                    return;
                }

                visited.Add(value);
                foreach (var child in value.Children)
                {
                    BuildTopology(child);
                }

                topology.Add(value);
            }
        }

        public void ZeroGrad()
        {
            this.Gradient = 0;
        }

        public override string ToString()
        {
            return $"value={this.Data}";
        }
        public int CompareTo(Value other) => this.Data.CompareTo(other.Data);
    }
}
