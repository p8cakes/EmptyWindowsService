/****************************** Module Header ******************************
 * Module Name:  Empty Windows Service project.
 * Project:      Empty Windows Service - extend and employ as necessary
 *
 * Partial class to provide Dispose and InitializeComponent methods
 *
 * Revisions:
 *     1. Sundar Krishnamurthy         sundar@passion8cakes.com             04/22/2016       Initial file created.
***************************************************************************/

namespace EmptyWindowsService {

    #region Partial EmptyService class
    /// <summary>
    /// Partial class to provide Dispose and InitializeComponent methods
    /// </summary>
    partial class EmptyService {

        #region Members
        /// <summary>Required designer variable</summary>
        private System.ComponentModel.IContainer components = null;
        #endregion

        #region Methods
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {

            if (disposing && (this.components != null)) {
                this.components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {

            this.components = new System.ComponentModel.Container();
            this.ServiceName = Constants.System.NtServiceName;
        }
        #endregion
        #endregion
    }
    #endregion
}
