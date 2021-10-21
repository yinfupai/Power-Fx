//------------------------------------------------------------------------------
// <copyright file="EventArgs.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.PowerFx.Core.Utils;

namespace Microsoft.PowerFx.Core.Logging
{
    public interface ITrackEventArgs
    {
        string EventName { get; }
        string SerializedJson { get; }
    }

    public interface IEndScenarioEventArgs
    {
        string ScenarioGuid { get; }
        string SerializedJson { get; }
    }

    internal sealed class TrackEventArgs : ITrackEventArgs
    {
        public string EventName { get; }
        public string SerializedJson { get; }

        internal TrackEventArgs(string eventName, string serializedJson)
        {
            Contracts.AssertNonEmpty(eventName);
            Contracts.AssertNonEmpty(serializedJson);

            EventName = eventName;
            SerializedJson = serializedJson;
        }
    }

    internal sealed class EndScenarioEventArgs: IEndScenarioEventArgs
    {
        public string ScenarioGuid { get; }
        public string SerializedJson { get; }

        internal EndScenarioEventArgs(string scenarioGuid, string serializedJson)
        {
            Contracts.AssertNonEmpty(scenarioGuid);
            Contracts.AssertNonEmpty(serializedJson);

            ScenarioGuid = scenarioGuid;
            SerializedJson = serializedJson;
        }
    }
}