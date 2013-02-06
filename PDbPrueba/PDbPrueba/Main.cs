using System;
using System.Data;
using Npgsql;
using System.Collections.Generic;

namespace PDbPrueba
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			NpgsqlConnection connection = new NpgsqlConnection
				("Server= localhost; Database= postgres; User Id=dbprueba; Password = 12345");
			connection.Open();
			
			//Comando para insertar datos en las columnas
			//IDbCommand command2 = connection.CreateCommand();
			//command2.CommandText ="insert into prueba (id, nombre) values (2, 'otro')";
			//command2.ExecuteNonQuery();
			// fin comando
			
			//comando para mostrar datos de tabla "prueba"
			IDbCommand command = connection.CreateCommand();
			command.CommandText ="select * from prueba";
			IDataReader datareader = command.ExecuteReader();
			
			
			Console.WriteLine(datareader.GetName(0));
			Console.WriteLine(datareader.GetName(1));
			
			while(datareader.Read() ){
				Console.WriteLine(datareader["id"]);
				Console.WriteLine(datareader["nombre"]);		
			}
			//fin comando
			
			
						
			datareader.Close();
			connection.Close();
			
			}
		
			
			
			private static string[] getColumnNames(IDataReader datareader) {
				List<string> names = new List<string>();
			
				for (int index=0; index < datareader.FieldCount; index++) {
					names.Add (datareader.GetName (index) );
					return names.ToArray();
				}
			}
		
		private static void showColumns(IDataReader datareader) {
				string[] columnNames = getColumnNames (datareader);
				int index = 0;
				foreach (string name in columnNames) {
					Console.WriteLine("index={0} column={1}", index++, name);
				}
			}
	}
}

