namespace seesharpulator.lib.calculation
{
    public class DivCalculation(decimal[] ops) : Calculation(ops)
    {
        public override decimal Calculate()
        {
            decimal total = 0;
            for (int i = 0; i < operands.Length; i++)
            {
                if (i == 0)
                {
                    total = operands[i];
                    continue;
                }
                total /= operands[i];
            }
            return total;
        }

        public override string Format()
        {
            string output = string.Join(" / ", operands);
            output += $" = {Calculate()}";
            return output;
        }
    }
}