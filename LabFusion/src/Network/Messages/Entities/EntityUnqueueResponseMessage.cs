﻿using LabFusion.Data;
using LabFusion.Entities;
using LabFusion.Exceptions;
using LabFusion.Syncables;
using LabFusion.Utilities;

namespace LabFusion.Network
{
    public class EntityUnqueueResponseData : IFusionSerializable
    {
        public const int Size = sizeof(ushort) * 2;

        public ushort queuedId;
        public ushort allocatedId;

        public void Serialize(FusionWriter writer)
        {
            writer.Write(queuedId);
            writer.Write(allocatedId);
        }

        public void Deserialize(FusionReader reader)
        {
            queuedId = reader.ReadUInt16();
            allocatedId = reader.ReadUInt16();
        }

        public static EntityUnqueueResponseData Create(ushort queuedId, ushort allocatedId)
        {
            return new EntityUnqueueResponseData()
            {
                queuedId = queuedId,
                allocatedId = allocatedId
            };
        }
    }

    public class EntityUnqueueResponseMessage : FusionMessageHandler
    {
        public override byte? Tag => NativeMessageTag.EntityUnqueueResponse;

        public override void HandleMessage(byte[] bytes, bool isServerHandled = false)
        {
            // Make sure this isn't handled by the server
            if (isServerHandled)
            {
                throw new ExpectedClientException();
            }

            using var reader = FusionReader.Create(bytes);
            var data = reader.ReadFusionSerializable<EntityUnqueueResponseData>();
            var (success, entity) = NetworkEntityManager.IdManager.UnqueueEntity(data.queuedId, data.allocatedId);

#if DEBUG
            if (success)
            {
                FusionLogger.Log($"Unqueued Entity with allocated id {entity.Id}.");
            }
#endif
        }
    }
}
