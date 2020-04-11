using System.Collections.Generic;
using System;
using System.Collections;
public class BoxTest
{
    struct Person
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public static void Run()
    {
        List<Person> persons = new List<Person>();
        persons.Add(new Person() { Name = "Jsack" });
        
        // 这里是直接复制一个Person出来
        Person person2 = persons[0];
        // 这里不会改变数列里面的Jsack
        person2.Name = "BanMing";

        Console.WriteLine($"Name:{person2.Name}");
        Console.WriteLine($"Name:{persons[0].Name}");
        // persons[0].Age = 1;

        // 输出
        // Name:BanMing
        // Name:Jsack
    }
}
