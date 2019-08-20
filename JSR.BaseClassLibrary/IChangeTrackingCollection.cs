// <copyright file="IChangeTrackingCollection.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System.Collections;
using System.ComponentModel;

namespace JSR.BaseClassLibrary
{
    /// <summary>
    /// <see cref="IChangeTrackingCollection{T}"/> implements <see cref="IList"/> and <see cref="IChangeTracking"/>.
    /// </summary>
    /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
    public interface IChangeTrackingCollection<T> : IList, IChangeTracking where T : IChangeTracking
    {
    }
}
