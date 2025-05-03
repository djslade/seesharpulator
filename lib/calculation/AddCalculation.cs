namespace seesharpulator.lib.calculation
{
    public class AddCalculation(decimal[] ops) : Calculation(ops)
    {
        public override decimal Calculate()
        {
            decimal total = 0;
            foreach (decimal i in operands)
            {
                total += i;
            }
            return total;
        }

        public override string Format()
        {
            string output = string.Join(" + ", operands);
            output += $" = {Calculate()}";
            return output;
        }
    }
}