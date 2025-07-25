using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using System.Globalization;
using Windows.ApplicationModel.Resources.Core;
using Windows.Globalization;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Kuriat_Invoice
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            nvSample.SelectionChanged += NvSample_SelectionChanged;
            // Set default selected item to Home (Accueil)
            nvSample.SelectedItem = nvSample.MenuItems[0];
            contentFrame.Content = new TextBlock { Text = "Home Page" };
            // Set default selected SelectorBarItem to Recent
            SelectorBar1.SelectedItem = SelectorBarItemRecent;
        }

        private void NvSample_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                // Show language selection ComboBox in settings
                var combo = new ComboBox
                {
                    Items = { "English", "Français" },
                    SelectedIndex = GetCurrentLanguageIndex()
                };
                combo.SelectionChanged += LanguageCombo_SelectionChanged;
                contentFrame.Content = combo;
            }
            else if (args.SelectedItem is NavigationViewItem item)
            {
                switch (item.Tag)
                {
                    case "Home":
                        contentFrame.Content = new TextBlock { Text = "Home Page" };
                        break;
                    case "facture":
                        contentFrame.Content = new TextBlock { Text = "Invoice Page" };
                        break;
                    case "devis":
                        contentFrame.Content = new TextBlock { Text = "Estimate Page" };
                        break;
                    case "bondachat":
                        contentFrame.Content = new TextBlock { Text = "Purchase Order Page" };
                        break;
                }
            }
        }

        private int GetCurrentLanguageIndex()
        {
            var lang = ApplicationLanguages.PrimaryLanguageOverride;
            if (string.IsNullOrEmpty(lang))
                lang = CultureInfo.CurrentUICulture.Name;
            return lang.StartsWith("fr") ? 1 : 0;
        }

        private void LanguageCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var combo = sender as ComboBox;
            var selectedLang = combo.SelectedIndex == 1 ? "fr-FR" : "en-US";
            ApplicationLanguages.PrimaryLanguageOverride = selectedLang;
            // Optionally, restart the app or reload the UI to apply changes
            var restartDialog = new ContentDialog
            {
                Title = "Restart Required",
                Content = "Please restart the app to apply the language change.",
                CloseButtonText = "OK"
            };
            _ = restartDialog.ShowAsync();
        }
    }
}
