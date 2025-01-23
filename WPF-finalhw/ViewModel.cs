using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;



namespace WPF_finalhw
{
    //теперь уже нету регистрации свойств зависимостей,а есть реализация интерфейса контракта INotifyPropertyChanged,который предполагает выставить подписку
    class ViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<RecordViewModel> RecordsList { get; set; }
        public ObservableCollection<SkillViewModel> SkillsList { get; set; }

        public ViewModel()
        {
            RecordsList = new ObservableCollection<RecordViewModel>();

            SkillsList = new ObservableCollection<SkillViewModel>
            {
            new SkillViewModel(new Skill("English")),
            new SkillViewModel(new Skill("JavaScript")),
            new SkillViewModel(new Skill("HTML+CSS")),
            new SkillViewModel(new Skill("React")),
            new SkillViewModel(new Skill("Angular"))
            };
        }



        private string currentName;
        public string CurrentName
        {
            get { return currentName; }
            set
            {
                currentName = value;
                RaisePropertyChanged(nameof(CurrentName));
            }
        }

        private int currentAge;
        public int CurrentAge
        {
            get { return currentAge; }
            set
            {
                currentAge = value;
                RaisePropertyChanged(nameof(CurrentAge));
            }
        }



        private string currentAddress;
        public string CurrentAddress
        {
            get { return currentAddress; }
            set
            {
                currentAddress = value;
                RaisePropertyChanged(nameof(CurrentAddress));
            }
        }

        private string currentStatus;
        public string CurrentStatus
        {
            get { return currentStatus; }
            set
            {
                currentStatus = value;
                RaisePropertyChanged(nameof(CurrentStatus));
            }
        }




        private string currentEmail;
        public string CurrentEmail
        {
            get { return currentEmail; }
            set
            {
                currentEmail = value;
                RaisePropertyChanged(nameof(CurrentEmail));
            }
        }





        private int index_selected_listbox = -1;

        public int Index_selected_listbox
        {
            get { return index_selected_listbox; }
            set
            {
                index_selected_listbox = value;

                RaisePropertyChanged(nameof(Index_selected_listbox));
            
            }
        }







        private DelegateCommand _AddCommand;
        public ICommand AddCommand
        {
            get
            {
                if (_AddCommand == null)
                {
                    _AddCommand = new DelegateCommand(Add, CanAdd);
                }
                return _AddCommand;
            }
        }
        private void Add(object o)
        {
            var selectedSkills = SkillsList
            .Where(skill => skill.IsSelected)
            .Select(skill => new Skill(skill.Name)) 
            .ToList();

            RecordsList.Add(new RecordViewModel(new Record(CurrentName, CurrentAge, CurrentAddress, CurrentStatus, CurrentEmail, selectedSkills)));
        }

        private bool CanAdd(object o)
        {
            if (CurrentName == "" || CurrentAge < 18 || CurrentAddress == "" || CurrentStatus == "" || CurrentEmail == "")
                return false;
            return true;
        }








        private DelegateCommand _ViewCommand;
        public ICommand ViewCommand
        {
            get
            {
                if (_ViewCommand == null)
                {
                    _ViewCommand = new DelegateCommand(View, CanView);
                }
                return _ViewCommand;
            }
        }
        private void View(object o)
        {

            RecordViewModel selectedRecord = RecordsList[index_selected_listbox];
            ResumeView resumeV = new ResumeView
            {
                DataContext = selectedRecord 
            };
            resumeV.ShowDialog();

        }

        private bool CanView(object o)
        {
            return Index_selected_listbox >= 0 && Index_selected_listbox < RecordsList.Count;
        }
















        private DelegateCommand _SaveCommand;
        public ICommand SaveCommand
        {
            get
            {
                if (_SaveCommand == null)
                {
                    _SaveCommand = new DelegateCommand(Save, CanSave);
                }
                return _SaveCommand;
            }
        }
        private void Save(object o)
        {

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            if (saveFileDialog1.ShowDialog() == true)
            {


                var lines = new List<string>();
                foreach (RecordViewModel recordViewModel in RecordsList)
                {
                    string skills = string.Join(",", recordViewModel.Skills.Select(s => s.Name)); 

                    lines.Add(recordViewModel.Name + ";"
                        + recordViewModel.Age + ";"
                        + recordViewModel.Address + ";"
                        + recordViewModel.Status + ";"
                        + recordViewModel.Email + ";"
                        + skills);
                }

                File.WriteAllLines(saveFileDialog1.FileName, lines);

            }

        }

        private bool CanSave(object o)
        {
            return RecordsList.Count > 0;
        }





        private DelegateCommand _OpenCommand;
        public ICommand OpenCommand
        {
            get
            {
                if (_OpenCommand == null)
                {
                    _OpenCommand = new DelegateCommand(Open, CanOpen);
                }
                return _OpenCommand;
            }
        }
        private void Open(object o)
        {

            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            if (openFileDialog1.ShowDialog() == true)
            {

                var lines = File.ReadAllLines(openFileDialog1.FileName);
                RecordsList.Clear();
                foreach (var line in lines)
                {
                    var parts = line.Split(';');
                    if (parts.Length == 6)
                    {
                        int age;
                        if (int.TryParse(parts[1], out age)) 
                        {

                            List<Skill> skills = parts[5].Split(',').Select(s => new Skill(s)).ToList();

                            Record newRecord = new Record(parts[0], age, parts[2], parts[3], parts[4], skills);
                            RecordsList.Add(new RecordViewModel(newRecord));

                        }
                    }

                }
            }

        }

        private bool CanOpen(object o)
        {
            return true;
        }




        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));//поскольку нет свойств зависимостей то требуется оповестить подписчика элементов управления при 
                                                                                      //Оповещаем впф инфраструктуру о том что ей нужно отреагировать и повызывать canExecute для всех команд, изменениях.  
                                                                                      //вернее оповестить подписчиков которые выполнили привязку
        }

        public event PropertyChangedEventHandler PropertyChanged;//выставляем подписку






    }
}
