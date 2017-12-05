#Attribue

**前言**

**是什么**
 
从字面意思上就可以理解为属性，作为一个标签来标识一个参数、属性或者方法或者是类，这个标签在代码运行时执行时或者在代码运行前对被标记的物体做一定的处理。同一个物体可以被标记多个，这个标记也会被继承。在C#与unity中给我们提供了很多这样的标签[DllImport]、[SerializeField]、[HideInInspector]、[DllImport]、[MenuItem]等等，一般是通过反射来调用。



**怎么用**

我们来写写定义自己的Attribute，首先是下面这几个法则：

●定义的类必须是public
●类名最好事以Attribute结尾，在你使用这个标签的时候可以不加后面的Attribute这个字。
●必须继承于System.Attribute

        public class MyAttribute : Attribute
        {
        }

        [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
        public class YourAttribute : Attribute
        {
        }

**AttributeUsageAttribute**

这是我们在编写自己的Attribute的时候可以对我们的Attribute做的一个属性设置。

        [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]

可以对自己编写的Attribute的标签做设置。第一个设置该标签可以用于什么位置；第二个设置可以被继承不，默认true；第三个允许同时多个标签，默认true。

**原理**



