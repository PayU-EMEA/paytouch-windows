using WP7Contrib.View.Transitions.Animation;

namespace NewMerchantSampleApp
{
    public partial class PaymentMethodWidget : AnimatedBasePage
    {
        public PaymentMethodWidget()
        {
            InitializeComponent();
            AnimationContext = LayoutRoot;
        }
    }
}