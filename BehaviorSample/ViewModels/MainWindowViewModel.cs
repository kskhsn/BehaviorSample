using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.ComponentModel;

using Livet;
using Livet.Commands;
using Livet.Messaging;
using Livet.Messaging.IO;
using Livet.EventListeners;
using Livet.Messaging.Windows;

using BehavirSample.Models;
using BehavirSample.Views;

namespace BehavirSample.ViewModels
{
    public class MainWindowViewModel : ViewModel,Views.IFileDropB,Views.IFileDropC
    {
        /* コマンド、プロパティの定義にはそれぞれ 
         * 
         *  lvcom   : ViewModelCommand
         *  lvcomn  : ViewModelCommand(CanExecute無)
         *  llcom   : ListenerCommand(パラメータ有のコマンド)
         *  llcomn  : ListenerCommand(パラメータ有のコマンド・CanExecute無)
         *  lprop   : 変更通知プロパティ(.NET4.5ではlpropn)
         *  
         * を使用してください。
         * 
         * Modelが十分にリッチであるならコマンドにこだわる必要はありません。
         * View側のコードビハインドを使用しないMVVMパターンの実装を行う場合でも、ViewModelにメソッドを定義し、
         * LivetCallMethodActionなどから直接メソッドを呼び出してください。
         * 
         * ViewModelのコマンドを呼び出せるLivetのすべてのビヘイビア・トリガー・アクションは
         * 同様に直接ViewModelのメソッドを呼び出し可能です。
         */

        /* ViewModelからViewを操作したい場合は、View側のコードビハインド無で処理を行いたい場合は
         * Messengerプロパティからメッセージ(各種InteractionMessage)を発信する事を検討してください。
         */

        /* Modelからの変更通知などの各種イベントを受け取る場合は、PropertyChangedEventListenerや
         * CollectionChangedEventListenerを使うと便利です。各種ListenerはViewModelに定義されている
         * CompositeDisposableプロパティ(LivetCompositeDisposable型)に格納しておく事でイベント解放を容易に行えます。
         * 
         * ReactiveExtensionsなどを併用する場合は、ReactiveExtensionsのCompositeDisposableを
         * ViewModelのCompositeDisposableプロパティに格納しておくのを推奨します。
         * 
         * LivetのWindowテンプレートではViewのウィンドウが閉じる際にDataContextDisposeActionが動作するようになっており、
         * ViewModelのDisposeが呼ばれCompositeDisposableプロパティに格納されたすべてのIDisposable型のインスタンスが解放されます。
         * 
         * ViewModelを使いまわしたい時などは、ViewからDataContextDisposeActionを取り除くか、発動のタイミングをずらす事で対応可能です。
         */

        /* UIDispatcherを操作する場合は、DispatcherHelperのメソッドを操作してください。
         * UIDispatcher自体はApp.xaml.csでインスタンスを確保してあります。
         * 
         * LivetのViewModelではプロパティ変更通知(RaisePropertyChanged)やDispatcherCollectionを使ったコレクション変更通知は
         * 自動的にUIDispatcher上での通知に変換されます。変更通知に際してUIDispatcherを操作する必要はありません。
         */

        public void Initialize()
        {
            this.ItemsA1 = new ObservableCollection<string>();
            this.ItemsA2 = new ObservableCollection<string>();
            this.ItemsB = new ObservableCollection<string>();
            this.ItemsC = new ObservableCollection<string>();
        }


        #region ItemsA変更通知プロパティ
        private ObservableCollection<string> _ItemsA1;

        public ObservableCollection<string> ItemsA1
        {
            get
            { return _ItemsA1; }
            set
            { 
                if (_ItemsA1 == value)
                    return;
                _ItemsA1 = value;
                RaisePropertyChanged();
            }
        }
        #endregion


        #region ItemA2変更通知プロパティ
        private ObservableCollection<string> _ItemsA2;

        public ObservableCollection<string> ItemsA2
        {
            get
            { return this._ItemsA2; }

            set
            { 
                if (this._ItemsA2 == value)
                {
                    return;
                }

                this._ItemsA2 = value;
                this.RaisePropertyChanged();
            }
        }
        #endregion




        #region ItemsB変更通知プロパティ
        private ObservableCollection<string> _ItemsB;

        public ObservableCollection<string> ItemsB
        {
            get
            { return _ItemsB; }
            set
            { 
                if (_ItemsB == value)
                    return;
                _ItemsB = value;
                RaisePropertyChanged();
            }
        }
        #endregion


        #region ItemsC変更通知プロパティ
        private ObservableCollection<string> _ItemsC;

        public ObservableCollection<string> ItemsC
        {
            get
            { return this._ItemsC; }

            set
            { 
                if (this._ItemsC == value)
                {
                    return;
                }

                this._ItemsC = value;
                this.RaisePropertyChanged();
            }
        }
        #endregion


        #region DropFilesA変更通知プロパティ
        private string[] _DropFilesA1;

        public string[] DropFilesA1
        {
            get
            { return this._DropFilesA1; }

            set
            { 
                if (this._DropFilesA1 == value)
                {
                    return;
                }

                this._DropFilesA1 = value;
                foreach (var item in value)
                    this.ItemsA1.Add(item);

                this.RaisePropertyChanged();
            }
        }
        #endregion

        public void DropFilesA2(string[] files)
        {
            if (files == null)
                return;

            foreach (var file in files)
                this.ItemsA2.Add(file);
        }

        void IFileDropB.Drop(string[] files)
        {
            foreach (var file in files)
                this.ItemsB.Add(file);
        }

        void IFileDropC.Drop(string[] files)
        {
            foreach (var file in files)
                this.ItemsC.Add(file);
        }
    }
}
