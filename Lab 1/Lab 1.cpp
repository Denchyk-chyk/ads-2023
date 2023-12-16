#include <locale>
#include <string>
#include <iostream>
#include <windows.h>

static int array[100]; //Створення порожнього статичного масиву
static int filledCount; //Кількість заповнених елементів

//Перевірка вводу на те, чи це ціле число
bool checkInput(int *value, std::string inputInfo, std::string outputInfo = "Некоректні дані")
{
    std::cout << inputInfo + ": ";
    std::string input;
    std::cin >> input;

    try {
        *value = std::stoi(input);
    }
    catch (...)
    {
        std::cout << outputInfo << std::endl;
        return false;
    }

    return true;
}

//Перевіряє виключення
bool checkException(bool condiiton, std::string info)
{
    if (condiiton)
        std::cout << info << std::endl;

    return condiiton;
}

//Випадкове заповнення масиву
void create()
{    
    //Ввід кількості та діапазону чисел та перевірка виключень
    int min, max;
    if (!checkInput(&filledCount, "Кількість чисел") ||
        checkException(filledCount > 100, "Завелика кількість чисел") ||
        !checkInput(&min, "Найменше можливе число") ||
        !checkInput(&max, "Найбільше можливе число") ||
        checkException(min > max, "Некоректний діапазон значень"))
        return;

    //Ініціалізація генератора випадкових чисел
    std::srand(static_cast<unsigned>(std::time(nullptr)));

    //Заповнення масиву та вивід
    for (size_t i = 0; i < filledCount; i++)
        array[i] = min + (std::rand() % (max - min + 1));
}

//Виведення масиву
void show()
{
    if (checkException(filledCount == 0, "Масив не заповнено"))
        return;

    for (size_t i = 0; i < filledCount - 1; i++)
        std::cout << array[i] << " | ";

    std::cout << array[filledCount - 1] << std::endl;
}

//Знаходження парних елементів після найбільшого
void findEven()
{
    //Знаходження максимального елемента
    int maxValue = 0, maxIndex = 0;
    for (size_t i = 0; i < filledCount; i++)
    {
        if (array[i] > maxValue)
        {
            maxValue = array[i];
            maxIndex = i;
        }
    }

    //Знаходження кількості парних елементів після нього
    int evenCount = 0;

    for (size_t i = maxIndex + 1; i < filledCount; i++)
    {
        if (array[i] % 2 == 0)
            evenCount++;
    }

    //Виключення
    if (checkException(evenCount == 0, "Парних елементів після максимального не виявлено"))
        return;

    //Їхній запис у новий масив і вивід на екран
    int* newArray = new int[evenCount];
    for (size_t i = 0, j = maxIndex; j < filledCount; j++)
    {
        if (array[j] % 2 == 0)
        {
            newArray[i] = array[j];
            std::cout << newArray[i] << " ";
            i++;
        }
    }
}

//Видалення потрібних елементів
void remove()
{
    //Ввід даних та перевірка виключень
    int start, count;
    if (!checkInput(&start, "Початковий індекс") ||
        checkException(start >= filledCount, "Початковий індекс виходить за межі масиву") ||
        !checkInput(&count, "Кількість чисел") ||
        checkException(count >= filledCount - start, "Недостатньо відповідних елементів в масиві"))
        return;

    //Створення нового масиву
    int length = filledCount - count;
    int* newArray = new int[length];

    //Заміщення порожніх місць
    for (size_t i = 0; i < length; i++)
        newArray[i] = array[(i < start) ? i : i + count];

    //Вивід
    std::cout << "Початковий масив:" << std::endl;
    for (int i = 0; i < filledCount; i++)
        std::cout << array[i] << " ";

    std::cout << std::endl << "Змінений масив:" << std::endl;
    for (int i = 0; i < length; i++)
        std::cout << newArray[i] << " ";
}

int main()
{
    //Налаштування консолі для виводу української мови
    SetConsoleOutputCP(1251);
    SetConsoleCP(1251);
    std::locale::global(std::locale(""));

    //Нескінченний масив, у якому користувач щоразу обирає дію
    char action = 0;
    while (true)
    {
        std::cout << "Виберіть операцію \n";
        std::cout << "1. Створити початковий масив \n";
        std::cout << "2. Вивести початковий масив \n";
        std::cout << "3. Створити масив із парних елементів після максимального \n";
        std::cout << "4. Вилучити елементи після вказаного \n";
        std::cout << "5. Закрити програму \n";

        //Вибір дії
        int action;
        if (!checkInput(&action, "Дія"))
            continue;

        if (action == 1)
            create();
        else if (action == 2)
            show();
        else if (action == 3)
            findEven();
        else if (action == 4)
            remove();
        else if (action == 5)
            exit(0);
        else
            std::cout << "Некоректні дані" << std::endl;

        std::cout << std::endl;
    }
}