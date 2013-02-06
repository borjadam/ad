using System;
using Gtk;
using System.Data;
using Npgsql;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
	
	protected void OnExecuteActionActivated (object sender, System.EventArgs e)
	{
		string connectionString = "Server=localhost;Database=dbprueba;User Id=dbprueba;Password=12345";
		NpgsqlConnection dbConnection = new NpgsqlConnection(connectionString);
		NpgsqlCommand selectCommand = dbConnection.CreateCommand();
		selectCommand.CommandText = "select * from articulo";
		NpgsqlDataAdapter dbDataAdapter = new NpgsqlDataAdapter();
		
		dbDataAdapter.SelectCommand = selectCommand;
		
		DataSet dataSet = new DataSet();
		
		dbDataAdapter.Fill(dataSet);
		
		Console.WriteLine("Tables.Count={0}", dataSet.Tables.Count);
		foreach (DataTable datatable in dataSet.Tables) {
			show (datatable);	
		}
		
		DataRow dataRow = dataSet.Tables[0].Rows[0];
		dataRow["nombre"] = DateTime.Now.ToString();
		
		Console.WriteLine("Tabla con los cambios");
		show (dataSet.Tables[0]);
		
//		NpgsqlCommandBuilder commandBuilder = new NpgsqlCommandBuilder ((NpgsqlDataAdapter)dbDataAdapter);
		
		dbDataAdapter.RowUpdating += delegate(object dbDataAdapterSender, NpgsqlRowUpdatingEventArgs eventArgs) {
			Console.WriteLine("RowUpdating command.CommandText={0}", eventArgs);
			
			foreach (IDataParameter dataParameter in eventArgs.Command.Parameters){
				Console.WriteLine("{0}={1}", dataParameter.ParameterName, dataParameter.Value);	
			}
		};
		
		dbDataAdapter.Update(dataSet.Tables[0]);
		
	}
	
	private void show(DataTable datatable) {
//		foreach (DataColumn dataColumn in datatable.Columns){
//			Console.WriteLine("Column.Name ={0}", dataColumn.ColumnName);
//		}
		foreach (DataRow dataRow in datatable.Rows){
			foreach (DataColumn dataColumn in datatable.Columns){
				Console.Write("[{0}={1}]", dataColumn.ColumnName, dataRow[dataColumn]);
			}
			Console.WriteLine();
		}	
	}
}

