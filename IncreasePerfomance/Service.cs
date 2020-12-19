using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace IncreasePerfomance
{
    class Service
    {
        private DataRepository dataRepository;

        private List<List<int>> DataFromFiles = new List<List<int>>();

        private int _maxCountOfThreads = Environment.ProcessorCount > 2 ? Environment.ProcessorCount - 1 : 1;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dataRepository">Data repository</param>
        public Service(DataRepository dataRepository)
        {
            this.dataRepository = dataRepository;
        }

        /// <summary>
        /// Loads data in parallel 
        /// </summary>
        public void LoadDataInTasks()
        {
            var tasks = new List<Task>(_maxCountOfThreads);
            var semafore = new SemaphoreSlim(_maxCountOfThreads);

            for (int i = 0; i < 10; i++)
            {
                DataFromFiles.Add(new List<int>());
            }

            for (int j = 0; j < 10; j++)
            {
                var index = j;
                semafore.Wait();
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    DataFromFiles[index] = dataRepository.Deserialize(index);
                }).ContinueWith(t => { semafore.Release(); }));
            }

            Task.WaitAll(tasks.ToArray());
        }

        /// <summary>
        /// Gets data 
        /// </summary>
        /// <returns>Collection of collections of integers</returns>
        public List<List<int>> GetData()
        {
            return DataFromFiles;
        }

        /// <summary>
        /// Reads data from file in one thread
        /// </summary>
        /// <returns>Collection of collections of integers</returns>
        public void GetDataSimple()
        {
            for (int i = 0; i < 10; i++)
            {
                DataFromFiles.Add(new List<int>());
            }

            for (int j = 0; j < 10; j++)
            {
                DataFromFiles[j] = dataRepository.Deserialize(j);
            }
        }

        /// <summary>
        /// Clears result
        /// </summary>
        public void Clear()
        {
            DataFromFiles.Clear();
        }
    }
}
