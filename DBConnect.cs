/****************************** Module Header ******************************
 * Module Name:  Empty Windows Service project.
 * Project:      Empty Windows Service - extend and employ as necessary
 *
 * DBConnect class to interface with MySQL backend and run procedures.
 * 
 * Revisions:
 *     1. Sundar Krishnamurthy         sundar@passion8cakes.com             04/22/2016       Initial file created.
***************************************************************************/

namespace EmptyWindowsService {

    #region Using directives
    using System.Text;

    using MySql.Data.MySqlClient;
    #endregion

    #region DBConnect class
    /// <summary>
    /// DBConnect class - interface with MySQL backend and run procedures
    /// </summary>
    internal class DBConnect {

        #region Members
        /// <summary>MySqlConnection to DB</summary>
        private MySqlConnection connection;
        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        internal DBConnect() {
            this.Initialize();
        }
        #endregion

        #region Methods
        #region Public/internal Methods
        /// <summary>
        /// Get Next City data for provided CityId
        /// </summary>
        /// <param name="cityId">City ID</param>
        internal string GetNextCityData(uint cityId) {

            var cityData = null as string;

            if (this.OpenConnection()) {

                string query = Constants.Table.Queries.GetCity;

                // Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.AddWithValue(Constants.Table.Queries.Parameters.CityId, cityId);

                // Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                // Read the data and store them in the list
                if (dataReader.Read()) {

                    var builder = new StringBuilder();
                    builder.Append(dataReader[Constants.Table.Cities.Name] as string);
                    builder.Append(Constants.Space);
                    builder.Append(dataReader[Constants.Table.States.Abbreviation] as string);

                    cityData = builder.ToString();
                    builder.Remove(0, builder.Length);
                }

                //close Connection
                this.CloseConnection();
            }

            return cityData;
        }

        /// <summary>
        /// Get the total number of cities in the database
        /// </summary>
        internal uint GetCityCount() {

            var cityCount = 0u;

            if (this.OpenConnection()) {

                string query = Constants.Table.Queries.GetCityCount;

                // Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);

                // Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                // Read the data and store them in the list
                if (dataReader.Read()) {
                    cityCount = (uint)((long)(dataReader[Constants.Table.CompleteRecordCount]));
                }

                //close Connection
                this.CloseConnection();
            }

            return cityCount;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Initialize DBConnect instance
        /// </summary>
        private void Initialize() {

            // Construct MySqlConnection instance, specified by connection string
            this.connection = new MySqlConnection(ConfigData.Instance.ConnectionString);
        }

        /// <summary>
        /// Open connection
        /// </summary>
        /// <returns>True if connection succeeded, false otherwise</returns>
        private bool OpenConnection() {

            bool connected = false;

            try {
                // Attempt connection
                this.connection.Open();

                connected = true;
            } catch (MySqlException ex) {

                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number) {

                    case 0:
                        // Fail if you cannot connect to the backend database
                        throw new EmptyException(Constants.Messages.Database.CannotConnect, ex);

                    case 1045:
                        // Fail if the username password combination is invalid to the database
                        throw new EmptyException(Constants.Messages.Database.InvalidCredentials, ex);
                }
            }

            return connected;
        }

        /// <summary>
        /// Close connection
        /// </summary>
        private void CloseConnection() {

            this.connection.Close();
        }
        #endregion
        #endregion
    }
    #endregion
}
