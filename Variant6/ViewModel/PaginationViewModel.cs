using Haley.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Variant6.Model;

namespace Variant6.ViewModel
{
    internal class PaginationViewModel:ChangeNotifier
    {
        #region Properties
        private PaginationModel pagination;
        public PaginationModel Pagination
        {
            get { return pagination; }
            set 
            { 
                pagination = value; 
                OnPropertyChanged(); 
            }
        }
        #endregion

        #region Command Properties
        public ICommand Cmd_change_page { get; set; }
        #endregion
        
        #region CommandMethods
        private void changePage(string obj)
        {
            try 
            { 
                int parameter=int.Parse(obj);
                int newpage=pagination.CurrentPage;
                switch(parameter)
                {
                    case 0:
                        newpage--;
                        if (newpage < 1) newpage = 1;
                        break;
                    case 1:
                        newpage++;
                        if(newpage>pagination.TotalPages)
                            newpage=pagination.TotalPages;
                        break;
                }
                pagination.CurrentPage=newpage;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void calculatePagination()
        {
            try
            {
                if (pagination.ItemsPerPage == 0) pagination.ItemsPerPage = 1;
                if(pagination.ItemsPerPage>pagination.TotalItems)
                    pagination.ItemsPerPage = pagination.TotalItems;
                pagination.TotalPages=pagination.TotalItems/pagination.ItemsPerPage;
                pagination.CurrentPage = 1;
            }
            catch(Exception)
            {

            }
        }
        #endregion
        #region Methods
        private void initiation()
        {
            Cmd_change_page = new DelegateCommand<string>(changePage, null);
        }
        public void seed(PaginationModel pag)
        {
            pagination = pag;
        }

        public PaginationViewModel()
        {
            initiation();
        }
        #endregion

    }
}
