namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Hello World!");
        }

        //public class MyConsoleObserver<T> : IObserver<T>
        //{
        //    public void OnNext(T value)
        //    {
        //        Console.WriteLine($"Value is {value}");
        //    }

        //    public void OnCompleted()
        //    {
        //        Console.WriteLine("Completed!");
        //    }

        //    public void OnError(Exception error)
        //    {
        //        Console.WriteLine("Error?å");
        //    }
        //}

        //public class MyConsoleObservable : IObservable<int>
        //{
        //    public Type ElementType => throw new NotImplementedException();

        //    public Expression Expression => throw new NotImplementedException();

        //    public IObservableProvider Provider => throw new NotImplementedException();

        //    public IDisposable Subscribe(IObserver<int> observer)
        //    {
        //        observer.OnNext(1);
        //        observer.OnNext(2);
        //        observer.OnNext(4);
        //        observer.OnCompleted();

        //        return Disposable.Empty;
        //    }
        //}
    }
}
