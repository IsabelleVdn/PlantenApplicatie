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
    // class and GUI (Davy)
    public class TabBeheerDadenViewModel : ViewModelBase
    {
        // button  commands (Davy)
        public ICommand AddManagementActCommand { get; set; }
        public ICommand EditManagementActCommand { get; set; }
        public ICommand RemoveManagementActCommand { get; set; }

        //private dao to use its methods (Davy)
        private readonly PlantenDao _plantenDao;

        // ObservableCollections to show the current plant managements (Davy)
        public ObservableCollection<BeheerMaand> BeheerMaanden { get; set; }

        // private variables (Davy)
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

        private Gebruiker _selectedGebruiker;
        private bool _IsManager;

        // constructor (Davy)
        public TabBeheerDadenViewModel(Plant selectedPlant, Gebruiker gebruiker)
        {
            
            SelectedGebruiker = gebruiker;
            SelectedPlant = selectedPlant;
            _plantenDao = PlantenDao.Instance;

            //buttoncommands
            AddManagementActCommand = new DelegateCommand(AddManagementAct);
            EditManagementActCommand = new DelegateCommand(EditManagementAct);
            RemoveManagementActCommand = new DelegateCommand(RemoveManagementAct);

            BeheerMaanden = new ObservableCollection<BeheerMaand>();

            //load in the values (Davy)
            LoadBeheerMaanden();
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

        // Getters and setters selected values (Davy)
        public Plant SelectedPlant
        {
            private get => _selectedPlant;
            set
            {
                _selectedPlant = value;
                OnPropertyChanged();
            }
        }   


        public BeheerMaand SelectedBeheerMaand
        {
            private get => _selectedBeheerMaand;
            set
            {
                _selectedBeheerMaand = value;
                LoadSelectedValues();
                OnPropertyChanged();
            }
        }

        public string TextInputBeheerdaad
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

        // show the different managements for the selected plants (Davy)
        public void LoadBeheerMaanden()
        {
            var beheermaanden = _plantenDao.GetBeheerMaanden(SelectedPlant);

            BeheerMaanden.Clear();

            foreach (var beheermaand in beheermaanden)
            {
                BeheerMaanden.Add(beheermaand);
            }
        }

        //make a managemant act for the current plant (Davy)
        private void AddManagementAct()
        {
            BeheerMaand beheerMaand = new BeheerMaand();
            beheerMaand.PlantId = SelectedPlant.PlantId;
            beheerMaand.Beheerdaad = TextInputBeheerdaad;
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

            _plantenDao.CreateBeheerMaand(beheerMaand);

            


            //reload the listview
            LoadBeheerMaanden();
        }

        //Edit a selected management (Davy)
        private void EditManagementAct()
        {
            BeheerMaand beheerMaand = SelectedBeheerMaand; 

            if (beheerMaand != null)
            {
                beheerMaand.PlantId = SelectedBeheerMaand.PlantId;
                beheerMaand.Beheerdaad = TextInputBeheerdaad;
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

                _plantenDao.EditBeheerMaand(beheerMaand);
            } else
            {
                MessageBox.Show("Gelieve eerst een beheersdaad te selecteren.");
            }


            LoadBeheerMaanden();
        }

        //When a management act gets selected the current values changes to the selected values (Jim)
        private void LoadSelectedValues()
        {
            var beheerMaand = SelectedBeheerMaand;

            TextInputBeheerdaad = beheerMaand.Beheerdaad;
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


        //delete management act (Davy)
        private void RemoveManagementAct()
        {
            BeheerMaand beheerMaand = SelectedBeheerMaand;

            //delete the selected management act otherwise if there is none user gets a notification to select one       
            if (SelectedBeheerMaand != null)
            {
                _plantenDao.RemoveBeheerMaand(beheerMaand);
            } else
            {
                MessageBox.Show("Gelieve eerst een beheersdaad te selecteren uit de lijst.");
            }

            //reload the list
            LoadBeheerMaanden();
        }
    }
}
