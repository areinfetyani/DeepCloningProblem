using System;
using Newtonsoft.Json;

public class DeepCloning
{
    public static void Main(string[] args)
    {
        Person a = new Person();
        a.Name = "Person A";

        Person b = new Person();
        b.Name = "Person B";

        a.Friend = b;
        b.Friend = a;

        Person clonedA = DeepClone(a);

        Console.WriteLine($"Original A: {a.Name} -> Friend: {a.Friend.Name}");
        Console.WriteLine($"Original B: {b.Name} -> Friend: {b.Friend.Name}");
        
        clonedA.Name = "Cloned Person A ";

        Console.WriteLine($"Clone A:    {clonedA.Name} -> Friend: {clonedA.Friend.Name}");
        Console.WriteLine($"Clone B:    {clonedA.Friend.Name} -> Friend: {clonedA.Friend.Friend.Name}");
    }

    public static T DeepClone<T>(T obj)
    {
        if (obj == null)
            return default;

        var settings = new JsonSerializerSettings
        {
            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
            TypeNameHandling = TypeNameHandling.Objects
        };

        string json = JsonConvert.SerializeObject(obj, settings);
        return JsonConvert.DeserializeObject<T>(json, settings);
    }

    public class Person
    {
        public string Name { get; set; }
        public Person Friend { get; set; }
    }
}
