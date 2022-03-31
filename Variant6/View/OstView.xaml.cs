using Haley.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Variant6.View
{
    /// <summary>
    /// Логика взаимодействия для OstView.xaml
    /// </summary>
    public partial class OstView : Window, INotifyPropertyChanged
    {
        private int maxCount;
        public int MaxCount
        {
            get { return maxCount; }
            set
            {
                maxCount = value;
                OnPropertyChanged("MaxCount");
            }
        }
        public OstView(int m)
        {
            MaxCount = m;
            InitializeComponent();
            DataContext= this;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
