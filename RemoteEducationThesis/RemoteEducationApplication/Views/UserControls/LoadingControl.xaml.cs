using ExtensionLibrary.DataTypes.Converters.Extensions;
using System.Windows;
using System.Windows.Media;
using WPFFramework.App.Base;

namespace Education.Application.Views.UserControls
{
	/// <summary>
	/// Interaction logic for LoadingStaticControl.xaml
	/// </summary>
	public partial class LoadingControl : WpfUserControl
	{
		#region DependencyProperties

		/// <summary>
		/// LoadingText dependency property.
		/// </summary>
		public static readonly DependencyProperty LoadingTextProperty =
		    DependencyProperty.Register("LoadingText", typeof(string), typeof(LoadingControl), new UIPropertyMetadata(LoadingTextPropertyChanged));

		/// <summary>
		/// LoadingBackground dependency property.
		/// </summary>
		public static readonly DependencyProperty LoadingBackgroundProperty =
		    DependencyProperty.Register("LoadingBackground", typeof(SolidColorBrush), typeof(LoadingControl), new UIPropertyMetadata(LoadingBackgroundPropertyChanged));

		/// <summary>
		/// LoadingForeground dependency property.
		/// </summary>
		public static readonly DependencyProperty LoadingForegroundProperty =
		    DependencyProperty.Register("LoadingForeground", typeof(SolidColorBrush), typeof(LoadingControl), new UIPropertyMetadata(LoadingForegroundPropertyChanged));

		/// <summary>
		/// LoadingAreaOpacity dependency property.
		/// </summary>
		public static readonly DependencyProperty LoadingAreaOpacityProperty =
			DependencyProperty.Register("LoadingAreaOpacity", typeof(double?), typeof(LoadingControl), new UIPropertyMetadata(LoadingAreaOpacityPropertyChanged));

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the loading text.
		/// </summary>
		public string LoadingText
		{
			get { return GetValue(LoadingTextProperty).ToString(); }
			set 
			{ 
				SetValue(LoadingTextProperty, value);
				OnPropertyChanged(this, x => x.LoadingText);
			}
		}

		/// <summary>
		/// Gets or sets the loading background.
		/// </summary>
		public SolidColorBrush LoadingBackground
		{
		    get { return GetValue(LoadingBackgroundProperty) as SolidColorBrush; }
		    set
		    {
		        SetValue(LoadingBackgroundProperty, value);
		        OnPropertyChanged(this, x => x.LoadingBackground);
		    }
		}

		/// <summary>
		/// Gets or sets the loading foreground.
		/// </summary>
		public SolidColorBrush LoadingForeground
		{
			get { return GetValue(LoadingForegroundProperty) as SolidColorBrush; }
			set 
			{
				SetValue(LoadingForegroundProperty, value);
				OnPropertyChanged(this, x => x.LoadingForeground);
			}
		}

		/// <summary>
		/// Gets or sets the loading area opacity.
		/// </summary>
		public double? LoadingAreaOpacity
		{
			get { return GetValue(LoadingAreaOpacityProperty).ToNullable<double>(); }
			set
			{
				SetValue(LoadingAreaOpacityProperty, value);
				OnPropertyChanged(this, x => x.LoadingAreaOpacity);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public int? LoadingAreaHeight { get; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public double? LoadingOpacity { get; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public int? LoadingTextFontSize { get; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string ImageSourcePath { get; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public SolidColorBrush LoadingWindowBrush { get; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public SolidColorBrush LoadingImageBrush { get; set; }
		
		#endregion

		#region Constructor

		/// <summary>
		/// 
		/// </summary>
		public LoadingControl()
		{
		    InitializeComponent();
		    Loaded += LoadingStaticControl_Loaded;
		}

		#endregion

		#region EventHandling

		/// <summary>
		/// Handles the Loaded event of the LoadingStaticControl window.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		private void LoadingStaticControl_Loaded(object sender, RoutedEventArgs e)
		{
			SetDefaultValues();
			DataContext = this;
		}

		/// <summary>
		/// Handles the PropertyChanged event of the LoadingText dependency property.
		/// </summary>
		/// <param name="o">The <see cref="System.Windows.DependencyObject"/> source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
		private static void LoadingTextPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
		{
			o.ExecuteIfNotNull<LoadingControl>(x => x.LoadingText = e.NewValue.ToString());
		}

		/// <summary>
		/// Handles the PropertyChanged event of the LoadingBackground dependency property.
		/// </summary>
		/// <param name="o">The <see cref="System.Windows.DependencyObject"/> source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
		private static void LoadingBackgroundPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
		{
			o.ExecuteIfNotNull<LoadingControl>(x => x.LoadingBackground = (SolidColorBrush)e.NewValue);
		}

		/// <summary>
		/// Handles the PropertyChanged event of the LoadingForeground dependency property.
		/// </summary>
		/// <param name="o">The <see cref="System.Windows.DependencyObject"/> source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
		private static void LoadingForegroundPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
		{
			o.ExecuteIfNotNull<LoadingControl>(x => x.LoadingForeground = (SolidColorBrush)e.NewValue);
		}

		/// <summary>
		/// Handles the PropertyChanged event of the LoadingAreaOpacity dependency property.
		/// </summary>
		/// <param name="o">The <see cref="System.Windows.DependencyObject"/> source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
		private static void LoadingAreaOpacityPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
		{
			o.ExecuteIfNotNull<LoadingControl>(x => x.LoadingAreaOpacity = e.NewValue.ToNullable<double>());
		}

		#endregion

		#region Methods

		/// <summary>
		/// Sets default values for the window.
		/// </summary>
		private void SetDefaultValues()
		{
			LoadingWindowBrush = LoadingWindowBrush ?? new SolidColorBrush(Colors.Black);
			LoadingImageBrush = LoadingImageBrush ?? new SolidColorBrush(Colors.White);
		}

		#endregion
	}
}
