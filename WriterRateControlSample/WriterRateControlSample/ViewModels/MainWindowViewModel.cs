using System;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using Avalonia.Collections;
using MFORMATSLib;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace WriterRateControlSample.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string Greeting => "Welcome to Avalonia!";

        public ReactiveCommand<Unit, Unit> StartRecordingCommand { get; }
        public ReactiveCommand<Unit, Unit> StopRecordingCommand { get; }
        public ReactiveCommand<Unit, Unit> OpenFolderCommand { get; }

        [Reactive] public AvaloniaList<WriterReaderClass> WriterReaderList { get; set; } = new AvaloniaList<WriterReaderClass>();


        [Reactive] public string FolderName { get; set; }
 

        public MainWindowViewModel()
        {
            StartRecordingCommand = ReactiveCommand.Create(StartRecAll);
            StopRecordingCommand = ReactiveCommand.Create(StopRecAll);
            OpenFolderCommand = ReactiveCommand.Create(() =>
            {
                Process.Start("explorer.exe", FolderName);
            });

            FolderName =  @$"c:\fr-rec1\{Guid.NewGuid().ToString()}";
            
            System.IO.Directory.CreateDirectory(FolderName);

            Observable.Start(() =>
            {
                for (int i = 0; i < 4; i++)
                {
                    WriterReaderList.Add(new WriterReaderClass(i + 1, FolderName));
                }
            });
        }

        private void StartRecAll()
        {
            foreach (var writerReaderClass in WriterReaderList)
            {
                writerReaderClass.Start();
            }

        }
        
        private void StopRecAll()
        {
            foreach (var writerReaderClass in WriterReaderList)
            {
                writerReaderClass.Stop();
            }
        }
    }
}