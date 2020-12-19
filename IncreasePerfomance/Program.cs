namespace IncreasePerfomance
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataRep = new DataRepository();

            var service = new Service(dataRep);

            service.GetDataSimple(); // 2930 ms

            var result = service.GetData();

            service.Clear();

            service.LoadDataInTasks(); // 15 ms

            result = service.GetData();
        }
    }
}
