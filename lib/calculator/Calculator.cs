using seesharpulator.lib.calculation;

namespace seesharpulator.lib.calculator
{
    public class Calculator
    {
        private readonly string[] operations = ["+", "-", "*", "/"];
        private List<Calculation> history = [];
        private bool running = true;

        public void ShowOptions()
        {
            Console.WriteLine("'calculate - run a calculation");
            Console.WriteLine("'history' - show calculation history");
            Console.WriteLine("'quit' - exit the amazing calculator");
        }

        public void Calculate()
        {
            Console.WriteLine($"Enter an operation, choices are {string.Join(", ", operations)}");
            var op = Console.ReadLine();
            if (!operations.Contains(op))
            {
                Console.WriteLine($"{op} is not a valid operation, calculation cancelled");
                return;
            }
            Console.WriteLine("Enter your operands, separated by a single space");
            var input = Console.ReadLine();
            if (input == null)
            {
                Console.WriteLine("Input was null, calculation cancelled");
                return;
            }
            string[] stringOperands = input.Split(" ");
            Console.WriteLine(stringOperands.Length);
            decimal[] operands = new decimal[stringOperands.Length];
            Console.WriteLine(operands.Length);
            for (int i = 0; i < stringOperands.Length; i++)
            {
                if (!decimal.TryParse(stringOperands[i], out operands[i]))
                {
                    Console.WriteLine($"{stringOperands[i]} is not a valid operand, calculation cancelled");
                    return;
                }
            }
            Calculation calculation;
            switch (op)
            {
                case "+":
                    calculation = new AddCalculation(operands);
                    break;
                case "-":
                    calculation = new SubCalculation(operands);
                    break;
                case "*":
                    calculation = new MultCalculation(operands);
                    break;
                case "/":
                    calculation = new DivCalculation(operands);
                    break;
                default:
                    Console.WriteLine("This shouldn't be possible, calculation cancelled");
                    return;
            }
            Console.WriteLine(calculation.Format());
            history.Add(calculation);
        }

        public void Start()
        {
            Console.WriteLine("Welcome to the famous calculator");
            Console.WriteLine("Type 'options' for a list of commands");
            while (running)
            {
                var input = Console.ReadLine();
                switch (input)
                {
                    case "calculate":
                        Calculate();
                        break;
                    default:
                        Console.WriteLine("Type 'options' for a list of commands");
                        break;
                }
            }
        }
    }
}