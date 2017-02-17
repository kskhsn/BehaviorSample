using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;

namespace BehavirSample.Views
{

    /// <summary>
    /// ViewModelに反映させるように必要
    /// dropされたファイルを伝えるためのもの
    /// </summary>
    public interface IFileDropB
    {
        void Drop(string[] files);
    }

    /// <summary>
    /// 依存プロパティにDrop処理の関数を入れたインターフェースを持たせる方法
    /// </summary>
    class FileDropBehaviorB : Behavior<FrameworkElement>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.PreviewDragEnter += AssociatedObject_PreviewDragEnter;
            this.AssociatedObject.Drop += AssociatedObject_Drop;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.PreviewDragEnter -= AssociatedObject_PreviewDragEnter;
            this.AssociatedObject.Drop -= AssociatedObject_Drop;
        }

        private void AssociatedObject_PreviewDragEnter(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.All;
            e.Handled = true;
        }

        private void AssociatedObject_Drop(object sender, DragEventArgs e)
        {
            this.FileDropHandler?.Drop(e.Data.GetData(DataFormats.FileDrop) as string[]);
        }

        #region FileDropHandler Dependency Property

        public IFileDropB FileDropHandler
        {
            get { return (IFileDropB)this.GetValue(FileDropHandlerProperty); }
            set { this.SetValue(FileDropHandlerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FileDropHandler.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FileDropHandlerProperty =
        DependencyProperty.Register(nameof(FileDropHandler), typeof(IFileDropB), typeof(FileDropBehaviorB));

        #endregion  FileDropHandler Dependency Property



    }
}
