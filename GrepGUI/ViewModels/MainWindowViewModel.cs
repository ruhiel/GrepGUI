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

        public ReactiveProperty<string> FolderPath { get; }

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
        }

        public void Dispose() => Disposable.Dispose();
    }
}
