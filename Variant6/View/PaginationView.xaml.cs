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
using Variant6.ViewModel;

namespace Variant6.View
{
    /// <summary>
    /// Логика взаимодействия для PaginationView.xaml
    /// </summary>
    public partial class PaginationView : UserControl
    {
        public PaginationView()
        {
            InitializeComponent();
        }

        private void tbxDisplayItems_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if(e.KeyboardDevice.IsKeyDown(Key.Enter))
                {
                    ((PaginationViewModel)this.DataContext).calculatePagination();

                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
