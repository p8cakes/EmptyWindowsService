/****************************** Module Header ******************************
 * Module Name:  Empty Windows Service project.
 * Project:      Empty Windows Service - extend and employ as necessary
 *
 * ConfigData: Singleton class to manage all configuration data
 *
 * Revisions:
 *     1. Sundar Krishnamurthy         sundar@passion8cakes.com             04/22/2016       Initial file created.
***************************************************************************/

namespace EmptyWindowsService {

    #region Using directives
    using System;
    using System.Configuration;
    using System.Globalization;
    using System.Text;
    #endregion

    #region ConfigData class
    /// <summary>
    /// Singleton class to manage all configuration data
    /// </summary>
    internal class ConfigData {

        #region Members
        /// <summary>Reference to ConfigData's instance - not lazy, constructed at AppDomain class-load</summary>
        private static ConfigData configData = new ConfigData();

        /// <summary>ThreadLock reference for making certain we can semaphore tasks</summary>
        private ThreadLock threadLock;
        #endregion

        #region Constructor
        /// <summary>
        /// Private constructor, since this is a singleton class
        /// </summary>
        private ConfigData() {
            this.LogLevel = LogLevels.Invalid;

            // Construct ThreadLock instance - default to true to make first caller run!
            this.threadLock = new ThreadLock(true);

            // Set default configData duration to 900000d = 900 seconds, 15 minutes
            this.Duration = 900000d;

            // Parse TimerDuration from config, if defined
            var timerDuration = ConfigurationManager.AppSettings[Constants.Configuration.TimerDuration];

            if (ulong.TryParse(timerDuration, out ulong confDuration)) {
                this.Duration = confDuration * Constants.Thousand;
            } else {
                throw new ConfigurationErrorsException(string.Format(Constants.Messages.MissingOrInvalidTimerDuration, timerDuration ?? Constants.Null));
            }

            // Parse logVerbose from config, if defined
            var logVerbose = ConfigurationManager.AppSettings[Constants.Configuration.LogLevel];

            if (!string.IsNullOrEmpty(logVerbose) && (Enum.TryParse(logVerbose, out LogLevels logLevel))) {
                this.LogLevel = logLevel;
            }

            // Fail if no LogLevel specified in config
            if (this.LogLevel == LogLevels.Invalid) {
                throw new ConfigurationErrorsException(string.Format(Constants.Messages.MissingLogLevel, logVerbose ?? Constants.Empty));
            }

            // Get run once setting from config
            if (bool.TryParse(ConfigurationManager.AppSettings[Constants.Configuration.RunOnce], out bool runOnce)) {
                this.RunOnce = runOnce;
            }

            // Get force interval setting from config
            if (bool.TryParse(ConfigurationManager.AppSettings[Constants.Configuration.ForceInterval], out bool forceInterval)) {
                this.ForceInterval = forceInterval;
            } else {
                // Default should be true
                this.ForceInterval = true;
            }

            // Get the StartAt value from config - the service would fail if this value occurs in the past for the current day
            var startAtParameter = ConfigurationManager.AppSettings[Constants.Configuration.StartAt];

            // Only proceed if the value is provided, or is not set to "[Now]" - both seem to be valid cases and we don't explicitly enforce a StartAt = "Now" consideration for configuration
            if (!string.IsNullOrEmpty(startAtParameter)) {

                // Check if this value is not equal to "[Now]"
                if (!startAtParameter.Equals(Constants.StartNow, StringComparison.InvariantCultureIgnoreCase)) {


                    var now = DateTime.UtcNow;
                    var cultureInfo = CultureInfo.InvariantCulture;

                    // Try and parse the date time value from configuration as a full date-time value
                    if (DateTime.TryParseExact(startAtParameter, Constants.FullDateTimeFormat, cultureInfo, DateTimeStyles.None, out DateTime startTimeValue)) {

                        // Set the startAt parameter to specified time
                        this.StartAt = startTimeValue;
                    } else if (DateTime.TryParseExact(startAtParameter, Constants.TimeFormat, cultureInfo, DateTimeStyles.None, out startTimeValue)) {

                        this.StartAt = startTimeValue;

                        // Add another day if this date-time occurs in the past
                        if (now > startTimeValue) {
                            this.StartAt = startTimeValue.AddDays(1);
                        }
                    } else {
                        // Fail as you couldn't parse the datetime as specified in configuration!
                        throw new ConfigurationErrorsException(string.Format(Constants.Messages.StartTimeIncorrect, startAtParameter));
                    }
                }
            }

            // MySQL Connection string part!
            // Get domain from config
            var mySqlEndpoint = ConfigurationManager.AppSettings[Constants.Configuration.Database.Endpoint];
            var mySqlDatabase = ConfigurationManager.AppSettings[Constants.Configuration.Database.DB];
            var mySqlLogin = ConfigurationManager.AppSettings[Constants.Configuration.Database.Login];
            var mySqlPassword = ConfigurationManager.AppSettings[Constants.Configuration.Database.Password];

            var builder = null as StringBuilder;

            foreach (var parameter in new string[] {
                        mySqlEndpoint,
                        mySqlDatabase,
                        mySqlLogin,
                        mySqlPassword
                    }) {
                // Fail if no parameter is found for this key
                if (string.IsNullOrEmpty(parameter)) {
                    // Construct StringBuilder for the first time
                    if (builder == null) {
                        builder = new StringBuilder();
                    }

                    if (builder.Length > 0) {
                        builder.Append(Constants.CommaSpace);
                    }

                    builder.Append(parameter);
                }
            }

            if (builder != null) {

                var builderMessage = builder.ToString();
                builder.Remove(0, builder.Length);

                throw new ConfigurationErrorsException(string.Format(Constants.Messages.Database.ConfigurationParameterMissing, builderMessage));
            }

            this.ConnectionString = string.Format(Constants.DatabaseConnectionString, mySqlEndpoint, mySqlDatabase, mySqlLogin, mySqlPassword);
        }
        #endregion

        #region Properties
        /// <summary>Singleton instance accessor</summary>
        internal static ConfigData Instance { get { return ConfigData.configData; } }

        /// <summary>Duration we need to wait between subsequent audits</summary>
        internal Double Duration { get; private set; }

        /// <summary>StartAt value to specify when to run (UTC), null value means nothing has been set via config</summary>
        internal DateTime? StartAt { get; private set; }

        /// <summary>Level of logging desired</summary>
        internal LogLevels LogLevel { get; private set; }

        /// <summary>Whether this application should run just one time</summary>
        internal bool RunOnce { get; private set; }

        /// <summary>Whether this application should force the wait interval after finishing one iteration, or not</summary>
        internal bool ForceInterval { get; private set; }

        /// <summary>Connection String to MySQL DB</summary>
        internal string ConnectionString { get; private set; }

        /// <summary>Gets the ThreadLock instance used to semaphore tasks</summary>
        internal ThreadLock ThreadLock { get { return this.threadLock; } }
        #endregion
    }
    #endregion
}
