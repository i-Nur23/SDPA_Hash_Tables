#include <iostream>
#include <string>
#include <locale>
#include <set>

struct HashHead
{
    string *begin;
    string *end;
};

struct HashCell
{
    HashCell* next;
    string key;
};

int main()
{
    std::cout << "Hello World!\n";
}
