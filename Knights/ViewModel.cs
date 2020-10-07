using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Commands;

namespace Knights
{
    class ViewModel : INotifyPropertyChanged
    {
        int size = 5;
        Coord coord;
        PathFinder finder;
        bool isStartAvailable = false;
        bool isDrawCellsAvailable = true;
        public Coord Coord
        {
            get => coord;
            set
            {
                coord = value;
                OnPropertyChanged();
            }
        }
        public string Size
        {
            get => size + "";
            set
            {
                if (value == "")
                    value = "5";
                size = int.Parse(value);
                OnPropertyChanged();
            }
        }
        public bool IsStartAvailable
        {
            get => isStartAvailable;
            set
            {
                isStartAvailable = value;
                OnPropertyChanged();
            }
        }
        public bool IsDrawCellsAvailable
        {
            get => isDrawCellsAvailable;
            set
            {
                isDrawCellsAvailable = value;
                OnPropertyChanged();
            }
        }
        public int Pace { get; set; } = 1;
        public ICommand Start { get; set; }
        public ICommand DrawCells { get; set; }
        public ViewModel()
        {
            Start = new Command(StartMethod);
            DrawCells = new Command(DrawCellsMethod);
        }
        private void DrawCellsMethod(object none)
        {
            IsStartAvailable = true;
        }
        private void StartMethod(object none)
        {
            finder = new PathFinder(new Random().Next(size), new Random().Next(size));
            Task.Run(()=>
            {
                IsStartAvailable = false;
                IsDrawCellsAvailable = false;

                foreach (var coord in finder.GetPath(size))
                {
                    MainWindow.DDispatcher.Invoke(()=> Coord = coord); 
                    Thread.Sleep(1000 / Pace);
                }

                IsDrawCellsAvailable = true;
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string prop ="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
