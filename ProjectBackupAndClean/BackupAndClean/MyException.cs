using System;
using System.Collections.Generic;
using System.Text;

namespace BackupAndClean {
    /// <summary>
    /// This exception indicates the source and destination.
    /// </summary>
    public class SourceDestinationEqualException : Exception {
        public SourceDestinationEqualException()
            : base() { }
    }
    /// <summary>
    /// This exception indicates when there is no more space on the disk.
    /// </summary>
    public class NoSpaceInDrive : Exception {
        public NoSpaceInDrive()
            : base() { }
    }
}
