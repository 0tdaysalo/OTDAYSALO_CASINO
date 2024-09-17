using System.Collections.Generic;


namespace SLOT_3
{
    public static class DictionariesOfPlayingSymbols
    {
        //класс для словарей для подсчета выплат по символам при разных сыгровках

        //у каждого словаря есть в конце названия символ который означает их сыгровку

        public static readonly Dictionary<int,    int> array_symbols_dic_multiplucation_S = new Dictionary<int,    int>()
        {
            //scatters
            {3, 10},
            {4, 12},
            {5, 15},
            {6, 20},
            {7, 30},
        };

        public static readonly Dictionary<int, double> array_symbols_dic_multiplucation_6 = new Dictionary<int, double>()
        {
            //чупики
            {5, 1.00},
            {6, 1.50},
            {7, 1.75},
            {8, 2.00},
            {9, 2.50},
            {10, 5.00},
            {11, 7.50},
            {12, 15.00},
            {13, 35.00},
            {14, 70.00},
            {15, 150.00},
        };

        public static readonly Dictionary<int, double> array_symbols_dic_multiplucation_5 = new Dictionary<int, double>()
        {
            //сердечки
            {5, 0.75},
            {6, 1.00},
            {7, 1.25},
            {8, 1.50},
            {9, 2.00},
            {10, 4.00},
            {11, 6.00},
            {12, 12.50},
            {13, 30.00},
            {14, 60.00},
            {15, 100.00},
        };

        public static readonly Dictionary<int, double> array_symbols_dic_multiplucation_4 = new Dictionary<int, double>()
        {
            //баклажаны
            {5, 0.5},
            {6, 0.75},
            {7, 1.00},
            {8, 1.25},
            {9, 1.50},
            {10, 3.00},
            {11, 4.50},
            {12, 10.00},
            {13, 20.00},
            {14, 40.00},
            {15, 60.00},
        };

        public static readonly Dictionary<int, double> array_symbols_dic_multiplucation_3 = new Dictionary<int, double>()
        {
            //звёзды
            {5, 0.4},
            {6, 0.5},
            {7, 0.75},
            {8, 1.00},
            {9, 1.25},
            {10, 2.0},
            {11, 3.0},
            {12, 5.0},
            {13, 10.0},
            {14, 20.0},
            {15, 40.0},
        };

        public static readonly Dictionary<int, double> array_symbols_dic_multiplucation_2 = new Dictionary<int, double>()
        {
            //мишки красные
            {5, 0.3},
            {6, 0.4},
            {7, 0.5},
            {8, 0.75},
            {9, 1.00},
            {10, 1.50},
            {11, 2.50},
            {12, 3.50},
            {13, 8.00},
            {14, 15.00},
            {15, 30.00},
        };

        public static readonly Dictionary<int, double> array_symbols_dic_multiplucation_1 = new Dictionary<int, double>()
        {
            //мишки фиол
            {5, 0.25},
            {6, 0.30},
            {7, 0.40},
            {8, 0.50},
            {9, 0.75},
            {10, 1.25},
            {11, 2.00},
            {12, 3.00},
            {13, 6.00},
            {14, 12.00},
            {15, 25.00},
        };

        public static readonly Dictionary<int, double> array_symbols_dic_multiplucation_0 = new Dictionary<int, double>()
        {
            //мишки желт
            {5, 0.20},
            {6, 0.25},
            {7, 0.30},
            {8, 0.40},
            {9, 0.50},
            {10, 1.00},
            {11, 1.50},
            {12, 2.50},
            {13, 5.00},
            {14, 10.00},
            {15, 20.00},
        };
    }
}