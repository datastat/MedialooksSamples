using System;
using Avalonia.Controls;

namespace AvaloniaDataGridTest.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            
            var eventDataGrid = this.FindControl<DataGrid>("MainDataGrid");

            eventDataGrid.KeyDown += (sender, args) => { args.Handled = true; };

        }
    }
}