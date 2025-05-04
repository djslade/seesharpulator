using seesharpulator.lib.calculation;

namespace seesharpulator.lib.calculator
{
    public class Calculator
    {
        private readonly string[] operations = ["+", "-", "*", "/"];
        private readonly List<Calculation> history = [];
        private bool running = true;

        private static void ShowOptions()
        {
            Console.WriteLine("'calculate - run a calculation");
            Console.WriteLine("'history' - show calculation history");
            Console.WriteLine("'reset' - clear calculation history");
            Console.WriteLine("'quit' - exit the amazing calculator");
        }

        private string GetOpFromUser()
        {
            Console.WriteLine($"Enter an operation, choices are {string.Join(", ", operations)}");
            var op = Console.ReadLine() ?? throw new NullInputException();
            if (!operations.Contains(op)) throw new InvalidInputException(op);
            return op;
        }

        private static decimal[] GetOperandsFromUser()
        {
            Console.WriteLine("Enter your operands, separated by a single space");
            var input = Console.ReadLine() ?? throw new NullInputException();
            string[] stringOperands = input.Split(" ");
            Console.WriteLine(stringOperands.Length);
            decimal[] operands = new decimal[stringOperands.Length];
            Console.WriteLine(operands.Length);
            for (int i = 0; i < stringOperands.Length; i++)
            {
                if (!decimal.TryParse(stringOperands[i], out operands[i]))
                {
                    throw new InvalidInputException(stringOperands[i]);
                }
            }
            return operands;
        }

        private static Calculation NewCalculation(string op, decimal[] operands)
        {
            return op switch
            {
                "+" => new AddCalculation(operands),
                "-" => new SubCalculation(operands),
                "*" => new MultCalculation(operands),
                "/" => new DivCalculation(operands),
                _ => throw new InvalidInputException(op),
            };
        }

        private void Calculate()
        {
            try
            {
                var op = GetOpFromUser();
                var operands = GetOperandsFromUser();
                var calculation = NewCalculation(op, operands);
                Console.WriteLine(calculation.Format());
                history.Add(calculation);
            }
            catch (NullInputException)
            {
                Console.WriteLine("Input was null, calculation cancelled");
            }
            catch (InvalidInputException e)
            {
                Console.WriteLine($"{e.Input} is not a valid input, calculation cancelled");
            }

        }

        private void ShowHistory()
        {
            if (history.Count == 0)
            {
                Console.WriteLine("No calculations to show");
                return;
            }
            foreach (Calculation calculation in history)
            {
                Console.WriteLine("");
                Console.WriteLine(calculation.Format());
            }
        }

        private void Reset()
        {
            history.Clear();
            Console.WriteLine("History cleared");
        }

        private void Quit()
        {
            Console.WriteLine("Thanks for using the fantastic calculator!");
            running = false;
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
                    case "history":
                        ShowHistory();
                        break;
                    case "reset":
                        Reset();
                        break;
                    case "quit":
                        Quit();
                        break;
                    case "options":
                        ShowOptions();
                        break;
                    default:
                        Console.WriteLine("Type 'options' for a list of commands");
                        break;
                }
            }
        }
    }
}