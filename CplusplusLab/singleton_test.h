#include <iostream>

class singleton_test
{
private:
    singleton_test() {}

public:
    static singleton_test &instance()
    {
        static singleton_test *instance = new singleton_test();
        return *instance;
    }

    int test = 2;
};

