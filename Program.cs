/***************
* Name: Michael Morton
* Date: 2025-03-23
* Project: MPG Calculator
*
* Main application class.
*/
using System.Data.SQLite;

public class MPGCalc
{
    //vehicle plate is needed globally, define it here
    private static string? plate = "ABC1234";
    static void Main(string[] args)
    {
        //Print header
        Console.WriteLine("Michael Morton - MPG Calculator");
        //Initialize Database
        string db_table = PromptForTableName(); 
        SQLiteConnection conn =
            DBCore.Connect("MPG-Calc.db");
        //Make sure the table exists
        DBCore.CreateTable(conn,db_table,DataEntry.GasReceiptTable);

        //Pull and print database items
        List<GasReceipt> receipts = DBCore.GetTable(conn,db_table,"");
        foreach(GasReceipt r in receipts)
        {
            //print receipt to console
            Console.Write(r);
            //Insert blank line for readability
            Console.WriteLine(receipts.Count() + " \n");
        }
        
        //Get two gas receipts from the user
        //They can use the printed table as the first one
        GasReceipt gr1;
        if(receipts.Count() == 0){
            //if no receipts in table, prompt for first receipt
            gr1 = PromptForData(plate);
            gr1.ID=1;
            //insert first receipt into table
            DBCore.Insert(conn,db_table,gr1);
            receipts = DBCore.GetTable(conn,db_table,"");
        }
        else
        {
            //Get last receipt and load it into gr1 object
            gr1=receipts[receipts.Count()-1];
        }
        //prompt for second gas receipt
        Console.WriteLine("Enter new gas receipt details.");
        GasReceipt gr2 = PromptForData(plate);
        //Display DataEntry object in Console.
        Console.WriteLine("\n" + gr1);
        Console.WriteLine(gr2);

        //Calculate MPG
        double MPG = GasReceipt.Compare(gr1,gr2);
        Console.WriteLine("\nPrice Total: " + GasReceipt.CalculatePrice(gr2.GallonsOfGas,gr2.PricePerGallon).ToString("c2"));
        Console.WriteLine("Miles per Gallon: " + MPG.ToString("0.00") + "\n");
        //Enter gas receipt details to database
        DBCore.Insert(conn,db_table,gr2);
        //Update old receipt to indicate it has been used for calculations and can be deleted
        gr1.MarkForDeletion();
        DBCore.UpdateSQLite(conn,db_table,gr1);
        Console.WriteLine(string.Format(
            "Receipt with ID {0} and Odometer {1} has been marked for deletion.\nDatabase has been updated.",
            gr1.ID,gr1.Odometer));
        
        
        //Prompt user for deletion of table (default: no)
        Console.Write("\nErase current table? (y/N): ");
        string? deleteString = Console.ReadLine();
        if(deleteString == null || deleteString != "y")
        {
        }
        else
        {
            List<GasReceipt> allReceipts = DBCore.GetTable(conn,db_table,"");
            //loop through all receipts and delete one by one
            foreach(GasReceipt r in allReceipts)
            {
                DBCore.DeleteSQLite(conn, db_table, r.ID);
            }
        }
    }
    private static string PromptForTableName()
    {
        Console.WriteLine("Enter vehicle information");
        Console.Write("\n   Enter state: ");
        string? state = Console.ReadLine();
        //perform basic validation
        if(plate==null || plate=="")
        {
            state="VA";
        }
        Console.Write("\n   Enter plate: ");
        plate = Console.ReadLine();
        //perform basic validation
        if(plate==null || plate=="")
        {
            plate = "ABC1234";
        }
        //return formatted table name
        return state+"_"+plate+"_Receipts";
    }
    private static GasReceipt PromptForData(string? plate)
    {
        //Input values from user
        Console.Write("Enter Odometer: ");
        string? odo = Console.ReadLine();
        int odo1 = Convert.ToInt32(odo);

        Console.Write("\n   Enter gallons of gas filled: ");
        string? gal = Console.ReadLine();
        double gal1 = Convert.ToDouble(gal);

        Console.Write("\n   Enter price/Gal: ");
        string? price = Console.ReadLine();
        double price1 = Convert.ToDouble(price);
        if(plate==null || plate==""){
            //Give default value for plate if null
            plate="ABC1234";
        }
        
        //Create GasReceipt object
        return new GasReceipt(odo1,gal1,price1,plate);
    }
}