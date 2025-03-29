/***************
* Name: Michael Morton
* Date: 2025-03-23
* Project: MPG Calculator
*
* GasReceipt class. Contains user inputted gas data from the
* terminal. Derived from DataEntry.
*/
public class GasReceipt : DataEntry
{
    public double GallonsOfGas {get;}
    public double PricePerGallon {get;}
    public GasReceipt(int CurrentOdometer, double gas,
        double price, string plate) : base(CurrentOdometer,plate)
    {
        base.Odometer=CurrentOdometer;
        GallonsOfGas=gas;
        PricePerGallon=price;
        base.VehiclePlate=plate;
    }
    public static double Compare(GasReceipt receipt1,
        GasReceipt receipt2)
    {
        //declare variables
        GasReceipt gr1;
        GasReceipt gr2;
        //Check and make sure that the receipts are in the
        //correct order
        if(receipt1.Odometer<receipt2.Odometer)
        {
            gr1=receipt1;
            gr2=receipt2;
        }
        else
        {
            gr1=receipt2;
            gr2=receipt1;
        }
        //initialize variables
        double gal1 = gr2.GallonsOfGas;
        int miles = gr2.Odometer - gr1.Odometer;
        return miles / gal1;
    }
    public static double CalculatePrice(double gallons, double
        price)
    {
        return gallons * price;
    }
    public override string ToDBEntry(string table)
    {
        return string.Format(
            "INSERT INTO {0} VALUES(null,{1},{2},{3},'{4}');"
            ,table,Odometer.ToString(),
            GallonsOfGas.ToString(),PricePerGallon.ToString(),
            VehiclePlate);
    }
    //Mark old receipts as used after they have been used in calculations of MPG
    public void MarkForDeletion()
    {
        if(VehiclePlate.StartsWith("USED-"))
        {
            Console.WriteLine("Receipt is already tagged as used");
        }
        else
        {
            VehiclePlate = "USED-" + VehiclePlate;
        }
    } 
    //Override ToString() method to give more detailed information about the object
    public override string ToString()
    {
        string IDstr;
        if(ID<1)
        {
            IDstr="[new]";
        }
        else
        {
            IDstr="#"+ID;
        }
        return string.Format(IDstr + " Data Entry <{0}>:\n"+
            "   Gallons of Gas: {1}\n"+
            "   Price per Gallon: {2}\n"+
            "   Vehicle Plate: {3}",Odometer,GallonsOfGas,
            PricePerGallon,VehiclePlate);
    }
}