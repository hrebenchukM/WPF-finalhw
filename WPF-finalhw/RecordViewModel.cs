using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;



namespace WPF_finalhw
{
    //теперь уже нету регистрации свойств зависимостей,а есть реализация интерфейса контракта INotifyPropertyChanged,который предполагает выставить подписку
    class RecordViewModel : INotifyPropertyChanged
    {
        private Record _record;
        public RecordViewModel(Record record)
        {

            _record = record;

        }



        public string Name
        {
            get
            {
                return _record.Name;
            }
            set
            {
                _record.Name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }

        public int Age
        {
            get
            {
                return _record.Age;
            }
            set
            {
                _record.Age = value;
                RaisePropertyChanged(nameof(Age));
            }
        }


        public string Address
        {
            get
            {
                return _record.Address;
            }
            set
            {
                _record.Address = value;
                RaisePropertyChanged(nameof(Address));
            }
        }

        public string Status
        {
            get
            {
                return _record.Status;
            }
            set
            {
                _record.Status = value;
                RaisePropertyChanged(nameof(Status));
            }
        }


        public string Email
        {
            get
            {
                return _record.Email;
            }
            set
            {
                _record.Email = value;
                RaisePropertyChanged(nameof(Email));
            }
        }




        public List<string> Skills
        {
            get
            {
                return _record.Skills;
            }
            set
            {
                _record.Skills = value;
                RaisePropertyChanged(nameof(Skills));
            }
        }



        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));//поскольку нет свойств зависимостей то требуется оповестить подписчика элементов управления при изменениях.
                                                                                      //Оповещаем впф инфраструктуру о том что ей нужно отреагировать и повызывать canExecute для всех команд,
                                                                                      //вернее оповестить подписчиков которые выполнили привязку
        }

        public event PropertyChangedEventHandler PropertyChanged;//выставляем подписку






    }
}
