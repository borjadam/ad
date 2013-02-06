using System;
using System.Data;
using Npgsql;

namespace PDataSet
{
	public class MyClass
	{
		public MyClass ()
		{
			
			string connectionString = "Server=localhost;Database=dbprueba;User Id=dbprueba;Password=12345";
			IDbConnection dbConnection = new NpgsqlConnection(connectionString);
			
			//creamos el DataSet
			DataSet dataSet = new PDataSet();
			
			//Creamos el DataTable y creamos variables para las filas y las columnas 
			DataTable dataTable = new DataTable();
			DataColumn columna;
			DataRow fila;
			
			//creamos una columna, establecemos el tipo y la añadimos al DataTable
			columna = new DataColumn();
			columna.ColumnName("id");
			columna.DataType = GetType("int");
			dataTable.Columns.Add(columna);
			
			//hacemos lo mismo para el resto de columnas
			columna = new DataColumn();
			columna.ColumnName("nombre");
			columna.DataType = GetType("String");
			dataTable.Columns.Add(columna);
			
			columna = new DataColumn();
			columna.ColumnName("precio");
			columna.DataType = GetType("double");
			dataTable.Columns.Add(columna);
			
			columna = new DataColumn();
			columna.ColumnName("categoria");
			columna.DataType = GetType("int");
			dataTable.Columns.Add(columna);
			
			//creamos 
			
		}
	}
}

