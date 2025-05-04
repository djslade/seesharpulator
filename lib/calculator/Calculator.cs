using seesharpulator.lib.calculation;

namespace seesharpulator.lib.calculator
{
    public class Calculator
    {
        private readonly string[] operations = ["+", "-", "*", "/", "sqrt", "pow", "!"];
        private readonly List<Calculation> history = [];
        private bool running = true;
        private List<decimal> carriedOperands = [];

        private static void ShowOptions()
        {
            Console.WriteLine("'calculate - run a calculation");
            Console.WriteLine("'history' - show calculation history");
            Console.WriteLine("'reset' - clear calculation history");
            Console.WriteLine("'carry' - carry result from previous calculations to the next");
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
            var stringOperands = input.Split(" ");
            var operands = new decimal[stringOperands.Length];
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
                var calculation = NewCalculation(op, [.. carriedOperands.Concat(operands)]);
                Console.WriteLine(calculation.Format());
                history.Add(calculation);
                carriedOperands.Clear();
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
            for (int i = 0; i < history.Count; i++)
            {
                Console.WriteLine("");
                Console.WriteLine($"ID: {i}");
                Console.WriteLine(history[i].Format());
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

        private void Carry()
        {
            try
            {
                Console.WriteLine("Enter the ID of the previous calculation you want to carry (shown in history)");
                var input = Console.ReadLine() ?? throw new NullInputException();
                if (!int.TryParse(input, out var value)) throw new InvalidInputException(input);
                if (value < 0 || value >= history.Count) throw new InvalidInputException(input);
                carriedOperands.Add(history[value].Calculate());
                Console.WriteLine($"Carried {history[value].Calculate()} over");
            }
            catch (NullInputException)
            {
                Console.WriteLine("User input was null, carry over cancelled");
            }
            catch (InvalidInputException e)
            {
                Console.WriteLine($"{e.Input} is not a valid ID, carry over cancelled");
            }
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
                    case "carry":
                        Carry();
                        break;
                    default:
                        Console.WriteLine("Type 'options' for a list of commands");
                        break;
                }
            }
        }
    }
}