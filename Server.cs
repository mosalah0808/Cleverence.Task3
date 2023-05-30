using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleverence.Task3
{
    public static class Server
    {
        private static int count = 0;
        private static readonly SemaphoreSlim readSemaphore = new SemaphoreSlim(1);
        private static readonly SemaphoreSlim writeSemaphore = new SemaphoreSlim(1);

        public static int GetCount()
        {
            readSemaphore.Wait(); // ждем, пока не будет доступен семафор для чтения
            int result = count;
            readSemaphore.Release(); // освобождаем семафор для чтения
            return result;
        }

        public static void AddToCount(int value)
        {
            writeSemaphore.Wait(); // ждем, пока не будет доступен семафор для записи

            try
            {
                readSemaphore.Wait(); // ждем, пока все потоки, которые читают, завершат чтение

                count += value;
            }
            finally
            {
                readSemaphore.Release(); // освобождаем семафор для чтения
                writeSemaphore.Release(); // освобождаем семафор для записи
            }
        }
    }
}
