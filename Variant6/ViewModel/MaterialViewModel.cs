using Haley.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Variant6.Model
{
    public class MaterialViewModel : ChangeNotifier
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
        private bool isChecked=false;
        public string Title {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }
        public int CountInPack
        {
            get { return countInPack; }
            set
            {
                countInPack = value;
                OnPropertyChanged();
            }
        }
        public string Unit {
            get { return unit; }
            set
            {
                unit = value;
                OnPropertyChanged();
            }
        }
        public double? CountInStock 
        {
            get { return countInStock; }
            set
            {
                countInStock = value;
                OnPropertyChanged();
            }
        }
        public double MinCount 
        {
            get { return minCount; }
            set
            {
                minCount = value;
                OnPropertyChanged();
            }
        }
        public string Description 
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged("");
            }
        }
        public decimal Cost 
        {
            get { return cost; }
            set
            {
                cost = value;
                OnPropertyChanged();
            }
        }
        public string Image {
            get { return image; }
            set
            {
                image = value;
                OnPropertyChanged();
            }
        }
        public string MaterialTypeID
        {
            get { return materialTypeID; }
            set
            {
                materialTypeID = value;
                OnPropertyChanged();
            }
        }
        public string Suppliers
        {
            get { return suppliers; }
            set
            {
                suppliers = value;
                OnPropertyChanged();
            }
        }

        public bool IsChecked
        {
            get { return IsChecked; }
            set
            {
                isChecked = value;
                OnPropertyChanged();
            }
        }
    }
}
