using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GISAServer.Search.Cache
{
    public class QueryCache
    {
        Dictionary<KeyValuePair<long, string>, QueryEntry> _cache = new Dictionary<KeyValuePair<long, string>, QueryEntry>();
        public int LIMIT { get; set; }
        public int VALID_TIME_SECONDS { get; set; }

        public QueryCache() {
            LIMIT = 50;
            VALID_TIME_SECONDS = 120;
        }

        public void Add(long user, string query, List<string> result)
        {
            if (_cache.Count >= LIMIT) SelectEntryToDelete();

            var entry = new QueryEntry();
            entry.query = query;
            entry.results = result;
            entry.user = user;
            _cache.Add(new KeyValuePair<long, string>(user, query), entry);
        }

        private void SelectEntryToDelete()
        {
            if (_cache.Count >= LIMIT)
            {
                var row = _cache.Values.OrderBy(e => e.date).First();
                _cache.Remove(new KeyValuePair<long, string>(row.user, row.query));
            }
        }

        public List<string> SearchInCache(long user, string query)
        {
            var pair = new KeyValuePair<long, string>(user, query);
            if (!_cache.ContainsKey(pair)) return null;
            var entry = _cache[pair];
            var now = DateTime.Now;
            if (new TimeSpan(entry.date.Ticks - now.Ticks).TotalSeconds > VALID_TIME_SECONDS) { _cache.Remove(pair); return null; }
            entry.counter++;
            return entry.results;
        }

        class QueryEntry
        {
            public string query;
            public List<string> results = new List<string>();
            public DateTime date = DateTime.Now;
            public long counter = 1;
            public long user;
        }
    }
}
