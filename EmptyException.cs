/****************************** Module Header ******************************
 * Module Name:  Empty Windows Service project.
 * Project:      Empty Windows Service - extend and employ as necessary
 *
 * EmptyException extends Exception for our application.
 *
 * Revisions:
 *     1. Sundar Krishnamurthy         sundar@passion8cakes.com             04/22/2016       Initial file created.
***************************************************************************/

namespace EmptyWindowsService {

    #region Using directives
    using System;
    #endregion

    #region EmptyException class
    /// <summary>
    /// EmptyException extends Exception for our application
    /// </summary>
    internal class EmptyException : Exception {

        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        internal EmptyException() {
        }

        /// <summary>
        /// EmptyException constructed with message
        /// </summary>
        /// <param name="message">Exception message</param>
        internal EmptyException(string message)
            : base(message) {
        }

        /// <summary>
        /// EmptyException constructed with message and innerException
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner Exception instance</param>
        internal EmptyException(string message, Exception innerException)
            : base(message, innerException) {
        }
        #endregion
    }
    #endregion
}
