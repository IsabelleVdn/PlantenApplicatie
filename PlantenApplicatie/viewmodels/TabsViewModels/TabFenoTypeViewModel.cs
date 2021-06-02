﻿using PlantenApplicatie.Data;
using PlantenApplicatie.Domain;
using Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace PlantenApplicatie.viewmodels
{
    public class TabFenoTypeViewModel : ViewModelBase
    {
        // private variabelen Davy & Jim
        private readonly PlantenDao _plantenDao = PlantenDao.Instance;
        
        private Plant _selectedPlant;
        
        private int? _selectedBladgrootte;
        
        private string? _selectedBladvorm;
        private string? _selectedBloeiWijze;
        private string? _selectedHabitus;
        private string? _selectedLevensvorm;
        private string? _selectedSpruitFenologie;
        private string? _selectedFenotypeEigenschappen;
        private string? _selectedFenoTypesMulti;
        private string? _selectedFenoMultiMaand;
        
        private FenotypeMulti? _selectedPlantFenoTypeMulti;

        // private variabelen (Davy)
        private Gebruiker _selectedGebruiker;
        private bool _isManager;

        // Constructor Davy
        public TabFenoTypeViewModel(Plant selectedPlant, Gebruiker gebruiker)
        {
            SelectedGebruiker = gebruiker;
            SelectedPlant = selectedPlant;
            //variabelen (Davy &Jim)

            EditFenoTypeCommand = new DelegateCommand(EditFenoType);
            AddFenotypeMultiCommand = new DelegateCommand(AddFenotypeMulti);
            EditFenotypeMultiCommand = new DelegateCommand(EditFenotypeMulti);
            DeleteFenotypeMultiPlantCommand = new DelegateCommand(DeleteFenotypeMultiPlant);
            
            // methoden om comboboxen Fenotype in te laden (Davy & Jim)
            LoadAllProperties();
            
            SetAuthorizedActionsByRole();
        }
        
        // collecties (lijsten) Davy & Jim
        public ObservableCollection<int> FenoBladgroottes { get; } = new();
        
        public ObservableCollection<string> FenoBladvormen { get; } = new();
        public ObservableCollection<string> FenoBloeiwijzes { get; } = new();
        public ObservableCollection<string> FenoHabitussen { get; } = new();
        public ObservableCollection<string> FenoLevensvormen { get; } = new();
        public ObservableCollection<string> FenoSpruitFenologieen { get; } = new();
        public ObservableCollection<string> FenotypeEigenschappen { get; } = new();
        public ObservableCollection<string> FenoMultiMaand { get; } = new();
        
        public ObservableCollection<FenotypeMulti> PlantFenoTypesMulti { get; } = new();

        public ObservableCollection<string> FenoTypesMulti { get; } = new();

        // knop commando's fenotype Davy
        public ICommand EditFenoTypeCommand { get; }
        public ICommand AddFenotypeMultiCommand { get; }
        public ICommand EditFenotypeMultiCommand { get; }
        public ICommand DeleteFenotypeMultiPlantCommand { get; }

        public bool IsManager
        {
            get => _isManager;
            set
            {
                _isManager = value;
                OnPropertyChanged("IsManager");
            }
        }

        public Gebruiker SelectedGebruiker
        {
            private get => _selectedGebruiker;
            set
            {
                _selectedGebruiker = value;
                OnPropertyChanged();
            }
        }

        // Getters and setters selected waardes (Davy & Jim)
        public Plant SelectedPlant
        {
            private get => _selectedPlant;
            set
            {
                _selectedPlant = value;
                OnPropertyChanged();
            }
        }


        public string? SelectedFenoTypesMulti
        {
            private get => _selectedFenoTypesMulti;
            set
            {
                _selectedFenoTypesMulti = value;

                OnPropertyChanged();
            }
        }

        public FenotypeMulti? SelectedPlantFenoTypeMulti
        {
            private get => _selectedPlantFenoTypeMulti;
            set
            {
                _selectedPlantFenoTypeMulti = value;
                OnPropertyChanged();
            }
        }


        public int? SelectedBladgrootte
        {
            get => _selectedBladgrootte;
            set
            {
                _selectedBladgrootte = value;
                OnPropertyChanged();
            }
        }
        public string? SelectedBladvorm
        {
            private get => _selectedBladvorm;
            set
            {
                _selectedBladvorm = value;
                OnPropertyChanged();
            }
        }
        public string? SelectedBloeiwijze
        {
            private get => _selectedBloeiWijze;
            set
            {
                _selectedBloeiWijze = value;
                OnPropertyChanged();
            }
        }
        public string? SelectedHabitus
        {
            private get => _selectedHabitus;
            set
            {
                _selectedHabitus = value;
                OnPropertyChanged();
            }
        }
        public string? SelectedLevensvorm
        {
            private get => _selectedLevensvorm;
            set
            {
                _selectedLevensvorm = value;
                OnPropertyChanged();
            }
        }
        public string? SelectedSpruitFenologie
        {
            private get => _selectedSpruitFenologie;
            set
            {
                _selectedSpruitFenologie = value;
                OnPropertyChanged();
            }
        }

        public string? SelectedFenotypeEigenschappen
        {
            private get => _selectedFenotypeEigenschappen;
            set
            {
                _selectedFenotypeEigenschappen = value;
                ChangeEigenschappen();
                OnPropertyChanged();
            }
        }

        public string? SelectedFenoMultiMaand
        {
            private get => _selectedFenoMultiMaand;
            set
            {
                _selectedFenoMultiMaand = value;
                OnPropertyChanged();
            }
        }

        private void LoadAllProperties()
        {
            LoadFenoBladgrootte();
            LoadFenoBladvorm();
            LoadFenoBloeiwijze();
            LoadFenoHabitus();
            LoadFenoLevensVorm();
            LoadFenoSpruitFenologie();
            LoadFenoTypesMultiPlant();
            LoadEigenschappen();
            LoadFenoMultiMaanden();
            LoadKleur();
            LoadSelectedValues();
        }
        
        //controleer welke rol de gebruiker heeft
        private void SetAuthorizedActionsByRole()
        {
            IsManager = SelectedGebruiker.Rol.ToLower() == "manager";
        }

        //laad de verschillende gegevens in de comboboxes (Davy & Jim)
        private void LoadFenoBladgrootte()
        {
            var bladgroottes = _plantenDao.GetFenoBladGrootte();

            FenoBladgroottes.Clear();

            foreach (var bladgrootte in bladgroottes)
            {
                FenoBladgroottes.Add(Convert.ToInt32(bladgrootte));
            }
        }

        private void LoadFenoBladvorm()
        {
            var bladvormen = _plantenDao.GetFenoBladVorm();

            FenoBladvormen.Clear();

            foreach (var bladvorm in bladvormen)
            {
                FenoBladvormen.Add(bladvorm);
            }
        }

        private void LoadFenoBloeiwijze()
        {
            var bloeiwijzes = _plantenDao.GetFenoBloeiWijze();

            FenoBloeiwijzes.Clear();

            foreach (var bloeiwijze in bloeiwijzes)
            {
                FenoBloeiwijzes.Add(bloeiwijze);
            }
        }

        private void LoadFenoHabitus()
        {
            var habitussen = _plantenDao.GetFenoHabitus();

            FenoHabitussen.Clear();

            foreach (var habitus in habitussen)
            {
                FenoHabitussen.Add(habitus);
            }
        }
        private void LoadFenoLevensVorm()
        {
            var levensvormen = _plantenDao.GetFenoLevensVorm();

            FenoLevensvormen.Clear();

            foreach (var levensvorm in levensvormen)
            {
                FenoLevensvormen.Add(levensvorm);
            }
        }
        private void LoadFenoSpruitFenologie()
        {
            var fenologieen = _plantenDao.GetFenoFenologie();

            FenoSpruitFenologieen.Clear();

            foreach (var fenologie in fenologieen)
            {
                FenoSpruitFenologieen.Add(fenologie);
            }
        }

        private void LoadFenoTypesMultiPlant()
        {
            var fenotypesMulti = _plantenDao.GetFenoMultis(SelectedPlant);

            PlantFenoTypesMulti.Clear();

            foreach (var fenotypeMulti in fenotypesMulti)
            {
                PlantFenoTypesMulti.Add(fenotypeMulti);
            }
        }

        private void LoadEigenschappen()
        {
            FenotypeEigenschappen.Clear();

            FenotypeEigenschappen.Add("bladhoogte");
            FenotypeEigenschappen.Add("bladkleur");
            FenotypeEigenschappen.Add("bloeihoogte");
            FenotypeEigenschappen.Add("bloeikleur");
        }

        private void LoadKleur()
        {
            var kleuren = _plantenDao.GetFenoKleur();
            
            FenoTypesMulti.Clear();

            foreach (var kleur in kleuren)
            {
                FenoTypesMulti.Add(kleur.NaamKleur);
            }
        }
        
        private void LoadHoogte()
        {
            FenoTypesMulti.Clear();

            var maxHoogte = 300;
            var hoogte = 0;

            while (hoogte <= maxHoogte)
            {
                FenoTypesMulti.Add(hoogte.ToString());
                hoogte += 10;
            }
        }
        
        private void LoadFenoMultiMaanden()
        {
            FenoMultiMaand.Clear();
            
            FenoMultiMaand.Add("Jan");
            FenoMultiMaand.Add("Feb");
            FenoMultiMaand.Add("Mar");
            FenoMultiMaand.Add("Apr");
            FenoMultiMaand.Add("May");
            FenoMultiMaand.Add("Jun");
            FenoMultiMaand.Add("Jul");
            FenoMultiMaand.Add("Aug");
            FenoMultiMaand.Add("Sep");
            FenoMultiMaand.Add("Okt");
            FenoMultiMaand.Add("Nov");
            FenoMultiMaand.Add("Dec");
        }

        private void LoadSelectedValues()
        {
            var fenotype = _selectedPlant.Fenotype
                .SingleOrDefault();

            if (fenotype?.Bladgrootte is null) return;

            SelectedBladgrootte = (int)fenotype.Bladgrootte;
            SelectedBladvorm = fenotype.Bladvorm;
            SelectedBloeiwijze = fenotype.Bloeiwijze;
            SelectedSpruitFenologie = fenotype.Spruitfenologie;
            SelectedHabitus = fenotype.Habitus;
            SelectedLevensvorm = fenotype.Levensvorm;
        }

        //wijzig de fenotype van de plant. Als een plant nog geen fenotype heeft word er eentje aangemaakt met de geselecteerde waardes (Jim)
        private void EditFenoType()
        {
            var fenotype = _plantenDao.GetFenotypeFromPlant(SelectedPlant);

            if (fenotype is null)
            {
                _plantenDao.AddFenotype(SelectedPlant, (int)SelectedBladgrootte!, SelectedBladvorm, null, 
                    SelectedBloeiwijze, SelectedHabitus, SelectedLevensvorm, SelectedSpruitFenologie);
            }
            else
            {
                _plantenDao.ChangeFenotype(SelectedPlant, SelectedBladgrootte, SelectedBladvorm, null, 
                    SelectedBloeiwijze, SelectedHabitus, SelectedLevensvorm, SelectedSpruitFenologie);
            }
        }

        //voeg de FenotypeMulti toe aan de plant met de geselecteerde waardes (Jim)
        private void AddFenotypeMulti()
        {
            _plantenDao.AddMultiFenotype(SelectedPlant, SelectedFenotypeEigenschappen, SelectedFenoMultiMaand, 
                SelectedFenoTypesMulti);
            
            LoadFenoTypesMultiPlant();
        }

        //wijzig de geselecteerde FenotypeMulti (Jim)

        private void EditFenotypeMulti()
        {
            _plantenDao.ChangeMultiFenotype(SelectedPlantFenoTypeMulti, SelectedFenotypeEigenschappen, 
                SelectedFenoMultiMaand, SelectedFenoTypesMulti);
            
            LoadFenoTypesMultiPlant();
        }

        //laad de geselecteerde waardes van een Fenotype multi in (Jim)
        // TODO: fix this 
        private void LoadSelectedValuesMulti()
        {
            SelectedFenotypeEigenschappen = SelectedPlantFenoTypeMulti.Eigenschap;
            SelectedFenoMultiMaand = SelectedPlantFenoTypeMulti.Maand;
            SelectedFenoTypesMulti = SelectedPlantFenoTypeMulti.Waarde;
        }

        //laat de verschillende waardes in op basis van de eigenschap dat geselecteerd is (Jim)
        private void ChangeEigenschappen()
        {
            switch (SelectedFenotypeEigenschappen.ToLower())
            {
                case "bladhoogte":
                    LoadHoogte();
                    break;
                case "bladkleur":
                    LoadKleur();
                    break;
                case "bloeihoogte":
                    LoadHoogte();
                    break;
                case "bloeikleur":
                    LoadKleur();
                    break;
            }
        }

        //verwijder de geselecteerde fenotypemulti van de listview (Jim)
        private void DeleteFenotypeMultiPlant()
        {
            if (SelectedPlantFenoTypeMulti is not null)
            {

                _plantenDao.RemoveMultiFenotype(SelectedPlantFenoTypeMulti);
            }
            else
            {
                MessageBox.Show("Gelieve een fenotype te selecteren uit de listview",
                   "Fout", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            
            LoadFenoTypesMultiPlant();
        }
    }
}
