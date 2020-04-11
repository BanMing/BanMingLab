
int a = 0; // 全局初始化区
char *p1;  // 全局未初始化区

int main(int argc, char const *argv[])
{
    int b;             //栈
    char s[] = "abc";  //栈
    char *p2;          //栈
    char *p3 = "1234"; //1234/0在常量区，p3在栈上
    static int c = 0;  //全局（静态）初始化区

    // 分配得来10和20字节的区域就在堆区
    // p1 = (char *)malloc(10);
    // p2 = (char *)malloc(20);

    // strcpy(p1,"1234");// 123456/0放在常量区，编译器可能会将它与p3所指向的"123456"   优化成一个地方

    return 0;
}
