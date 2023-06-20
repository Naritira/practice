// граф.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//

#include <iostream>
#define _USE_MATH_DEFINES
#include <math.h>
#include <fstream>
#include <iomanip>

double f1(double &x) {
    return (3.1* pow(exp(1), -x) + abs(cos(sqrt(M_PI*x)))- 0.625);
}
double f2(double& x) {
    return pow(x, 25);
}

double f3(double& x) {
    return pow(x, -50);
}

int main()
{
    std::ofstream a("a.txt");
    std::ofstream b("b.txt");
    std::ofstream c("c.txt");
    std::ofstream d("d.txt");
    double xo = -2.01;

    if (a.is_open()) {
        
        double previous_y = f1(xo);
        bool hasBreak = false;
        for (double x = -2.01; x < 1; x += 0.01) {
            double y = f1(x);
            a << std::fixed;
            if (std::isnan(y) || std::isinf(y)) {
                hasBreak = true;
            }
            else {
                if (hasBreak) {
                    a << "break" << std::endl;
                    hasBreak = false;
                }
                a << x << " " << y << std::endl;
            }

            previous_y = y;
        }

        

        a.close();
    }
     xo = 3.01;
    if (b.is_open()) {
        double previous_y = f2(xo);
        bool hasBreak = false;
        b << std::fixed;
        for (double x = 3.01; x <= 5; x += 0.01) {
            double y = f2(x);

            if (std::isnan(y) || std::isinf(y)) {
                hasBreak = true;
            }
            else {
                if (hasBreak) {
                    b << "break" << std::endl;
                    hasBreak = false;
                }

                if (y > 100000000000) {
                    y /= 1000000000000;
                }

                b << x << " " << y << std::endl;
            }

            previous_y = y;
        }

        b.close();
    }

    double xq;
    std::cin >> xo >> xq;
    if (c.is_open()) {
        double previous_y = f3(xo);
        bool hasBreak = false;
        for (double x = xo; x < xq; x += 0.01) {
            double y = f3(x);
            c << std::fixed;
            if (std::isnan(y) || std::isinf(y) || y > 100) {
                hasBreak = true;
            }
            else {
                if (hasBreak) {
                    c << "break" << std::endl;
                    hasBreak = false;
                }
                if (y <= 100) {
                    c << x << " " << y << std::endl;
                }
            }
            previous_y = y;
        }
        c.close();
    }
    if (d.is_open()) {
        double previous_y = f3(xo);
        bool hasBreak = false;
        for (double x = xo; x < 0; x += 0.01) {
            double y = f3(x);
            hasBreak = false;
            d << std::fixed;
            if (std::isnan(y) || std::isinf(y) || y > 100) {
                hasBreak = true;
            }
            else {
                if (hasBreak) {
                    d << "break" << std::endl;
                    hasBreak = false;
                }
                if (y <= 100) {
                    d << x << " " << y << std::endl;
                }
            }
            previous_y = y;
        }

        previous_y = f1(xo);
        hasBreak = false;
        for (double x = -2.01; x < 1; x += 0.01) {
            double y = f1(x);
            d << std::fixed;
            if (std::isnan(y) || std::isinf(y)) {
                hasBreak = true;
            }
            else {
                if (hasBreak) {
                    d << "break" << std::endl;
                    hasBreak = false;
                }
                d << x << " " << y << std::endl;
            }

            previous_y = y;
        }
        previous_y = f2(xo);
        hasBreak = false;
        d << std::fixed;
        for (double x = 3.01; x <= 5; x += 0.01) {
            double y = f2(x);

            if (std::isnan(y) || std::isinf(y)) {
                hasBreak = true;
            }
            else {
                if (hasBreak) {
                    d << "break" << std::endl;
                    hasBreak = false;
                }

                if (y > 100000000000) {
                    y /= 1000000000000;
                }

                d << x << " " << y << std::endl;
            }

            previous_y = y;
        }

    }
}
   

// Запуск программы: CTRL+F5 или меню "Отладка" > "Запуск без отладки"
// Отладка программы: F5 или меню "Отладка" > "Запустить отладку"

// Советы по началу работы 
//   1. В окне обозревателя решений можно добавлять файлы и управлять ими.
//   2. В окне Team Explorer можно подключиться к системе управления версиями.
//   3. В окне "Выходные данные" можно просматривать выходные данные сборки и другие сообщения.
//   4. В окне "Список ошибок" можно просматривать ошибки.
//   5. Последовательно выберите пункты меню "Проект" > "Добавить новый элемент", чтобы создать файлы кода, или "Проект" > "Добавить существующий элемент", чтобы добавить в проект существующие файлы кода.
//   6. Чтобы снова открыть этот проект позже, выберите пункты меню "Файл" > "Открыть" > "Проект" и выберите SLN-файл.
