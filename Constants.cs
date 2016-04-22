/****************************** Module Header ******************************
 * Module Name:  Empty Windows Service project.
 * Project:      Empty Windows Service - extend and employ as necessary
 *
 * Constants static class to collect and list all strings used in the Windows Service
 *
 * Revisions:
 *     1. Sundar Krishnamurthy         sundar@passion8cakes.com             04/22/2016       Initial file created.
***************************************************************************/

namespace EmptyWindowsService {

    #region Constants class
    /// <summary>
    /// Constants static class to collect and list all strings used in the Windows Service
    /// </summary>
    internal static class Constants {

        #region Members
        /// <summary>One thousand, to convert seconds to milliseconds</summary>
        internal const double Thousand = 1000d;

        /// <summary>Null, as string</summary>
        internal const string Null = "null";

        /// <summary>Space, as character</summary>
        internal const char Space = ' ';

        /// <summary>Yes, as string</summary>
        internal const string Yes = "Yes";

        /// <summary>No, as string</summary>
        internal const string No = "No";

        /// <summary>The comma</summary>
        internal const char Comma = ',';

        /// <summary>Open Square bracket string</summary>
        internal const string OpenSquareBracket = "[";

        /// <summary>Close Square bracket with space string</summary>
        internal const string CloseSquareBracketSpace = "] ";

        /// <summary>Comma array</summary>
        internal static readonly char[] CommaArray = new char[] {
                                                         Constants.Comma
                                                     };

        /// <summary>The comma with a trailing space</summary>
        internal const string CommaSpace = ", ";

        /// <summary>The semicolon</summary>
        internal const char Semicolon = ';';

        /// <summary>Command-line option to run this in debug mode</summary>
        internal const string DebugOption = "-debug";

        /// <summary>Empty message for data not-found</summary>
        internal const string Empty = "[Empty]";

        /// <summary>Start now - value for the StartAt parameter below to do this right away!</summary>
        internal const string StartNow = "[Now]";

        /// <summary>Key attribute in XML</summary>
        internal const string Key = "key";

        /// <summary>Value attribute in XML</summary>
        internal const string Value = "value";

        /// <summary>Database Connection String</summary>
        internal const string DatabaseConnectionString = "SERVER={0};DATABASE={1};UID={2};PASSWORD={3}";

        /// <summary>Time Format string used for parsing</summary>
        internal const string FullDateTimeFormat = "yyyy-MM-dd HH:mm:ss";

        /// <summary>Time Format string used for parsing</summary>
        internal const string TimeFormat = "HH:mm:ss";

        #region System class
        /// <summary>
        /// Collect together all System-related constants
        /// </summary>
        internal static class System {

            #region Members
            /// <summary>Text to display on NT Service list.</summary>
            internal const string ServiceName = "Empty Windows Service";

            /// <summary>App Name to register on NT Service task list.</summary>
            internal const string NtServiceName = "EmptyWindowsService";

            /// <summary>Service Description on NT Service task list.</summary>
            internal const string ServiceDesc = "Empty Windows Service - visit cities for Lemon Margarita!";

            /// <summary>Application Event type</summary>
            internal const string Application = "Application";
            #endregion
        }
        #endregion

        #region Messages class
        /// <summary>
        /// Collect together all Message strings
        /// </summary>
        internal static class Messages {

            #region Members
            /// <summary>Starting Service message</summary>
            internal const string StartingService = "Starting Service";

            /// <summary>Stopping Service message</summary>
            internal const string StoppingService = "Stopping Service";

            /// <summary>Event log status message to be written on task start-up.</summary>
            internal const string LaunchingNewTask = "Launching new task";

            /// <summary>Event log status message to be written on task completion.</summary>
            internal const string CompletedNewTask = "Completed new task";

            // <summary>Event log status message to be written when Stop is clicked and a worker may be busy.</summary>
            internal const string FlaggingThreadPoolStart = "Attempting to shut down service, waiting for a busy worker";

            /// <summary>Status message for Post Daemon object being called to perform tasks</summary>
            internal const string PostDaemonSingletonInService = "Post Daemon instance in service, performing tasks";

            /// <summary>Service first run has been scheduled for</summary>
            internal const string ServiceFirstRunScheduledFor = "Service first run scheduled for: {0}";

            /// <summary>Exception message to throw when LogLevel string is not supplied to set incorrectly</summary>
            internal const string MissingLogLevel = "Invalid or missing LogLevels key in config file, please use 1, 2, 3 or 4. Found: {0}";

            /// <summary>Exception message to throw when ForceInterval string is not supplied to set incorrectly</summary>
            internal const string MissingForceInterval = "Invalid or missing ForceInterval key in config file, please use true or false. Found: {0}";

