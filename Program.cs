/****************************** Module Header ******************************
 * Module Name:  Empty Windows Service project.
 * Project:      Empty Windows Service - extend and employ as necessary
 *
 * Program class with static Main method to run application
 *
 * Revisions:
 *     1. Sundar Krishnamurthy         sundar@passion8cakes.com             04/22/2016       Initial file created.
***************************************************************************/

namespace EmptyWindowsService {

    #region Using directives
    using System;
    using System.ServiceProcess;
    #endregion

    #region Program class
    /// <summary>
    /// Process class, primary class with Main method.
    /// </summary>
    internal static class Program {

        #region Methods
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        internal static void Main(string[] args) {

            // Whether we launch the service as is, or run as a regular console app
            var launchService = true;

            // If you're running this from the command line in debug mode, probably run forever!
            var forever = true;

            // Check for the presence of a command line parameter: -debug
            foreach (var arg in args) {

                // We won't launch the service if we have a -debug option in the command line (case insensitive)
                if (string.Equals(arg, Constants.DebugOption, StringComparison.InvariantCultureIgnoreCase)) {
                    launchService = false;
                }
            }

            // Launch service as-is
            if (launchService) {
                var ServicesToRun = new ServiceBase[] {
                    new EmptyService()
                };

                ServiceBase.Run(ServicesToRun);
            } else {

                // Get current privilege
                var identity = System.Security.Principal.WindowsIdentity.GetCurrent();
                var principal = new System.Security.Principal.WindowsPrincipal(identity);

                // Create event source if it does not exist, and we have administrator privileges - check the latter condition first!
                if (principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator)) {

                    // Create if this does not exist
                    if (!System.Diagnostics.EventLog.SourceExists(Constants.System.NtServiceName)) {
                        System.Diagnostics.EventLog.CreateEventSource(Constants.System.NtServiceName, Constants.System.Application);
                    }
                }

                // Invoke Start manually in main thread
                var windowsService = new EmptyService();
                windowsService.Start(args, debugFlag: true);

                // Run forever, until killed by a Ctrl-C!
                while (forever) {
                    // Make main thread sleep so timer can do its work
                    System.Threading.Thread.Sleep(int.MaxValue);
                }
            }
        }
        #endregion
    }
    #endregion
}
