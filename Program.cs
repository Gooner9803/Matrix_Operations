using System;
using static System.Console;
using System.Collections.Generic;

namespace ConsoleApp6
{
    struct Matrix
    {
        public string Name;
        public double[,] Values;
        public Matrix(string name, double[,] values)
        {
            Name = name;
            Values = values;
        }
    }
    
    class Program
    {
        
        static void Main(string[] args)
        {
            bool stopworking = false;
            while (!stopworking)
            {
                WriteLine("For addition press 1.");
                WriteLine("For subtraction press 2.");
                WriteLine("For scalar multiplication press 3.");
                WriteLine("For inversing matrice press 4.");
                WriteLine("For transposing matrice press 5.");
                WriteLine("For determining whether a matrix is orthogonal or not press 6.");
                WriteLine("For finding the smallest element in matrix press 7.");
                WriteLine("For finding the largest element in matrix press 8.");
                string choice = ReadLine();
                switch (choice)
                {
                    case "1":
                        stopworking = true;
                        WriteLine("Create first Matrix..");
                        Matrix matrixa1 = CreateMatrix();
                        WriteLine("Create second Matrix..");
                        Matrix matrixa2 = CreateMatrix();
                        if ((matrixa1.Values.GetLength(0) == matrixa2.Values.GetLength(0)) && (matrixa1.Values.GetLength(1) == matrixa2.Values.GetLength(1)))
                        {
                            Matrix Resulta = Addition(matrixa1, matrixa2);
                            ShowResultMatrix(Resulta);
                            ReadLine();
                        }
                        else
                        {
                            WriteLine("Warning! Two matrices may be added only if they have the same number of rows and columns.");
                            ReadLine();
                        }
                        break;
                    case "2":
                        stopworking = true;
                        WriteLine("Create first Matrix..");
                        Matrix matrixs1 = CreateMatrix();
                        WriteLine("Create second Matrix..");
                        Matrix matrixs2 = CreateMatrix();
                        if ((matrixs1.Values.GetLength(0) == matrixs2.Values.GetLength(0)) && (matrixs1.Values.GetLength(1) == matrixs2.Values.GetLength(1)))
                        {
                            Matrix Results = Subtraction(matrixs1, matrixs2);
                            ShowResultMatrix(Results);
                            ReadLine();
                        }
                        else
                        {
                            WriteLine("Warning! Two matrices may be subtracted only if they have the same number of rows and columns.");
                            ReadLine();
                        }
                        break;
                    case "3":
                        stopworking = true;
                        Write("Write a scalar: ");
                        bool IsRightScalar = double.TryParse(ReadLine(), out double scalar);
                        while (!IsRightScalar)
                        {
                            WriteLine("Wrong format! Try again!");
                            Write("Write a scalar: ");
                            IsRightScalar = double.TryParse(ReadLine(), out scalar);
                        }
                        WriteLine("Create  Matrix..");
                        Matrix matrixsm = CreateMatrix();
                        Matrix Resultsm = ScalarMultiplication(matrixsm, scalar);
                        ShowResultMatrix(Resultsm);
                        ReadLine();
                        break;
                    case "4":
                        stopworking = true;
                        WriteLine("Create  Matrix..");
                        Matrix matrixi = CreateMatrix();
                        Matrix Resulti = InverseMatrix(matrixi);
                        ShowResultMatrix(Resulti);
                        ReadLine();
                        break;
                    case "5":
                        stopworking = true;
                        WriteLine("Create  Matrix..");
                        Matrix matrixt = CreateMatrix();
                        Matrix Resultt = TransposeMatrix(matrixt);
                        ShowResultMatrix(Resultt);
                        ReadLine();
                        break;
                    case "6":
                        stopworking = true;
                        WriteLine("Create Matrix..");
                        Matrix matrixo = CreateMatrix();
                        if(IsMatrixOrthogonal(matrixo))
                        {
                            WriteLine("Matrix is orthogonal.");
                        }
                        else
                        {
                            WriteLine("Matrix isn't orthogonal.");

                        }
                        ReadLine();
                        break;
                    case "7":
                        stopworking = true;
                        WriteLine("Create Matrix..");
                        Matrix matrixmx = CreateMatrix();
                        double maximal = MaximalValue(matrixmx);
                        WriteLine($"The largest element is {maximal}.");
                        ReadLine();
                        break;
                    case "8":
                        stopworking = true;
                        WriteLine("Create Matrix..");
                        Matrix matrixmn = CreateMatrix();
                        double minimal = MinimalValue(matrixmn);
                        WriteLine($"The largest element is {minimal}.");
                        ReadLine();
                        break;
                    default:
                        WriteLine("Unknown command! Try again!");
                        break;
                }
            }
        }
        
