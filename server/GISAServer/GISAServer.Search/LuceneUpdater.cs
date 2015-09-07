using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

using log4net;

using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;

namespace GISAServer.Search
{
    public abstract class LuceneUpdater
    {
        // Logging initializations
        private static readonly ILog log = LogManager.GetLogger(typeof(LuceneUpdater));
        
        protected FSDirectory index;
        protected abstract Document GetDocument(long id);
        protected abstract IndexWriter GetIndexWriter();

        public abstract void CreateIndex();

        /// <summary>
        ///  Constructor.
        /// </summary>
        /// <param name="index">Index path.</param>
        public LuceneUpdater(FSDirectory index)
        {
            this.index = index;

            // Debug messages
            log.DebugFormat("Testing if '{0}' index exists...",this.index.Directory.Name);

            if (!IndexReader.IndexExists(this.index))
            {
                // Debug messages
                log.Debug("Index doesn't exist!");
                log.Debug("Creating one...");

                try
                {
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();

                    this.CreateIndex();

                    stopwatch.Stop();

                    // Debug messages
                    log.DebugFormat("Index created at {0} in {1:00}:{2:00}:{3:00}!", index, stopwatch.Elapsed.Hours, stopwatch.Elapsed.Minutes,stopwatch.Elapsed.Seconds);
                }
                catch (Exception e)
                {
                    log.Fatal(string.Format("Error creating index '{0}'!",this.index.Directory.Name), e);
                }
            }

            // Debug messages
            log.Debug("Lucene updater initialized!");
        }
        
        /// <summary>
        /// Gets the data need to update a given idNivel
        /// in the lucene index.
        /// </summary>
        /// <param name="id">id to get data from.</param>
        public void Update(long id)
        {
            try
            {
                ForceUnlockIndex();
                this.Delete(id);
                this.Add(id);
            }
            catch (LockObtainFailedException e)
            {
                log.Error(string.Format("Unable to update id {0} due to lock obtain failure on index {1}!", id, this.index.Directory.Name), e);
            }
            catch (Exception e)
            {
                log.Error(string.Format("Unable to update id {0} on index {1}!", id, this.index.Directory.Name), e);
            }
            
        }

        /// <summary>
        /// Gets the data need to update a list of idNivel
        /// in the lucene index.
        /// </summary>
        /// <param name="id">id to get data from.</param>
        public void Update(List<long> ids)
        {
            try
            {
                ids.Distinct().ToList().ForEach(id => Update(id));
            }
            catch (LockObtainFailedException e)
            {
                log.Error(string.Format("Unable to update id {0} due to lock obtain failure on index {1}!", ids, this.index.Directory.Name), e);
            }
            catch (Exception e)
            {
                log.Error(string.Format("Unable to update id {0} on index {1}!", ids, this.index.Directory.Name), e);
            }
        }

        public void OptimizedUpdate(List<long> ids)
        {
            var ticks = DateTime.Now.Ticks;

            try
            {
                var unique_ids = ids.Distinct().ToList();
                Delete(unique_ids);
                Add(unique_ids);
            }
            catch (LockObtainFailedException e)
            {
                log.Error(string.Format("Unable to update id {0} due to lock obtain failure on index {1}!", ids, this.index.Directory.Name), e);
            }
            catch (Exception e)
            {
                log.Error(string.Format("Unable to update id {0} on index {1}!", ids, this.index.Directory.Name), e);
            }

            log.Debug("OptimizedUpdate (" + ids.Count.ToString() + "): " + new TimeSpan(DateTime.Now.Ticks - ticks).ToString());
        }

        /// <summary>
        /// Optimize index.
        /// </summary>
        public void Optimize()
        {
            using(IndexWriter indexWriter = GetIndexWriter())
            {
                indexWriter.Optimize();
                indexWriter.Dispose();
            }
        }
        
        /// <summary>
        /// Delete the entry with the given id.        
        /// </summary>
        /// <param name="id">Document to delete ID</param>
        private void Delete(long id)
        {
            using(IndexReader indexReader = IndexReader.Open(this.index, false))
            {
                indexReader.DeleteDocuments(new Term("id", id.ToString()));
                indexReader.Dispose();
            }
        }

        /// <summary>
        /// Delete the entry with the given list of id.        
        /// </summary>
        /// <param name="id">Document to delete ID</param>
        private void Delete(List<long> id)
        {
            using(IndexReader indexReader = IndexReader.Open(this.index, false))
            {
                id.ForEach(i => indexReader.DeleteDocuments(new Term("id", i.ToString())));
                indexReader.Dispose();
            }
        }

        public void Add(long id)
        {
            using (IndexWriter indexWriter = GetIndexWriter())
            {
                indexWriter.AddDocument(this.GetDocument(id));
                indexWriter.Dispose();
            }
        }

        public void Add(List<long> ids)
        {
            using (IndexWriter indexWriter = GetIndexWriter())
            {
                ids.ForEach(id => indexWriter.AddDocument(this.GetDocument(id)));
                indexWriter.Dispose();
            }
        }

        protected void ForceUnlockIndex()
        {
            try
            {
                if (IndexWriter.IsLocked(this.index)) IndexWriter.Unlock(this.index);
                var lockFilePath = Path.Combine(this.index.Directory.Name, "write.lock");
                if (File.Exists(lockFilePath)) File.Delete(lockFilePath);
            }
            catch (Exception e)
            {
                var msg = string.Format("{0} is locked and was unable to unlock.", this.index.Directory.Name);
                log.Error(msg, e);
                throw new LockObtainFailedException(msg, e);
            }
        }
    }
}
