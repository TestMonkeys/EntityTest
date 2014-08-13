// ****************************************************************
// License Info:
// Portions Copyright © 2002-2009 Charlie Poole or 
// Copyright © 2002-2004 James W. Newkirk, Michael C. Two,
// Alexei A. Vorontsov or Copyright © 2000-2002 Philip A. Craig 
// ****************************************************************

using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace TestMonkey.Assertion.Constraints
{
    /// <summary>
    ///     BinarySerializableConstraint tests whether
    ///     an object is serializable in binary format.
    /// </summary>
    public class BinarySerializableConstraint : Constraint
    {
        private readonly BinaryFormatter serializer = new BinaryFormatter();

        /// <summary>
        ///     Test whether the constraint is satisfied by a given value
        /// </summary>
        /// <param name="actual">The value to be tested</param>
        /// <returns>True for success, false for failure</returns>
        public override bool Matches(object actual)
        {
            internalActual = actual;

            if (actual == null)
                throw new ArgumentException();

            var stream = new MemoryStream();

            try
            {
                serializer.Serialize(stream, actual);

                stream.Seek(0, SeekOrigin.Begin);

                object value = serializer.Deserialize(stream);

                return value != null;
            }
            catch (SerializationException)
            {
                return false;
            }
        }

        /// <summary>
        ///     Write the constraint description to a MessageWriter
        /// </summary>
        /// <param name="writer">The writer on which the description is displayed</param>
        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.Write("binary serializable");
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
            writer.Write("<{0}>", internalActual.GetType().Name);
        }

        /// <summary>
        ///     Returns the string representation
        /// </summary>
        protected override string GetStringRepresentation()
        {
            return "<binaryserializable>";
        }
    }
}