using RemoteEducationApplication.Shared;
using System;
using System.ComponentModel;
using System.Timers;
using System.Windows;

namespace RemoteEducationApplication.Views.UserControls
{
    /// <summary>
    /// Interaction logic for Clock.xaml
    /// </summary>
    public partial class Clock : UserControlBase
    {
        #region Fields

        private Timer _timer;
        private DateTime _time;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        public DateTime Time 
        { 
            get
            { 
                return _time; 
            }
            set
            {
                _time = value;
                NotifyPropertyChanged("Time");
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="RemoteEducationApplication.Views.UserControls.Clock"/> class.
        /// </summary>
        public Clock()
        {
            InitializeComponent();
            InitializeTimer();
            Loaded += Clock_Loaded;

            DataContext = this;
        }

        #endregion

        #region EventHandlers

        /// <summary>
        /// Handles the Loaded event of the Clock control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/>
        /// instance containing the event data.</param>
        private void Clock_Loaded(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);

            if(window != null)
                window.Closing += window_Closing;
        }

        /// <summary>
        /// Handles the Closing event of the Window element.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/>
        /// instance containing the event data.</param>
        void window_Closing(object sender, CancelEventArgs e)
        {
            _timer.Stop();
            _timer = null;
        }

        /// <summary>
        /// Handles the Elapsed event of the timer instance.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Timer.ElapsedEventArgs"/>
        /// instance containing the event data.</param>
        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Time = e.SignalTime;
        }

        #endregion

        #region Methods

        /// <summary>
        /// <para>Creates a new instance of the <see cref="System.Timers.Timer"/> class.</para>
        /// <para>Sets the parameters and starts the timer.</para>
        /// </summary>
        private void InitializeTimer()
        {
            Time = DateTime.Now;

            _timer = new Timer();
            _timer.Interval = 1000;
            _timer.Elapsed += timer_Elapsed;
            _timer.Start();
        }

        #endregion
    }
}
