namespace Cdt312_assign_4
{
    class Neuron
    {
        public double Value;
        public bool Hidden;
        public Neuron[] links;
        public Neuron(double newValue, bool setHidden)
        {
            Value = newValue;
            Hidden = setHidden;
        }
    }
}
