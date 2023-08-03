using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace iTunes_XML_Alternatives
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        public MainPage()
        {
            this.InitializeComponent();

            if (localSettings.Values["beetsDb"] != null)
            {
                txtBeetsDb.Text = localSettings.Values["beetsDb"].ToString();
            }

            if (localSettings.Values["source"] != null)
            {
                txtSource.Text = localSettings.Values["source"].ToString();
            }

            if (localSettings.Values["destination"] != null)
            {
                txtDestination.Text = localSettings.Values["destination"].ToString();
            }

            validateForm();
        }

        private async void btnBeetsDbBrowse_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.HomeGroup;
            picker.FileTypeFilter.Add(".db");
            picker.FileTypeFilter.Add("*");

            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                txtBeetsDb.Text = file.Path;
            }
        }

        private async void btnSourceBrowse_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.MusicLibrary;
            picker.FileTypeFilter.Add(".xml");

            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                txtSource.Text = file.Path;
            }
        }

        private async void btnDestinationBrowse_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileSavePicker();
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.MusicLibrary;
            picker.FileTypeChoices.Add("XML", new List<string>() { ".xml" });
            picker.SuggestedFileName = "iTunes Media Library.xml";

            Windows.Storage.StorageFile file = await picker.PickSaveFileAsync();
            if (file != null)
            {
                txtDestination.Text = file.Path;
            }
        }

        private void txtBeetsDb_TextChanged(object sender, TextChangedEventArgs e)
        {
            localSettings.Values["beetsDb"] = txtBeetsDb.Text;
            validateForm();
        }

        private void txtSource_TextChanged(object sender, TextChangedEventArgs e)
        {
            localSettings.Values["source"] = txtSource.Text;
            validateForm();
        }

        private void txtDestination_TextChanged(object sender, TextChangedEventArgs e)
        {
            localSettings.Values["destination"] = txtDestination.Text;
            validateForm();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            CoreApplication.Exit();
        }

        private async void btnWrite_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog msgbox = new MessageDialog("Not implemented yet", "Info");

            // Add commands and set their callbacks; both buttons use the same callback function instead of inline event handlers
            msgbox.Commands.Add(new UICommand(
                "Close",
                new UICommandInvokedHandler(this.CommandInvokedHandler)));

            // Set the command that will be invoked by default
            msgbox.DefaultCommandIndex = 0;

            // Set the command to be invoked when escape is pressed
            msgbox.CancelCommandIndex = 1;

            // Show the message dialog
            await msgbox.ShowAsync();
        }

        private void validateForm()
        {
            if (txtBeetsDb.Text == "")
            {
                btnWrite.IsEnabled = false;
            }
            else if (txtSource.Text == "")
            {
                btnWrite.IsEnabled = false;
            }
            else if (txtDestination.Text == "")
            {
                btnWrite.IsEnabled = false;
            }
            else
            {
                btnWrite.IsEnabled = true;
            }
        }

        private void CommandInvokedHandler(IUICommand command)
        {
            
        }
    }
}
