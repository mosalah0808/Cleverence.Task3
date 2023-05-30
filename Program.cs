using Cleverence.Task3;

int readerCount = 4;
int writerCount = 2;

// Запустим несколько потоков-читателей
for (int i = 0; i < readerCount; i++)
{
    new Thread(() =>
    {
        while (true)
        {
            int count = Server.GetCount();
            Console.WriteLine($"Reader {Thread.CurrentThread.ManagedThreadId} read count: {count}");
            Thread.Sleep(1000);
        }
    }).Start();
}

// Запустим несколько потоков-писателей
for (int i = 0; i < writerCount; i++)
{
    new Thread(() =>
    {
        while (true)
        {
            int value = new Random().Next(1, 3);
            Server.AddToCount(value);
            Console.WriteLine($"Writer {Thread.CurrentThread.ManagedThreadId} added {value} to count");
            Thread.Sleep(2000);
        }
    }).Start();
}

Console.ReadLine();