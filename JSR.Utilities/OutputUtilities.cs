// <copyright file="OutputUtilities.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace JSR.Utilities
{
    /// <summary>
    /// Enum for type of evaluated implementation.
    /// </summary>
    public enum ImplementationTypeEnum
    {
        /// <summary>
        /// Evaluate property types.
        /// </summary>
        PropertyValue,

        /// <summary>
        /// Evaluate child class types.
        /// </summary>
        ClassValue,

        /// <summary>
        /// Evaluate list item types.
        /// </summary>
        ListItemValue,
    }

    /// <summary>
    /// Provides common Console output formatting.
    /// </summary>
    public static class OutputUtilities
    {
        /// <summary>
        /// Evaluates if a property's value implements a specific interface or inheritance before providing messaging to the Console.
        /// </summary>
        /// <param name="implementationType">Type of implementation to evaluate.</param>
        /// <param name="propertyName">Name of property.</param>
        /// <param name="typeToEvaluate">Type of property.</param>
        /// <param name="expectedImplementation">Expected interface of inheritance.</param>
        /// <param name="methodName">Name of calling method.</param>
        /// <returns>True if the evaluation was true.</returns>
        public static bool ExpectedImplementation(ImplementationTypeEnum implementationType, string propertyName, Type typeToEvaluate, Type expectedImplementation, [CallerMemberName] string methodName = null)
        {
            bool implementsType = expectedImplementation.IsAssignableFrom(typeToEvaluate);

            if (!implementsType)
            {
                Console.WriteLine("------POSSIBLE EXPECTED IMPLEMENTATION ERROR SEE BELOW FOR MORE INFORMATION------");
            }

            Console.WriteLine($"{methodName} | Property Name: {propertyName} | {GetImplementationType(implementationType)}: {typeToEvaluate} | Implements {expectedImplementation}: {implementsType}");

            return implementsType;
        }

        /// <summary>
        /// Evaluates if a property is readwrite before providing messaging to the Console.
        /// </summary>
        /// <param name="property">Property to evaluate.</param>
        /// <param name="methodName">Name of calling method.</param>
        /// <returns>True if the property is Read-Write.</returns>
        public static bool EvaluateIsReadWriteProperty(PropertyInfo property, [CallerMemberName] string methodName = null)
        {
            bool isReadWrite = PropertyUtilities.CheckIfPropertyIsReadWrite(property);

            if (!isReadWrite)
            {
                Console.WriteLine("------POSSIBLE EXPECTED READ-WRITE ERROR SEE BELOW FOR MORE INFORMATION------");
            }

            Console.WriteLine($"{methodName} | Property Name: {property.Name} | Is Read-Write: {isReadWrite}");

            return isReadWrite;
        }

        private static string GetImplementationType(ImplementationTypeEnum implementationType)
        {
            switch (implementationType)
            {
                case ImplementationTypeEnum.PropertyValue:
                    return "Property Type";
                case ImplementationTypeEnum.ClassValue:
                    return "Child Object Type";
                case ImplementationTypeEnum.ListItemValue:
                    return "List Item Type";
                default:
                    return string.Empty;
            }
        }
    }
}
