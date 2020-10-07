using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Knights
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int step = 0;
        Border[,] labels2d;
        static public Dispatcher DDispatcher { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Sizer.Text = "5";
            Slider.Value = 10;
            Desk.DataContextChanged += Desk_DataContextChanged;
            DDispatcher = Dispatcher;
        }

        private void Desk_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(Desk.DataContext is Coord))
                return;

            Coord temp = Desk.DataContext as Coord;
            ((labels2d[temp.X, temp.Y].Child as Viewbox).Child as Label).Content = ++step;
        }

        private void Sizer_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Regex.IsMatch(e.Text, @"\d"))
                e.Handled = true;
        }

        private void Sizer_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!Regex.IsMatch((sender as TextBox).Text, @"\d{1,2}$"))
                (sender as TextBox).Text = Regex.Replace((sender as TextBox).Text, @"\D", "");
            if ((sender as TextBox).Text.Length > 2)
                (sender as TextBox).Text = (sender as TextBox).Text.Substring(0, 2);
            if ((sender as TextBox).Text == "")
                (sender as TextBox).Text = "5";
            if (int.Parse((sender as TextBox).Text) > 25)
                (sender as TextBox).Text = "25";
        }
        private void Start(object sender, MouseButtonEventArgs e)
        {
            Sizer.IsEnabled = false;

            Desk.Children.Clear();
            Desk.RowDefinitions.Clear();
            Desk.ColumnDefinitions.Clear();

            HorseSpace.RowDefinitions.Clear();
            HorseSpace.ColumnDefinitions.Clear();

            Keyboard.ClearFocus();
            var size = int.Parse(Sizer.Text);
            labels2d = new Border[size, size];
            step = 0;
            Task.Run(() => Dispatcher.Invoke(()=>
            {
                for (int i = 0; i < size; i++)
                {
                    HorseSpace.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    HorseSpace.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                    Desk.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    Desk.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                }
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        Label temp;
                        if ((i + j) % 2 == 0)
                        {
                            temp = new Label { Style = Resources["CellStyle"] as Style };
                            labels2d[j, i] = new Border { Child = new Viewbox { Child = temp, StretchDirection = StretchDirection.Both }, BorderThickness = new Thickness(0), Background = Brushes.Black};
                        }
                        else
                        {
                            temp = new Label { Style = Resources["CellStyle"] as Style };
                            labels2d[j, i] = new Border { Child = new Viewbox { Child = temp, StretchDirection = StretchDirection.Both }, BorderThickness = new Thickness(0), Background = Brushes.Transparent };
                        }

                        Desk.Children.Add(labels2d[j, i]);
                        Grid.SetRow(labels2d[j, i], i);
                        Grid.SetColumn(labels2d[j, i], j);
                    }
                }
                Sizer.IsEnabled = true;
            }));
            Horse.Visibility = Visibility.Visible;
        }

        private void Sizer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Keyboard.ClearFocus();
        }

        private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Keyboard.ClearFocus();
        }

        private void Sizer_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (int.Parse((sender as TextBox).Text) < 5)
                (sender as TextBox).Text = "5";
        }
    }
}
