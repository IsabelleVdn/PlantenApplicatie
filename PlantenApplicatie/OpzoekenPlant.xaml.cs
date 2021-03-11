﻿using PlantenApplicatie.Data;
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
using PlantenApplicatie.Domain;

namespace PlantenApplicatie
{
    /// <summary>
    /// Interaction logic for OpzoekenPlant.xaml
    /// </summary>
    public partial class OpzoekenPlant : Window
    {
        private PlantenDao plantenDAO;
        public static Plant _selectedPlant { get; set; }

        public OpzoekenPlant()
        {
            InitializeComponent();
            plantenDAO = PlantenDao.Instance;
            Start();

        }

        private void Start()
        {
            cmbFamilie.ItemsSource = plantenDAO.GetUniqueFamilyNames();
            cmbGeslacht.ItemsSource = plantenDAO.GetUniqueGenusNames();
            cmbSoort.ItemsSource = plantenDAO.GetUniqueSpeciesNames();
            cmbType.ItemsSource = plantenDAO.GetTypes();
            lvPlanten.ItemsSource = plantenDAO.GetPlanten();
        }

        private void ResetInputFields()
        {
            txtPlantnaam.Text = string.Empty;
            txtNLNaam.Text = string.Empty;
            cmbFamilie.SelectedValue = null;
            cmbGeslacht.SelectedValue = null;
            cmbSoort.SelectedValue = null;
            txtVariant.Text = string.Empty;
        }

        private void txtPlantnaam_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                MessageBox.Show("abc");
                //lvPlanten.ItemsSource = getPlantenByName(sender.ToString());
            }
        }

        private void btnDetailsPlant_Click(object sender, RoutedEventArgs e)
        {
            GetPlantDetails(lvPlanten);
        }

        private void txtNLnaam_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                MessageBox.Show("abc");
                //lvPlanten.ItemsSource = getPlantenByName(sender.ToString());
            }
        }

        private void txtVariant_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                MessageBox.Show("abc");
                //lvPlanten.ItemsSource = getPlantenByName(sender.ToString());
            }
        }

        private void btnZoeken_Click(object sender, RoutedEventArgs e)
        {
            var family = cmbFamilie.SelectedValue is null ? null : cmbFamilie.SelectedValue.ToString();
            var genus = cmbGeslacht.SelectedValue is null ? null : cmbGeslacht.SelectedValue.ToString();
            var species = cmbSoort.SelectedValue is null ? null : cmbSoort.SelectedValue.ToString();

            var list = plantenDAO.SearchByProperties(txtPlantnaam.Text,
                family, genus,
                species, txtVariant.Text);

            lvPlanten.ItemsSource = list;
            
            ResetInputFields();
        }

       

        public void GetPlantDetails(ListView lv)
        {
            // nieuw venster initialiseren
            PlantDetails plantDetails = new PlantDetails();
            // object Plant toewijzen door geselecteerd item uit listview te casten
            _selectedPlant = (Plant)lv.SelectedItem;

            // initialiseer labels en waarden
            //plantDetails.lblGroep.Content = _plant.;
            plantDetails.lblPlantnaam.Content = _selectedPlant.Fgsv;
            plantDetails.lblBladGrootteResult.Content = _selectedPlant.Familie;
            
            plantDetails.lblBladvormResult.Content = _selectedPlant.Geslacht;
            plantDetails.lblHabitusResult.Content = _selectedPlant.Soort;
            plantDetails.lblRatioResult.Content = _selectedPlant.Variant;

            // toon plantdetails venster
            plantDetails.Show();
        }
    }
}
