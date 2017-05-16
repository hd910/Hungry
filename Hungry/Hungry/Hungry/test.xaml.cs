using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hungry
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class test : ContentPage
    {
        public test()
        {
            InitializeComponent();
            RelativeLayout view = new RelativeLayout();
            AdMobView admobView = new AdMobView()
            {
                WidthRequest = 320,
                HeightRequest = 50
            };
            //Label admobView = new Label()
            //{
            //    TextColor = Color.LightGray,
            //    Text = "Swipe to skip",
            //    FontSize = 16,
            //    InputTransparent = true
            //};
            view.Children.Add(admobView, Constraint.Constant(0), Constraint.Constant(0),
                Constraint.RelativeToParent((parent) => {
                    return parent.Width;
                }),
                Constraint.RelativeToParent((parent) => {
                    return parent.Height;
                }));

            Content = view;
        }
    }

}