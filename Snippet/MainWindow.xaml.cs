using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Snippet
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void onTextChangd(object sender, TextChangedEventArgs e)
        {
            Console.WriteLine(TBInput.Text);
            var key = RegexUtils.GetProfileOrHashtagKey(TBInput.Text, true);
            if (key == null)
            {
                TBOutput.Text = "null";
                return;
            }
            TBOutput.Text = key;
        }
    }

    public class RegexUtils
    {

        private static readonly Regex TwitterProfilePattern =
            new Regex(@"^http(s)?://(mobile\.)?(twitter)\.(com(\.\w{2})?)/(?<profileName>[\w]+)(/)?$",
                RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private static readonly Regex TwitterHashtagPattern =
            new Regex(@"^http(s)?://(mobile\.)?(twitter)\.(com(\.\w{2})?)/hashtag/(?<hashtag>[\w]+)(/)?$",
                RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public static string GetProfileOrHashtagKey(string url, bool enableHashtag)
        {
            if (string.IsNullOrEmpty(url))
            {
                return null;
            }

            var match = TwitterProfilePattern.Match(url);
            if (match.Success)
            {
                return "@" + match.Groups["profileName"].Value;
            }

            if (!enableHashtag)
            {
                return null;
            }

            match = TwitterHashtagPattern.Match(url);
            if (!match.Success)
            {
                return null;
            }

            return "#" + match.Groups["hashtag"].Value;
        }

    }
}
