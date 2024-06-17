﻿using Il2CppSLZ.Bonelab;

using LabFusion.Utilities;

namespace LabFusion.Entities;

public class SimpleGripEventsExtender : EntityComponentArrayExtender<SimpleGripEvents>
{
    public static FusionComponentCache<SimpleGripEvents, NetworkEntity> Cache = new();

    protected override void OnRegister(NetworkEntity networkEntity, SimpleGripEvents[] components)
    {
        foreach (var events in components)
        {
            Cache.Add(events, networkEntity);
        }
    }

    protected override void OnUnregister(NetworkEntity networkEntity, SimpleGripEvents[] components)
    {
        foreach (var events in components)
        {
            Cache.Remove(events);
        }
    }
}