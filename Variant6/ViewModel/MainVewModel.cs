using Haley.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Variant6.Model;
using Variant6.View;

namespace Variant6.ViewModel
{
    internal class MainVewModel : ChangeNotifier
    {
        #region Properties
        private PaginationViewModel topPagination;
        public PaginationViewModel TopPagination
        {
            get { return topPagination; }
            set
            {
                topPagination = value;
                OnPropertyChanged();
            }
        }
        private PaginationViewModel bottomPagination;
        public PaginationViewModel BottomPagination
        {
            get { return bottomPagination; }
            set
            {
                bottomPagination = value;
                OnPropertyChanged();
            }
        }
        private List<MaterialViewModel> fullList;
        public List<MaterialViewModel> FullList
        {
            get { return fullList; }
            set
            {
                fullList = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<MaterialViewModel> demo_list;
        public ObservableCollection<MaterialViewModel> DemoList
        {
            get { return demo_list; }
            set
            {
                demo_list = value;
                OnPropertyChanged();
            }
        }
        public List<string> Filter { get; set; }
        private string selectedCount;
        public string SelectedCount
        {
            get { return selectedCount; }
            set
            {
                selectedCount = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public MainVewModel()
        {
            demo_list = new ObservableCollection<MaterialViewModel>();
            fullList = new List<MaterialViewModel>();
            using (ModelDB db = new ModelDB())
            {
                var query = from p in db.Material
                            select new
                            {
                                Title = p.Title,
                                CountInPack = p.CountInPack,
                                Unit = p.Unit,
                                CountInstock = p.CountInStock,
                                MinCount = p.MinCount,
                                Desc = p.Description,
                                Cost = p.Cost,
                                Image = p.Image,
                                MaterialType = p.MaterialType.Title
                            };
                foreach (var item in query)
                {
                    MaterialViewModel ourMaterial = new MaterialViewModel();
                    ourMaterial.Title = item.Title;
                    ourMaterial.CountInPack = item.CountInPack;
                    ourMaterial.Unit = item.Unit;
                    ourMaterial.CountInStock = item.CountInstock;
                    ourMaterial.MinCount = item.MinCount;
                    ourMaterial.Description = item.Desc;
                    ourMaterial.Cost = item.Cost;
                    if (item.Image != "")
                        ourMaterial.Image = item.Image;
                    else
                        ourMaterial.Image = @"/Materials/picture.png";
                    ourMaterial.MaterialTypeID = item.MaterialType.ToString();
                    List<MaterialSupplier> sup = db.MaterialSupplier.Where(s => s.MaterialID.Equals(item.Title)).ToList();
                    string res = "";
                    foreach (MaterialSupplier supp in sup)
                        res += supp.SupplierID + ",";
                    if (res.Length != 0)
                        res = res.Substring(0, res.Length - 2);
                    ourMaterial.Suppliers = res;
                    FullList.Add(ourMaterial);
                }
            }
            topPagination = new PaginationViewModel();
            bottomPagination = new PaginationViewModel();
            PaginationModel topModel = new PaginationModel(fullList.Count(), 15);
            PaginationModel bottomModel = new PaginationModel(2500, 30);
            topPagination.seed(topModel);
            bottomPagination.seed(bottomModel);
            Filter = new List<string>();
            Filter.Add("Наименование");
            Filter.Add("Остаток на складе");
            Filter.Add("Стоимость");
            topPagination.Pagination.PropertyChanged += Pagination_PropertyChanged;
            processPage(null);
        }

        private void Pagination_PropertyChanged(object Sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentPage")
            {
                processPage(null);
            }
        }
        private void processPage(object param)
        {
            try
            {
                List<MaterialViewModel> page_list = new List<MaterialViewModel>();
                int start_counting = (topPagination.Pagination.CurrentPage - 1) * topPagination.Pagination.ItemsPerPage;
                int ending_count = start_counting + topPagination.Pagination.ItemsPerPage;
                page_list = FullList.Skip(start_counting).
                    Take(topPagination.Pagination.ItemsPerPage).ToList();
                DemoList = new ObservableCollection<MaterialViewModel>(page_list);
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        #region Command
        private RelayCommand filterCommand;
        public RelayCommand FilterCommand
        {
            get
            {
                return filterCommand ??
                  (filterCommand= new RelayCommand(obj =>
                  {
                      if(SelectedCount.Equals("Наименование"))
                        FullList=FullList.OrderBy(p => p.Title).ToList();
                      if (SelectedCount.Equals("Остаток на складе"))
                          FullList = FullList.OrderBy(p => p.CountInStock).ToList();
                      if (SelectedCount.Equals("Стоимость"))
                          FullList=FullList.OrderBy(p => p.Cost).ToList();
                      processPage(null);
                  }));
            }
        }
        #endregion
    }
}
