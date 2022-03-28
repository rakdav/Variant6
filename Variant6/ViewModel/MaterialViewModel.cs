using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Variant6.Model
{
    public class MaterialViewModel : INotifyPropertyChanged
    {
        private string title;
        private int countInPack;
        private string unit;
        private double? countInStock;
        private double minCount;
        private string description;
        private decimal cost;
        private string image;
        private string materialTypeID;
        private string suppliers;
        public string Title {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }
        public int CountInPack
        {
            get { return countInPack; }
            set
            {
                countInPack = value;
                OnPropertyChanged("CountInPack");
            }
        }
        public string Unit {
            get { return unit; }
            set
            {
                unit = value;
                OnPropertyChanged("Unit");
            }
        }
        public double? CountInStock 
        {
            get { return countInStock; }
            set
            {
                countInStock = value;
                OnPropertyChanged("Unit");
            }
        }
        public double MinCount 
        {
            get { return minCount; }
            set
            {
                minCount = value;
                OnPropertyChanged("MinCount");
            }
        }
        public string Description 
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }
        public decimal Cost 
        {
            get { return cost; }
            set
            {
                cost = value;
                OnPropertyChanged("Cost");
            }
        }
        public string Image {
            get { return image; }
            set
            {
                image = value;
                OnPropertyChanged("Image");
            }
        }
        public string MaterialTypeID
        {
            get { return materialTypeID; }
            set
            {
                materialTypeID = value;
                OnPropertyChanged("MaterialTypeID");
            }
        }
        public string Suppliers
        {
            get { return suppliers; }
            set
            {
                suppliers = value;
                OnPropertyChanged("Suppliers");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
