// <copyright file="UtilityMock.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;

namespace JSR.Utilities.Tests
{
    /// <summary>
    /// Class to test Utility Features.
    /// </summary>
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
    }
}
