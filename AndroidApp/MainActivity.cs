﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Input;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Service.Notification;
using Android.Views;
using Android.Widget;
using AndroidApp.AndPlugin;
using OmniGui;
using OmniGui.Xaml;
using OmniGui.Xaml.Templates;
using OmniXaml;
using OmniXaml.Services;
using OmniXaml.Attributes;
using WpfApp;

namespace AndroidApp
{
    [Activity(Label = "AndroidApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var view = new OmniGuiView(ApplicationContext);
            Platform.Current = new AndroidPlatform(view, this);
            OmniGuiPlatform.PropertyEngine = new OmniGuiPropertyEngine();

            var layout = LoadLayout(ReadTextFromAsset("Layout.xaml"));
            
            view.Layout = layout;

            
            ActionBar.Hide();
            this.Window.AddFlags(WindowManagerFlags.Fullscreen);
            

            SetContentView(view);
        }

        private string ReadTextFromAsset(string assetName)
        {
            using (var reader = new StreamReader(Assets.Open(assetName)))
            {
                return reader.ReadToEnd();
            }
        }

        private Layout LoadLayout(string xaml)
        {
            var assemblies = new[]
            {
                Assembly.Load(new AssemblyName("OmniGui")),
                Assembly.Load(new AssemblyName("OmniGui.Xaml")),
                Assembly.Load(new AssemblyName("AndroidApp")),
                Assembly.Load(new AssemblyName("Common")),
            };

            var locator = new TypeLocator(() => ControlTemplates);
            var xamlLoader = new OmniGuiXamlLoader(assemblies, () => ControlTemplates, locator);

            var loadXaml = xamlLoader.Load(xaml);
            var container = (Container)xamlLoader.Load(ReadTextFromAsset("Container.xaml"));

            ControlTemplates = container.ControlTemplates;

            var layout = (Layout) loadXaml;
            new TemplateInflator().Inflate(layout, ControlTemplates);

            return layout;
        }

        public ICollection<ControlTemplate> ControlTemplates { get; set; }

        [TypeConverterMember(typeof(ICommand))]
        public static Func<string, ConvertContext, (bool, object)> CommandConverter = (str, v) => (true, new DelegateCommand(() =>
        {
            new AlertDialog.Builder(Application.Context)
            .SetTitle("Salutations")
            .SetMessage("Se me va la vida a puñaos")
            .Show();
        }));
    }
}