// ****************************************************************
// License Info:
// Portions Copyright © 2002-2009 Charlie Poole or 
// Copyright © 2002-2004 James W. Newkirk, Michael C. Two,
// Alexei A. Vorontsov or Copyright © 2000-2002 Philip A. Craig 
// ****************************************************************

using System;
using System.IO;

namespace TestMonkey.Assertion.Constraints
{
    /// <summary>
    ///     EmptyDirectoryConstraint is used to test that a directory is empty
    /// </summary>
    public class EmptyDirectoryConstraint : Constraint
    {
        private int files;
        private int subdirs;

        /// <summary>
        ///     Test whether the constraint is satisfied by a given value
        /// </summary>
        /// <param name="actual">The value to be tested</param>
        /// <returns>True for success, false for failure</returns>
        public override bool Matches(object actual)
        {
            internalActual = actual;

            var dirInfo = actual as DirectoryInfo;
            if (dirInfo == null)
                throw new ArgumentException("The internalActual value must be a DirectoryInfo", "actual");

            files = dirInfo.GetFiles().Length;
            subdirs = dirInfo.GetDirectories().Length;

            return files == 0 && subdirs == 0;
        }

        /// <summary>
        ///     Write the constraint description to a MessageWriter
        /// </summary>
        /// <param name="writer">The writer on which the description is displayed</param>
        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.Write("An empty directory");
        }

        /// <summary>
        ///     Write the internalActual value for a failing constraint test to a
        ///     MessageWriter. The default implementation simply writes
        ///     the raw value of internalActual, leaving it to the writer to
        ///     perform any formatting.
        /// </summary>
        /// <param name="writer">The writer on which the internalActual value is displayed</param>
        public override void WriteActualValueTo(MessageWriter writer)
        {
            var dir = internalActual as DirectoryInfo;
            if (dir == null)
                base.WriteActualValueTo(writer);
            else
            {
                writer.WriteActualValue(dir);
                writer.Write(" with {0} files and {1} directories", files, subdirs);
            }
        }
    }
}