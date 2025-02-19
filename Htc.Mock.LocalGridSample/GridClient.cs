﻿// GridClient.cs is part of the Htc.Mock solution.
// 
// Copyright (c) 2021-2021 ANEO. All rights reserved.
// * Wilfried KIRSCHENMANN (https://github.com/wkirschenmann)
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Htc.Mock.RequestRunners;

using Microsoft.Extensions.Logging;

namespace Htc.Mock.LocalGridSample
{
  public class GridClient : IGridClient
  {
    private readonly ILoggerFactory loggerFactory_;
    private readonly GridWorker     gridWorker_;
    private readonly GridData       gridData_ = new();

    public GridClient(ILoggerFactory loggerFactory)
    {
      loggerFactory_ = loggerFactory;
      gridWorker_ = new(new DistributedRequestRunnerFactory(this,
                                                            loggerFactory.CreateLogger<DistributedRequestRunner>(),
                                                            true,
                                                            true,
                                                            true),
                        loggerFactory.CreateLogger<GridWorker>());
    }

    /// <inheritdoc />
    public ISessionClient CreateSubSession(string taskId) => new SessionClient(loggerFactory_, gridWorker_, gridData_, taskId);

/// <inheritdoc />
    public ISessionClient CreateSession() => new SessionClient(loggerFactory_, gridWorker_, gridData_, string.Empty);
  }
}