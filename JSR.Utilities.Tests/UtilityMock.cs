// <copyright file="UtilityMock.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

namespace JSR.Utilities.Tests
{
    /// <summary>
    /// Class to test Utility Features.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1044:Properties should not be write only", Justification = "Used for testing purposes.")]
    public class UtilityMock
    {
        private string writeOnlyStringProperty;

        /// <summary>
        /// Gets the string property.
        /// </summary>
        public string ReadOnlyStringProperty { get; }

        /// <summary>
        /// Sets the string property.
        /// </summary>
        public string WriteOnlyStringProperty
        {
            set
            {
                writeOnlyStringProperty = value;
            }
        }

        /// <summary>
        /// Gets or sets the string property.
        /// </summary>
        public string PublicStringProperty { get; set; }

        /// <summary>
        /// Gets or sets the int property.
        /// </summary>
        public int PublicIntProperty { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the bool property is true or false.
        /// </summary>
        public bool PublicBoolProperty { get; set; }

        /// <summary>
        /// Gets the value set to the write only property <see cref="WriteOnlyStringProperty"/>.
        /// </summary>
        /// <returns>A string value.</returns>
        public string GetWriteOnlyStringValue()
        {
            return writeOnlyStringProperty;
        }
    }
}
