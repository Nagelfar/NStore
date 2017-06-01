﻿using System;

namespace NStore.Persistence
{
    public class NullPartitionConsumer : IPartitionConsumer
    {
        public static readonly NullPartitionConsumer Instance = new NullPartitionConsumer();

        private NullPartitionConsumer()
        {
        }

        public ScanAction Consume(long partitionIndex, object payload)
        {
			return ScanAction.Continue;
		}

        public void Completed()
        {
            
        }

        public void OnError(Exception ex)
        {
            throw ex;
        }
    }
}