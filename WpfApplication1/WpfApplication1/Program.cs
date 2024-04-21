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

    public void poletela() {
        
        //double height = 15, distance = 100;
        
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
            
            x = start_v * Math.Cos(a) * t + 9.8 * t * t / 2;
            y =  start_v * Math.Sin(a) * t; 
            if (y < 0)
                break;
            if (y < h && x > stena_x && x < (stena_x + w))
                Collision += Ops_Collision;
            
            /*Console.WriteLine($" X : {x}; Y : {y} ");
            alfa -= t;
            */
        }
   }
   
   public void Ops_Collision(object sender, EventArgs e) 
        {
            Window win = new Window();
            win.Title = "Ooooooops! Collision!";
            win.ShowDialog(); 
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
        qestion.Content = "Нажми меня!";  //свойству Content объекта Button задается текстовая строка
        qestion.Click += ButtonOnClick;
        Content = qestion; //сам объект Button задаётся свойству Content объекта Window
    }
    void ButtonOnClick(object sender, RoutedEventArgs args)
    {
        letychaya_fignya fruit = new letychaya_fignya();

        fruit.poletela();
    }
}
