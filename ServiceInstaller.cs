/****************************** Module Header ******************************
 * Module Name:  Empty Windows Service project.
 * Project:      Empty Windows Service - extend and employ as necessary
 *
 * ServiceInstaller class, to install this as an NT/Windows Service.
 *
 * Revisions:
 *     1. Sundar Krishnamurthy         sundar@passion8cakes.com             04/22/2016       Initial file created.
***************************************************************************/

namespace EmptyWindowsService {

    #region Using directives
    using System;
    using System.ComponentModel;
    using System.Configuration.Install;
    #endregion

    #region ServiceInstaller class
    /// <summary>
    /// ServiceInstaller class, to install this as an NT/Windows Service.
    /// </summary>
    [RunInstaller(true)]
    public partial class ServiceInstaller : Installer {

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public ServiceInstaller() {
            InitializeComponent();
        }
        #endregion
    }
    #endregion
}
