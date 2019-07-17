// <copyright file="PropertyChangeAssertTests.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JSR.TestAsserts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSR.Utilities.Tests
{
    /// <summary>
    /// Random tests to check functionality.
    /// </summary>
    [TestClass]
    public class PropertyChangeAssertTests
    {
        /// <summary>
        /// Tests the ChangesValues by type method.
        /// </summary>
        [TestMethod]
        public void ChangesValuesByType()
        {
            PropertyChangeAssert.ChangesValues(typeof(UtilityMock));

            List<string> stringAsClass = typeof(UtilityMock).GetProperties().Where(x => x.PropertyType.IsClass).Select(x => x.Name).ToList();

            foreach (string stringProperty in stringAsClass)
            {
                Console.WriteLine($"String as Class: {stringProperty}");
            }

            if (stringAsClass.Count == 0)
            {
                Console.WriteLine("No strings found as classes");
            }

            List<string> stringAsList = typeof(UtilityMock).GetProperties().Where(x => typeof(IList).IsAssignableFrom(x.PropertyType)).Select(x => x.Name).ToList();

            foreach (string stringProperty in stringAsList)
            {
                Console.WriteLine($"String as List: {stringProperty}");
            }

            if (stringAsList.Count == 0)
            {
                Console.WriteLine($"No strings found as lists");
            }
        }
    }
}
