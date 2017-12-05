using System;
using System.Reflection;

public enum Animal {
    Dog = 1,
    Cat,
    Bird
}

public class AnimalTypeAttribute : Attribute {
    public AnimalTypeAttribute (Animal animal) {
        thePet = animal;
        Console.WriteLine("thePet:"+thePet);
    }
    protected Animal thePet;
    public Animal Pet { get { return thePet; } set { thePet = value; } }

}

public class AnimalTypeTestClass {
    [AnimalType (Animal.Dog)]
    public void DogMethod () { }

    [AnimalType (Animal.Cat)]
    public void CatMethod () { }

    [AnimalType (Animal.Bird)]
    public void BirdMethod () { }
}
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
    public void PrintTest(){
        AnimalTypeTestClass testClass=new AnimalTypeTestClass();
        testClass.DogMethod();
        testClass.CatMethod();
        testClass.BirdMethod();
    }
}