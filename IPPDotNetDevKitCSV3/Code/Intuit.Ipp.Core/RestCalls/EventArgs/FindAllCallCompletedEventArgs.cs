////********************************************************************
// <copyright file="FindAllCallCompletedEventArgs.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains logic for REST request handler.</summary>
////********************************************************************

namespace Intuit.Ipp.Core
{
    using System;
    using System.Collections.Generic;
    using Intuit.Ipp.Data;
    using Intuit.Ipp.Exception;

    /// <summary>
    /// Event argument is class used to communicate after FindAll operation completed.
    /// </summary>
    public partial class FindAllCallCompletedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the FindAllCallCompletedEventArgs class.
        /// </summary>
        public FindAllCallCompletedEventArgs()
        {
            this.Entities = new List<IEntity>();
        }

        /// <summary>
        /// Gets or sets Entities from the result.
        /// </summary>
        public IList<IEntity> Entities
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets Ids Exception.
        /// </summary>
        public IdsException Error
        {
            get;
            set;
        }
    }
}
