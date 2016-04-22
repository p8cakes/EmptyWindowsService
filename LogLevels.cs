/****************************** Module Header ******************************
 * Module Name:  Empty Windows Service project.
 * Project:      Empty Windows Service - extend and employ as necessary
 *
 * LogLevels to enumerate levels of logging configured.
 *
 * Revisions:
 *     1. Sundar Krishnamurthy         sundar@passion8cakes.com             04/22/2016       Initial file created.
***************************************************************************/

namespace EmptyWindowsService {

    #region LogLevels enumeration
    /// <summary>
    /// Enumerate levels of logging configured.
    ///     0: Invalid - do not use.
    ///     1: Normal, don't log to event or DB.
    ///     1: Verbose, log starts and stops to event-log.
    ///     2: VeryVerbose, log timer wake-ups to event-log.
    /// </summary>
    internal enum LogLevels {

        // Invalid - do not use.
        Invalid = 0,

        // Normal - don't log to event or DB.
        Normal = 1,

        // Verbose - log starts and stops to event log.
        Verbose = 2,

        // VeryVerbose - log timer wake-ups to event log.
        VeryVerbose = 3
    }
    #endregion
}
