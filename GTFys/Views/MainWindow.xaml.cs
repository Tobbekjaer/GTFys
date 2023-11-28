﻿using System;
using System.Collections.Generic;
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

namespace GTFys.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void physioButtonClicked(object sender, RoutedEventArgs e)
        {
            // Create an instance of PhysioLoginWindow
            PhysioLoginWindow physioLoginWindow = new PhysioLoginWindow();
            // Open a new PhysioLoginWindow
            physioLoginWindow.Show();
        }

        private void patientButtonClicked(object sender, RoutedEventArgs e)
        {
            // Create an instance of PatientLoginWindow
            PatientLoginWindow patientLoginWindow = new PatientLoginWindow();
            // Open a new PhysioLoginWindow
            patientLoginWindow.Show();
        }


    }
}
