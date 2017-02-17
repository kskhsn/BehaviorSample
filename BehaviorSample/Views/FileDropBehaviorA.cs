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
    /// 依存プロパティにstring[]型を作る方法
    /// </summary>
    class FileDropBehaviorA:Behavior<FrameworkElement>
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
            //previewでの処理。おまじないw。
            e.Effects = DragDropEffects.All;
            e.Handled = true;
        }
        private void AssociatedObject_Drop(object sender, DragEventArgs e)
        {
            //こんな感じでdropされたファイル名が取得可能
            this.DropFiles = e.Data.GetData(DataFormats.FileDrop) as string[];
        }
        
        #region DropFiles Dependency Property

        public string[] DropFiles
        {
            get { return (string[])this.GetValue(DropFilesProperty); }
            set { this.SetValue(DropFilesProperty, value); }
        }


        /// <summary>
        /// FrameworkPropertyMetadataOptions.BindsTwoWayByDefaultは付けなくても可
        /// ただしその場合は,xamlのbinding modeをtwowayをつける必要がある。
        /// FrameworkPropertyMetadataOptions.BindsTwoWayByDefaultをここでつけておくとその必要はない。
        /// </summary>
        public static readonly DependencyProperty DropFilesProperty =
        DependencyProperty.Register(nameof(DropFiles), typeof(string[]), typeof(FileDropBehaviorA), new FrameworkPropertyMetadata(default(string[]), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        #endregion  DropFiles Dependency Property



    }
}
