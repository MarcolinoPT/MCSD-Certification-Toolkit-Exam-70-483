namespace ConsoleApplication16.Chapter_5_Creating_and_Implementing_Class_Hierarchies
{
    using System;

    public class Managing_Object_Life_Cycle
    {
        public class Providing_Destructors
        {
            public class IDisposableExample : IDisposable
            {
                private bool disposedValue = false; // To detect redundant calls

                // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
                // ~IDisposableExample() {
                //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
                //   Dispose(false);
                // }

                // This code added to correctly implement the disposable pattern.
                public void Dispose()
                {
                    // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
                    Dispose(true);
                    // TODO: uncomment the following line if the finalizer is overridden above.
                    // GC.SuppressFinalize(this);
                }

                protected virtual void Dispose(bool disposing)
                {
                    if (!disposedValue)
                    {
                        if (disposing)
                        {
                            // TODO: dispose managed state (managed objects).
                        }

                        // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                        // TODO: set large fields to null.

                        disposedValue = true;
                    }
                }
            }

            public class DisposableClass : IDisposable
            {
                // A name to keep track of the object.
                public string Name = "";
                // Keep track if whether resources are already freed.
                private bool resourcesAreFreed = false;
                // Destructor to clean up unmanaged resources
                // but not managed resources.
                ~DisposableClass()
                {
                    FreeResources(false);
                }
                // Free managed and unmanaged resources.
                public void Dispose()
                {
                    FreeResources(true);
                }
                // Free resources.
                private void FreeResources(bool freeManagedResources)
                {
                    Console.WriteLine(Name + ": FreeResources");
                    if (!resourcesAreFreed)
                    {
                        // Dispose of managed resources if appropriate.
                        if (freeManagedResources)
                        {
                            // Dispose of managed resources here.
                            Console.WriteLine(Name + ": Dispose of managed resources");
                        }
                        // Dispose of unmanaged resources here.
                        Console.WriteLine(Name + ": Dispose of unmanaged resources");
                        // Remember that we have disposed of resources.
                        resourcesAreFreed = true;
                        // We don't need the destructor because
                        // our resources are already freed.
                        GC.SuppressFinalize(this);
                    }
                }
            }
        }
    }
}