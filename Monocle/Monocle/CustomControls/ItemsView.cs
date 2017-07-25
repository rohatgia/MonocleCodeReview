using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Monocle.CustomControls
{
    //https://forums.xamarin.com/discussion/comment/274585/#Comment_274585

    public class ItemsView : ScrollView
    {
        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(
            "ItemTemplate",
            typeof(DataTemplate),
            typeof(ItemsView),
            null,
            propertyChanged: (bindable, value, newValue) => Populate(bindable));

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
            "ItemsSource",
            typeof(IEnumerable),
            typeof(ItemsView),
            null,
            BindingMode.OneWay,
            propertyChanged: (bindable, value, newValue) => Populate(bindable));

        public IEnumerable ItemsSource
        {
            get
            {
                return (IEnumerable)this.GetValue(ItemsSourceProperty);
            }

            set
            {
                this.SetValue(ItemsSourceProperty, value);
            }
        }

        public DataTemplate ItemTemplate
        {
            get
            {
                return (DataTemplate)this.GetValue(ItemTemplateProperty);
            }

            set
            {
                this.SetValue(ItemTemplateProperty, value);
            }
        }

        private static void Populate(BindableObject bindable)
        {
            var repeater = (ItemsView)bindable;

            // Clean
            repeater.Content = null;

            // Only populate once both properties are recieved
            if (repeater.ItemsSource == null || repeater.ItemTemplate == null)
            {
                return;
            }

            // Create a stack to populate with items
            var list = new StackLayout();

            foreach (var viewModel in repeater.ItemsSource)
            {
                var content = repeater.ItemTemplate.CreateContent();
                if (!(content is View) && !(content is ViewCell))
                {
                    throw new Exception($"Invalid visual object {nameof(content)}");
                }

                var view = content is View ? content as View : ((ViewCell)content).View;
                view.BindingContext = viewModel;

                list.Children.Add(view);
            }

            // Set stack as conent to this ScrollView
            repeater.Content = list;
        }
    }
}
