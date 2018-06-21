using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;


namespace API.Models
{

    [Table("Logger")]
    public partial class Logger
    {
        public enum ErrorType
        {
            /// <value>Informational message</value>
            Informational = 0,
            /// <value>Error message</value>
            Error = 1,
            /// <value>Debug message</value>
            Debug = 2
        }
        #region Properties
        public int ID { get; set; }
        public string MessageType { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public DateTime LogDate { get; set; }
        public string URL { get; set; }
        public string UserName { get; set; }
        #endregion
    }
}
