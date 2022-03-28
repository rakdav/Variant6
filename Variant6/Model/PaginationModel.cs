using Haley.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Variant6.Model
{
    internal class PaginationModel:ChangeNotifier
    {
        public int MaxItemsPerPage { get; set; }
        private int totalItems;
        public int TotalItems 
        { 
            get { return totalItems;}
            set 
            {
                totalItems = value; 
                OnPropertyChanged();
            } 
        }
        private int itemsPerPage;
        public int ItemsPerPage
        {
            get { return itemsPerPage; }
            set
            {
                itemsPerPage = value;
                OnPropertyChanged();
            }
        }
        private int currentPage;
        public int CurrentPage 
        {
            get { return currentPage; }
            set 
            { 
                currentPage = value; 
                OnPropertyChanged(); 
            }
        }
        private int totalPages;
        public int TotalPages
        {
            get { return totalPages; }
            set
            {
                totalPages = value;
                OnPropertyChanged();
            }
        }
        private void Init(int totalCount,int pageCount)
        {
            totalItems=totalCount;
            itemsPerPage=pageCount;
            int reminder_check = 0;
            Math.DivRem(totalItems, itemsPerPage,out reminder_check);
            totalPages = totalItems / itemsPerPage;
            if (reminder_check != 0) totalPages++;
            currentPage = 1;
            MaxItemsPerPage = 15;
        }
        public PaginationModel(int totalCount,int pageCount)
        {
            Init(totalCount,pageCount);
        }
    }
}
