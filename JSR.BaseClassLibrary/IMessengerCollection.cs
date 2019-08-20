// <copyright file="IMessengerCollection.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System.Collections;

namespace JSR.BaseClassLibrary
{
    /// <summary>
    /// <see cref="IMessengerCollection{T}"/> implements <see cref="IList"/> and <see cref="IMessenger"/>.
    /// </summary>
    /// <typeparam name="T">Type that implements <see cref="IMessenger"/>.</typeparam>
    public interface IMessengerCollection<T> : IList, IMessenger where T : IMessenger
    {
    }
}
