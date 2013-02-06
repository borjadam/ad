using System;
using System.Data;
using Npgsql;
using System.Collections.Generic;

namespace PDbPrueba2
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			NpgsqlConnection connection = new NpgsqlConnection
				("Server= localhost; Database= postgres; User Id=dbprueba; Password = 12345");
			connection.Open();
			
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
	}
}
