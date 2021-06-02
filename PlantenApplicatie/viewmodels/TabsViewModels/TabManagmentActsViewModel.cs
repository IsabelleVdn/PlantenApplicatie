﻿using PlantenApplicatie.Data;
using PlantenApplicatie.Domain;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace PlantenApplicatie.viewmodels
{
    // klasse BeheerDadenViewModel (Davy)
    public class TabManagmentActsViewModel : ViewModelBase
    {
        // knop commando's (Davy)
        public ICommand AddManagementActCommand { get; set; }
        public ICommand EditManagementActCommand { get; set; }
        public ICommand RemoveManagementActCommand { get; set; }

        // Hiermee kunnen we de data opvragen aan de databank.
        private readonly PlantenDao _plantenDao;

        // ObservableCollection om de beheermaanden weer te geven (Davy)
        public ObservableCollection<BeheerMaand> BeheerMaanden { get; set; }

        // private variabelen Davy
        private Plant _selectedPlant;
        private BeheerMaand _selectedBeheerMaand;
        private string _textInputBeheerdaad;
        private string _textInputDescription;
        private bool _isCheckedJanuary;
        private bool _isCheckedFebruary;
        private bool _isCheckedMarch;
        private bool _isCheckedApril;
        private bool _isCheckedMay;
        private bool _isCheckedJune;
        private bool _isCheckedJuly;
        private bool _isCheckedAugust;
        private bool _isCheckedSeptember;
        private bool _isCheckedOctober;
        private bool _isCheckedNovember;
        private bool _isCheckedDecember;

        private User _selectedGebruiker;
        private bool _IsManager;

        // constructor (Davy)
        public TabManagmentActsViewModel(Plant selectedPlant, User gebruiker)
        {
            SelectedUser = gebruiker;
            SelectedPlant = selectedPlant;
            _plantenDao = PlantenDao.Instance;

            AddManagementActCommand = new DelegateCommand(AddManagementAct);
            EditManagementActCommand = new DelegateCommand(EditManagementAct);
            RemoveManagementActCommand = new DelegateCommand(RemoveManagementAct);

            BeheerMaanden = new ObservableCollection<BeheerMaand>();

            LoadManagmentMonths();
            UserRole();
        }
        public bool IsManager
        {
            get => _IsManager;
            set
            {
                _IsManager = value;
                OnPropertyChanged("IsManager");
            }
        }


        //controleer welke rol de gebruiker heeft
        private void UserRole()
        {
            switch (SelectedUser.Rol.ToLower())
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
        public User SelectedUser
        {
            private get => _selectedGebruiker;
            set
            {
                _selectedGebruiker = value;
                OnPropertyChanged();
            }
        }

        // Getters and setters selected waardes (Davy)
        public Plant SelectedPlant
        {
            private get => _selectedPlant;
            set
            {
                _selectedPlant = value;
                OnPropertyChanged();
            }
        }   

        public BeheerMaand SelectedManagementMonth
        {
            private get => _selectedBeheerMaand;
            set
            {
                _selectedBeheerMaand = value;
                LoadSelectedValues();
                OnPropertyChanged();
            }
        }

        public string TextInputManagementAct
        {
            get { return _textInputBeheerdaad; }
            set
            {
                _textInputBeheerdaad = value;
                OnPropertyChanged();
            }
        }
        public string TextInputDescription
        {
            get { return _textInputDescription; }
            set
            {
                _textInputDescription = value;
                OnPropertyChanged();
            }
        }

        public bool IsCheckedJanuary
        {
            get { return _isCheckedJanuary; }
            set
            {
                if (_isCheckedJanuary == value)
                {
                    return;
                }

                _isCheckedJanuary = value;
                OnPropertyChanged();

            }
        }

        public bool IsCheckedFebruary
        {
            get { return _isCheckedFebruary; }
            set
            {
                if (_isCheckedFebruary == value)
                {
                    return;
                }

                _isCheckedFebruary = value;
                OnPropertyChanged();

            }
        }
        public bool IsCheckedMarch
        {
            get { return _isCheckedMarch; }
            set
            {
                if (_isCheckedMarch == value)
                {
                    return;
                }

                _isCheckedMarch = value;
                OnPropertyChanged();

            }
        }
        public bool IsCheckedApril
        {
            get { return _isCheckedApril; }
            set
            {
                if (_isCheckedApril == value)
                {
                    return;
                }

                _isCheckedApril = value;
                OnPropertyChanged();

            }
        }
        public bool IsCheckedMay
        {
            get { return _isCheckedMay; }
            set
            {
                if (_isCheckedMay == value)
                {
                    return;
                }

                _isCheckedMay = value;
                OnPropertyChanged();

            }
        }
        public bool IsCheckedJune
        {
            get { return _isCheckedJune; }
            set
            {
                if (_isCheckedJune == value)
                {
                    return;
                }

                _isCheckedJune = value;
                OnPropertyChanged();

            }
        }
        public bool IsCheckedJuly
        {
            get { return _isCheckedJuly; }
            set
            {
                if (_isCheckedJuly == value)
                {
                    return;
                }

                _isCheckedJuly = value;
                OnPropertyChanged();

            }
        }
        public bool IsCheckedAugust
        {
            get { return _isCheckedAugust; }
            set
            {
                if (_isCheckedAugust == value)
                {
                    return;
                }

                _isCheckedAugust = value;
                OnPropertyChanged();

            }
        }
        public bool IsCheckedSeptember
        {
            get { return _isCheckedSeptember; }
            set
            {
                if (_isCheckedSeptember == value)
                {
                    return;
                }

                _isCheckedSeptember = value;
                OnPropertyChanged();

            }
        }
        public bool IsCheckedOctober
        {
            get { return _isCheckedOctober; }
            set
            {
                if (_isCheckedOctober == value)
                {
                    return;
                }

                _isCheckedOctober = value;
                OnPropertyChanged();

            }
        }
        public bool IsCheckedNovember
        {
            get { return _isCheckedNovember; }
            set
            {
                if (_isCheckedNovember == value)
                {
                    return;
                }

                _isCheckedNovember = value;
                OnPropertyChanged();

            }
        }
        public bool IsCheckedDecember
        {
            get { return _isCheckedDecember; }
            set
            {
                if (_isCheckedDecember == value)
                {
                    return;
                }

                _isCheckedDecember = value;
                OnPropertyChanged();
            }
        }

        // geef de BeheerMaanden weer in de listview (Davy)
        public void LoadManagmentMonths()
        {
            var beheermaanden = _plantenDao.GetManagementActs(SelectedPlant);

            BeheerMaanden.Clear();

            foreach (var beheermaand in beheermaanden)
            {
                BeheerMaanden.Add(beheermaand);
            }
        }

        // maak een beheerdaad aan (Davy)
        private void AddManagementAct()
        {
            BeheerMaand beheerMaand = new BeheerMaand();
            beheerMaand.PlantId = SelectedPlant.PlantId;
            beheerMaand.Beheerdaad = TextInputManagementAct;
            beheerMaand.Omschrijving = TextInputDescription;
            beheerMaand.Jan = IsCheckedJanuary;
            beheerMaand.Feb = IsCheckedFebruary;
            beheerMaand.Mrt = IsCheckedMarch;
            beheerMaand.Apr = IsCheckedApril;
            beheerMaand.Mei = IsCheckedMay;
            beheerMaand.Jun = IsCheckedJune;
            beheerMaand.Jul = IsCheckedJuly;
            beheerMaand.Aug = IsCheckedAugust;
            beheerMaand.Sept = IsCheckedSeptember;
            beheerMaand.Okt = IsCheckedOctober;
            beheerMaand.Nov = IsCheckedNovember;
            beheerMaand.Dec = IsCheckedDecember;

            _plantenDao.CreateManagementAct(beheerMaand);

            


            // geef de aangepaste lijst terug weer 
            LoadManagmentMonths();
        }

        // wijzig een beheerdaad (Davy)
        private void EditManagementAct()
        {
            BeheerMaand beheerMaand = SelectedManagementMonth; 

            if (beheerMaand != null)
            {
                beheerMaand.PlantId = SelectedManagementMonth.PlantId;
                beheerMaand.Beheerdaad = TextInputManagementAct;
                beheerMaand.Omschrijving = TextInputDescription;
                beheerMaand.Jan = IsCheckedJanuary;
                beheerMaand.Feb = IsCheckedFebruary;
                beheerMaand.Mrt = IsCheckedMarch;
                beheerMaand.Apr = IsCheckedApril;
                beheerMaand.Mei = IsCheckedMay;
                beheerMaand.Jun = IsCheckedJune;
                beheerMaand.Jul = IsCheckedJuly;
                beheerMaand.Aug = IsCheckedAugust;
                beheerMaand.Sept = IsCheckedSeptember;
                beheerMaand.Okt = IsCheckedOctober;
                beheerMaand.Nov = IsCheckedNovember;
                beheerMaand.Dec = IsCheckedDecember;

                _plantenDao.EditManagementAct(beheerMaand);
            } else
            {
                MessageBox.Show("Gelieve eerst een beheersdaad te selecteren.");
            }


            LoadManagmentMonths();
        }

        //Wanneer er een beheersdaad geselecteerd word, dan verandert de waardes naar de geselecteerde waardes (Jim)
        private void LoadSelectedValues()
        {
            var beheerMaand = SelectedManagementMonth;

            TextInputManagementAct = beheerMaand.Beheerdaad;
            TextInputDescription = beheerMaand.Omschrijving;
            IsCheckedJanuary = beheerMaand.Jan ?? false;
            IsCheckedFebruary = beheerMaand.Feb ?? false;
            IsCheckedMarch = beheerMaand.Mrt ?? false;
            IsCheckedApril = beheerMaand.Apr ?? false;
            IsCheckedMay = beheerMaand.Mei ?? false;
            IsCheckedJune = beheerMaand.Jun ?? false;
            IsCheckedJuly = beheerMaand.Jul ?? false;
            IsCheckedAugust = beheerMaand.Aug ?? false;
            IsCheckedSeptember = beheerMaand.Sept ?? false;
            IsCheckedOctober = beheerMaand.Okt ?? false;
            IsCheckedNovember = beheerMaand.Nov ?? false;
            IsCheckedDecember = beheerMaand.Dec ?? false;
        }


        // verwijder beheerdaad (Davy)
        private void RemoveManagementAct()
        {
            // toewijzen object BeheerMaand aan geselecteerd object BeheerMaand uit listview
            BeheerMaand beheerMaand = SelectedManagementMonth;

            // ken een string waarde toe uit methode verwijder BeheerMaand uit database            
            if (SelectedManagementMonth != null)
            {
                _plantenDao.RemoveManagementAct(beheerMaand);
            } else
            {
                MessageBox.Show("Gelieve eerst een beheersdaad te selecteren uit de lijst.");
            }

            // toon opnieuw de listview met lijst BeheerMaanden
            LoadManagmentMonths();
        }
    }
}
