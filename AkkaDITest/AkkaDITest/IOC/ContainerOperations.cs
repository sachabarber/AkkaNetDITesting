using System;
using Autofac;

namespace AkkaDITest.IOC
{
    public class ContainerOperations
    {
        private static readonly Lazy<ContainerOperations> _instance = new Lazy<ContainerOperations>(() => new ContainerOperations());
        private IContainer localContainer;
        private object _syncLock = new Object();

        private ContainerOperations()
        {
            CreateContainer();
        }

        public void ReInitialise()
        {
            lock (_syncLock)
            {
                localContainer = null;
            }
        }


        public static ContainerOperations Instance => _instance.Value;


        public IContainer Container
        {
            get
            {
                lock (_syncLock)
                {
                    if (localContainer == null)
                    {
                        CreateContainer();

                    }
                    return localContainer;
                }
            }
        }

        private void CreateContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new GlobalAutofacModule());


            AddExtraModulesCallBack?.Invoke(builder); // DLS

            localContainer = builder.Build();

        }

        public Action<ContainerBuilder> AddExtraModulesCallBack { get; set; } // DLS
    }
}
