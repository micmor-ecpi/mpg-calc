/***************
* Name: Michael Morton
* Date: 2025-03-23
* Project: MPG Calculator
*
* DataEntry class. Contains user inputted data from the
* terminal.
*/
public class DataEntry
{
    public int Odometer {get;}
    public double GallonsOfGas {get;}
    public double PricePerGallon {get;}
    public string VehiclePlate {get;set;} = "";
    public DataEntry(int CurrentOdometer, double gas,
    double price, string plate)
    {
        Odometer=CurrentOdometer;
        GallonsOfGas=gas;
        PricePerGallon=price;
        VehiclePlate=plate;
    }
    public static double Compare(DataEntry receipt1, DataEntry
        receipt2)
    {
        //declare variables
        DataEntry de1;
        DataEntry de2;
        //Check and make sure that the receipts are in the
        //correct order
        if(receipt1.Odometer<receipt2.Odometer)
        {
            de1=receipt1;
            de2=receipt2;
        }
        else
        {
            de1=receipt2;
            de2=receipt1;
        }
        //initialize variables
        double gal1 = de2.GallonsOfGas;
        int miles = de2.Odometer - de1.Odometer;
        return miles / gal1;
    }
    public static double CalculatePrice(double gallons, double
        price)
    {
        return gallons * price;
    }
    public string ToDBEntry()
    {
        return string.Format(
            "INSERT INTO {0} (null,{1},{2},{3},'{4}');"
            ,"VA_123456_Receipts",Odometer.ToString(),
            GallonsOfGas.ToString(),PricePerGallon.ToString(),
            VehiclePlate);
    } 
    public override string ToString()
    {
        return string.Format("Data Entry <{0}>:\n"+
            "   Gallons of Gas: {1}\n"+
            "   Price per Gallon: {2}\n"+
            "   Vehicle Plate: {3}",Odometer,GallonsOfGas,
            PricePerGallon,VehiclePlate);
    }
}