        static Matrix CreateMatrix()
        {
            Write("Write the name of the new matrix: ");
            string Name = ReadLine();
            Write("The number of  matrix's rows: ");
            bool IsRightRow_1 = uint.TryParse(ReadLine(), out uint Rows);
            while (!IsRightRow_1 || Rows == 0)
            {
                WriteLine("Wrong format! Try again!");
                Write("The number of  matrix's rows: ");
                IsRightRow_1 = uint.TryParse(ReadLine(), out Rows);
            }
            Write("The number of  matrix's columns: ");
            bool IsRightRow_2 = uint.TryParse(ReadLine(), out uint Columns);
            while (!IsRightRow_1 || Columns == 0)
            {
                WriteLine("Wrong format! Try again!");
                Write("The number of  matrix's columns: ");
                IsRightRow_2 = uint.TryParse(ReadLine(), out Columns);
            }
            double[,] Values = new double[Rows, Columns];
            WriteLine("Setting values....");
            WriteLine("Set  your value, if you want to generate random value, write 'random'.");
            Random random = new Random();
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    bool RightValue = false;
                    while (!RightValue)
                    {
                        Write($"[{i + 1},{j + 1}] = ");
                        string str = ReadLine();
                        RightValue = double.TryParse(str, out double value);
                        if (RightValue)
                        {
                            Values[i, j] = value;
                        }
                        else if (str == "random")
                        {
                            RightValue = true;
                            Values[i, j] = random.Next(0, 100);
                            WriteLine($"Value: {Values[i, j]}");
                        }
                        else
                        {
                            WriteLine("Wrong format! Try again!");
                        }
                    }
                }
            }
            Matrix newMatrix = new Matrix(Name, Values);
            WriteLine($"Matrix {Name} is created!");
            return newMatrix;
        }
        
        static Matrix Addition(Matrix a, Matrix b)
        {
            if ((a.Values.GetLength(0) == b.Values.GetLength(0)) && (a.Values.GetLength(1) == b.Values.GetLength(1)))
            {
                double[,] addedValues = new double[a.Values.GetLength(0), a.Values.GetLength(1)];
                for (int i = 0; i < a.Values.GetLength(0); i++)
                {
                    for (int j = 0; j < a.Values.GetLength(1); j++)
                    {
                        addedValues[i, j] = a.Values[i, j] + b.Values[i, j];
                    }
                }
                Matrix addedMatrix = new Matrix($"{a.Name}+{b.Name}", addedValues);
                return addedMatrix;
            }
            else
            {
                throw new Exception("Two matrices may be added only if they have the same number of rows and columns.");
            }

        }
        
        static Matrix Subtraction(Matrix a, Matrix b)
        {
            if ((a.Values.GetLength(0) == b.Values.GetLength(0)) && (a.Values.GetLength(1) == b.Values.GetLength(1)))
            {
                double[,] subtractedValues = new double[a.Values.GetLength(0), a.Values.GetLength(1)];
                for (int i = 0; i < a.Values.GetLength(0); i++)
                {
                    for (int j = 0; j < a.Values.GetLength(1); j++)
                    {
                        subtractedValues[i, j] = a.Values[i, j] - b.Values[i, j];
                    }
                }
                Matrix subtractedMatrix = new Matrix($"{a.Name}-{b.Name}", subtractedValues);
                return subtractedMatrix;
            }
            else
            {
                throw new Exception("Two matrices may be added only if they have the same number of rows and columns.");
            }

        }

        static Matrix ScalarMultiplication(Matrix a, double scalar)
        {
            double[,] scalarMultiplicatedValues = new double[a.Values.GetLength(0), a.Values.GetLength(1)];
            for (int i = 0; i < a.Values.GetLength(0); i++)
            {
                for (int j = 0; j < a.Values.GetLength(1); j++)
                {
                    scalarMultiplicatedValues[i, j] = scalar * a.Values[i, j];
                }
            }
            Matrix scalarMultiplicatedMatrix = new Matrix($"{scalar}*{a.Name}", scalarMultiplicatedValues);
            return scalarMultiplicatedMatrix;
        }
        
        static Matrix InverseMatrix(Matrix a)
        {
            double[,] inversedValues = new double[a.Values.GetLength(0), a.Values.GetLength(1)];
            for (int i = 0; i < a.Values.GetLength(0); i++)
            {
                for (int j = 0; j < a.Values.GetLength(1); j++)
                {
                    inversedValues[i, j] = -1 * a.Values[i, j];
                }
            }
            Matrix inversedMatrix = new Matrix($"-{a.Name}", inversedValues);
            return inversedMatrix;
        }
        
        static Matrix TransposeMatrix(Matrix a)
        {
            double[,] transposedValues = new double[a.Values.GetLength(1), a.Values.GetLength(0)];
            for (int i = 0; i < a.Values.GetLength(1); i++)
            {
                for (int j = 0; j < a.Values.GetLength(0); j++)
                {
                    transposedValues[i, j] = a.Values[j, i];
                }
            }
            Matrix transposedMatrix = new Matrix($"({a.Name})T", transposedValues);
            return transposedMatrix;
        }
        
        static bool IsMatrixOrthogonal(Matrix a)
        {
            if (a.Values.GetLength(0) == a.Values.GetLength(1))
            {
                Matrix b = TransposeMatrix(a);
                double[,] orthValues = new double[a.Values.GetLength(0), a.Values.GetLength(1)];
                for (int i = 0; i < a.Values.GetLength(0); i++)
                {
                    for (int j = 0; j < a.Values.GetLength(1); j++)
                    {
                        orthValues[i, j] = a.Values[i, j] * b.Values[i, j];
                    }
                }
                Matrix c = new Matrix($"I({a.Name})", orthValues);
                bool isIdentity = IsMatrixIdentity(c);
                
                return isIdentity;
            }
            else
            {
                return false;
            } 
        }
        
        static bool IsMatrixIdentity(Matrix a)
        {
            for(int i=0;i<a.Values.GetLength(0);i++)
            {
                for (int j = 0; j < a.Values.GetLength(1); j++)
                {
                    if(i==j)
                    {
                        if (a.Values[i, j] != 1)
                        {
                            return false;
                        }
                    }
                    if (i != j)
                    {
                        if(a.Values[i, j] != 0)
                        return false;
                    }

                }
            }
            return true;
        }
        
        static void ShowResultMatrix(Matrix a)
        {
            WriteLine("The result is...");
            WriteLine($"The name of matrix   : {a.Name}");
            WriteLine($"The number of rows   : {a.Values.GetLength(0)}");
            WriteLine($"The number of columns: {a.Values.GetLength(1)}");
            for(int i=0;i<a.Values.GetLength(0);i++)
            {
                for(int j=0;j<a.Values.GetLength(1);j++)
                {
                    WriteLine($"The value of [{i+1},{j+1}]: {a.Values[i,j]} ");
                }
            }


        }
        
        static double MaximalValue(Matrix a)
        {
            double Maximal = double.MinValue;
            for(int i=0;i<a.Values.GetLength(0);i++)
            {
                for (int j = 0; j < a.Values.GetLength(1); j++)
                {
                    if(a.Values[i,j]>Maximal)
                    {
                        Maximal = a.Values[i, j];
                    }
                }
            }
            return Maximal;
        }
        
        static double MinimalValue(Matrix a)
        {
            double Minimal = double.MaxValue;
            for (int i = 0; i < a.Values.GetLength(0); i++)
            {
                for (int j = 0; j < a.Values.GetLength(1); j++)
                {
                    if (a.Values[i, j] < Minimal)
                    {
                        Minimal = a.Values[i, j];
                    }
                }
            }
            return Minimal;
        }
    }
}
