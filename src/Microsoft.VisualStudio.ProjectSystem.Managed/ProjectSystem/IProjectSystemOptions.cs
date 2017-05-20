﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Threading.Tasks;

namespace Microsoft.VisualStudio.ProjectSystem
{
    /// <summary>
    ///     Provides properties for retrieving options for the project system.
    /// </summary>
    internal interface IProjectSystemOptions
    {
        /// <summary>
        ///     Gets a value indicating if the project output pane is enabled.
        /// </summary>
        /// <value>
        ///     <see langword="true"/> if the project output pane is enabled; otherwise, <see langword="false"/>.
        /// </value>
        Task<bool> GetIsProjectOutputPaneEnabledAsync();

        /// <summary>
        ///     Gets a value indicating if the project fast up to date check is enabled.
        /// </summary>
        /// <value>
        ///     <see langword="true"/> if the project fast up to date check is enabled; otherwise, <see langword="false"/>
        /// </value>
        Task<bool> GetIsFastUpToDateCheckDisabledAsync();
    }
}
