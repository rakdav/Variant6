using Haley.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Variant6.Model;
using Variant6.View;

namespace Variant6.ViewModel
{
    internal class MainVewModel : ChangeNotifier
    {
        #region Fields
        private bool ascdesc=true;
        #endregion
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
        private List<MaterialViewModel> FullList;
        private List<MaterialViewModel> AllList;

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
        public List<string> Materials { get; set; }
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
        private string selectedMaterial;
        public string SelectedMaterial
        {
            get { return selectedMaterial; }
            set
            {
                selectedMaterial = value;
                OnPropertyChanged();
            }
        }

        private string descAsc;
        public string DescAsc
        {
            get { return descAsc; }
            set
            {
                descAsc = value;
                OnPropertyChanged();
            }
        }
        private bool btnVisiable;
        public bool BtnVisiable
        {
            get { return btnVisiable; }
            set
            {
                btnVisiable = value;
                OnPropertyChanged();
            }
        }
        private string textFilter;
        public string TextFilter
        {
            get { return textFilter; }
            set
            {
                textFilter = value;
                OnPropertyChanged();
            }
        }
        private Visibility visible;
        public Visibility Visible
        {
            get { return visible; }
            set
            {
                visible = value;
                OnPropertyChanged();
            }
        }

        private MaterialViewModel selectedItems;
        public MaterialViewModel SelectedItems
        {
            get { return selectedItems; }
            set { selectedItems = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public MainVewModel()
        {
            DemoList = new ObservableCollection<MaterialViewModel>();
            FullList = new List<MaterialViewModel>();
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
                AllList = new List<MaterialViewModel>(FullList);
                List<MaterialType> materialTypes = db.MaterialType.ToList();
                Materials = new List<string>();
                Materials.Add("Все");
                foreach (MaterialType t in materialTypes) Materials.Add(t.Title);
            }
            topPagination = new PaginationViewModel();
            bottomPagination = new PaginationViewModel();
            PaginationModel topModel = new PaginationModel(FullList.Count(), 15);
            PaginationModel bottomModel = new PaginationModel(2500, 30);
            topPagination.seed(topModel);
            bottomPagination.seed(bottomModel);
            Filter = new List<string>();
            Filter.Add("Наименование");
            Filter.Add("Остаток на складе");
            Filter.Add("Стоимость");
            topPagination.Pagination.PropertyChanged += Pagination_PropertyChanged;
            processPage(null);
            DescAsc = "Asc";
            BtnVisiable = false;
            Visible = Visibility.Hidden;
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
                      BtnVisiable = true;
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
        private RelayCommand ascDescCommand;
        public RelayCommand AscDescCommand
        {
            get
            {
                return ascDescCommand ??
                  (ascDescCommand = new RelayCommand(obj =>
                  {
                      ascdesc= !ascdesc;
                      if (SelectedCount.Equals("Наименование"))
                          if (ascdesc == true)
                          {
                              FullList = FullList.OrderBy(p => p.Title).ToList();
                              DescAsc = "Asc";
                          }
                          else
                          {
                              FullList = FullList.OrderByDescending(p => p.Title).ToList();
                              DescAsc = "Desc";
                          }
                      else if (SelectedCount.Equals("Остаток на складе"))
                          if (ascdesc == true)
                          {
                              FullList = FullList.OrderBy(p => p.CountInStock).ToList();
                              DescAsc = "Asc";
                          }
                          else
                          {
                              FullList = FullList.OrderByDescending(p => p.CountInStock).ToList();
                              DescAsc = "Desc";
                          }
                      else if (SelectedCount.Equals("Стоимость"))
                          if (ascdesc == true)
                          {
                              FullList = FullList.OrderBy(p => p.Cost).ToList();
                          }
                          else
                          {
                              FullList = FullList.OrderByDescending(p => p.Cost).ToList();
                              DescAsc = "Desc";
                          }
                      processPage(null);
                  }));
            }
        }
        private RelayCommand materialCommand;
        public RelayCommand MaterialCommand
        {
            get
            {
                return materialCommand ??
                  (materialCommand = new RelayCommand(obj =>
                  {
                      FullList.Clear();
                      if (SelectedMaterial.Equals("Все"))
                          FullList = AllList;
                      else
                      {
                          FullList = AllList.Where(p => p.MaterialTypeID.Equals(SelectedMaterial)).ToList();

                      }
                      processPage(null);
                  }));
            }
        }
        private RelayCommand filterText;
        public RelayCommand FilterText
        {
            get
            {
                return filterText ??
                  (filterText = new RelayCommand(obj =>
                  {
                      if (TextFilter.Length != 0)
                      {
                          FullList = AllList.Where(p => p.Title.StartsWith(TextFilter)).ToList();
                      }
                      else
                      {
                          FullList = AllList;
                      }
                      processPage(null);
                  }));
            }
        }
        private RelayCommand selectionChanged;
        public RelayCommand SelectionChanged
        {
            get
            {
                return selectionChanged ??
                  (selectionChanged = new RelayCommand(obj =>
                  {
                      if (SelectedItems is MaterialViewModel)
                      {
                          var sel = SelectedItems as MaterialViewModel;
                          MaterialViewModel model = (MaterialViewModel)sel;
                          FullList.Where(p=>p.Title.Equals(model.Title)).
                            FirstOrDefault().IsChecked=!model.IsChecked;
                      }

                      Visible = Visibility.Visible;
                  }));
            }
        }
        private RelayCommand amount;
        public RelayCommand Amount
        {
            get
            {
                return amount ??
                    (amount = new RelayCommand(obj =>
                    {
                       int min = (int)FullList.Max(p => p.CountInStock);
                       OstView ostView = new OstView(min);
                       if(ostView.ShowDialog()==true)
                       {
                            int ost = ostView.MaxCount;
                            foreach (var item in FullList.Where(p => p.IsChecked == true).ToList())
                            {
                                item.MinCount = ost;
                                using(ModelDB db=new ModelDB())
                                {
                                    Material material = db.Material.Where(p => p.Title.Equals(item.Title)).FirstOrDefault();
                                    if (material != null)
                                    {
                                        material.MinCount = ost;
                                        db.SaveChanges();
                                    }
                                }
                            }
                       }
                        processPage(null);
                    }));
            }
        }

        #endregion
    }
}
