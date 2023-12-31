﻿using GTFys.Application;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GTFys.UI
{
    /// <summary>
    /// Interaction logic for PhysioProfilePage.xaml
    /// </summary>
    public partial class PhysioProfilePage : Page
    {
        private string selectedImagePath;

        public PhysioProfilePage()
        {
            InitializeComponent();

            LoadPhysioInfo();
        }

        PhysioRepo physioRepo = new PhysioRepo();
        private async void btnUpdatePhysio_Click(object sender, RoutedEventArgs e)
        {
            bool updateSuccessful = await physioRepo.PhysioUpdateUser(tbFirstName.Text, tbLastName.Text, tbUsername.Text,
                tbPassword.Password.ToString(), tbEmail.Text, tbPhone.Text, tbCPR.Text, tbAddress.Text,
                Convert.ToInt32(tbZipCode.Text), tbCity.Text, selectedImagePath);

            if (updateSuccessful)
            {
                // Show a success message to the user
                MessageBox.Show("Din opdatering succesfuld!", "Succesfuld opdatering", MessageBoxButton.OK, MessageBoxImage.Information);

                // Reload the physio information to update the displayed values
                LoadPhysioInfo();
            }
            else
            {
                // Show an error message to the user
                MessageBox.Show("Fejl ved opdatering. Læs venligst informationen igennem og prøv igen.", "Fejl ved opdatering", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UploadImage_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Select a picture",
                Filter = "Image Files|*.png;*.jpg;*.jpeg;*.gif;*.bmp",
                Multiselect = false
            };

            // Show OpenFileDialog
            bool? result = openFileDialog.ShowDialog();

            // Check if the user selected a file
            if (result == true)
            {
                // Get the selected file path
                selectedImagePath = openFileDialog.FileName;

                // Load the selected image into the Image control
                iProfilePicture.Source = new BitmapImage(new Uri(selectedImagePath));
            }
        }

        private void LoadPhysioInfo()
        {
            // Set all values in the textbox to the values of the logged in physio (CurrentPhysio)
            tbCPR.Text = PhysioService.CurrentPhysio?.CPR;
            tbFirstName.Text = PhysioService.CurrentPhysio?.FirstName;
            tbLastName.Text = PhysioService.CurrentPhysio?.LastName;
            tbUsername.Text = PhysioService.CurrentPhysio?.Username;
            tbPassword.Password = PhysioService.CurrentPhysio?.Password.ToString();
            tbPhone.Text = PhysioService.CurrentPhysio?.Phone;
            tbEmail.Text = PhysioService.CurrentPhysio?.Email;
            tbAddress.Text = PhysioService.CurrentPhysio?.Address;
            tbCity.Text = PhysioService.CurrentPhysio?.City;
            tbZipCode.Text = PhysioService.CurrentPhysio?.ZipCode.ToString();
            if (!string.IsNullOrEmpty(PhysioService.CurrentPhysio?.ProfilePicture)) {
                byte[] imageBytes = Convert.FromBase64String(PhysioService.CurrentPhysio?.ProfilePicture);
                iProfilePicture.Source = GetImageFromDatabase(imageBytes);
            }
        }

        // Method to convert bytes to image 
        private BitmapImage GetImageFromDatabase(byte[] imageData)
        {
            BitmapImage image = null;

            using (MemoryStream stream = new MemoryStream(imageData)) {
                image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = stream;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.EndInit();
                image.Freeze();
            }
            return image;
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            // Open a new instance of PhysioFrontPageWindow
            PhysioFrontPageWindow physioFrontPageWindow = new PhysioFrontPageWindow();  
            // Close the current window hosting the page
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow != null) {
                physioFrontPageWindow.Show();
                parentWindow.Close();
            }

        }

    }
}
