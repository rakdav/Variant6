﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
using Variant6.Model;

namespace Variant6.View
{
    /// <summary>
    /// Логика взаимодействия для AddEditView.xaml
    /// </summary>
    public partial class AddEditView : Window, INotifyPropertyChanged
    {
        public MaterialViewModel material { get; set; }
        public List<string> List { get; set; }
        public List<string> Supp { get; set; }
        private List<string> suppList;
        public List<string> SuppList
        {
            get { return suppList; }
            set
            {
                suppList = value;
                OnPropertyChanged("SuppList");
            }
        }
        public AddEditView()
        {
            InitializeComponent();
            DataContext = this;
            SuppList = new List<string>();
            using (ModelDB dB = new ModelDB())
            {
                List = dB.MaterialType.Select(p => p.Title).ToList();
                Supp = dB.Supplier.Select(p => p.Title).ToList();
            }
            material = new MaterialViewModel();

           // ImagesLoad.Source = new BitmapImage(new Uri(@"/Resources/logo.png"));

            //ImagesLoad.Source = new BitmapImage(new Uri(@"..\..\Materials\9304.jpg", UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            DialogResult=false;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SuppList.Add(suppl.SelectedItem.ToString());
            supL.Items.Add(suppl.SelectedItem.ToString());
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SuppList.Remove(supL.SelectedItem.ToString());
            supL.Items.RemoveAt(supL.SelectedIndex);
        }

        private void btnImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                string fileName = openFileDialog.FileName;
                string newPath = @"..\..\Materials\" + openFileDialog.SafeFileName;
                FileInfo fileInf = new FileInfo(fileName);
                if (fileInf.Exists)
                {
                    fileInf.CopyTo(newPath, true);
                }
                Resources.Add(openFileDialog.SafeFileName, newPath);
                ImagesLoad.Source = new BitmapImage(new Uri(newPath, UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };
            }

        }
    }
}
