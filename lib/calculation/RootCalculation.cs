namespace seesharpulator.lib.calculation
{
    public class RootCalculation(decimal[] ops) : Calculation(ops)
    {
        public override decimal Calculate()
        {
            return Convert.ToDecimal(Math.Sqrt(decimal.ToDouble(operands[0])));
        }

        public override string Format()
        {
            return $"square root of {operands[0]} = {Calculate()}";
        }
    }
}