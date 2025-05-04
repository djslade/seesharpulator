namespace seesharpulator.lib.calculation
{
    public class PowCalculation(decimal[] ops) : Calculation(ops)
    {
        private double GetExponent()
        {
            double pow = operands.Length > 1 ? decimal.ToDouble(operands[1]) : 1;
            return pow;
        }
        public override decimal Calculate()
        {
            var pow = GetExponent();
            return Convert.ToDecimal(Math.Pow(decimal.ToDouble(operands[0]), pow));
        }

        public override string Format()
        {
            var pow = GetExponent();
            return $"{operands[0]} pow {pow} = {Calculate()}";
        }
    }
}