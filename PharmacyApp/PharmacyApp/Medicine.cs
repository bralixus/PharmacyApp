using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using PharmacyApp;

public class Medicine : ActiveRecord
{
    private int? _iD { get; set; }
    private string _name { get; set; }
    private string _manufacturer { get; set; }
    private double _price { get; set; }
    private int? _amount { get; set; }
    private bool? _withPrescription { get; set; }

    public Medicine(int id, string name, string manufacturer, double price, int amount, bool withPrescription)
    {
        _iD = id;
        _name = name;
        _manufacturer = manufacturer;
        _price = price;
        _amount = amount;
        _withPrescription = withPrescription;
    }

    public Medicine()
    {

    }

    public override void Save(int ID)
    {
        Open();

        SqlTransaction transaction = connection.BeginTransaction();

        SqlCommand cmd = new SqlCommand()
        {
            CommandText = "INSERT INTO [Pharmacy].[dbo].[Medicines] ( [Name], [Manufacturer], [Price], [Amount], [WithPrescription] ) " +
                          "VALUES ( @Name, @Manufacturer, @Price, @Amount, @WithPrescription ); " +
                          "WHERE ID = @ID" +
                          "SELECT SCOPE_IDENTITY(); ",
            CommandType = CommandType.Text,
            Connection = connection,
            Transaction = transaction
        };
        SqlParameter Identity = new SqlParameter()
        {
            ParameterName = "@ID",
            Value = ID,
            DbType = DbType.Int32
        };
        SqlParameter Name = new SqlParameter()
        {
            ParameterName = "@Name",
            Value = _name,
            DbType = DbType.String
        };
        SqlParameter Manufacturer = new SqlParameter()
        {
            ParameterName = "@Manufacturer",
            Value = _manufacturer,
            DbType = DbType.String
        };
        SqlParameter Price = new SqlParameter()
        {
            ParameterName = "@Price",
            Value = _price,
            DbType = DbType.Int32
        };
        SqlParameter Amount = new SqlParameter()
        {
            ParameterName = "@Amount",
            Value = _amount,
            DbType = DbType.Int32
        };
        SqlParameter WithPrescription = new SqlParameter()
        {
            ParameterName = "@WithPrescription",
            Value = _withPrescription,
            DbType = DbType.String
        };

        cmd.Parameters.Add(Name);
        cmd.Parameters.Add(Manufacturer);
        cmd.Parameters.Add(Price);
        cmd.Parameters.Add(Amount);
        cmd.Parameters.Add(WithPrescription);
        cmd.ExecuteNonQuery();
        transaction.Commit();
        Close();
    }
    public void Show()
    {
        Open();
        List<Medicine> medicine = new List<Medicine>();
        SqlCommand cmd = new SqlCommand();

        cmd.CommandText = "SELECT * FROM [Medicines] ";
        cmd.CommandType = CommandType.Text;
        cmd.Connection = connection;

        using (SqlDataReader reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                medicine.Add(new Medicine(Convert.ToInt32(reader.GetValue(0)),
                    Convert.ToString(reader.GetValue(1)),
                    Convert.ToString(reader.GetValue(2)),
                    Convert.ToInt32(reader.GetValue(3)),
                    Convert.ToInt32(reader.GetValue(4)),
                    Convert.ToBoolean(reader.GetValue(5))
                ));

            }
            foreach (var row in medicine)
            {
                Console.WriteLine($"{row._iD}, Nazwa: {row._name}, Producent: {row._manufacturer}, Cena: {row._price}, Ilość: {row._amount}, Na receptę: {row._withPrescription}");
            }
        }

