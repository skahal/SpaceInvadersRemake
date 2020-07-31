using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using Vexe.Fast.Reflection;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class TestScript : MonoBehaviour
{
    public int loops = 100000;

    static FieldInfo instField = typeof(BenchmarkSubject).GetField("InstanceField");
    static FieldInfo staticField = typeof(BenchmarkSubject).GetField("StaticField");
    static PropertyInfo instProperty = typeof(BenchmarkSubject).GetProperty("InstanceProperty");
    static PropertyInfo staticProperty = typeof(BenchmarkSubject).GetProperty("StaticProperty");
    static MethodInfo instanceMethod = typeof(BenchmarkSubject).GetMethod("InstanceMethod");
    static MethodInfo staticMethod = typeof(BenchmarkSubject).GetMethod("StaticMethod");
    static object[] args = new object[] { 3.14f };

    BenchmarkSubject strong = new BenchmarkSubject();
    Stopwatch sw = new Stopwatch();
    StringBuilder builder = new StringBuilder();
    string benchmarkResult;

    public void OnGUI()
    {
        if (GUILayout.Button("Class demo"))
            ClassDemo();

        if (GUILayout.Button("Struct demo"))
            StructDemo();

        if (GUILayout.Button("Benchmark"))
            benchmarkResult = Benchmarks();

        if (!string.IsNullOrEmpty(benchmarkResult))
            GUILayout.TextArea(benchmarkResult);

        GUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("DbgAsm1"))
                GenDbgAsm1();

            if (GUILayout.Button("DbgAsm2"))
                GenDbgAsm2();

            if (GUILayout.Button("DbgAsm3"))
                GenDbgAsm3();
        }
        GUILayout.EndHorizontal();
    }

    public void ClassDemo()
    {
        // get the class metadata
        var type = typeof(TestClass);
        var property = type.GetProperty("TestProperty");
        var instanceMethod = type.GetMethod("Add");
        var staticMethod = type.GetMethod("Print", BindingFlags.Static | BindingFlags.Public);

        // create an instance of it
        var ctor = type.DelegateForCtor(Type.EmptyTypes);
        object test = ctor(null);

        // create a weakly-typed setter/getter for 'TestProerty'
        var setter = property.DelegateForSet();
        var getter = property.DelegateForGet();

        // set the property on our target 'test' to some value and print it
        setter(ref test, "Hello");
        Debug.Log(getter(test));

        // create a delegate to call the instance Sum method
        // call Sum on our 'test' instance with 1, 2. print the result
        var instMethodCaller = instanceMethod.DelegateForCall();
        var sum = instMethodCaller(test, new object[] { 1, 2 });
        Debug.Log(sum);

        // create a delegate to call the static Print method
        // notice we pass 'null' as the target (thers' no target)
        // because static members live on a type-basis, not instance-basis
        var staticMethodCaller = staticMethod.DelegateForCall();
        staticMethodCaller(null, new object[] { "IL is pretty metal!" });

        // NOTE: in real code, you do not want to allocate the arguments arrays everytime you want to call the method.
        // instead you allocate once, and reuse
    }

    public void StructDemo()
    {
        // get the struct metadata
        var type = typeof(TestStruct);
        var field = type.GetField("value", BindingFlags.NonPublic | BindingFlags.Instance);
        var byrefMethod = type.GetMethod("ByRef");

        // create an instance of it
        var ctor = type.DelegateForCtor<TestStruct>(typeof(int));
        TestStruct test = ctor(new object[] { 10 });

        // for demo purposes this time create strongly-typed delegates to set/get the 'value' field
        var setter = field.DelegateForSet<TestStruct, int>();
        var getter = field.DelegateForGet<TestStruct, int>();

        setter(ref test, 13);

        Debug.Log(getter(test)); // 13... or 10? ;)

        // when passing arguments to a method by reference,
        // they need to be layed out in a object[]
        var args = new object[] { 5, 2, "IN" };
        var byrefCall = byrefMethod.DelegateForCall();
        byrefCall(test, args);
        Debug.Log(args[0]);	// -1
        Debug.Log(args[1]);	// 2 (by value, doesn't change even if the method tries to modify it)
        Debug.Log(args[2]);	// "OUT"

        // Here, we have 3 arguments, the first and third are passed by reference
        // the second by value. the method changes the value of the first and third
        // argument and so we see -1 and "OUT" instead of 1 and "IN"
    }

    // here's some benchmarks between directly calling fields/properties gettesr/setters and invoking methods,
    // then doing the same thing with reflection, then with the weakly typed API version, then the strong one!
    public string Benchmarks()
    {
        Action<string, Action> measure = (msg, code) =>
        {
            sw.Start();
            for (int i = 0; i < loops; i++)
                code();
            sw.Stop();

            builder.Append(msg);
            builder.Append(": ");
            builder.Append(sw.ElapsedMilliseconds.ToString());
            builder.AppendLine("ms");

            sw.Reset();
        };

        builder.Length = 0;

        object weak = strong;
        var xform = transform;
        var vec = Vector3.zero;

        UnityEngine.Profiling.Profiler.BeginSample("Direct");
        measure("Direct set instance field", () => strong.InstanceField = 10);
        measure("Direct get instance field", () => { var x = strong.InstanceField; });
        measure("Direct set instance property", () => strong.InstanceProperty = "Str");
        measure("Direct get instance property", () => { var x = strong.InstanceProperty; });
        measure("Direct set static field", () => BenchmarkSubject.StaticField = xform);
        measure("Direct get static field", () => { var x = BenchmarkSubject.StaticField; });
        measure("Direct set static property", () => BenchmarkSubject.StaticProperty = vec);
        measure("Direct get static property", () => { var x = BenchmarkSubject.StaticProperty; });
        measure("Direct invoke instance method", () => strong.InstanceMethod());
        measure("Direct invoke static method", () => BenchmarkSubject.StaticMethod(3.14f));
        UnityEngine.Profiling.Profiler.EndSample();

        builder.AppendLine("-----");

        UnityEngine.Profiling.Profiler.BeginSample("Reflection");
        measure("Reflection instance set field", () => instField.SetValue(strong, 10));
        measure("Reflection instance get field", () => instField.GetValue(strong));
        measure("Reflection instance set property", () => instProperty.SetValue(strong, "Str", null));
        measure("Reflection instance get property", () => instProperty.GetValue(strong, null));
        measure("Reflection static set field", () => staticField.SetValue(null, xform));
        measure("Reflection static get field", () => staticField.GetValue(null));
        measure("Reflection static set property", () => staticProperty.SetValue(null, vec, null));
        measure("Reflection static get property", () => staticProperty.GetValue(null, null));
        measure("Reflection invoke instance method", () => instanceMethod.Invoke(strong, null));
        measure("Reflection invoke static method", () => staticMethod.Invoke(null, args));
        UnityEngine.Profiling.Profiler.EndSample();

        builder.AppendLine("-----");

        // weakly typed API - deals with 'object'
        // very useful if you don't know the target and member types in advance
        var weakInstanceFieldSetter = instField.DelegateForSet();
        var weakInstanceFieldGetter = instField.DelegateForGet();
        var weakInstancePropertySetter = instProperty.DelegateForSet();
        var weakInstancePropertyGetter = instProperty.DelegateForGet();
        var weakStaticFieldSetter = staticField.DelegateForSet();
        var weakStaticFieldGetter = staticField.DelegateForGet();
        var weakStaticPropertySetter = staticProperty.DelegateForSet();
        var weakStaticPropertyGetter = staticProperty.DelegateForGet();
        var weakInstanceMethodCaller = instanceMethod.DelegateForCall();
        var weakStaticMethodCaller = staticMethod.DelegateForCall();

        UnityEngine.Profiling.Profiler.BeginSample("Weak Cached");
        measure("weak cached delegate instance set field", () => weakInstanceFieldSetter(ref weak, 10));
        measure("weak cached delegate instance get field", () => weakInstanceFieldGetter(weak));
        measure("weak cached delegate instance set property", () => weakInstancePropertySetter(ref weak, "Str"));
        measure("weak cached delegate instance get property", () => weakInstancePropertyGetter(weak));
        measure("weak cached delegate static set field", () => weakStaticFieldSetter(ref weak, xform));
        measure("weak cached delegate static get field", () => weakStaticFieldGetter(null));
        measure("weak cached delegate static set property", () => weakStaticPropertySetter(ref weak, vec));
        measure("weak cached delegate static get property", () => weakStaticPropertyGetter(null));
        measure("weak cached delegate invoke instance method", () => weakInstanceMethodCaller(weak, null));
        measure("weak cached delegate invoke static method", () => weakStaticMethodCaller(null, args));
        UnityEngine.Profiling.Profiler.EndSample();

        builder.AppendLine("-----");

        // strongly typed versions. only useful if you know in adavnce the target and member types!
        // (we'll talk to why would you use strong types in a moment)
        var strongInstanceFieldSetter = instField.DelegateForSet<BenchmarkSubject, int>();
        var strongInstanceFieldGetter = instField.DelegateForGet<BenchmarkSubject, int>();
        var strongInstancePropertySetter = instProperty.DelegateForSet<BenchmarkSubject, string>();
        var strongInstancePropertyGetter = instProperty.DelegateForGet<BenchmarkSubject, string>();
        var strongStaticFieldSetter = staticField.DelegateForSet<BenchmarkSubject, Transform>(); // doesn't actually matter what target type we specify, cause there's no target
        var strongStaticFieldGetter = staticField.DelegateForGet<BenchmarkSubject, Transform>();
        var strongStaticPropertySetter = staticProperty.DelegateForSet<BenchmarkSubject, Vector3>();
        var strongStaticPropertyGetter = staticProperty.DelegateForGet<BenchmarkSubject, Vector3>();
        var strongInstanceMethodCaller = instanceMethod.DelegateForCall<BenchmarkSubject, object>(); // object here means just no parameters
        var strongStaticMethodCaller = staticMethod.DelegateForCall<object, object>(); // method is static, doesn't matter what type we choose for the target. so we just wrote 'object'

        UnityEngine.Profiling.Profiler.BeginSample("Strong Cached");
        measure("strong cached delegate instance set field", () => strongInstanceFieldSetter(ref strong, 10));
        measure("strong cached delegate instance get field", () => strongInstanceFieldGetter(strong));
        measure("strong cached delegate instance set property", () => strongInstancePropertySetter(ref strong, "Str"));
        measure("strong cached delegate instance get property", () => strongInstancePropertyGetter(strong));
        measure("strong cached delegate static set field", () => strongStaticFieldSetter(ref strong, xform));
        measure("strong cached delegate static get field", () => strongStaticFieldGetter(null));
        measure("strong cached delegate static set property", () => strongStaticPropertySetter(ref strong, vec));
        measure("strong cached delegate static get property", () => strongStaticPropertyGetter(null));
        measure("strong cached delegate invoke instance method", () => strongInstanceMethodCaller(strong, null));
        measure("strong cached delegate invoke static method", () => strongStaticMethodCaller(null, args));
        UnityEngine.Profiling.Profiler.EndSample();

        return builder.ToString();
    }

    // some profile marks to monitor performance and gc
    void Update()
    {
        object weak = strong;

        UnityEngine.Profiling.Profiler.BeginSample("ProfilerMarks.Init");
        // generates the getter code the first time it's called then caches the result
        // so that any subsequent call it will return the cached result
        var strongFieldGetter = instField.DelegateForGet<BenchmarkSubject, int>();
        var strongFieldSetter = instField.DelegateForSet<BenchmarkSubject, int>();
        var weakFieldGetter = instField.DelegateForGet();
        var weakFieldSetter = instField.DelegateForSet();
        var propertyGetter = instProperty.DelegateForGet();
        var propertySetter = instProperty.DelegateForSet();
        var instMethodCaller = instanceMethod.DelegateForCall();
        var staticMethodCaller = staticMethod.DelegateForCall();
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("ProfilerMarks.StrongFieldGet/Set");
        strongFieldSetter(ref strong, 10);
        var fieldValue0 = strongFieldGetter(strong);
        UnityEngine.Profiling.Profiler.EndSample();

        // unfortunately, due to the fact that our field is an 'int' (value-type)
        // using weak setter/getters will allocate memory to box the value
        // thus generate garbage. in my machine (with a 32-bit Unity editor), 
        // each set/get generates 12 bytes (might be a couple of bytes more in a 64-bit editor)
        // this won't happen if the field was a reference type (no need to box)
        // a way around that would be to use the strong typed version
        // but that requires you to know the type of target and field in advance...
        // in most cases though when writing editor scripts, 12 bytes shouldn't be much a problem
        UnityEngine.Profiling.Profiler.BeginSample("ProfilerMarks.WeakFieldGet/Set");
        weakFieldSetter(ref weak, 10);
        var fieldValue1 = weakFieldGetter(weak);
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("ProfilerMarks.WeakPropertyGet/Set");
        propertySetter(ref weak, "SomeStr");
        var propValue = propertyGetter(strong); // passed in strong. doesn't matter, both point to the same object
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("ProfilerMarks.MethodCalls");
        instMethodCaller(weak, null);
        staticMethodCaller(strong, args);
        UnityEngine.Profiling.Profiler.EndSample();
    }

    // these assemblies will be generated in the root project folder
    // they're just for debugging purposes
    // if you're ever curious as to what code is being generated for your delegates,
    // you can load up the assemblies ILSpy and view the code
    public void GenDbgAsm1()
    {
        var t = typeof(TestStruct);
        var f = t.GetField("value", BindingFlags.NonPublic | BindingFlags.Instance);
        var m = t.GetMethod("ByRef");
        FastReflection.GenDebugAssembly("dbg1.dll", f, null, m, null, null);
    }

    public void GenDbgAsm2()
    {
        var t = typeof(TestClass);
        var p = t.GetProperty("TestProperty");
        var m = t.GetMethod("Add");
        FastReflection.GenDebugAssembly<TestClass>("dbg2.dll", null, p, m, null, null);
    }

    public void GenDbgAsm3()
    {
        var t = typeof(BenchmarkSubject);
        var p = t.GetProperty("StaticProperty");
        var f = t.GetField("StaticField");
        FastReflection.GenDebugAssembly("dbg3.dll", f, p, null, null, null);
    }

    public struct TestStruct
    {
        private int value;

        public TestStruct(int value)
        {
            this.value = value;
        }

        public void ByRef(ref int x, int y, out string z)
        {
            x = -1;
            y = 10;
            z = "OUT";
        }
    }

    public class TestClass
    {
        public string TestProperty { get; set; }

        public int Add(int x, int y)
        {
            return x + y;
        }

        public static void Print(string msg)
        {
            if (!string.IsNullOrEmpty(msg))
                Debug.Log(msg);
        }
    }

    public class BenchmarkSubject
    {
        public int InstanceField;
        public static Transform StaticField;
        public string InstanceProperty { get; set; }
        public static Vector3 StaticProperty { get; set; }
        public void InstanceMethod() { }
        public static void StaticMethod(float x) { }
    }
}
