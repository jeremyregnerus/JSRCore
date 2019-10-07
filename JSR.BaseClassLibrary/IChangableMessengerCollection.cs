// <copyright file="IChangableMessengerCollection.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System.Collections;

namespace JSR.BaseClassLibrary
{
    /// <summary>
    /// <see cref="IChangableMessengerCollection{T}"/> implements <see cref="IList"/>, <see cref="INotifyOnChanged"/> and <see cref="IMessenger"/>.
    /// </summary>
    /// <typeparam name="T">Type that implements <see cref="IMessenger"/>.</typeparam>
    public interface IChangableMessengerCollection<T> : IChangableCollection<T>, IMessenger where T : IChangableMessenger
    {
    }
}
