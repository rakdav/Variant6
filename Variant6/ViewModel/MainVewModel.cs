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
    internal class MainVewModel : INotifyPropertyChanged
    {
        public ObservableCollection<MaterialViewModel> OurMaterials { get; set; }
        public MainVewModel()
        {
            OurMaterials =new ObservableCollection<MaterialViewModel>();
            using(ModelDB db= new ModelDB())
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
                foreach(var item in query)
                {
                    MaterialViewModel ourMaterial = new MaterialViewModel();
                    ourMaterial.Title = item.Title;
                    ourMaterial.CountInPack = item.CountInPack;
                    ourMaterial.Unit = item.Unit;
                    ourMaterial.CountInStock = item.CountInstock;
                    ourMaterial.MinCount = item.MinCount;
                    ourMaterial.Description = item.Desc;
                    ourMaterial.Cost = item.Cost;
                    ourMaterial.Image = item.Image;
                    ourMaterial.MaterialTypeID = item.MaterialType.ToString();
                    //List<Supplier> sup=db.Supplier.Where(s=>s.Material.Equals(item.Title)).ToList();
                    //string res = "";
                    //foreach (Supplier supp in sup)
                    //    res += supp.Title+",";
                    //res=res.Substring(0, res.Length-2);
                    //ourMaterial.Suppliers = res;
                    OurMaterials.Add(ourMaterial);               
                }    
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