        Close();
        
    }

    public override void Reload(int ID)
    {
        List<Medicine> medicine = new List<Medicine>();
        Console.Write("Podaj nazwę leku:");
        string name = Console.ReadLine();
        Console.Write("Podaj producenta leku: ");
        string manufacturer = Console.ReadLine();
        Console.Write("Podaj cenę leku: ");
        double price =  double.Parse(Console.ReadLine());
        Console.Write("Podaj ilość dostępnych leków: ");
        int amount = int.Parse(Console.ReadLine());
        Console.Write("Czy lek jest na receptę? (pozostaw puste dla nie, lub wpisz \"tak\")");
        bool withPrescription = Boolean.Parse(Console.ReadLine());
        medicine.Add(new Medicine(ID, name, manufacturer, price,amount,withPrescription));

        Open();

        SqlTransaction transaction = connection.BeginTransaction();

        SqlCommand cmd = new SqlCommand()
        {
            CommandText = "UPDATE [Pharmacy].[dbo].[Medicines] ( [Name], [Manufacturer], [Price], [Amount], [WithPrescription] ) " +
                          "SET [Name] = @Name, [Manufacturer] = @Manufacturer, [Price] = @Price, [Amount] = @Amount, [WithPrescription] = @WithPrescription " +
                          "WHERE ID = @ID;",
            CommandType = CommandType.Text,
            Connection = connection,
            Transaction = transaction
        };
        SqlParameter Identity = new SqlParameter()
        {
            ParameterName = "@ID",
            Value = ID,
            DbType = DbType.Int32
        };
        SqlParameter Name = new SqlParameter()
        {
            ParameterName = "@Name",
            Value = _name,
            DbType = DbType.String
        };
        SqlParameter Manufacturer = new SqlParameter()
        {
            ParameterName = "@Manufacturer",
            Value = _manufacturer,
            DbType = DbType.String
        };
        SqlParameter Price = new SqlParameter()
        {
            ParameterName = "@Price",
            Value = _price,
            DbType = DbType.Int32
        };
        SqlParameter Amount = new SqlParameter()
        {
            ParameterName = "@Amount",
            Value = _amount,
            DbType = DbType.Int32
        };
        SqlParameter WithPrescription = new SqlParameter()
        {
            ParameterName = "@WithPrescription",
            Value = _withPrescription,
            DbType = DbType.String
        };

        cmd.Parameters.Add(Name);
        cmd.Parameters.Add(Manufacturer);
        cmd.Parameters.Add(Price);
        cmd.Parameters.Add(Amount);
        cmd.Parameters.Add(WithPrescription);
        cmd.ExecuteNonQuery();
        transaction.Commit();
        Close();
    }

    public override void Delete(int ID)
    {
        Open();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "DELETE FROM [Pharmacy].[dbo].[Medicines]" +
                          " WHERE ID = @ID ";

        SqlParameter parameter = new SqlParameter()
        {
            ParameterName = "@ID",
            Value = ID,
            DbType = DbType.Int32,
            Direction = ParameterDirection.Input,
        };
        cmd.Parameters.Add(parameter);
        cmd.ExecuteNonQuery();
        Close();
    }
}

//public class Order:ActiveRecord
//{
//    private int _iD { get; set; }
//    private int? _prescriptionID { get; set; }
//    private int _medicineID { get; set; }
//    private DateTime _date { get; set; }
//    private int _amount { get; set; }

//    public Order(int id, int prescriptionID, int medicineID, DateTime date, int amount)
//    {
//        _iD = id;
//        _prescriptionID = prescriptionID;
//        _medicineID = medicineID;
//        _date = date;
//        _amount = amount;
//    }

//    public override void DeleteOrder(int ID)
//    {
//        SqlCommand cmd = new SqlCommand();
//        cmd.CommandText = "DELETE FROM [Pharmacy].[dbo].[Orders]" +
//                          " WHERE ID = @ID ";
//        Open();
//        SqlParameter parameter = new SqlParameter()
//        {
//            ParameterName = "@ID",
//            Value = ID,
//            DbType = DbType.Int32,
//            Direction = ParameterDirection.Input,
//        };
//        cmd.Parameters.Add(parameter);
//        cmd.ExecuteNonQuery();
//        Close();
//    }
//}

//public class Prescription:ActiveRecord
//{
//    private int _iD { get; set; }
//    private string _customerName { get; set; }
//    private string _pesel { get; set; }
//    private int _prescriptionNumber { get; set; }

//    public Prescription(int iD, string customerName, string pesel, int prescriptionNumber)
//    {
//        _iD = iD;
//        _customerName = customerName;
//        _pesel = pesel;
//        _prescriptionNumber = prescriptionNumber;
//    }

//    public override void DeletePrescription(int ID)
//    {
//        SqlCommand cmd = new SqlCommand();
//        cmd.CommandText = "DELETE FROM [Pharmacy].[dbo].[Prescriptions]" +
//                          " WHERE ID = @ID ";
//        Open();
//        SqlParameter parameter = new SqlParameter()
//        {
//            ParameterName = "@ID",
//            Value = ID,
//            DbType = DbType.Int32,
//            Direction = ParameterDirection.Input,
//        };
//        cmd.Parameters.Add(parameter);
//        cmd.ExecuteNonQuery();
//        Close();
//    }
//}



