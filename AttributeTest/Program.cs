using System;
using System.Reflection;

namespace AttributeTest {
    class Program {
        static void Main (string[] args) {
            AnimalTypeTestClass testClass = new AnimalTypeTestClass ();
            Type type = testClass.GetType ();
            // Iterate through all the methods of the class.
            foreach (MethodInfo mInfo in type.GetMethods ()) {
                // Iterate through all the Attributes for each method.
                foreach (Attribute attr in
                    Attribute.GetCustomAttributes (mInfo)) {
                    // Check for the AnimalType attribute.
                    if (attr.GetType () == typeof (AnimalTypeAttribute))
                        Console.WriteLine (
                            "Method {0} has a pet {1} attribute.",
                            mInfo.Name, ((AnimalTypeAttribute) attr).Pet);
                }

            }
        }
    }
}