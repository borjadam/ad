using System;
using Gtk;

public delegate int MyFunction (int a, int b);

public partial class MainWindow: Gtk.Window
{	
	private ListStore listStore;
	public MainWindow (): base (Gtk.WindowType.Toplevel)	
		
	{
		Build ();
		
		MyFunction f;
		
		MyFunction[] functions = new MyFunction[]{suma, resta, multiplica};
		int random = new Random().Next(3);
		f = functions[random];
		
		
//		if (new Random().Next(2) ==0){
//			f=suma;
//		}
//		else{
//			f=resta;
//		}
		
		Console.WriteLine("f={0}", f(5,3));
		
		//comboBox
		CellRenderer cellRenderer = new CellRendererText();
		combobox.PackStart(cellRenderer, false);
		combobox.AddAttribute (cellRenderer, "text", 1);
		
		listStore = new ListStore(typeof(string), typeof(string));
		combobox.Model = listStore;
		
		listStore.AppendValues("1", "Uno");
		listStore.AppendValues("2", "Dos");
		listStore.AppendValues("3", "Tres");
		listStore.AppendValues("4", "Cuatro");
		listStore.AppendValues("5", "Cinco");
		
		combobox.Changed += delegate {
			
			TreeIter treeIter;
			
			if (combobox.GetActiveIter (out treeIter)) {  //item seleccionado
				object value = listStore.GetValue(treeIter, 0);
				Console.WriteLine ("ComboBox.Changed delegate id={0}", value);
			}
				
		};
		
		combobox.Changed += comboBoxChanged;		
	}
	
	private int suma (int a, int b) {
			return a+b;
		}
	
	private int resta (int a, int b) {
			return a-b;
		}
	private int multiplica (int a, int b) {
			return a*b;
		}
	
	private void comboBoxChanged(object obj, EventArgs args) {
		
			TreeIter treeIter;
			
			if (combobox.GetActiveIter (out treeIter)) {  //item seleccionado
				object value = listStore.GetValue(treeIter, 0);
				Console.WriteLine ("ComboBox.Changed id={0}", value);
			}
	}
	
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
