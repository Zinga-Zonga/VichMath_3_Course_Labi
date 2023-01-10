
void printX(double[] X, int n)
{
    for (int i = 0; i < n; i++)
    {
        Console.WriteLine($"x{i + 1} = {X[i].ToString("G17")}");
    }
}
void printMatrix(double[,] matrix)
{
    for (int i = 0; i < matrix.GetLength(0); i++)
    {
        Console.Write("| ");
        for (int j = 0; j < matrix.GetLength(1); j++)
        {
            if (i == j) Console.BackgroundColor = ConsoleColor.Blue;
            Console.Write(String.Format("{0,9}", matrix[i, j].ToString("G3")));

            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(" | ");
        }
        Console.WriteLine();
    }

    Console.BackgroundColor = ConsoleColor.Black;
}

void TestCorectly(double[] matrixF, double[,] HilbertMatrix, double[] xArray)
{
    
}

double[,] fillGilbert(double[,] matrix, int n)
{
    for (int i = 0; i < n; i++)
    {
        for (int j = 0; j < n; j++)
        {
            matrix[i, j] = 1.0 / ((i + 1) + (j + 1) - 1);
        }

    }
    return matrix;
}

double[] fillGilbertFree(double[] F, int n)
{
    for (int i = 0; i < n; i++)
    {
        F[i] = (double)n / ((i + 1) * (i + 1));
    }
    return F;
}

double[] Gauss(double[,] matrix, double[] y, int n)
{
    double[] x = new double[n];
    int k = 0;
    const double eps = 0;
    while (k < n)
    {
        for (int i = k; i < n; i++)
        {
            double temp = matrix[i, k];
            if (Math.Abs(temp) < eps)
            {
                continue;
            }
            for (int j = 0; j < n; j++)
            {
                matrix[i, j] = matrix[i, j] / temp;
            }
            y[i] = y[i] / temp;
            if (i == k)
            {
                continue;
            }
            for (int j = 0; j < n; j++)
            {
                matrix[i, j] = matrix[i, j] - matrix[k, j];
            }
            y[i] = y[i] - y[k];
        }
        k++;
    }
    for (k = n - 1; k >= 0; k--)
    {
        x[k] = y[k];
        for (int i = 0; i < k; i++)
        {
            y[i] = y[i] - matrix[i, k] * x[k];
        }
    }
    return x;
}



double[] GaussVibor(double[,] a, double[] y, int n)
{
    
    double max;
    int k, index;
    const double eps = 0.0000;  //точность
    double[] x = new double[n];
    k = 0;

    while (k < n)
    {
        double temp;
        //поиск главного элемента
        max = Math.Abs(a[k, k]);
        index = k;
        for (int i = k + 1; i < n; i++)
        {
            if (Math.Abs(a[i, k]) > max)
            {
                max = Math.Abs(a[i, k]);
                index = i;
            }
        }
        //перестановка строк
        if (max < eps)
        {
            //нет ненулевых диагональных элементов
            Console.WriteLine($"Решение получить невозможно из-за нулевого столбца \n{index} матрицы А");
            return null;
        }
        for (int j = 0; j < n; j++)
        {
            temp = a[k, j];
            a[k, j] = a[index, j];
            a[index, j] = temp;
        }
        temp = y[k];
        y[k] = y[index];
        y[index] = temp;
        //нормализация уравнений
        for (int i = k; i < n; i++)
        {
            double d = a[i, k];
            
            if (Math.Abs(temp) < eps) continue; //для нулевого коэффициента пропустить
            for (int j = 0; j < n; j++)
            {
                a[i, j] = a[i, j] / d;
                
            }
               
            y[i] = y[i] / d;
            if (i == k) continue; //уравнение не вычитать само из себя
            for (int j = 0; j < n; j++)
            {
                a[i, j] = a[i, j] - a[k, j];
                
            }
    

                y[i] = y[i] - y[k];
        }
        Console.WriteLine("----------------------------------------");
        printMatrix(a);
        Console.WriteLine("----------------------------------------");
        k++;
    }
    //обратная подстановка
    for (k = n - 1; k >= 0; k--)
    {
        x[k] = y[k];
        for (int i = 0; i < k; i++)
            y[i] = y[i] - a[i, k] * x[k];
    }
    return x;
}


int n = 11;
Console.WriteLine($"Решение матрицы Гильберта размерности {n}х{n}");



double[,] matrix = new double[n, n];
double[] F = new double[n];
double[] x = new double[n];

matrix = fillGilbert(matrix, n);
F = fillGilbertFree(F, n);
x = Gauss(matrix, F, n);
Console.WriteLine("Без выбора главного элемента матрицы:");
printX(x, n);


matrix = fillGilbert(matrix, n);
F = fillGilbertFree(F, n);
x = GaussVibor(matrix, F, n);
Console.WriteLine("С выбором главного элемента матрицы:");
printX(x, n);

