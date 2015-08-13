using NewMerchantSampleApp.Model;
using NewMerchantSampleApp.Services;
using PayU.SDK;
using System;
using System.IO.IsolatedStorage;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WP7Contrib.View.Transitions.Animation;

namespace NewMerchantSampleApp
{
    public partial class MainPage : AnimatedBasePage
    {
        public MainPage()
        {
            InitializeComponent();
            AnimationContext = LayoutRoot;

            LongListSelectorProducts.ItemsSource = ProductService.GetProducts();
        }

        private void ButtonBuyNow_OnClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var product = button.Tag as Product;

            NavigationService.Navigate(new Uri(string.Format("/CartPage.xaml?{0}={1}",
                                                             StringKeys.ProductNameParam,
                                                             product.Name),
                                               UriKind.Relative));
        }

        protected override void AnimationsComplete(AnimationType animationType)
        {
            base.AnimationsComplete(animationType);

            if (animationType == AnimationType.NavigateForwardIn)
            {
                var settings = IsolatedStorageSettings.ApplicationSettings;

                if (settings.Contains(App.LastOrderStatus))
                {
                    NavigationService.Navigate(new Uri(string.Format("/OrderResultPage.xaml?{0}={1}&{2}=true",
                                                                     StringKeys.OrderStatusParam,
                                                                     settings[App.LastOrderStatus],
                                                                     StringKeys.DoNotRemoveBackEntryParam),
                                                       UriKind.Relative));

                    settings.Remove(App.LastOrderStatus);
                }
            }
        }

        private void ApplicationBarMenuItemSettings_OnClick(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/SettingsPage.xaml", UriKind.Relative));
        }

        private void ApplicationBarMenuItemLogOut_OnClick(object sender, EventArgs e)
        {
            PayUService.Instance.LogOutUser();
            MessageBox.Show("Zostałeś wylogowany.");
        }

        private void ApplicationBarMenuItemWidget_OnClick(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/PaymentMethodWidget.xaml", UriKind.Relative));
        }
    }
}