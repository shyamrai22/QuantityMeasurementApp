using QuantityMeasurementApp.Utility;

class Program
{
  public static void Main()
  {
    bool result1 = MeasurementHelper.AreFeetEqual(1.0, 1.0);
    bool result2 = MeasurementHelper.AreInchesEqual(2.0, 2.0);

    Console.WriteLine("1.0 == 1.0 (feet) -->" + result1);
    Console.WriteLine("2.0 == 2.0 (inches) --> " + result2);
  }
}