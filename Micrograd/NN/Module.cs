namespace Micrograd.NN
{
    public abstract class Module
    {
        public abstract Value[] GetParameters();

        public void ZeroGrad()
        {
            foreach (var p in GetParameters())
            {
                p.ZeroGrad();
            }
        }
    }
}
