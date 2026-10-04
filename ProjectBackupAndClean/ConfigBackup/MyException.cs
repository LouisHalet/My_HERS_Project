using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigBackup {
    /// <summary>
    /// This exception indicates the source and destination.
    /// </summary>
    public class SourceDestinationEqualException : Exception {
        public SourceDestinationEqualException()
            : base() { }
    }
    /// <summary>
    /// This exception indicates whether a bad day is selected in the drop-down list.
    /// </summary>
    public class InvalidDaySelectionException : Exception {
        public InvalidDaySelectionException()
            : base() { }
    }
}
