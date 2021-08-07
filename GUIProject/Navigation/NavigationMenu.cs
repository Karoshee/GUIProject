using OurUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUIProject.Navigation
{
    public class NavigationMenu
    {
        protected Menu _Menu { get; set; }

        public string Message { get; set; }

        protected Dictionary<string, Action> _Items { get; set; }

        public NavigationMenu(string message)
        {
            Message = message;
            _Items = new Dictionary<string, Action>();
            _Menu = new Menu(Message);
        }

        public void AddItems(params (string text, Action action)[] items)
        {
            foreach (var item in items)
            {
                _Items.Add(item.text, item.action);
            }
            _Menu = new Menu(Message, _Items.Select(item => item.Key).ToArray());
        }

        public virtual void Show()
        {
            _Items.ElementAt(_Menu.Show()).Value?.Invoke();
        }

        public void Empty() { }
    }

    public class NavigationMenu<T> : NavigationMenu
    {
        private IEnumerable<T> _BindedItems { get; set; }

        private Func<T, string> _DisplayMethod { get; set; }

        private Action<T> _SelectAction { get; set; }

        public NavigationMenu(string message)
            : base(message)
        {

        }

        public void BindItems(IEnumerable<T> items, Func<T, string> displayMethod = null, Action<T> selectAction = null)
        {
            if (displayMethod == null)
                displayMethod = i => i?.ToString();

            if (selectAction == null)
                selectAction = i => { };

            _BindedItems = items;
            _DisplayMethod = displayMethod;
            _SelectAction = selectAction;
        }

        public override void Show()
        {
            if (_BindedItems is not null && _BindedItems.Any())
            {
                string[] allElements = _BindedItems
                    .Select(item => _DisplayMethod(item))
                    .Concat(_Items.Select(item => item.Key))
                    .ToArray();

                int selectedIndex = new Menu(Message, allElements).Show();
                int bindedItemsCount = _BindedItems.Count();

                if (selectedIndex < bindedItemsCount)
                    _SelectAction(_BindedItems.ElementAt(selectedIndex));
                else
                {
                    _Items.ElementAt(selectedIndex - bindedItemsCount);
                }
                    
            }
            else
                base.Show();
        }
    }
}
