// <copyright file="IChangableCollection.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace JSR.BaseClassLibrary
{
    /// <summary>
    /// <see cref="IChangableCollection{T}"/> implements <see cref="IChangableObject"/> into a <see cref="IList"/>.
    /// </summary>
    /// <typeparam name="T">Type that implements <see cref="IChangableObject"/>.</typeparam>
    public interface IChangableCollection<T> : IChangableObject, IChangeTrackingCollection<T>, IList where T : IChangableObject
    {
    }
}
