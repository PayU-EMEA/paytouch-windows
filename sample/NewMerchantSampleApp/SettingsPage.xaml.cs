using NewMerchantSampleApp.Model;
using System;
using System.IO.IsolatedStorage;
using System.Windows.Navigation;
using WP7Contrib.View.Transitions.Animation;

namespace NewMerchantSampleApp
{
    public partial class SettingsPage : AnimatedBasePage
    {
        public SettingsPage()
        {
            InitializeComponent();
            AnimationContext = LayoutRoot;

            var settings = IsolatedStorageSettings.ApplicationSettings;

            if (settings.Contains(StringKeys.UserEmail))
                TextBoxEmail.Text = settings[StringKeys.UserEmail].ToString();
        }

        private void ApplicationBarIconButtonSave_OnClick(object sender, EventArgs e)
        {
            var settings = IsolatedStorageSettings.ApplicationSettings;

            if (!settings.Contains(StringKeys.UserEmail))
                settings.Add(StringKeys.UserEmail, TextBoxEmail.Text);

            settings[StringKeys.UserEmail] = TextBoxEmail.Text;
            NavigationService.GoBack();
        }

        private void ApplicationBarIconButtonCancel_OnClick(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}