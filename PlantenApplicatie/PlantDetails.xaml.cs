﻿using PlantenApplicatie.Data;
using PlantenApplicatie.Domain;
using PlantenApplicatie.viewmodels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PlantenApplicatie
{
    /// <summary>
    /// Interaction logic for PlantDetails.xaml
    /// </summary>
    public partial class PlantDetails : Window
    {
        public PlantDetails(Plant selectedPlant)
        {
            InitializeComponent();
            DataContext = new PlantDetailsViewModel(selectedPlant);
        }
    }
}

