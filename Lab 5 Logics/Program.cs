using Lab_5_Logics;

Console.OutputEncoding = System.Text.Encoding.UTF8;

/*Розкладка*/
Console.WriteLine(Layout.Switch("3 nfrs ckjdf (wt p Dtkbrj])/k Nf q cksd e;t yt NHB!"));

/*Числа*/
Numbers numbers = new();
Printer.Print(numbers.Create("-27, 14, -5, 61, -36, 42, -19, 7, -10, 31, -15, 20, -8, 44, -29, 12, -17, 25, -13, 6, -23, 18, -40, 37, -4, 9, -33, 16, -21, 28, -11, 35, -2, 22, -38, 39, -6, 3, -31, 26, -16, 41, -9, 5, -25, 30, -14, 11, -20, 38, -7, 24, -35, 32, -1, 4, -28, 23, -18, 45, -32, 10, -22, 19, -39, 34, -3, 15, -26, 21, -12, 40, -37, 2, -24, 27, -30, 1, -34, 29, 0, -43, 47, -46, 48, -50, 49, -45, 51, -42, 53, -41, 54, -40, 52, -44, 50, -47, 55, -49, 56, -48"));
numbers.Find(20, out string info);
Console.WriteLine(info);
numbers.Max(out int max);
Console.WriteLine(max);

/*Рядок*/
Line line = new();
Printer.Print(line.Create("ирима-римована"));
Console.WriteLine(line.Delete(out var letters));
Printer.Print(letters);

/*Документ*/
Document document = new();
Printer.Print(document.Read(@"D:\Документи\Навчання\ІФНТУНГ\2 Курс\1 Семестр\Алгоритми та структури даних\Лабраторні\Реалізації\Visual С++ & C#\Labs\Lab 5 Logics\Test.txt"));
Console.WriteLine(document.Delete('a', out Tree<string, char> words));
Printer.Print(words);

/*Тестове деревце*/
Tree<int, char> test = Tree<int, char>.Instant((1, 'q'), (2, 'w'), (3, 'e'), (4, 'r'), (5, 't'), (6, 'y'));
Printer.Print(test);
Tree<int, char>.Delete(test);
Printer.Print(test);
Tree<int, char>.Delete(test.Left);
Printer.Print(test);

Console.Read();