using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Task6
{
    class WeatherControl : DependencyObject
    {
        public static readonly DependencyProperty TempProperty; //объявляем свойство зависимости
        private string windDirection;
        private int windSpeed;

        private enum Precipitation { Sun = 0, Cloudy = 1, Rain = 3, Snow = 4 };

        public string WindDirection
        {
            get => windDirection;
            set => windDirection = value;
        }

        public int WindSpeed
        {
            get => windSpeed;
            set => windSpeed = value;
        }

        public int Temp //в свойстве обращаемся к методам GetValue и SetValue, в качестве аргументов
        { //передаем свойство зависимости TempProperty
            get => (int)GetValue(TempProperty);
            set => SetValue(TempProperty, value);
        }

        public WeatherControl(string windDirection, int windSpeed, int temp)
        {
            this.WindDirection = windDirection;
            this.WindSpeed = windSpeed;
            this.Temp = temp;
        }
        static WeatherControl() //статический конструктор для регистрации TempProperty
        {
            TempProperty = DependencyProperty.Register (
                nameof(Temp), //имя
                typeof(int), //тип данных
                typeof(WeatherControl), //тип кому принадлежит (класс WeatherControl)
                new FrameworkPropertyMetadata( //
                    0, //значение по умолчанию
                    FrameworkPropertyMetadataOptions.AffectsMeasure | //флаги
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    null, //действия при изменении
                    new CoerceValueCallback(CoerceAge)), //коррекция введенного значения (создаем экземпляр делегата
                                                         // CoerceValueCallback и передаем в качестве аргумента название метода
                new ValidateValueCallback(ValidateAge)); //пятый, необязательный аргумент в методе Register, а 
                                                         //именно экземпляр делегата ValidateValueCallback, в качестве аргумента метода, который будет
                                                         //проводить валидацию ValidateAge
        }
        private static bool ValidateAge(object value)
        {
            int v = (int)value;
            if (v >= -50 && v <= 50)
            {
                return true;
            }
            else
                return false;
        }
        private static object CoerceAge(DependencyObject d, object baseValue)
        {
            int v = (int)baseValue;
            if (v >= -50 && v <= 50)
            {
                return v;
            }
            else
                return 0;
        }
    }
}
