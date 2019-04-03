using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;

namespace WpfTreeView
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class WpfTreeView : UserControl
    {
        public IList<CommonTreeViewItemModel> ItemsSourceData
        {
            get { return (IList<CommonTreeViewItemModel>)innerTree.ItemsSource; }
        }


        public WpfTreeView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 设置数据源, 以及各个字段
        /// </summary>
        /// <typeparam name="TSource">数据源类型</typeparam>
        /// <typeparam name="TId">主键类型</typeparam>
        /// <param name="itemsArray">数据源列表</param>
        /// <param name="captionSelector">指定显示为Caption的属性</param>
        /// <param name="idSelector">指定主键属性</param>
        /// <param name="parentIdSelector">指定父项目主键属性</param>
        public void SetItemsSourceData<TSource, TId>(IEnumerable<TSource> itemsArray, Func<TSource, string> captionSelector,Func<TSource,bool> isExpandedSelector, Func<TSource, TId> idSelector, Func<TSource, TId> parentIdSelector)
                where TId : IEquatable<TId>
        {
            var list = new List<CommonTreeViewItemModel>();

            foreach (var item in itemsArray.Where(a => object.Equals(default(TId), parentIdSelector(a))))
            {
                var tvi = new CommonTreeViewItemModel();
                tvi.Caption = captionSelector(item).ToString();
                tvi.IsExpanded = isExpandedSelector(item);
                tvi.Id = idSelector(item);
                tvi.Tag = item;
                tvi.TagType = item.GetType();
                list.Add(tvi);
                RecursiveAddChildren(tvi, itemsArray, captionSelector, idSelector, parentIdSelector);
            }

            innerTree.ItemsSource = list;
            innerTree.DataContext = list;
            return;
        }

        /// <summary>
        /// 递归加载children
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TId"></typeparam>
        /// <param name="parent"></param>
        /// <param name="itemsArray"></param>
        /// <param name="captionSelector"></param>
        /// <param name="idSelector"></param>
        /// <param name="parentIdSelector"></param>
        /// <returns></returns>
        private CommonTreeViewItemModel RecursiveAddChildren<TSource, TId>(CommonTreeViewItemModel parent, IEnumerable<TSource> itemsArray, Func<TSource, string> captionSelector, Func<TSource, TId> idSelector, Func<TSource, TId> parentIdSelector)
        {

            foreach (var item in itemsArray.Where(a => object.Equals(parent.Id, parentIdSelector(a))))
            {
                var tvi = new CommonTreeViewItemModel();
                tvi.Caption = captionSelector(item);
                tvi.Id = idSelector(item);
                tvi.Tag = item;
                tvi.TagType = item.GetType();
                tvi.Parent = parent;
                parent.Children.Add(tvi);
                RecursiveAddChildren(tvi, itemsArray, captionSelector, idSelector, parentIdSelector);
            }
            return parent;
        }
    }


    /// <summary>
    /// 通用的TreeViewItem模型, 仅包含最基础CheckBox(如果你觉得不好看, 在CommonTreeView.xaml中修改样式), 还包含一个 Tag(包含的对象)
    /// 业务逻辑: 父项目 被勾选 或者 取消勾选, 那么它的所有子项目全部改成 被勾选 或者 取消勾选; 只有所有的子项目都被勾选时, 父项目才自发的改成被勾选
    /// 没有单独写在一个cs文件中, 主要是那样会修改两处namespace
    /// 所有的字段都改为protected, 方便继承修改
    /// </summary>
    public class CommonTreeViewItemModel : INotifyPropertyChanged
    {

        #region 属性

        public event PropertyChangedEventHandler PropertyChanged;

        protected object id;
        /// <summary>
        /// 唯一性Id
        /// </summary>
        public object Id
        {
            get { return id; }
            set { id = value; }
        }

        protected string caption;
        /// <summary>
        /// 标题
        /// </summary>
        public string Caption
        {
            get { return caption; }
            set { caption = value; if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Caption")); }
        }

        protected bool isChecked;
        /// <summary>
        /// 是否被勾选
        /// </summary>
        public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                isChecked = value;
                if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs("IsChecked"));
                SetIsCheckedByParent(value);
                if (Parent != null) Parent.SetIsCheckedByChild(value);
            }
        }

        protected bool isExpanded;
        /// <summary>
        /// 是否被展开
        /// </summary>
        public bool IsExpanded
        {
            get { return isExpanded; }
            set { isExpanded = value; if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs("IsExpanded")); }
        }


        protected CommonTreeViewItemModel parent;
        /// <summary>
        /// 父项目
        /// </summary>
        public CommonTreeViewItemModel Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        protected List<CommonTreeViewItemModel> children = new List<CommonTreeViewItemModel>();
        /// <summary>
        /// 含有的子项目
        /// </summary>
        public List<CommonTreeViewItemModel> Children
        {
            get { return children; }
            set { children = value; }
        }


        /// <summary>
        /// 包含的对象
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        /// 包含对象的类型
        /// </summary>
        public Type TagType { get; set; }
        #endregion


        #region 业务逻辑, 如果你需要改成其他逻辑, 要修改的也就是这两行

        /// <summary>
        /// 子项目的isChecked改变了, 通知 是否要跟着改变 isChecked
        /// </summary>
        /// <param name="value"></param>
        public virtual void SetIsCheckedByChild(bool value)
        {
            if (this.isChecked == value)
            {
                return;
            }

            bool isAllChildrenChecked = this.Children.All(c => c.IsChecked == true);
            this.isChecked = isAllChildrenChecked;
            if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs("IsChecked"));
            if (Parent != null) Parent.SetIsCheckedByChild(value);
        }

        /// <summary>
        /// 自己的isChecked改变了, 所有子项目都要跟着改变
        /// </summary>
        /// <param name="value"></param>
        public virtual void SetIsCheckedByParent(bool value)
        {
            this.isChecked = value;
            if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs("IsChecked"));
            foreach (var child in Children)
            {
                child.SetIsCheckedByParent(value);
            }
        }
        #endregion

    }

    public class WpfTreeViewItem:INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Caption { get; set; }
        public int ParentId { get; set; }
        public bool isExpanded;
        /// <summary>
        /// 是否被展开
        /// </summary>
        public bool IsExpanded
        {
            get { return isExpanded; }
            set { isExpanded = value; if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs("IsExpanded")); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

}
