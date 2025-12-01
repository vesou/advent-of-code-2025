// See https://aka.ms/new-console-template for more information

int currentPosition = 0;
int test = 0 - 200;
test = Math.Abs(test);
double test2 = Math.Floor(test / 100d);
double test3 = test2 + currentPosition == 0 ? 0d : 1d;
double numberOfTimesCrossed = Math.Floor(test / 100.0) + (currentPosition == 0 ? 0 : 1);
Console.WriteLine(numberOfTimesCrossed);
Console.WriteLine(test2);
Console.WriteLine(test3);


