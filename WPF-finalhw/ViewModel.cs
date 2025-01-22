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

        public ViewModel()
        {
            RecordsList = new ObservableCollection<RecordViewModel>();
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


        private List<string> currentSkills = new List<string>();
        public List<string> CurrentSkills
        {
            get { return currentSkills; }
            set
            {
                currentSkills = value;
                RaisePropertyChanged(nameof(CurrentSkills));
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
                if (index_selected_listbox >= 0 && index_selected_listbox < RecordsList.Count)
                {
                    RecordViewModel selectedRecord = RecordsList[index_selected_listbox];
                    CurrentName = selectedRecord.Name;
                    CurrentAge = selectedRecord.Age;
                    CurrentAddress = selectedRecord.Address;
                    CurrentStatus = selectedRecord.Status;
                    CurrentEmail = selectedRecord.Email;
                    CurrentSkills = new List<string>(selectedRecord.Skills);
                }
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
            Record newRecord = new Record();
            newRecord.Name = CurrentName;
            newRecord.Age = CurrentAge;
            newRecord.Address = CurrentAddress;
            newRecord.Status = CurrentStatus;
            newRecord.Email = CurrentEmail;
            newRecord.Skills = new List<string> (CurrentSkills);
            RecordsList.Add(new RecordViewModel(newRecord));
        }

        private bool CanAdd(object o)
        {
            //if (CurrentName == "" || CurrentAge == 0 || CurrentAddress == "" || CurrentStatus == "" || CurrentEmail == "" )
            //    return false;
            return true;
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

                    lines.Add(recordViewModel.Name + ";" + recordViewModel.Age + ";" + recordViewModel.Address + ";" + recordViewModel.Status + ";" + recordViewModel.Email + ";" +
                    string.Join(",", recordViewModel.Skills));
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
                    if (parts.Length == 5)
                    {
                        int age;
                        if (int.TryParse(parts[1], out age)) 
                        {
                            RecordsList.Add(new RecordViewModel(
                             new Record(parts[0], 
                             age, parts[2], 
                             parts[3], 
                             parts[4],
                             new List<string>(parts[5].Split(',')))));
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
