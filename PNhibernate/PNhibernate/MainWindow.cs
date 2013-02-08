using System;
using Gtk;
using NHibernate.Cfg;
using Serpis.Ad;
using NHibernate.Tool.hbm2ddl;
using NHibernate;
using Npgsql;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		
		Configuration configuration = new Configuration();
		configuration.Configure();
		configuration.SetProperty(NHibernate.Cfg.Environment.Hbm2ddlKeyWords, "none");
		configuration.AddAssembly(typeof (Categoria).Assembly);
		
		new SchemaExport(configuration).Execute(true, false, false);
		
		ISessionFactory sessionFactory = configuration.BuildSessionFactory();
		
		insertCategoria(sessionFactory);
		
		
		sessionFactory.Close();

		
	}
	
	private void loadArticulo(ISessionFactory sessionFactory){
		
		using (ISession session = sessionFactory.OpenSession()){
		Articulo articulo = (Articulo)session.Load(typeof(Articulo), 2L);
		Console.WriteLine("Articulo Id={0} Nombre={1} Precio={2}", 
			                  articulo.Id, articulo.Nombre, articulo.Precio);
			if(articulo.Categoria == null) {
				Console.WriteLine("CategoriaNull");
			} else {
				Console.WriteLine("Categoria.Nombre={0}", articulo.Categoria.Nombre);
			}
		}
		
	}
	
	
	private void updateCategoria(ISessionFactory sessionFactory){
		ISession session = sessionFactory.OpenSession();
		
		try {
			Categoria categoria = (Categoria)session.Load(typeof(Categoria), 2L);	
			Console.WriteLine("Categoria Id={0} Nombre{1}", categoria.Id, categoria.Nombre);
			categoria.Nombre = DateTime.Now.ToString();
			session.SaveOrUpdate(categoria);
				
			session.Flush();
		
		} finally {
			session.Close();
		}
	}
	
	private void insertCategoria(ISessionFactory sessionFactory){
		ISession session = sessionFactory.OpenSession();		
		
		try {
			Categoria categoria = new Categoria();
			categoria.Nombre = "Nueva " + DateTime.Now.ToString();
			session.SaveOrUpdate(categoria);
					
			session.Flush();
		} finally {		
			session.Close();
		}
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected void OnExecuteActionActivated (object sender, System.EventArgs e)
	{
		throw new System.NotImplementedException ();
	}

}
