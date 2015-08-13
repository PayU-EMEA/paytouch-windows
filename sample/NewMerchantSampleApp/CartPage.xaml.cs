using NewMerchantSampleApp.Model;
using NewMerchantSampleApp.Services;
using PayU.SDK;
using PayU.SDK.Model;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using WP7Contrib.View.Transitions.Animation;

namespace NewMerchantSampleApp
{
    public partial class CartPage : AnimatedBasePage
    {
        private Product _product;
        private PayUService _payUService = PayUService.Instance;

        public CartPage()
        {
            InitializeComponent();
            AnimationContext = LayoutRoot;

            _payUService.PaymentMethodChanged += _payUService_PaymentMethodChanged;
            _payUService.SendOrderCompleted += _payUService_SendOrderCompleted;
        }

        private void _payUService_SendOrderCompleted(object sender, ServiceResponse<SendOrderStatus> e)
        {
            Overlay.Visibility = Visibility.Collapsed;
        }

        private void _payUService_PaymentMethodChanged(object sender, EventArgs e)
        {
            ButtonPay.IsEnabled = _payUService.IsPaymentMethodSelected;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (NavigationContext.QueryString.ContainsKey(StringKeys.ProductNameParam))
            {
                _product = ProductService.GetProducts().First(x => x.Name == NavigationContext.QueryString[StringKeys.ProductNameParam]);

                TextBlockProductName.Text = _product.Name;
                TextBlockPrice.Text = _product.Price.ToString();
                TextBlockTotalPrice.Text = _product.Price.ToString();
                ImageProduct.Source = new BitmapImage(new Uri(_product.ImageUrl, UriKind.Relative));
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            if (e.NavigationMode == NavigationMode.Back)
            {
                _payUService.PaymentMethodChanged -= _payUService_PaymentMethodChanged;
                _payUService.SendOrderCompleted -= _payUService_SendOrderCompleted;
                _payUService = null;
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Overlay.Visibility = Visibility.Visible;

            _payUService.SendOrderAsync(description: _product.Name,
                                        notifyUrl: "http://notify.me/notify-endpoint-wp",
                                        externalOrderId: DateTime.Now.Ticks.ToString(),
                                        amount: int.Parse(TextBlockTotalPrice.Text) * 100,
                                        currency: Currency.PLN);
        }
    }
}