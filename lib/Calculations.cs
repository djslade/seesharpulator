namespace seesharpulator.lib
{
    public abstract class Calculation(decimal[] ops)
    {
        protected readonly decimal[] operands = ops;

        public abstract decimal Calculate();

        public abstract string Format();
    }

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

    public class SubCalculation(decimal[] ops) : Calculation(ops)
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
                total -= operands[i];
            }
            return total;
        }

        public override string Format()
        {
            string output = string.Join(" - ", operands);
            output += $" = {Calculate()}";
            return output;
        }
    }

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

