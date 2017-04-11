using System;
using System.Reflection;

namespace Cben.Aspects
{
    //THIS NAMESPACE IS WORK-IN-PROGRESS

    internal abstract class AspectAttribute : Attribute
    {
        public Type InterceptorType { get; set; }

        protected AspectAttribute(Type interceptorType)
        {
            InterceptorType = interceptorType;
        }
    }

    internal interface ICbenInterceptionContext
    {
        object Target { get; }

        MethodInfo Method { get; }

        object[] Arguments { get; }

        object ReturnValue { get; }

        bool Handled { get; set; }
    }

    internal interface ICbenBeforeExecutionInterceptionContext : ICbenInterceptionContext
    {

    }


    internal interface ICbenAfterExecutionInterceptionContext : ICbenInterceptionContext
    {
        Exception Exception { get; }
    }

    internal interface ICbenInterceptor<TAspect>
    {
        TAspect Aspect { get; set; }

        void BeforeExecution(ICbenBeforeExecutionInterceptionContext context);

        void AfterExecution(ICbenAfterExecutionInterceptionContext context);
    }

    internal abstract class CbenInterceptorBase<TAspect> : ICbenInterceptor<TAspect>
    {
        public TAspect Aspect { get; set; }

        public virtual void BeforeExecution(ICbenBeforeExecutionInterceptionContext context)
        {
        }

        public virtual void AfterExecution(ICbenAfterExecutionInterceptionContext context)
        {
        }
    }

    internal class Test_Aspects
    {
        internal class MyAspectAttribute : AspectAttribute
        {
            public int TestValue { get; set; }

            public MyAspectAttribute()
                : base(typeof(MyInterceptor))
            {
            }
        }

        internal class MyInterceptor : CbenInterceptorBase<MyAspectAttribute>
        {
            public override void BeforeExecution(ICbenBeforeExecutionInterceptionContext context)
            {
                Aspect.TestValue++;
            }

            public override void AfterExecution(ICbenAfterExecutionInterceptionContext context)
            {
                Aspect.TestValue++;
            }
        }

        public class MyService
        {
            [MyAspect(TestValue = 41)] //Usage!
            public void DoIt()
            {

            }
        }

        public class MyClient
        {
            private readonly MyService _service;

            public MyClient(MyService service)
            {
                _service = service;
            }

            public void Test()
            {
                _service.DoIt();
            }
        }
    }
}
