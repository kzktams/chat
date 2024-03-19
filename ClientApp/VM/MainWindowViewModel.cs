
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Workshop05.ClientApp;

namespace ClientApp.VM
{
    public class MainWindowViewModel: ObservableRecipient
    {

        public string Sender { get; set; } = "";

        private string inputText = "";

        public string InputText
        {
            get { return inputText; }
            set { 
                inputText = value;
                OnPropertyChanged(); 
                (SendCommand as RelayCommand).NotifyCanExecuteChanged(); }
        }


        public RestCollection<Message> Messages { get; set; }

        public ICommand SendCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }


        public MainWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Messages = new RestCollection<Message>("http://localhost:44869/", "message", "hub");
                SendCommand = new RelayCommand(() => {
                    Messages.Add(new Message() { Sender = this.Sender, SentMessage = InputText });
                    InputText = "";
                }, () => { return InputText != null && InputText != ""; });

                Messages.CollectionChanged += Messages_CollectionChanged;

                
            }
        }

        private void Messages_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.Messenger.Send("Changed", "ChatChanged");
        }

    }
}
