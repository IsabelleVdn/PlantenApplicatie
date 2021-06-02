﻿using System;
using PlantenApplicatie.Domain;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using PlantenApplicatie.Data;
using Prism.Commands;

namespace PlantenApplicatie.viewmodels
{
    // class and GUI (Lily)
    public class TabAbiotiekViewModel : ViewModelBase
    {

        //set private variables(Lily)
        private const string Eigenschap = "habitat";

        private readonly PlantenDao _plantenDao;
        private readonly Plant _selectedPlant;

        private string? _selectedInsolation;
        private string? _selectedSoilType;
        private string? _selectedMoistureRequirement;
        private string? _selectedNutritionRequirement;
        private string? _selectedAntagonianEnvironment;


        // private variables for user (Davy)
        private Gebruiker _selectedGebruiker;
        private bool _IsManager;

        //constructor(Lily)
        public TabAbiotiekViewModel(Plant selectedPlant, Gebruiker gebruiker)
        {
            SelectedGebruiker = gebruiker;
            _plantenDao = PlantenDao.Instance;
            _selectedPlant = selectedPlant;

            //observable collections for the comboboxes, gets filled by methods inside the DAO
            Insolations = new ObservableCollection<string>(_plantenDao.GetAbioBezonning());
            SoilTypes = new ObservableCollection<string>(_plantenDao.GetAbioGrondsoort());
            MoistureRequirements = new ObservableCollection<string>(_plantenDao.GetAbioVochtbehoefte());
            NutritionRequirements = new ObservableCollection<string>(_plantenDao.GetAbioVoedingsbehoefte());
            AntagonianEnvironments = new ObservableCollection<string>(_plantenDao.GetAbioAntagonischeOmgeving());
            SelectedPlantHabitats = new ObservableCollection<string>(_plantenDao.GetAbioHabitatNames(_selectedPlant));
            PlantHabitats = new ObservableCollection<string>(_plantenDao.GetAbioHabitatNames());

            //Commands for the buttons
            EditAbiotiekCommand = new DelegateCommand(EditAbiotiek);
            RemoveHabitatCommand = new DelegateCommand(RemoveHabitat);
            AddHabitatCommand = new DelegateCommand(AddHabitat);

            //load the values in of the selected plant
            LoadStandards();
            UserRole();
        }


        //boolean to check which functions the user can perform on the application (Davy)
        public bool IsManager
        {
            get => _IsManager;
            set
            {
                _IsManager = value;
                OnPropertyChanged("IsManager");
            }
        }


        //check which roles the user has. and if the user is an old student(Gebruiker)
        //He can only observe the selected values of the plant (Davy,Jim)
        private void UserRole()
        {
            switch (SelectedGebruiker.Rol.ToLower())
            {
                case "manager":
                    IsManager = true;
                    break;
                case "data-collector":
                    IsManager = false;
                    break;
                case "gebruiker":
                    IsManager = false;
                    break;
            }
        }

        //the selected user is the account with which you login. This getter setter is given at the start and passes to all other viewmodels (Davy)
        public Gebruiker SelectedGebruiker
        {
            private get => _selectedGebruiker;
            set
            {
                _selectedGebruiker = value;
                OnPropertyChanged();
            }
        }

        //observable collections (Lily)
        public ObservableCollection<string> Insolations { get; }
        public ObservableCollection<string> SoilTypes { get; }
        public ObservableCollection<string> MoistureRequirements { get; }
        public ObservableCollection<string> NutritionRequirements { get; }
        public ObservableCollection<string> AntagonianEnvironments { get; }

        public ObservableCollection<string> SelectedPlantHabitats { get; }
        public ObservableCollection<string> PlantHabitats { get; }


        //getters en setters (Lily)
        public string? SelectedInsolation
        {
            private get { return _selectedInsolation; }
            set
            {
                _selectedInsolation = value;
                OnPropertyChanged();
            }
        }

        public string? SelectedSoilType
        {
            private get { return _selectedSoilType; }
            set
            {
                _selectedSoilType = value;
                OnPropertyChanged();
            }
        }

        public string? SelectedMoistureRequirement
        {
            private get { return _selectedMoistureRequirement; }
            set
            {
                _selectedMoistureRequirement = value;
                OnPropertyChanged();
            }
        }

        public string? SelectedNutritionRequirement
        {
            private get { return _selectedNutritionRequirement; }
            set
            {
                _selectedNutritionRequirement = value;
                OnPropertyChanged();
            }
        }

        public string? SelectedAntagonianEnvironment
        {
            private get { return _selectedAntagonianEnvironment; }
            set
            {
                _selectedAntagonianEnvironment = value;
                OnPropertyChanged();
            }
        }

        //public strings for the selected value in the abiomulti listviews.
        public string? SelectedAbioPlantHabitat { get; set; }
        public string? SelectedAbioHabitat { get; set; }

        //button commands (Lily)
        public ICommand EditAbiotiekCommand { get; }
        public ICommand RemoveHabitatCommand { get; }
        public ICommand AddHabitatCommand { get; }

        //Load the selected standards in from the selected plant. If there is none the selected values are null (Lily)
        private void LoadStandards()
        {
            var abiotiek = _selectedPlant.Abiotiek.SingleOrDefault();

            if (abiotiek is null) return;

            SelectedInsolation = abiotiek.Bezonning;
            SelectedSoilType = abiotiek.Grondsoort;
            SelectedMoistureRequirement = abiotiek.Vochtbehoefte;
            SelectedNutritionRequirement = abiotiek.Voedingsbehoefte;
            SelectedAntagonianEnvironment = abiotiek.AntagonischeOmgeving;
        }

        //edit the abiotiek of the selected plant, if there is none an abiotiek will be added to the plant with the selected values (Lily)
        private void EditAbiotiek()
        {
            var abiotiek = _selectedPlant.Abiotiek.SingleOrDefault();

            Console.WriteLine(_selectedPlant.Abiotiek.Count);

            if (abiotiek is null)
            {
                _plantenDao.AddAbiotiek(_selectedPlant, SelectedInsolation, SelectedSoilType,
                    SelectedMoistureRequirement, SelectedNutritionRequirement,
                    SelectedAntagonianEnvironment);
            }
            else
            {
                _plantenDao.ChangeAbiotiek(abiotiek, SelectedInsolation, SelectedSoilType,
                    SelectedMoistureRequirement, SelectedNutritionRequirement,
                    SelectedAntagonianEnvironment);
            }
        }

        //delete the selected habitat from the plant (Lily)
        private void RemoveHabitat()
        {
            var habitatAbbreviation = _plantenDao.GetAbioHabitatAbbreviation(SelectedAbioPlantHabitat);
            var abiothiekMulti = _plantenDao.GetAbioMulti(_selectedPlant)
                .Single(am => am.Eigenschap == Eigenschap && am.Waarde == habitatAbbreviation);

            _plantenDao.DeleteAbiotiekMulti(abiothiekMulti);

            SelectedPlantHabitats.Remove(SelectedAbioPlantHabitat);
        }

        //add a habitat to the plant (Lily)
        private void AddHabitat()
        {
            if (SelectedPlantHabitats.Contains(SelectedAbioHabitat)) return;

            var habitatAbbreviation = _plantenDao.GetAbioHabitatAbbreviation(SelectedAbioHabitat);

            _plantenDao.AddAbiotiekMulti(_selectedPlant, Eigenschap, habitatAbbreviation);

            SelectedPlantHabitats.Add(SelectedAbioHabitat);
        }
    }
}