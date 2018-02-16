using GrepGUI.Models;
using Microsoft.WindowsAPICodePack.Dialogs;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;

namespace GrepGUI.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private CompositeDisposable Disposable { get; } = new CompositeDisposable();

        public ReactiveCommand FolderOpenCommand { get; } = new ReactiveCommand();

        public ReactiveCommand SearchCommand { get; } = new ReactiveCommand();

        public ReactiveProperty<string> FolderPath { get; }

        public DispatchObservableCollection<FileInfo> FileInfoList { get; } = new DispatchObservableCollection<FileInfo>();

        public MainWindowViewModel()
        {
            FolderPath = new ReactiveProperty<string>().AddTo(Disposable);

            FolderOpenCommand.Subscribe(_ =>
            {
                var dlg = new CommonOpenFileDialog("検索ディレクトリ選択");
                // フォルダ選択モード。
                dlg.IsFolderPicker = true;
                var ret = dlg.ShowDialog();
                if (ret == CommonFileDialogResult.Ok)
                {
                    FolderPath.Value = dlg.FileName;
                }
            });

            SearchCommand.Subscribe(_ =>
            {
                FileInfoList.AddRange(FileSearcher.Search(FolderPath.Value, null));
            });
        }

        public void Dispose() => Disposable.Dispose();
    }
}
