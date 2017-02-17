using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BehavirSample.Views
{
    /// <summary>
    /// ViewModelに反映させるように必要
    /// dropされたファイルを伝えるためのもの
    /// </summary>
    public interface IFileDropC
    {
        void Drop(string[] files);
    }

    /// <summary>
    /// 添付プロパティにDrop処理の関数を入れたインターフェースを持たせる方法
    /// </summary>
    class FileDropBehaviorC
    {


        #region FileDropHandler Dependency Property

        public static IFileDropC GetFileDropHandler(UIElement target)
        {
            return (IFileDropC)target.GetValue(FileDropHandlerProperty);
        }

        public static void SetFileDropHandler(UIElement target, IFileDropC value)
        {
            target.SetValue(FileDropHandlerProperty, value);
        }


        /// <summary>
        /// コールバックでイベントの購読の定義
        /// </summary>
        public static readonly DependencyProperty FileDropHandlerProperty =
        DependencyProperty.RegisterAttached("FileDropHandler", typeof(IFileDropC), typeof(FileDropBehaviorC), new PropertyMetadata(OnFileDropHandlerChanged));


        private static void OnFileDropHandlerChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var element = obj as UIElement;
            if (element != null)
            {
                //この場合、イベントの購読解除はどこでやるんだ？
                element.PreviewDragOver += Element_PreviewDragOver;
                element.Drop += Element_Drop;
            }

        }
        private static void Element_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.All;
            e.Handled = true;
        }

        private static void Element_Drop(object sender, DragEventArgs e)
        {
            GetFileDropHandler(sender as UIElement)?.Drop(e.Data.GetData(DataFormats.FileDrop) as string[]);
        }

       
        #endregion  FileDropHandler Dependency Property



    }
}
