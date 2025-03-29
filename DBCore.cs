/***************
* Name: Michael Morton
* Date: 2025-03-25
* Project: MPG Calculator
*
* Database core functionality class. Implements database
* communications with SQLite
*/
using System.Data.SQLite;

public class DBCore
{
    public static SQLiteConnection Connect(string dbName)
    {
        string cs = @"Data Source=" + dbName;
        SQLiteConnection conn = new SQLiteConnection(cs);

        try
        {
            conn.Open();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return conn;
    }
    public static void NonQuerySQLite(SQLiteConnection conn,
        string sql)
    {
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }
    public static SQLiteDataReader QuerySQLite(SQLiteConnection
        conn, string sql)
    {
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;

        return cmd.ExecuteReader();
    }
    public static void CreateTable(SQLiteConnection conn,
        string table, string tableFields)
    {
        string sql = "CREATE TABLE IF NOT EXISTS " +
        table +tableFields+";";
        NonQuerySQLite(conn,sql);
    }
    //Create new record in gas receipts table
    public static void Insert(SQLiteConnection conn, string table, GasReceipt gr)
    {
        NonQuerySQLite(conn, gr.ToDBEntry(table));
    }
    //Get Gas Receipts table
    public static List<GasReceipt> GetTable(SQLiteConnection conn,
    string table, string query)
    {
        List<GasReceipt> receipts = new List<GasReceipt>();
        string sql = "SELECT * FROM " + table +
        "\n" + query;

        SQLiteDataReader r = QuerySQLite(conn, sql);

        while(r.Read())
        {
            GasReceipt gr = new GasReceipt(
                r.GetInt32(1), r.GetDouble(2), r.GetDouble(3),
                r.GetString(4)
            );
            gr.ID = r.GetInt32(0);
            receipts.Add(gr);
        }
        return receipts;
    }
    public static void UpdateSQLite(SQLiteConnection conn,
        string table, GasReceipt gr)
    {
        string sql = string.Format(
            "UPDATE {0} SET Odometer={1}, Gallons={2}, Price={3},"
            +"Plate='{4}' WHERE ID={5}",table, gr.Odometer,gr.GallonsOfGas,
            gr.PricePerGallon,gr.VehiclePlate, gr.ID);

            NonQuerySQLite(conn, sql);
    }
    public static void DeleteSQLite(SQLiteConnection conn,
        string table, int ID)
    {
        string sql = "DELETE from " + table + " WHERE ID="+
        ID;
        NonQuerySQLite(conn, sql);
    }
}