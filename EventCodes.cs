/****************************** Module Header ******************************
 * Module Name:  Empty Windows Service project.
 * Project:      Empty Windows Service - extend and employ as necessary
 *
 * EventCodes enumeration to describe event codes to be written to log.
 *
 * Revisions:
 *     1. Sundar Krishnamurthy         sundar@passion8cakes.com             04/22/2016       Initial file created.
***************************************************************************/

namespace EmptyWindowsService {

    #region EventCodes enumeration
    /// <summary>
    /// EventCodes enumeration to describe event codes to be written to log.
    ///     501: Normal, don't log to event or DB.
    ///     502: StoppingService, stopping the service.
    ///     503: VeryVerbose, log timer wake-ups to event-log.
    ///     504: CompletedTask - finished scheduled task.
    ///     505: FlagThreadpoolStoppage - NT Service thread attempting to flag down threadpools that may be auditing
    ///     506: LogStartTime - log the starting time to event log
    ///     507: CityFound - next city we visit for some lemon margarita!
    ///     601: Error - something foul happened
    /// </summary>
    internal enum EventCodes {

        // Starting the Service.
        StartingService = 501,

        // Stopping the Service.
        StoppingService = 502,

        // StartingTask - starting new task.
        StartingTask = 503,

        // CompletedTask - finished scheduled task.
        CompletedTask = 504,

        // FlagThreadPoolStoppage - NT Service thread attempting to flag down threadpools that may be auditing
        FlagThreadPoolStoppage = 505,

        // Log the starting time to Event Log
        LogStartTime = 506,

        // City is found
        CityFound = 507,

        // Error executing the task this time
        Error = 601
    }
    #endregion
}
