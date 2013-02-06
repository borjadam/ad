using System;
using Gtk;
using System.Data;
using Npgsql;
using System.Collections.Generic;
using PGtkDbPrueba;


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


	protected void OnBotonBuscarClicked (object sender, System.EventArgs e)
	{
		
//		while (treeView.GetColumn(0) != null) {
//			treeView.RemoveColumn(treeView.GetColumn(0));
//		}
		
		TreeViewColumn[] treeViewColumns = treeView.Columns;
		foreach (TreeViewColumn treeViewColumn in treeViewColumns) {
			treeView.RemoveColumn(treeViewColumn);
		}
		
		NpgsqlConnection connection = new NpgsqlConnection
				("Server= localhost; Database= dbprueba; User Id=dbprueba; Password = 12345");
			connection.Open();
		
		string sentencia = campoSentencia.Text;
		
		IDbCommand command = connection.CreateCommand();
		command.CommandText = sentencia;
		IDataReader datareader = command.ExecuteReader();
		
		//treeView.AppendColumn (datareader.GetName(0), new CellRendererText (), "text", 0);
		//treeView.AppendColumn (datareader.GetName(1), new CellRendererText (), "text", 1);
		
		//manera de nombrar las columnas
		for (int i = 0; i < datareader.FieldCount; i++){
			treeView.AppendColumn(datareader.GetName(i), new CellRendererText (), "text", i);
		}
		//
		
		//manera de crear los tipos
		Type[] types = TypeExtensions.GetTypes (typeof(string), datareader.FieldCount);
		
		//sin crear un metodo aparte
		
		//Type[] types = new Type[ datareader.FieldCount ];
		//for (int index = 0; index < datareader.FieldCount; index++){
			//types[index] = typeof(string);
		//}
		
		ListStore listStore = new ListStore(types);
		//
		
		treeView.Model = listStore;
		
		
		while(datareader.Read() ) {
			string[] values = new string [datareader.FieldCount];
			
			for (int index = 0; index < datareader.FieldCount; index++) {
				values[index] = datareader[index].ToString();				
			}
			
			listStore.AppendValues (values);	
		}
		datareader.Close();
		connection.Close();
	}
}
