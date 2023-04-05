using System;

namespace MatrixCalculator
{   
    /// <summary>
    /// Главный класс программы.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Точка входа программы.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // Переменная, сообщающая, хочет ли пользователь покинуть программу.
            bool exit;
            // Цикл повторяется пока пользователь не захочет покинуть программу.
            do
            {
                Console.Clear();
                // Вызываем метод, который реализует пользовательский интерфейс.
                Menu(out exit);
                // Дополнительно проверяем, действительно ли пользователь желает покинуть программу.
                if (exit)
                {
                    Console.Clear();
                    Console.WriteLine("Вы уверены, что хотите выйти?\n" +
                        "Для выхода нажмите ESC, для возвращения - любую другую клавишу.");
                    exit = Console.ReadKey().Key == ConsoleKey.Escape;
                }
            } while (!exit);
        }
        
        // Список доступных в меню опций.
        static readonly string menuOptions = "Для продолжения нажмите соответствующую клавишу:\n" +
                "1. Найти след матрицы.\n2. Транспонировать матрицу.\n" +
                "3. Найти сумму матриц.\n4. Найти разность матриц.\n" +
                "5. Найти произведение двух матриц.\n6. Домножить матрицу на число.\n" +
                "7. Найти определитель матрицы.\n8. Решить СЛАУ (методом Крамера).\n" +
                "9. Выйти из программы.";
        
        /// <summary>
        /// Метод реализует "пользовательский интерфейс".
        /// </summary>
        /// <param name="exit"> Переменная типа bool, которая сообщает нужно ли завершить работу программы. </param>
        public static void Menu(out bool exit)
        {
            // По умолчанию пользователь не желает покинуть программу.
            exit = false;
            // Выводим список доступных опций.
            Console.WriteLine(menuOptions);
            // Проверяем нажатую пользователем клавишу и выполняем соответствующее действие.
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    MenuOptionTrace();
                    break;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    MenuOptionTranspose();
                    break;
                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    MenuOptionSum();
                    break;
                case ConsoleKey.D4:
                case ConsoleKey.NumPad4:
                    MenuOptionDifference();
                    break;
                case ConsoleKey.D5:
                case ConsoleKey.NumPad5:
                    MenuOptionMatrixMultiply();
                    break;
                case ConsoleKey.D6:
                case ConsoleKey.NumPad6:
                    MenuOptionNumberMultiply();
                    break;
                case ConsoleKey.D7:
                case ConsoleKey.NumPad7:
                    MenuOptionDeterminant();
                    break;
                case ConsoleKey.D8:
                case ConsoleKey.NumPad8:
                    MenuOptionKramer();
                    break;
                case ConsoleKey.D9:
                case ConsoleKey.NumPad9:
                    exit = true;
                    break;
            }
        }
        
        /// <summary>
        /// Метод реализует 1-ю опцию меню ("Найти след матрицы").
        /// </summary>
        public static void MenuOptionTrace()
        {
            // Переменная сообщает о необходимости продолжить работу цикла.
            bool toContinue;
            // Инициализируем нашу будущую матрицу.
            double[,] matrix;
            // Повторяем работу цикла пока ввод не будет корректным или пользователь не пожелает вернуться.
            do
            {
                toContinue = false;
                // Вводим матрицу.
                matrix = Input("Нахождение следа матрицы.\n" +
                    "Для продолжения введите эту матрицу любым из предложенных способов.\n" +
                    "Примечание: она должна быть квадратной.\n");
                // Если вместо матрицы вернулся null, то пользователь хочет вернуться в меню.
                if (matrix == null)
                    return;
                // Т.к. метод ввода универсальный, проверяем, удовлетворяет ли размер матрицы условию.
                if (matrix.GetLength(0) != matrix.GetLength(1))
                {
                    Console.WriteLine("Неверный размер матрицы! Она должна быть квадратной!\n" +
                        "Для возвращения в меню нажмите ESC, для повторного ввода - любую другую клавишу.");
                    if (Console.ReadKey().Key == ConsoleKey.Escape)
                        return;
                    // Если пользователь не вернулся в меню, то повторяем ввод.
                    toContinue = true;
                }
            } while (toContinue);
            // Выводим результат.
            Console.Clear();
            Console.WriteLine("Введённая матрица:");
            Print(matrix);
            Console.WriteLine($"\nСлед матрицы: {Trace(matrix):F3}");
            Console.WriteLine("\nДля возвращения в меню нажмите любую клавишу.");
            Console.ReadKey();
        }
        
        /// <summary>
        /// Метод реализует 2-ю опцию меню ("Траспонировать матрицу").
        /// </summary>
        public static void MenuOptionTranspose()
        {
            // Вводим матрицу.
            var matrix = Input("Транспонирование матрицы.\n" +
                   "Для продолжения введите эту матрицу любым из предложенных способов.\n");
            // Если вместо матрицы вернулся null, то пользователь желает вернуться в меню.
            if (matrix == null)
                return;
            // Выводим результат.
            Console.Clear();
            Console.WriteLine("Введённая матрица:");
            Print(matrix);
            Console.WriteLine("\nТранспонированная матрица:");
            Print(Transpose(matrix));
            Console.WriteLine("\nДля возвращения в меню нажмите любую клавишу.");
            Console.ReadKey();
        }

        /// <summary>
        /// Метод реализует 3-ю опцию меню ("Сложить две матрицы").
        /// </summary>
        public static void MenuOptionSum()
        {
            // Переменная сообщает о необходимости продолжить работу цикла.
            bool toContinue;
            // Инициализируем матрицы.
            double[,] firstMatrix, secondMatrix;
            // Цикл повторяется пока ввод не будем корректным или пользователь не захочет вернуться в меню.
            do
            {
                toContinue = false;
                // Вводим 1-ю матрицу. Если возвращается null, то пользователь хочет вернуться в меню.
                firstMatrix = Input("Нахождение суммы матриц.\n" +
                       "Для продолжения введите 1-ю матрицу любым из предложенных способов.\n");
                if (firstMatrix == null)
                    return;
                // Вводим 2-ю матрицу. Если возвращается null, то пользователь хочет вернуться в меню.
                secondMatrix = Input("Нахождение суммы матриц.\n" +
                       "Для продолжения введите 2-ю матрицу любым из предложенных способов.\n");
                if (secondMatrix == null)
                    return;
                // Т.к. метод ввода универсальный, проверяем матрицы на соответствие условию задачи.
                if (firstMatrix.GetLength(1) != secondMatrix.GetLength(0) ||
                    firstMatrix.GetLength(1) != secondMatrix.GetLength(1))
                {
                    Console.WriteLine("\nМатрицы должны быть одинаковых размеров!\n" +
                        "Для возвращения в меню нажмите ESC, для повторного ввода - любую другую клавишу.");
                    if (Console.ReadKey().Key == ConsoleKey.Escape)
                        return;
                    toContinue = true;
                }
            } while (toContinue);
            // Выводим результат.
            Console.Clear();
            Console.WriteLine("Первая введённая матрица:");
            Print(firstMatrix);
            Console.WriteLine("\nВторая введённая матрица:");
            Print(secondMatrix);
            Console.WriteLine("\nСумма этих матриц:");
            Print(Sum(firstMatrix, secondMatrix));
            Console.WriteLine("\nДля возвращения в меню нажмите любую клавишу.");
            Console.ReadKey();
        }
        
        /// <summary>
        /// Метод реализует 3-ю опцию меню ("Найти разность матриц").
        /// </summary>
        public static void MenuOptionDifference()
        {
            // Переменная сообщает о необходимости продолжить работу цикла.
            bool toContinue;
            // Инициализируем матриц.
            double[,] firstMatrix, secondMatrix;
            // Цикл повторяется пока ввод не будем корректным или пользователь не захочет вернуться в меню.
            do
            {
                toContinue = false;
                // Вводим 1-ю матрицу. Если возвращается null, то пользователь хочет вернуться в меню.
                firstMatrix = Input("Нахождение разности матриц.\n" +
                       "Для продолжения введите 1-ю матрицу любым из предложенных способов.\n");
                if (firstMatrix == null)
                    return;
                // Вводим 2-ю матрицу. Если возвращается null, то пользователь хочет вернуться в меню.
                secondMatrix = Input("Нахождение разности матриц.\n" +
                       "Для продолжения введите 2-ю матрицу любым из предложенных способов.\n");
                if (secondMatrix == null)
                    return;
                // Т.к. метод ввода универсальный, проверяем матрицы на соответствие условию задачи.
                if (firstMatrix.GetLength(0) != secondMatrix.GetLength(0) ||
                    firstMatrix.GetLength(1) != secondMatrix.GetLength(1))
                {
                    Console.WriteLine("Матрицы должны быть одинаковых размеров!\n" +
                        "Для возвращения в меню нажмите ESC, для повторного ввода - любую другую клавишу.");
                    if (Console.ReadKey().Key == ConsoleKey.Escape)
                        return;
                    toContinue = true;
                }
            } while (toContinue);
            // Выводим результат.
            Console.Clear();
            Console.WriteLine("Первая введённая матрица:");
            Print(firstMatrix);
            Console.WriteLine("\nВторая введённая матрица:");
            Print(secondMatrix);
            Console.WriteLine("\nРазность этих матриц:");
            Print(Sum(firstMatrix, Multiply(-1, secondMatrix)));
            Console.WriteLine("\nДля возвращения в меню нажмите любую клавишу.");
            Console.ReadKey();
        }
        
        /// <summary>
        /// Метод реализует 4-ю опцию меню ("Умножить матрицы").
        /// </summary>
        public static void MenuOptionMatrixMultiply()
        {
            // Переменная сообщает о необходимости продолжить работу цикла.
            bool toContinue;
            // Инициализируем матриц.
            double[,] firstMatrix, secondMatrix;
            // Цикл повторяется пока ввод не будем корректным или пользователь не захочет вернуться в меню.
            do
            {
                toContinue = false;
                // Вводим 1-ю матрицу. Если возвращается null, то пользователь хочет вернуться в меню.
                firstMatrix = Input("Нахождение произведения матриц.\n" +
                       "Для продолжения введите 1-ю матрицу любым из предложенных способов.\n");
                if (firstMatrix == null)
                    return;
                // Вводим 2-ю матрицу. Если возвращается null, то пользователь хочет вернуться в меню.
                secondMatrix = Input("Нахождение произведения матриц.\n" +
                       "Для продолжения введите 2-ю матрицу любым из предложенных способов.\n");
                if (secondMatrix == null)
                    return;
                // Т.к. метод ввода универсальный, проверяем матрицы на соответствие условию задачи.
                if (firstMatrix.GetLength(1) != secondMatrix.GetLength(0))
                {
                    Console.WriteLine("\nМатрицы должны быть размеров M x N и N x K!\n" +
                        "Для возвращения в меню нажмите ESC, для повторного ввода - любую другую клавишу.");
                    if (Console.ReadKey().Key == ConsoleKey.Escape)
                        return;
                    toContinue = true;
                }
            } while (toContinue);
            // Выводим результат.
            Console.Clear();
            Console.WriteLine("Первая введённая матрица:");
            Print(firstMatrix);
            Console.WriteLine("\nВторая введённая матрица:");
            Print(secondMatrix);
            Console.WriteLine("\nПроизведение этих матриц:");
            Print(Multiply(firstMatrix, secondMatrix)) ;
            Console.WriteLine("\nДля возвращения в меню нажмите любую клавишу.");
            Console.ReadKey();
        }
        
        /// <summary>
        /// Метод реализует 5-ю опцию меню ("Умножить матрицу на число").
        /// </summary>
        public static void MenuOptionNumberMultiply()
        {
            // Вводим матрицу. Если возвращается null, то пользователь хочет вернуться в меню.
            var matrix = Input("Умножение матрицы на число.\n" +
                   "Для продолжения введите эту матрицу любым из предложенных способов.\n");
            if (matrix == null)
                return;
            // Переменная сообщает о необходимости продолжить работу цикла.
            bool toContinue;
            // Инициализируем переменную для записи множителя.
            double multiplier;
            // Повторяем цикл, пока ввод не будет корректным или пользователь не захочет выйти.
            do
            {
                Console.Clear();
                Console.Write("Введите число, на которое хотите умножить матрицу: ");
                // Вводим множитель.
                toContinue = !double.TryParse(Console.ReadLine(), out multiplier);
                // Если ввод некорректный, проверяем, хочет ли пользователь вернуться в меню.
                if (toContinue)
                {
                    Console.WriteLine("\nОшибка ввода!" +
                            "Для выхода в меню нажмите ESC, для повторного ввода - любую другую клавишу.");
                    if (Console.ReadKey().Key == ConsoleKey.Escape)
                        return;
                }
            } while (toContinue);
            // Выводим результат.
            Console.Clear();
            Console.WriteLine("Введённая матрица:");
            Print(matrix);
            Console.WriteLine($"\nМатрица, домноженная на {multiplier}:");
            Print(Multiply(multiplier, matrix));
            Console.WriteLine("\nДля возвращения в меню нажмите любую клавишу.");
            Console.ReadKey();
        }

        /// <summary>
        /// Метод реализует 6-ю опцию меню ("Найти определитель матрицы").
        /// </summary>
        public static void MenuOptionDeterminant()
        {
            // Переменная сообщает о необходимости продолжить работу цикла.
            bool toContinue;
            // Инициализируем матрицу.
            double[,] matrix;
            // Повторяем цикл, пока ввод не будет корректным или пользователь не захочет выйти.
            do
            {
                toContinue = false;
                // Вводим матрицу. Если возвращается null, то пользователь хочет вернуться в меню.
                matrix = Input("Нахождение определителя матрицы.\n" +
                    "Для продолжения введите эту матрицу любым из предложенных способов.\n" +
                    "Примечание: она должна быть квадратной.\n");
                if (matrix == null)
                    return;
                // Т.к. метод ввода универсальный, проверяем матрицы на соответствие условию задачи.
                if (matrix.GetLength(0) != matrix.GetLength(1))
                {
                    Console.WriteLine("Неверный размер матрицы! Она должна быть квадратной!\n" +
                        "Для возвращения в меню нажмите ESC, для повторного ввода - любую другую клавишу.");
                    if (Console.ReadKey().Key == ConsoleKey.Escape)
                        return;
                    toContinue = true;
                }
            } while (toContinue);
            // Выводим результат.
            Console.Clear();
            Console.WriteLine("Введённая матрица:");
            Print(matrix);
            Console.WriteLine($"\nОпределитель матрицы: {Determinant(matrix):F3}");
            Console.WriteLine("\nДля возвращения в меню нажмите любую клавишу.");
            Console.ReadKey();
        }

        /// <summary>
        /// Метод реализует 7-ю опцию меню ("Решить СЛАУ методом Крамера").
        /// </summary>
        public static void MenuOptionKramer()
        {
            // Переменная сообщает о необходимости продолжить работу цикла.
            bool toContinue;
            // Инициализируем матрицу.
            double[,] matrix;
            // Повторяем цикл, пока ввод не будет корректным или пользователь не захочет выйти.
            do
            {
                toContinue = false;
                // Вводим матрицу. Если возвращается null, то пользавотель хочет вернуться в меню.
                matrix = Input("Решение СЛАУ методом Крамера.\n" +
                    "Для продолжения запишите систему в виде матрицы.\n" +
                    "Примечание: она должна быть размера N x (N + 1).\n");
                if (matrix == null)
                    return;
                // Т.к. метод ввода универсальный, проверяем матрицы на соответствие условию задачи.
                if (matrix.GetLength(0) != matrix.GetLength(1) - 1)
                {
                    Console.WriteLine("Неверный размер матрицы! Она должна быть размера N x (N + 1)!\n" +
                        "Для возвращения в меню нажмите ESC, для повторного ввода - любую другую клавишу.");
                    if (Console.ReadKey().Key == ConsoleKey.Escape)
                        return;
                    toContinue = true;
                }
            } while (toContinue);
            // Выводим результат.
            Console.Clear();
            Console.WriteLine("Введённая матрица:");
            Print(matrix);
            Console.WriteLine($"\nРешение СЛАУ:\n{Kramer(matrix)}");
            Console.WriteLine("\nДля возвращения в меню нажмите любую клавишу.");
            Console.ReadKey();
        }
        
        /// <summary>
        /// Метод реализует ввод матрицы разными способами.
        /// </summary>
        /// <param name="message"> Сообщение, которое выводится в начале работы метода. </param>
        /// <returns> Вовзращает введённую матрицу. </returns>
        public static double[,] Input(string message)
        {
            // Переменная сообщает о необходимости продолжить работу цикла.
            bool toContinue;
            // Инициализируем матрицу.
            double[,] matrix;
            // Повторяем цикл, пока ввод не будет корректным или пользователь не захочет выйти.
            do
            {
                Console.Clear();
                Console.WriteLine(message);
                // Сообщаем пользователю о возможных способах ввода.
                Console.WriteLine("Для того чтобы ввести матрицу, выберите способ ввода.\n" +
                    "Для этого нажмите соответствующую клавишу:\n" +
                    "1. Вручную.\n2. Из файла.\n3. Заполнить случайно.");
                // Вызываем метод для выбора способа ввода.
                matrix = InputOptions(out toContinue);
                if (!toContinue && matrix == null)
                {
                    Console.WriteLine("\nВыбранной опции не существует!\n" +
                        "Для выхода в меню нажмите ESC, для повторного ввода - любую другую клавишу.");
                    if (Console.ReadKey().Key == ConsoleKey.Escape)
                        return null;
                    toContinue = true;
                }
                else
                    toContinue = false;
            } while (toContinue);
            return matrix;
        }
        
        /// <summary>
        /// Метод предлашает на выбор несколько способов ввода.
        /// </summary>
        /// <param name="rightOption"> Переменная типа bool сообщает, была ли выбрана верная опция. </param>
        /// <returns> Возвращает введённую матрицу. </returns>
        public static double[,] InputOptions(out bool rightOption)
        {
            // Инициализируем картеж для возвращения.
            (int, int) size;
            // Переменная сообщает о необходимости продолжить работу цикла.
            rightOption = true;
            // Проверяем нажатую пользователем клавишу.
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    size = InputSize();
                    if (size.Item1 == 0 || size.Item2 == 0)
                        return null;
                    return ManualInput(size.Item1, size.Item2);
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    return InitFileInput();
                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    size = InputSize();
                    if (size.Item1 == 0 || size.Item2 == 0)
                        return null;
                    return RandomInput(size.Item1, size.Item2);
                default:
                    rightOption = false;
                    return null;
            }
        }
        
        /// <summary>
        /// Метод находит матрицу с вычеркнутыми строкой и столбцом.
        /// </summary>
        /// <param name="matrix"> Исходная матрица. </param>
        /// <param name="rowNo"> Номер строки, которую нужно вычеркнуть. </param>
        /// <param name="columnNo"> Номер столбца, кторый нужно вычеркнуть. </param>
        /// <returns> Возвращает исходную матрицу с вычеркнутыми строкой и столбцом. </returns>
        public static double[,] GetMinor(double[,] matrix, int rowNo, int columnNo)
        {
            // Находим размер исходной матрицы.
            var rowsNumber = matrix.GetLength(0);
            var columnNumber = matrix.GetLength(1);
            // Проверяем, нужно ли вообще вычёркивать что-либо.
            if (rowNo >= 0 && rowNo < rowsNumber) 
                rowsNumber--;
            if (columnNo >= 0 && columnNo < columnNumber) 
                columnNumber--;
            // Инициализируем новую матрицу.
            var minor = new double[rowsNumber, columnNumber];
            // Инициализируем переменную, которая позволяет пропускать ненужную строку.
            var addI = 0;
            // Заполняем каждый элемент новой матрицы.
            for (var i = 0; i < rowsNumber; i++)
            {
                // Находим матрицу с вычеркнутыми строкой и столбцом.
                var addJ = 0;
                // Если достигаем ненужную строку, увеличиваем вспомогательную переменную, чтобы пропустить это строку.
                if (i == rowNo)
                    addI++;
                for (var j = 0; j < columnNumber; j++)
                {
                    // Если достигаем ненужный столбец, увеличиваем вспомогательную переменную, чтобы пропустить его.
                    if (j == columnNo)
                        addJ++;
                    minor[i, j] = matrix[i + addI, j + addJ];
                }
            }
            return minor;
        }
        
        /// <summary>
        /// Метод считает определитель матрицы.
        /// </summary>
        /// <param name="matrix"> Матрица, для которой нужно найти определитель. </param>
        /// <returns> Возвращает определитель матрицы. </returns>
        public static double Determinant(double[,] matrix)
        {
            // Запоминаем размер матрицы.
            int matrixSize = matrix.GetLength(0);
            // Если в матрице один элемент, то определитель этой матрицы - этот элемент.
            if (matrixSize == 1)
                return matrix[0, 0];
            // Для матрицы любого другого размера используем разложение по строке.
            // Инициализируем переменную для записи определителя.
            double det = 0;
            // Реализуем метод нахождения определителя через разложение по строке.
            for (int i = 0; i < matrixSize; i++)
                det += (long) (Math.Pow(-1, i) * matrix[0, i] * Determinant(GetMinor(matrix, 0, i)));
            return det;
        }

        /// <summary>
        /// Метод выводит матрицу.
        /// </summary>
        /// <param name="matrix"> Матрица, которую необходимо вывести. </param>
        public static void Print(double[,] matrix)
        {
            // Запоминаем размеры матрицы.
            var rowsNumber = matrix.GetLength(0);
            var columnsNumber = matrix.GetLength(1);
            // Инициализируем массив для записи пропусков, которые нужно поставить перед элементами.
            var spaces = new string[rowsNumber, columnsNumber];
            // Проходим каждый столбец.
            for (int j = 0; j < columnsNumber; j++)
            {
                // Находим число максимальной длины.
                var maxLength = 0;
                for (int i = 0; i < rowsNumber; i++)
                {
                    var length = ((int)matrix[i, j]).ToString().Length;
                    if (length > maxLength)
                        maxLength = length;
                }
                // Заполняем массив пропусков согласно числу максимальной длины.
                for (int i = 0; i < rowsNumber; i++)
                {
                    var length = ((int)matrix[i, j]).ToString().Length;
                    for (int k = 0; k < maxLength - length; k++)
                        spaces[i, j] += " ";
                }
            }
            // Вывод каждый элемент массива вместе с пропусками.
            for (int i = 0; i < matrix.GetLength(0); i++, Console.WriteLine())
                for (int j = 0; j < matrix.GetLength(1); j++)
                    Console.Write("{0}{1:F3}\t", spaces[i, j], matrix[i, j]);
        }
        
        /// <summary>
        /// Метод вычисляет след матрицы.
        /// </summary>
        /// <param name="matrix"> Матрица, для которой нужно вычислить след. </param>
        /// <returns> Возвращаем след матрицы. </returns>
        public static double Trace(double[,] matrix)
        {
            // Инициализируем переменную для записи следа.
            double sum = 0;
            // Проходим по главной диагонали и суммируем элементы.
            for (int i = 0; i < matrix.GetLength(0); i++) 
                sum += matrix[i, i];
            return sum;
        }

        /// <summary>
        /// Метод транспонирует матрицу.
        /// </summary>
        /// <param name="matrix"> Матрица, которую нужно транспонировать. </param>
        /// <returns> Возвращает транспонированную матрицу. </returns>
        public static double[,] Transpose(double[,] matrix)
        {
            // Запоминаем размеры матрицы.
            var rowsNumber = matrix.GetLength(0);
            var columnsNumber = matrix.GetLength(1);
            // Инициализируем новую матрицу.
            var newMatrix = new double[columnsNumber, rowsNumber];
            // Заполняем новую матрицу.
            for (var i = 0; i < rowsNumber; i++)
            for (var j = 0; j < columnsNumber; j++)
                newMatrix[j, i] = matrix[i, j];
            return newMatrix;
        }
        
        /// <summary>
        /// Метод находит сумму двух матриц.
        /// </summary>
        /// <param name="matrix1"> Первая матрица. </param>
        /// <param name="matrix2"> Вторая матрица. </param>
        /// <returns></returns>
        public static double[,] Sum(double[,] matrix1, double[,] matrix2)
        {
            // Сохраняем размеры матриц и инициализируем результирующую.
            var rowNumber = matrix1.GetLength(0);
            var columnNumber = matrix1.GetLength(1);
            var result = new double[rowNumber, columnNumber];
            // Заполняем результирующую матрицу.
            for (var i = 0; i < rowNumber; i++)
            for (var j = 0; j < columnNumber; j++)
                result[i, j] = matrix1[i, j] + matrix2[i, j];

            return result;
        }
        
        /// <summary>
        /// Умножает матрицу на число.
        /// </summary>
        /// <param name="multiplier"> Число, на которое нужно умножить матрицу. </param>
        /// <param name="matrix"> Матрица, которую нужно умножить на число. </param>
        /// <returns></returns>
        public static double[,] Multiply(double multiplier, double[,] matrix)
        {
            // Сохраняем размеры матрицы и инициализируем результрующую.
            var rowNumber = matrix.GetLength(0);
            var columnNumber = matrix.GetLength(1);
            var result = new double[rowNumber, columnNumber];
            // Заполняем результрующую матрицу.
            for (var i = 0; i < rowNumber; i++)
            for (var j = 0; j < columnNumber; j++)
                result[i, j] = multiplier * matrix[i, j];

            return result;
        }

        public static double[,] Multiply(double[,] matrix1, double[,] matrix2)
        {
            var rowNumber = matrix1.GetLength(0);
            var columnNumber = matrix2.GetLength(1);
            var additionalSize = matrix1.GetLength(1);
            var result = new double[rowNumber, columnNumber];
            
            for (var i = 0; i < rowNumber; i++)
            for (var j = 0; j < columnNumber; j++)
            {
                result[i, j] = 0;
                for (var k = 0; k < additionalSize; k++)
                    result[i, j] += matrix1[i, k] * matrix2[k, j];
            }

            return result;
        }
        
        /// <summary>
        /// Метод решает СЛАУ методом Крамера.
        /// </summary>
        /// <param name="system"> Матрица с коэффицентами уравнения. </param>
        /// <returns> Возвращает решение системы в виде строки. </returns>
        public static string Kramer(double[,] system)
        {
            // Сохраняем размер матрицы.
            var columnNumber = system.GetLength(1);
            // Вычисляем общий определитель.
            var detGeneral = Determinant(GetMinor(system, -1, columnNumber - 1));
            // Инициализируем строку, в которую будем записывать ответ.
            var result = "";
            // Если матрица имеет решение, вычисляем определитель для каждой переменной и находим её значение.
            if (detGeneral != 0)
                for (var i = 0; i < columnNumber - 1; i++)
                {
                    var detVariable = Determinant(GetMinor(system, -1, i));
                    // Т.к. GetMinor не ставит столбец свободных коэффицентов на нужное место, умножаем на -1.
                    if (i < columnNumber - 2)
                        detVariable *= -1;    
                    if (i > 0)
                        result += "; ";
                    result += $"X{i + 1} = {detVariable / detGeneral:F3}";
                }
            else
                result = "Система имеет бесконечно много решений или несовместна.";
            return result;
        }
        
        /// <summary>
        /// Метод реализует ввод размера матрицы.
        /// </summary>
        /// <returns> Возвращает размеры вводимой матрицы в виде картежа. </returns>
        public static (int, int) InputSize()
        {
            // Переменная соообщает о необходимости продолжения работы цикла.
            bool inputIsCorrect;
            // Инициализируем переменные для записи размера.
            int rowsNumber, columnsNumber = 0;
            // Цикл повторяется пока ввод не будет корректным или пользователь не захочет вернутьс я в меню.
            do
            {
                // Вводим размеры и проверяем их на корректность.
                // Если ввод некорректный, предлагаем пользователю повторить ввод или вернуться в меню.
                Console.Clear();
                Console.Write("Введите количество строк в задаваемой матрице: ");
                string input = Console.ReadLine();
                inputIsCorrect = int.TryParse(input, out rowsNumber);
                if (!inputIsCorrect)
                {
                    Console.WriteLine("Ошибка ввода!\n" +
                        "Для выхода в меню нажмите ESC, для повторного ввода - любую другую клавишу.");
                    if (Console.ReadKey().Key == ConsoleKey.Escape)
                        return (0, 0);
                    continue;
                }
                Console.Write("Введите количество столбцов в задаваемой матрице: ");
                input = Console.ReadLine();
                inputIsCorrect = int.TryParse(input, out columnsNumber);
                if (!inputIsCorrect)
                {
                    Console.WriteLine("Ошибка ввода!\n" +
                        "Для выхода в меню нажмите ESC, для повторного ввода - любую другую клавишу.");
                    if (Console.ReadKey().Key == ConsoleKey.Escape)
                        return (0, 0);
                }
            } while (!inputIsCorrect);
            return (rowsNumber, columnsNumber);
        }
        
        /// <summary>
        /// Метод преобразует введённую строку в элементы массива.
        /// </summary>
        /// <param name="matrix"> Матрица, ввод которой производится. </param>
        /// <param name="rowNo"> Номер строки. ввод которой производится. </param>
        /// <returns> Вовзвращает true, если ввод корректный, иначе - false. </returns>
        public static bool InputLineManually(double[,] matrix, int rowNo)
        {
            // Считываем строку и проверяем, не пустая ли она.
            var input = Console.ReadLine();
            var inputIsCorrect = input != null;
            if (!inputIsCorrect) return false;
            // Делим строку на подстроки, в каждой из которых записано одно число.
            var numbersOfMatrix = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var columnsNumber = matrix.GetLength(1);
            // Проверяем совпадае ли кол-во введённых элементов с кол-вом элементов в строке матрицы.
            inputIsCorrect = columnsNumber == numbersOfMatrix.Length;
            // Записываем полученные числа в массив (если это возможно).
            for (var j = 0; inputIsCorrect && j < columnsNumber; j++)
                inputIsCorrect = double.TryParse(numbersOfMatrix[j], out matrix[rowNo, j]);
            return inputIsCorrect;
        }

        // Правила ввода вручную.
        static readonly string manualInputRules = "Чтобы корректно ввести матрицу, для каждой её строки введите все " +
                                                "элементы этой строки через пробел.\nНапример,\n" +
                                                "1 2\n3 4\nбудет считано так:\na11 = 1; a12 = 2; a21 = 3; a22 = 4.\n";
        
        /// <summary>
        /// Реализует ввод матрицы вручную.
        /// </summary>
        /// <param name="rowsNumber"> Кол-во строк в матрице. </param>
        /// <param name="columnsNumber"> Кол-во столбцов в матрице. </param>
        /// <returns> Возвращает матрицу в виде двумерного массива. </returns>
        public static double[,] ManualInput(int rowsNumber, int columnsNumber)
        {
            // Инициализируем матрицу.
            double[,] matrix;
            // Переменная соообщает о необходимости продолжения работы цикла.
            bool inputIsCorrect;
            // Цикл будет работать, пока введённые данные не будут корректными или пользователь не захочет вернуться.
            do
            {           
                // Задаём размер матрицы.
                matrix = new double[rowsNumber, columnsNumber];
                inputIsCorrect = true;
                Console.Clear();
                Console.WriteLine(manualInputRules + $"\nВведите матрицу размера {rowsNumber}x{columnsNumber}:");
                // Считываем и преобразуем каждую строку.
                for (var i = 0; inputIsCorrect && i < rowsNumber; i++)
                    inputIsCorrect = InputLineManually(matrix, i);
                if (!inputIsCorrect)
                {
                    Console.WriteLine("Ошибка ввода!\n" +
                        "Для выхода в меню нажмите ESC, для повторного ввода - любую другую клавишу.");
                    if (Console.ReadKey().Key == ConsoleKey.Escape)
                        return null;
                }
            } while (!inputIsCorrect);
            
            return matrix;
        }
        
        /// <summary>
        /// Метод преобразует массив строк в матрицу.
        /// </summary>
        /// <param name="lines"> Массив строк с элементами матрицы. </param>
        /// <param name="matrix"> Заполняемая матрица. </param>
        /// <returns> Возвращает заполненную матрицу (или null). </returns>
        public static bool LinesToMatrix(string[] lines, out double[,] matrix)
        {
            // Первая строка - размеры матрицы. Пробуем преобразовать в переменную типа int.
            var elements = lines[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            // Переменная сообщает, является ли ввод корректным.
            bool inputIsCorrect = int.TryParse(elements[0], out int rowsNumber) &
                int.TryParse(elements[1], out int columnsNumber) && (rowsNumber == lines.Length - 1);
            if (!inputIsCorrect)
            {
                matrix = null;
                return false;
            }
            // Создаём результирующую матрицу.
            matrix = new double[rowsNumber, columnsNumber];
            // Каждая из последующих строк содержит несколько элементов матрицы.
            for (var i = 0; inputIsCorrect && i < rowsNumber; i++)
            {
                // Разделяем строку на элементы и преобразуем в тип double.
                elements = lines[i + 1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                inputIsCorrect = columnsNumber == elements.Length;
                for (var j = 0; inputIsCorrect && j < columnsNumber; j++)
                    inputIsCorrect = double.TryParse(elements[j], out matrix[i, j]);
            }
            if (!inputIsCorrect)
            {
                matrix = null;
                return false;
            }
            return true;
        }

        // Правила считывания матрицы из файла.
        static string fileInputRules = "Пользователь вводит путь к файлу, в котором хранится " +
            "матрица в следующем формате:\nНа первой строке записаны 2 числа n - кол-во строк, " +
            "m - кол-во столбцов через пробел.\nДалее для каждой строки выписаны все её " +
            "элементы через пробел.\nНапример,\n3 3\n1,0 2 3\n4 5,1 6\n7 8 9\nбудет считано так:\n" +
            "a11 = 1; a12 = 2;   a13 = 3;\na21 = 4; a22 = 5,1; a23 = 6;\n" +
            "a31 = 7; a32 = 8;   a33 = 9;";
        
        /// <summary>
        /// Метод инициализирует процесс считывания матрицы из файла.
        /// </summary>
        /// <returns> Возвращает считанную матрицу (или null). </returns>
        public static double[,] InitFileInput()
        {
            // Переменная соообщает о необходимости продолжения работы цикла.
            bool inputIsCorrect;
            // Инициализируем матрицу.
            double[,] matrix;
            // Цикл будет повторяться пока ввод не будет корректным или пользователь не захочет вернуться в меню.
            do
            {
                Console.Clear();
                Console.WriteLine(fileInputRules);
                // Пользователь вводит путь к файлу.
                Console.Write("\nВведите путь к файлу: ");
                var path = Console.ReadLine();
                // Считывание матрицы.
                matrix = FileInput(path, out inputIsCorrect);
                if (!inputIsCorrect)
                {
                    Console.WriteLine("Для возвращения в меню нажмите ESC, " +
                        "для повторного ввода - любую другую клавишу.");
                    if (Console.ReadKey().Key == ConsoleKey.Escape)
                        return null;
                }
            } while (!inputIsCorrect);
            return matrix;
        }

        /// <summary>
        /// Метод реализует процесс считывания матрицы из файла.
        /// </summary>
        /// <param name="path"> Путь к файлу. </param>
        /// <param name="inputIsCorrect"> Переменная, сообщающая о корректности/некорректности ввода. </param>
        /// <returns> Возвращает считанную матрицу (или null). </returns>
        public static double[,] FileInput(string path, out bool inputIsCorrect)
        {
            // Проверяем, существует ли файл по заданному пути.
            try
            {
                // Считываем весь файл и записываем его в массив строк.
                var lines = System.IO.File.ReadAllLines(path);
                // Заполняем матрицу с помощью массива считанных строк.
                inputIsCorrect = LinesToMatrix(lines, out var matrix);
                if (!inputIsCorrect)
                {
                    Console.WriteLine("Ошибка ввода!");
                    return null;
                }
                return matrix;
            }
            catch
            {
                inputIsCorrect = false;
                Console.WriteLine("По данному пути нет файла!");
                return null;
            }
        }

        /// <summary>
        /// Метод реализиует заполнение матрицы заданного размера случайными числами.
        /// </summary>
        /// <param name="rowsNumber"> Кол-во строк. </param>
        /// <param name="columnsNumber"> Кол-во столбцов. </param>
        /// <returns> Возвращает заполненную матрицу. </returns>
        public static double[,] RandomInput(int rowsNumber, int columnsNumber)
        {
            // Переменная соообщает о необходимости продолжения работы цикла.
            bool inputIsCorrect;
            // Инициализируем переменные для границ диапазона генерации.
            double minNumber, maxNumber;
            // Цикл будет повторяться пока ввод не будет корректным или пользователь не захочет вернуться в меню.
            do
            {
                // Пользователь вводит границы диапазона генерации.
                Console.Clear();
                Console.WriteLine($"Матрица размера {rowsNumber}x{columnsNumber} будет сегнерирована " +
                    "случайно.\nКаждый её элемент будет находиться в диапозоне [a;b).");
                Console.Write("Введите a: ");
                inputIsCorrect = double.TryParse(Console.ReadLine(), out minNumber);
                Console.Write("Введите b: ");
                inputIsCorrect &= double.TryParse(Console.ReadLine(), out maxNumber);
                if (!inputIsCorrect)
                {
                    Console.WriteLine("Ошибка ввода!\nДля возвращения в меню нажмите ESC, " +
                        "для повторного ввода - любую другую клавишу.");
                    if (Console.ReadKey().Key == ConsoleKey.Escape)
                        return null;
                }
            } while (!inputIsCorrect);
            // Инициализируем матрицу.
            var matrix = new double[rowsNumber, columnsNumber];
            Random rnd = new Random();
            // Заполняем матрицу случайными числами из заданного диапазона.
            for (var i = 0; i < rowsNumber; i++)
                for (var j = 0; j < columnsNumber; j++)
                    matrix[i, j] = (maxNumber - minNumber) * rnd.NextDouble() + minNumber;
            return matrix;
        }
    }
}