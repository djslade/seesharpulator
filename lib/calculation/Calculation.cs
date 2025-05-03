namespace seesharpulator.lib.calculation
{
    public abstract class Calculation(decimal[] ops)
    {
        protected readonly decimal[] operands = ops;

        public abstract decimal Calculate();

        public abstract string Format();
    }
}