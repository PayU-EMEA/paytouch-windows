using NewMerchantSampleApp.Model;
using PayU.SDK;
using System.Linq;
using System.Windows;
using System.Windows.Navigation;
using WP7Contrib.View.Transitions.Animation;

namespace NewMerchantSampleApp
{
    public partial class OrderResultPage : AnimatedBasePage
    {
        private string _orderStatus;

        public OrderResultPage()
        {
            InitializeComponent();
            AnimationContext = LayoutRoot;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (NavigationContext.QueryString.ContainsKey(StringKeys.OrderStatusParam))
            {
                var removeBackEntry = !NavigationContext.QueryString.ContainsKey(StringKeys.DoNotRemoveBackEntryParam);

                var result = NavigationContext.QueryString[StringKeys.OrderStatusParam];
                _orderStatus = result;

                if (result == SendOrderStatus.Success.ToString())
                {
                    ViewboxFail.Visibility = Visibility.Collapsed;

                    if (NavigationService.BackStack.Count() > 1)
                        NavigationService.RemoveBackEntry();

                    TextBlockSubtitle.Text = "dziękujemy";
                    TextBlockPageDescription.Text = "Twoja transakcja została przyjęta do realizacji.";
                    ButtonContinue.Content = "kontynuuj zakupy";
                }
                else if (result == SendOrderStatus.Failure.ToString())
                {
                    ViewBoxSuccess.Visibility = Visibility.Collapsed;
                    TextBlockSubtitle.Text = "błąd";
                    TextBlockPageDescription.Text = "Wystąpił błąd podczas realizacji płatności. Proszę spróbować ponownie.";
                    ButtonContinue.Content = "wróć do koszyka";
                }
                else if (result == SendOrderStatus.Canceled.ToString())
                {
                    ViewBoxSuccess.Visibility = Visibility.Collapsed;
                    TextBlockSubtitle.Text = "anulowano";
                    TextBlockPageDescription.Text = "Realizacja płatności została anulowana.";
                    ButtonContinue.Content = "wróć do koszyka";
                }

                if (!removeBackEntry)
                    ButtonContinue.Content = "kontynuuj zakupy";
            }
        }

        private void ButtonContinue_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}