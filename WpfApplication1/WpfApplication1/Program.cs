using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;


class letychaya_fignya {
        
    public double start_v; //начальное ускорение
    public double alfa; //угол полёта. в градусах
    
    private double x,y;

    public delegate void MyEventHandler(object sender, EventArgs e);

    public event MyEventHandler Collision;

    public event MyEventHandler Earth;

    public void poletela() {
        
        Console.WriteLine("Введите ускорение, угол фигни: ");
        start_v = Convert.ToDouble(Console.ReadLine());
        alfa  = Convert.ToDouble(Console.ReadLine());
        
        double stena_x, h, w;
        Console.WriteLine("Введите расстояние до стенки, высоту и ширину стенки: "); //создаём препятствие

        stena_x = Convert.ToDouble(Console.ReadLine());
        h = Convert.ToDouble(Console.ReadLine());
        w = Convert.ToDouble(Console.ReadLine());
        

        for (double t = 0; t < 20  ; t += 0.1){

            double a = alfa* (Math.PI /180); // теперь в радианах 
            
            x = 5 + start_v * Math.Cos(a) * t + 9.8 * t * t / 2;
            y = 5 + start_v * Math.Sin(a) * t;

            Console.WriteLine("X : {0}  Y : {1} ", x, y);
            alfa -= t;
            
            if (y <= 0)
            {
                Earth(this, new EventArgs() );
                return;
            }
            if (y <= h && x > stena_x && x < (stena_x + w))
            {
                Collision(this, new EventArgs());
                return;
            }
        }
   }
   
   
    
}

/*class Stenka{ //класс, в котором будет обработчик события
    
public

    double mainX, heightS, widthS;//расстояние до стенки, высота, ширина стенки соотв. стенка по умолчанию стоит на земле

     //конструктор
    public Stenka(double x, double h, double s){
        mainX = x;
        heightS = h;
        widthS = s;
    }

    void Collision(){//обработчик события
        Console.WriteLine("Ooooooooops! It was collision!");
    }

}*/

public class Program : Window
{
    [STAThread]
    public static void Main()
    {
        Application app = new Application();
        app.Run(new Program());
    }
    public Program()
    {
        Title = "Нажатие кнопки"; //название окна
        Button qestion = new Button(); //этим классом представлена кнопка со свойством Content и событием Click
        qestion.Content = "Запуск?";  //свойству Content объекта Button задается текстовая строка
        qestion.Click += ButtonOnClick;
        Content = qestion; //сам объект Button задаётся свойству Content объекта Window
    }

    public void Ops_Collision(object sender, EventArgs e)
    {
        Console.WriteLine("Ooooooops! Collision!");
        MessageBox.Show("Ooooooops! Collision!");
    }

    public void Ops_Earth(object sender, EventArgs e)
    {
        Console.WriteLine("There in no collision!");
        MessageBox.Show("There in no collision!");
    }

    void ButtonOnClick(object sender, RoutedEventArgs args)
    {
        letychaya_fignya fruit = new letychaya_fignya();
        fruit.Earth += Ops_Earth;
        fruit.Collision += Ops_Collision;
        fruit.poletela();
    }
}
