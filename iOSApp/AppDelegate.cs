using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Foundation;
using iOSApp.Omni;
using OmniGui;
using OmniGui.Xaml;
using OmniGui.Xaml.Templates;
using UIKit;

namespace iOSApp
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        // class-level declarations

        public override UIWindow Window
        {
            get;
            set;
        }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            OmniGuiPlatform.PropertyEngine = new OmniGuiPropertyEngine();
            Platform.Current = new iOSPlatform();
            
            var layout = LoadLayout();

            // create a new window instance based on the screen size
            Window = new UIWindow(UIScreen.MainScreen.Bounds);

            // If you have defined a root view controller, set it here:
            var vc = new UIViewController();
            vc.View = new OmniGuiView(layout);
            Window.RootViewController = vc;

            // make the window visible
            Window.MakeKeyAndVisible();

            return true;
        }

        private static Layout LoadLayout()
        {
            var assemblies = new[]
            {
                Assembly.Load(new AssemblyName("OmniGui")),
                Assembly.Load(new AssemblyName("OmniGui.Xaml")),
                Assembly.Load(new AssemblyName("iOSApp")),
            };

            var loader = new OmniGuiXamlLoader(assemblies, () => ControlTemplates, new TypeLocator(() => ControlTemplates));

            var xaml = File.ReadAllText("Layout.xaml");
            var containerXaml = File.ReadAllText("Container.xaml");
            var container = (Container)loader.Load(containerXaml);
            ControlTemplates = container.ControlTemplates;
            
            var layout = (Layout)loader.Load(xaml);
            new TemplateInflator().Inflate(layout, ControlTemplates);

            return layout;
        }

        public static ICollection<ControlTemplate> ControlTemplates { get; set; } = new List<ControlTemplate>();

        public override void OnResignActivation(UIApplication application)
        {
            // Invoked when the application is about to move from active to inactive state.
            // This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
            // or when the user quits the application and it begins the transition to the background state.
            // Games should use this method to pause the game.
        }

        public override void DidEnterBackground(UIApplication application)
        {
            // Use this method to release shared resources, save user data, invalidate timers and store the application state.
            // If your application supports background exection this method is called instead of WillTerminate when the user quits.
        }

        public override void WillEnterForeground(UIApplication application)
        {
            // Called as part of the transiton from background to active state.
            // Here you can undo many of the changes made on entering the background.
        }

        public override void OnActivated(UIApplication application)
        {
            // Restart any tasks that were paused (or not yet started) while the application was inactive. 
            // If the application was previously in the background, optionally refresh the user interface.
        }

        public override void WillTerminate(UIApplication application)
        {
            // Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
        }
    }
}


