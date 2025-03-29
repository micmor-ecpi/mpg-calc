/***************
* Name: Michael Morton
* Date: 2025-03-23
* Project: MPG Calculator
*
* DataEntry class. Contains user inputted data from the
* terminal.
*/
public abstract class DataEntry
{
    //Table definitions
    public static string GasReceiptTable {get;} = "(ID integer PRIMARY KEY"+
    ",Odometer integer\n"+
    ",Gallons double\n"+
    ",Price double\n"+
    ",Plate varchar(10))"; //Table definition for Gas receipts
    //Basic properties
    public int Odometer {get; protected set;}
    public string VehiclePlate {get;set;} = "";
    //SQL properties
    public int ID {get;set;} = -1;
    //Constructor
    public DataEntry(int CurrentOdometer, string plate)
    {
        Odometer=CurrentOdometer;
        VehiclePlate=plate;
    }
    public abstract string ToDBEntry(string table);
}