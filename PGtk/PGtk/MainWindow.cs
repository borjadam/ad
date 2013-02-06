using System;
using Gtk;
using System.IO;

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

	protected void OnNewActionActivated (object sender, System.EventArgs e)
	{
		//throw new System.NotImplementedException ();
		campoTexto.Buffer.Clear();	
	}
	
	protected void OnQuitActionActivated (object sender, System.EventArgs e)
	{
		Application.Quit ();
		
	}

	protected void OnOpenActionActivated (object sender, System.EventArgs e)
	{
		FileChooserDialog ventanaAbrir = 
			new FileChooserDialog("Nuevo archivo",null, FileChooserAction.Open, Stock.Cancel, 
		       					ResponseType.Cancel, Stock.Open, ResponseType.Accept);
		
			ResponseType responde = (ResponseType)ventanaAbrir.Run();

		if(responde == ResponseType.Accept) {
			campoTexto.Buffer.Text = File.ReadAllText (ventanaAbrir.Filename);		                                       
		}
		ventanaAbrir.Destroy();
	}

	protected void OnNewAction1Activated (object sender, System.EventArgs e)
	{
		return OnNewActionActivated(object sender, System.EventArgs e);
	}

	protected void OnOpenAction1Activated (object sender, System.EventArgs e)
	{
		
	}
}