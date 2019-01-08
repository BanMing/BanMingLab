unity version 2018.2.14
unity job system 
unity多线程-任务机制
实现了一个任务队列，然后每一个现在就直接去队列里去任务

https://docs.unity3d.com/Manual/JobSystemJobSystems.html
https://en.wikipedia.org/wiki/Job_(computing)

资源分配时会对多线程处理有很大的挑战。

NativeContainer
线程安全在内存中传递存储，可以在多线程中分享。

Note: The Entity Component System (ECS) package extends the Unity.Collections namespace to include other types of NativeContainer:

·NativeList - a resizable NativeArray.

·NativeHashMap - key and value pairs.

·NativeMultiHashMap - multiple values per key.

·NativeQueue - a first in, first out (FIFO) queue.


我们在多任务使用他时，我们可以设置jobs的依赖关系，在一个job需要用到nativecontainer时，先看他所依赖的jobs是否已经结束，结束了才能使用。我们不能再一个任务使用一个NativeContainer时，另一个任务不能使用。

我们可以设置数据结构的读写权限。
````
[ReadOnly]
public NativeArray<int> input;
````
对于这个的静态变量，是不被保护的，在使用时容易让unity崩溃。


设置jobhandle处理顺序，依赖0：

````
JobHandle firstJobHandle = firstJob.Schedule();
secondJob.Schedule(firstJobHandle);
````

多任务并发
大量同样的操作在一个物体上。