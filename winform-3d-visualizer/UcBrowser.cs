using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp.WinForms;
using CefSharp;
using CefSharp.WinForms.Internals;

namespace winform_3d_visualizer
{
    public partial class UcBrowser : UserControl
    {
        private ChromiumWebBrowser _browser;
        public event EventHandler<IsBrowserInitializedChangedEventArgs> IsBrowserInitializedChanged;


        public UcBrowser()
        {
            InitializeComponent();


            var bitness = Environment.Is64BitProcess ? "x64" : "x86";
            var version = String.Format("Chromium: {0}, CEF: {1}, CefSharp: {2}, Environment: {3}", Cef.ChromiumVersion, Cef.CefVersion, Cef.CefSharpVersion, bitness);
            DisplayOutput(version);

            _browser = new ChromiumWebBrowser("about:blank")
            {
                Dock = DockStyle.Fill,
            };
            this.Controls.Add(_browser);

            _browser.LoadingStateChanged += OnBrowserLoadingStateChanged;
            _browser.ConsoleMessage += OnBrowserConsoleMessage;
            _browser.StatusMessage += OnBrowserStatusMessage;
            _browser.TitleChanged += OnBrowserTitleChanged;
            _browser.AddressChanged += OnBrowserAddressChanged;
            _browser.RegisterJsObject("jsBind", new JSBinding());
            _browser.IsBrowserInitializedChanged += _browser_IsBrowserInitializedChanged;
        }

        private void _browser_IsBrowserInitializedChanged(object sender, IsBrowserInitializedChangedEventArgs e)
        {
            this.InvokeOnUiThreadIfRequired(() =>
            IsBrowserInitializedChanged?.Invoke(sender, e));
        }

        public void NavigateTo(string url)
        {
            LoadUrl(url);
        }

        public string DocumentText
        {
            set
            {
                _browser.LoadString(value, "about:blank");
            }
        }


        private void OnBrowserConsoleMessage(object sender, ConsoleMessageEventArgs args)
        {
            DisplayOutput(string.Format("Line: {0}, Source: {1}, Message: {2}", args.Line, args.Source, args.Message));
        }

        private void OnBrowserStatusMessage(object sender, StatusMessageEventArgs args)
        {
            this.InvokeOnUiThreadIfRequired(() => Console.WriteLine(args.Value));
        }

        private void OnBrowserLoadingStateChanged(object sender, LoadingStateChangedEventArgs args)
        {

        }

        private void OnBrowserTitleChanged(object sender, TitleChangedEventArgs args)
        {
            this.InvokeOnUiThreadIfRequired(() => Text = args.Title);
        }

        private void OnBrowserAddressChanged(object sender, AddressChangedEventArgs args)
        {
            this.InvokeOnUiThreadIfRequired(() => Console.WriteLine(args.Address));
        }


        public void DisplayOutput(string output)
        {
            this.InvokeOnUiThreadIfRequired(() => Console.WriteLine(output));
        }

        private void LoadUrl(string url)
        {
            if (Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
            {
                _browser.Load(url);
            }
        }
    }
}
