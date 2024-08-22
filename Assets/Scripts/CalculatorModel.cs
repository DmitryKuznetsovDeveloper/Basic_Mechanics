public sealed class CalculatorModel
{
    public double MultiplyNumber(double number, double multiplier) => number * multiplier;
    public double DivNumber(double number, double divNumber) => number / divNumber;
    public double Addition(double number, double addNumber) => number + addNumber;
    public double Subtraction(double number, double subNumber) => number - subNumber;
    public double NegativeNumber(double number) =>  number * -1;

    public string DelLastNumber(string number)
    {
        if (number.Length == 0) 
            return "0";
        
        return number.Remove(number.Length - 1, 1);
    }

}
