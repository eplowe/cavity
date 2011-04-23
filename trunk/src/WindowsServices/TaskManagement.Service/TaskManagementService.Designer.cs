namespace Cavity
{
    using System.ComponentModel;

    internal partial class TaskManagementService
    {
        private IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            // 
            // TaskManagementService
            // 
            this.AutoLog = false;
            this.CanPauseAndContinue = true;
            this.CanShutdown = true;
            this.ServiceName = "TaskManagementService";

        }

        #endregion
    }
}
