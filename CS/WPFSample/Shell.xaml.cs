using System.Windows;

using WPFSample.ViewModels;

namespace WPFSample
{
    /// <summary>
    /// Interaction logic for Shell.xaml
    /// </summary>
    public partial class Shell : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Shell"/> class.
        /// </summary>
        public Shell()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Shell"/> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        public Shell(ShellViewModel viewModel)
            : this()
        {
            DataContext = viewModel;
        }
    }
}
