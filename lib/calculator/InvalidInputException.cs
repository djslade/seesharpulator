namespace seesharpulator.lib.calculator
{
    public class InvalidInputException(string invalidInput) : Exception("user input was invalid")
    {
        private readonly string input = invalidInput;

        public string Input { get => input; }
    }
}