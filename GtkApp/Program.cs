using System;
using Gtk;
using Window = Gtk.Window;

namespace OmniGui.Gtk
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			//var loader = new OmniXaml.Services.BasicXamlLoader(new[] { typeof(MainClass).Assembly, typeof(MyClass).Assembly });
			//var instance = loader.Load(@"<MyClass xmlns=""root"" />");

			Application.Init();
			//var win = new Window("System.Drawing and Gtk#");

			//var dw = new DrawingArea();
			//dw.ConfigureEvent += Dw_ConfigureEvent;
			//win.Add(dw);

			//dw.ExposeEvent += (o, a) => Dw_ExposeEvent(a);

			//dw.SetSizeRequest(300, 300);
			//win.Show();
			//dw.Show();
            var window = new MainWindow();
		    var something = new OmniGuiWidget();
            
		    window.Add(something);
            window.Show();
		    something.Show();
            Application.Run();
		}	
	}
}