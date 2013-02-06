using System;
using Gtk;
using NHibernate.Cfg;
using Serpis.Ad;
using NHibernate.Tool.hbm2ddl;
using NHibernate;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		
		Configuration configuration = new Configuration();
		configuration.Configure();
		configuration.SetProperty(NHibernate.Cfg.Environment.Hbm2ddlKeyWords, "none");
		configuration.AddAssembly(typeof (Categoria).Assembly);
		
		//new SchemaExport(configuration).Execute(true, false, false);
		
		ISessionFactory sessionFactory = configuration.BuildSessionFactory();
		
		updateCategoria(sessionFactory);
		
		insertCategoria(sessionFactory);
		
		sessionFactory.Close();

		
	}
	
	
	private void updateCategoria(ISessionFactory sessionFactory){
		ISession session = sessionFactory.OpenSession();
		
		Categoria categoria = (Categoria)session.Load(typeof(Categoria), 2L);	
		Console.WriteLine("Categoria Id={0} Nombre{1}", categoria.Id, categoria.Nombre);
		categoria.Nombre = DateTime.Now.ToString();
		session.SaveOrUpdate(categoria);
				
		session.Flush();
		
		session.Close();		
	}
	
	private void insertCategoria(ISessionFactory sessionFactory){
		ISession session = sessionFactory.OpenSession();		
		
		Categoria categoria = new Categoria();
		categoria.Nombre = "Nueva " + DateTime.Now.ToString();
		session.SaveOrUpdate(categoria);
				
		session.Flush();
		
		session.Close();
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
