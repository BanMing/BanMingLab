using System;
using System.Reflection;

public class DoTest {

    //msdn example
    public void test () {
        AnimalTypeTestClass testClass = new AnimalTypeTestClass ();
        Type type = testClass.GetType ();
        foreach (MethodInfo mInfo in type.GetMethods ()) {
            foreach (Attribute attr in Attribute.GetCustomAttributes (mInfo)) {
                if (attr.GetType () == typeof (AnimalTypeAttribute))
                    Console.WriteLine ("Method {0} has a pet {1} attribute.", mInfo.Name, ((AnimalTypeAttribute) attr).Pet);
            }

        }
    }
    [Developer("sss","222")]
    public void PrintTest () {
        AnimalTypeTestClass testClass = new AnimalTypeTestClass ();
        testClass.DogMethod ();
        testClass.CatMethod ();
        testClass.BirdMethod ();
    }
}