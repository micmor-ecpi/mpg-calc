/***************
* Name: Michael Morton
* Date: 2025-03-23
* Project: MPG Calculator
*
* Main application class.
*/
public class MPGCalc
{
    static void Main(string[] args)
    {
        //Print header
        Console.WriteLine("Michael Morton - MPG Calculator");

        DataEntry de1 = PromptForData();
        DataEntry de2 = PromptForData();
        //Display DataEntry object in Console.
        Console.WriteLine(de1);
        Console.WriteLine(de2);

        //Calculate MPG
        double MPG = DataEntry.Compare(de1,de2);
        Console.WriteLine("\nMiles per Gallon: " + MPG);

        //Display SQL code for the user
        Console.WriteLine(
            "Code for entering into SQL database:");
        Console.WriteLine(de1.ToDBEntry());
        Console.WriteLine(de2.ToDBEntry());
    }
    private static DataEntry PromptForData()
    {
        //Input values from user
        Console.Write("\nEnter Odometer: ");
        string? odo = Console.ReadLine();
        int odo1 = Convert.ToInt32(odo);

        Console.Write("\nEnter gallons of gas filled: ");
        string? gal = Console.ReadLine();
        double gal1 = Convert.ToDouble(gal);

        Console.Write("\nEnter price/Gal: ");
        string? price = Console.ReadLine();
        double price1 = Convert.ToDouble(price);

        Console.Write("\nEnter vehicle plate number: ");
        string? plate = Console.ReadLine();
        if(plate==null || plate==""){
            //Give default value for plate if null
            plate="ABC1234";
        }
        
        //Create DataEntry object
        return new DataEntry(odo1,gal1,price1,plate);
    }
}