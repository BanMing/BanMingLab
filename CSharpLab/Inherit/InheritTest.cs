
namespace Inherit
{
    public class InheritTest
    {
        private class Base
        {

            public virtual void Menthod() { }
        }

        private class InheritClass1 : Base
        {

            public override void Menthod()
            {
                System.Console.WriteLine("@@@@@@@");
            }
        }

        private class InheritClass2 : InheritClass1
        {
            public override void Menthod()
            {

                base.Menthod();
                System.Console.WriteLine("###############");
            }
        }

        public static void Run()
        {
            InheritClass2 obj = new InheritClass2();
            obj.Menthod();
        }
    }

}