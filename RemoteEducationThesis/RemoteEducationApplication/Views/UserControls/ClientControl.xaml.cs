using RemoteEducationApplication.Client;
using RemoteEducationApplication.Helpers;
using RemoteEducationApplication.Shared;
using RemoteEducationApplication.Extensions;
using System.Windows.Controls;

namespace RemoteEducationApplication.Views.UserControls
{
    /// <summary>
    /// Interaction logic for ClientControl.xaml
    /// </summary>
    public partial class ClientControl : UserControlBase
    {
        //#region Fields

        //private bool _isClientExpanded;

        //#endregion

        //#region Properties

        ///// <summary>
        ///// 
        ///// </summary>
        //public bool IsClientExpanded 
        //{
        //    get
        //    {
        //        return _isClientExpanded;
        //    } 
        //    set
        //    {
        //        _isClientExpanded = value;
        //        NotifyPropertyChanged("IsClientExpanded");
        //    }
        //}

        //#endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="RemoteEducationApplication.Views.UserControls.ClientControl"/> 
        /// class.
        /// </summary>
        public ClientControl()
        {
            InitializeComponent();
        }

        #endregion

        #region Events & Delegates

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">The <see cref="RemoteEducationApplication.Shared.ApplicationBarEventArgs"/>
        /// instance containing the event data.</param>
        public delegate void ClientClickEventHandler(object sender, ApplicationBarEventArgs e);

        /// <summary>
        /// ClientCloseEventHandler handler.
        /// </summary>
        public event ClientClickEventHandler ClientClick;

        #endregion

        #region EventHandling

        /// <summary>
        /// Invokes RectangleClickEventHandler.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        public void OnCloseClick(string clientName, string commandName)
        {
            if (ClientClick != null)
                ClientClick(this, new ApplicationBarEventArgs(commandName, clientName));
        }

        /// <summary>
        /// Handles the RectangleClick event of the appBar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RemoteEducationApplication.Shared.ApplicationBarEventArgs"/>
        /// instance containing the event data.</param>
        private void appBar_RectangleClick(object sender, ApplicationBarEventArgs e)
        {
            OnCloseClick(this.GetTag(), e.CommandName);
        }   

        #endregion  
    }
}
