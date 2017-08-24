﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NStore.Persistence
{
    public class Recorder : ISubscription
    {
        private readonly IList<IChunk> _data = new List<IChunk>();
        private readonly IDictionary<long, IChunk> _map = new Dictionary<long, IChunk>();
        public IEnumerable<object> Data => _data.Select(x=>x?.Payload);
        public int Length => _data.Count;
        public bool ReadCompleted { get; private set; }

        public Task<bool> OnNextAsync(IChunk chunk)
        {
            _data.Add(chunk);
            _map[chunk.Index] = chunk;
            return Task.FromResult(true);
        }

        public Task CompletedAsync(long position)
        {
            ReadCompleted = true;
            return Task.CompletedTask;
        }

        public Task StoppedAsync(long position)
        {
            ReadCompleted = true;
            return Task.CompletedTask;
        }

        public Task OnStartAsync(long position)
        {
            return Task.CompletedTask;
        }

        public Task OnErrorAsync(long position, Exception ex)
        {
            throw ex;
        }

        public async Task ReplayAsync(Func<IChunk, Task> action)
        {
            await ReplayAsync(action, 0).ConfigureAwait(false);
        }

        public async Task ReplayAsync(Func<IChunk, Task> action, int startAt)
        {
            for (var i = startAt; i < _data.Count; i++)
            {
                await action(_data[i]).ConfigureAwait(false);
            }
        }

        public void Replay(Action<IChunk> action)
        {
            Replay(action, 0);
        }

        public void Replay(Action<IChunk> action, int startAt)
        {
            for (var i = startAt; i < _data.Count; i++)
            {
                action(_data[i]);
            }
        }

        public T[] ToArray<T>()
        {
            return _data.Select(x => x.Payload).Cast<T>().ToArray();
        }

        public bool IsEmpty => _data.Count == 0;
        public IChunk this[int position] => _data[position];

        public long GetIndex(int position) => _data[position].Index;
        public IChunk ByIndex(int index) => _map[index];
    }
}