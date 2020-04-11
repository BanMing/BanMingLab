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
        Console.WriteLine ("thePet:" + thePet);
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