namespace seesharpulator.lib.calculation
{
    public class MultCalculation(decimal[] ops) : Calculation(ops)
    {
        public override decimal Calculate()
        {
            decimal total = 1;
            foreach (decimal i in operands)
            {
                total *= i;
            }
            return total;
        }

        public override string Format()
        {
            string output = string.Join(" * ", operands);
            output += $" = {Calculate()}";
            return output;
        }
    }
}