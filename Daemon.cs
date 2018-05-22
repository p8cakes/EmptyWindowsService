/****************************** Module Header ******************************
 * Module Name:  Empty Windows Service project.
 * Project:      Empty Windows Service - extend and employ as necessary
 *
 * Daemon class to perform task of fetching next random city from DB, and writing to event log
 *
 * Revisions:
 *     1. Sundar Krishnamurthy         sundar@passion8cakes.com             04/22/2016       Initial file created.
***************************************************************************/

namespace EmptyWindowsService {

    #region Using directives
    using System;
    using System.Diagnostics;
    #endregion

    #region Daemon class
    /// <summary>
    /// Daemon class to perform task of fetching next random city from DB, and writing to event log
    /// </summary>
    internal class Daemon {

        #region Members
        /// <summary>Total number of cities that we have</summary>
        private uint cityCount;

        /// <summary>A random instance of the Random class :)</summary>
        private Random random;

        /// <summary>Reference to ConfigData singleton instance</summary>
        private ConfigData configData;

        /// <summary>DBConnect instance</summary>
        private DBConnect dbConnect;
        #endregion

        #region Constructor
        /// <summary>
        /// Obtain references to ConfigData singleton, construct DBConnect instance
        /// </summary>
        internal Daemon() {

            // Get reference to ConfigData singleton
            this.configData = ConfigData.Instance;

            // New DBConnect instance for talking to DB
            this.dbConnect = new DBConnect();

            // New random instance
            this.random = new Random();

            this.cityCount = dbConnect.GetCityCount();
        }
        #endregion

        #region Methods
        #region public/internal Methods
        /// <summary>
        /// Main method - well, visit next city!
        /// </summary>
        internal void VisitNextCity() {

            if (configData.LogLevel == LogLevels.VeryVerbose) {
                EventLog.WriteEntry(Constants.System.NtServiceName,
                    Constants.Messages.AppSpecific.NextVisitStarted,
                    EventLogEntryType.Information,
                    (int)EventCodes.StartingTask);
            }

            var nextCityId = ((uint)(this.random.Next() % this.cityCount)) + 1u;

            var city = dbConnect.GetNextCityData(nextCityId);

            EventLog.WriteEntry(Constants.System.NtServiceName,
                string.Format(Constants.Messages.AppSpecific.CityVisit, city),
                EventLogEntryType.Information,
                (int)EventCodes.CityFound);
        }
        #endregion
        #endregion
    };
    #endregion
}