            /// <summary>Exception message to throw when configured StartAt value occurs in the past - it has to be in the future</summary>
            internal const string StartTimeInPast = "Start time occurs in the past. Found: {0}, please employ one in the future!";

            /// <summary>Exception message to throw when configured StartAt value is incorrect, and can't be parsed!</summary>
            internal const string StartTimeIncorrect = "Start time is incorrect: {0}, please employ one that is correct!";

            /// <summary>Exception message to throw when TimerDuration key in the configuration has an invalid value</summary>
            internal const string MissingOrInvalidTimerDuration = "Missing or invalid TimerDuration value in configuration! Found: {0}";
            #endregion

            #region AppSpecific class
            /// <summary>
            /// App Specific messages
            /// </summary>
            internal static class AppSpecific {
                /// <summary>Started next visit!</summary>
                internal const string NextVisitStarted = "Started next visit!";

                /// <summary>Message to write about a city visit!</summary>
                internal const string CityVisit = "I had some great lemon margaritas in {0}";
            }
            #endregion

            #region Database class
            /// <summary>
            /// Collect together all Database messages
            /// </summary>
            internal static class Database {
                #region Members
                /// <summary>Exception message to throw when one or more configuration parameters missing for database</summary>
                internal const string ConfigurationParameterMissing = "Configuration Parameter(s) for database absent: {0}";

                /// <summary>Exception message to throw when you cannot connect to the database</summary>
                internal const string CannotConnect = "Cannot connect to the database";

                /// <summary>Exception message to throw when invalid login/password combination is supplied</summary>
                internal const string InvalidCredentials = "Invalid login/password supplied";
                #endregion
            }
            #endregion
        }
        #endregion

        #region Table class
        /// <summary>
        /// Collect together all Database table-related constants for column names and stored procedures
        /// </summary>
        internal static class Table {

            /// <summary>Complete Record Count - how many did we find in a table?</summary>
            internal const string CompleteRecordCount = "completeRecordCount";

            #region States class
            /// <summary>
            /// Column names for the States table (we only bother about abbreviations)
            /// </summary>
            internal static class States {
                #region Members
                /// <summary>Abbreviation column in states table</summary>
                internal const string Abbreviation = "abbreviation";
                #endregion
            }
            #endregion

            #region Cities class
            /// <summary>
            /// Column names for the Cities table (we only bother about name)
            /// </summary>
            internal static class Cities {
                #region Members
                /// <summary>Name column in cities table</summary>
                internal const string Name = "name";
                #endregion
            }
            #endregion

            #region Queries class
            /// <summary>
            /// Queries that we execute to fetch data
            /// </summary>
            internal static class Queries {
                #region Members
                /// <summary>Get Next City from DB, for provided cityId</summary>
                internal const string GetCity = "call getCity( @cityId );";

                /// <summary>Get Next City from DB, for provided cityId</summary>
                internal const string GetCityCount = "call getCityCount();";

                #region Parameters we furnish to stored procedures
                /// <summary>
                /// Parameters we furnish to stored procedures
                /// </summary>
                internal static class Parameters {
                    #region Members
                    /// <summary>City ID parameter</summary>
                    internal const string CityId = "@cityId";
                    #endregion
                }
                #endregion
                #endregion
            }
            #endregion
        }
        #endregion

        #region Configuration class
        /// <summary>
        /// Collect together all Configuration-related strings
        /// </summary>
        internal static class Configuration {

            #region Members
            /// <summary>Configuration key for timer duration.</summary>
            internal const string TimerDuration = "TimerDuration";

            /// <summary>Configuration key for event logging (1, 2, 3 or 4)</summary>
            internal const string LogLevel = "LogLevel";

            /// <summary>When do we start this, a blank value or [Now] means right now!</summary>
            internal const string StartAt = "StartAt";

            /// <summary>Whether this application needs to run just once</summary>
            internal const string RunOnce = "RunOnce";

            /// <summary>Whether this application needs to force the interval between runs?</summary>
            internal const string ForceInterval = "ForceInterval";

            /// <summary>Whether this application needs to force create an email and send it out once when the system comes up</summary>
            internal const string DebugTestRunOneTime = "DebugTestRunOneTime";

            #region Database class
            /// <summary>
            /// Collect together all configuration strings for database connections
            /// </summary>
            internal static class Database {

                #region Members
                /// <summary>Endpoint parameter</summary>
                internal const string Endpoint = "Endpoint";

                /// <summary>Database parameter</summary>
                internal const string DB = "Database";

                /// <summary>Login Parameter</summary>
                internal const string Login = "Login";

                /// <summary>Password parameter</summary>
                internal const string Password = "Password";
                #endregion
            }
            #endregion
            #endregion
        }
        #endregion
        #endregion
    }
    #endregion
}
