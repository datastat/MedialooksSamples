using System.Reactive;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Collections;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace AvaloniaDataGridTest.ViewModels
{
    public class TestListViewModel : ViewModelBase
    {
        public AvaloniaList<int> TestList { get; set; } = new AvaloniaList<int>();
        [Reactive] public string TextTest { get; set; }

        public TestListViewModel()
        {
            TestList.Add(1);
            TestList.Add(2);
            TestList.Add(3);
            TestList.Add(4);
            TestList.Add(5);
        }
    }
}