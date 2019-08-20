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
    /// <see cref="IChangableCollection{T}"/> implements <see cref="IChangeTrackingCollection{T}"/>, and <see cref="IChangable"/>.
    /// </summary>
    /// <typeparam name="T">Type of list items that implement <see cref="IChangable"/>.</typeparam>
    public interface IChangableCollection<T> : IChangeTrackingCollection<T>, IChangable where T : IChangable
    {
    }
}
