using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Knights
{
    /// <summary>
    /// Interaction logic for KnightsControl.xaml
    /// </summary>
    public partial class KnightsControl : UserControl
    {
        public KnightsControl()
        {
            InitializeComponent();
            this.DataContextChanged += KnightsControl_DataContextChanged;
        }

        private void KnightsControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is Coord)
            {
                Grid.SetColumn(this, (DataContext as Coord).X);
                Grid.SetRow(this, (DataContext as Coord).Y);
            }
        }
    }
}
