﻿// <copyright file="MockBaseMessengerParent.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

namespace JSR.BaseClasses.Tests.Mocks
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Mock")]
    public class MockBaseMessengerParent : MockBaseMessenger
    {
        private MockBaseMessenger child = new();

        public MockBaseMessengerParent()
        {
            AddChildMessaging(child);
        }

        public MockBaseMessenger Child { get => child; set => SetValue(ref child, value); }
    }
}