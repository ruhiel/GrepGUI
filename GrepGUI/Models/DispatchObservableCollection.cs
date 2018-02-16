using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace GrepGUI.Models
{
    public class DispatchObservableCollection<T> : ObservableCollection<T>
    {
        // CollectionChangedイベントを発行するときに使用するディスパッチャ
        public Dispatcher EventDispatcher { get; set; }

        #region コンストラクタ
        public DispatchObservableCollection()
        {
            InitializeEventDispatcher();
        }
        public DispatchObservableCollection(IEnumerable<T> collection)
            : base(collection)
        {
            InitializeEventDispatcher();
        }
        public DispatchObservableCollection(List<T> list)
            : base(list)
        {
            InitializeEventDispatcher();
        }
        private void InitializeEventDispatcher() =>
            // インスタンスが作られた時のDispatcherを取得
            EventDispatcher = Dispatcher.CurrentDispatcher;
        #endregion

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (IsValidAccess())
            {
                // UIスレッドならそのまま実行
                base.OnCollectionChanged(e);
            }
            else
            {
                // UIスレッドじゃなかったらDispatcherにお願いする
                Action<NotifyCollectionChangedEventArgs> changed = OnCollectionChanged;
                EventDispatcher.Invoke(changed, e);
            }
        }

        public void AddRange(IEnumerable<T> enumerable)
        {
            foreach(var item in enumerable)
            {
                Add(item);
            }
        }

        // UIスレッドからのアクセスかどうかを判定する
        private bool IsValidAccess() =>
            // Dispatcherが設定されていないときは、どうしようもないのでOKにしとく
            // Dispatcherが設定されていたら、今のスレッドとDispatcherのスレッドを見比べる
            EventDispatcher == null ||
                EventDispatcher.Thread == Thread.CurrentThread;
    }
}
