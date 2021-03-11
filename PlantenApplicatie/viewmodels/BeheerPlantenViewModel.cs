﻿using PlantenApplicatie.Data;
using PlantenApplicatie.Domain;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace PlantenApplicatie.viewmodels
{
    class BeheerPlantenViewModel : ViewModelBase
    {
        public ICommand showPlantDetailsCommand { get; set; }

        public ObservableCollection<Plant> Plants { get; set; }

        public ObservableCollection<TfgsvType> Types { get; set; }

        public ObservableCollection<string> Soorten { get; set; }
        public ObservableCollection<string> Families { get; set; }
        public ObservableCollection<string> Genus { get; set; }

        // hiermee kunnen we de data opvragen aan de databank.
        public PlantenDao _plantenDao;

        public Plant _selectedPlant;

        public BeheerPlantenViewModel(PlantenDao plantenDao)
        {
            showPlantDetailsCommand = new DelegateCommand(showPlantDetails);

            Plants = new ObservableCollection<Plant>();
            Types = new ObservableCollection<TfgsvType>();
            Soorten = new ObservableCollection<string>();
            Families = new ObservableCollection<string>();
            Genus = new ObservableCollection<string>();

            this._plantenDao = plantenDao;
        }

        private void showPlantDetails()
        {
            GetPlantDetails();
        }

        public void LoadPlants()
        {
            var plants = _plantenDao.GetPlanten();
            Plants.Clear();
            foreach(var plant in plants)
            {
                Plants.Add(plant);
            }
        }

        public void LoadTypes()
        {
            var types = _plantenDao.GetTypes();
            Types.Clear();
            foreach(var type in types)
            {
                Types.Add(type);
            }
        }

        public void LoadSoorten()
        {
            var soorten = _plantenDao.GetUniqueSpeciesNames();
            Soorten.Clear();
            foreach(var soort in soorten)
            {
                Soorten.Add(soort);
            }
        }

        public void LoadFamilies()
        {
            var families = _plantenDao.GetUniqueFamilyNames();
            Families.Clear();
            foreach (var familie in families)
            {
                Families.Add(familie);
            }
        }

        public void LoadGenus()
        {
            var genus = _plantenDao.GetUniqueGenusNames();
            Genus.Clear();
            foreach (var gene in genus)
            {
                Genus.Add(gene);
            }
        }

        public Plant SelectedPlant
        {
            get { return _selectedPlant; }
            set
            {
                _selectedPlant = value;

                if (value is not null)
                {

                }
                OnPropertyChanged();
            }
        }
        public static Plant plant = new Plant();
        public void GetPlantDetails()
        {
            
            // nieuw venster initialiseren
            PlantDetails plantDetails = new PlantDetails();
            // object Plant toewijzen door geselecteerd item uit listview te casten
            _selectedPlant = SelectedPlant;
            plant = _selectedPlant;
            // initialiseer labels en waarden
            plantDetails.lblPlantnaamGroot.Content = _selectedPlant.Fgsv;
            plantDetails.lblFamilieResult.Content = _selectedPlant.Familie;
            plantDetails.lblTypeResult.Content = _selectedPlant.Type;
            plantDetails.lblGeslachtResult.Content = _selectedPlant.Geslacht;
            plantDetails.lblSoortResult.Content = _selectedPlant.Soort;
            plantDetails.lblVariantResult.Content = _selectedPlant.Variant;
            


            // toon plantdetails venster
            plantDetails.Show();
        }
    }
}
