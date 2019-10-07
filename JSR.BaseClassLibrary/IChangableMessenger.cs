// <copyright file="IChangableMessenger.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

namespace JSR.BaseClassLibrary
{
    /// <summary>
    /// <see cref="IChangableMessenger"/> implements <see cref="INotifyOnChanged"/> and <see cref="IMessenger"/>.
    /// </summary>
    public interface IChangableMessenger : INotifyOnChanged, IMessenger
    {
    }
}
