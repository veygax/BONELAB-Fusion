﻿using System;

using LabFusion.Data;

namespace LabFusion.Network
{
    public class PropReferenceData : IFusionSerializable, IDisposable
    {
        public const int Size = sizeof(byte) + sizeof(ushort);

        public byte smallId;
        public ushort syncId;

        public void Serialize(FusionWriter writer)
        {
            writer.Write(smallId);
            writer.Write(syncId);
        }

        public void Deserialize(FusionReader reader)
        {
            smallId = reader.ReadByte();
            syncId = reader.ReadUInt16();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public static PropReferenceData Create(byte smallId, ushort syncId)
        {
            return new PropReferenceData()
            {
                smallId = smallId,
                syncId = syncId,
            };
        }
    }
}