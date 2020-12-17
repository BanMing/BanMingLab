#include <iostream>
#include "singleton_test.h"

int main(int, char **)
{
    std::cout << "Hello, world!\n";
    std::cout << singleton_test::instance().test << std::endl;
    singleton_test::instance().test = 3;
    std::cout << singleton_test::instance().test << std::endl;
}
