using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;


struct Pair
{
    public double x;
    public double y;

    public Pair(double x0, double y0)
    {
        x = x0;
        y = y0;
    }
    public void print()
    {
        Console.WriteLine(x + " " + y);
    }
}

class letychaya_fignya {
        
    public double start_v; //начальное ускорение
    public double alfa; //угол полёта. в градусах
    
    private double x,y;

    public Pair [] coordinates = new Pair[200]; //для canvas
    public double stena_x, h, w;

    public delegate void MyEventHandler(object sender, EventArgs e);

    public event MyEventHandler Collision;

    public event MyEventHandler Earth;

    public void poletela(double start_v, double alfa) {
        

        Random rnd = new Random();
        stena_x = rnd.Next(0,100);
        h = rnd.Next(0, 70);
        w = rnd.Next(0, 20);
        

        for (double t = 0; t < 20  ; t += 0.1){

            double a = alfa* (Math.PI /180); // теперь в радианах 
            
            x = 5 + start_v * Math.Cos(a) * t + 9.8 * t * t / 2;//вычисляем координаты х, у
            y = 5 + start_v * Math.Sin(a) * t;

            coordinates[Convert.ToInt16(t * 10)] = new Pair(x, y);

            //Console.WriteLine("X : {0}  Y : {1} ", x, y); кусок кода для проверки, не убирать
            alfa -= t;
            
            if (y <= 0)
            {
                Earth(this, new EventArgs() ); //вызываем событие, передаём ему аргументы
                return;
            }
            if (y <= h && x > stena_x && x < (stena_x + w))
            {
                Collision(this, new EventArgs()); //вызываем событие, передаём ему аргументы
                return;
            }
        }
   }
    
}



public class Program : Window
{
    Grid grid;
    TextBox velocity;
    TextBox corner;
    Canvas picture;
    letychaya_fignya fruit;

    [STAThread]

    public static void Main()
    {
        Application app = new Application();
        app.Run(new Program());
    }
    public Program()
    {
        Title = "Игрушка"; //название окна
        
        //Content = qestion; //сам объект Button задаётся свойству Content объекта Window

        grid = new Grid(); //создаём грид
        //grid.HorizontalAlignment = HorizontalAlignment.Left;
        //grid.VerticalAlignment = VerticalAlignment.Top;
        
        grid.ShowGridLines = true;

        //создаём столбцы
        ColumnDefinition colDef1 = new ColumnDefinition();
        ColumnDefinition colDef2 = new ColumnDefinition();

        //добавляем столбцы
        grid.ColumnDefinitions.Add(colDef1);
        grid.ColumnDefinitions.Add(colDef2);

        //создаём строки
        RowDefinition rowDef1 = new RowDefinition();
        RowDefinition rowDef2 = new RowDefinition();
        RowDefinition rowDef3 = new RowDefinition();

        //добавляем строки
        grid.RowDefinitions.Add(rowDef1);
        grid.RowDefinitions.Add(rowDef2);
        grid.RowDefinitions.Add(rowDef3);

        //создаём, добавляем канвас с анимацией
        picture = new Canvas();
        Grid.SetRow(picture, 0);
        Grid.SetColumn(picture, 1);

        //создаём кнопку, добавляем в грид
        Button qestion = new Button();
        qestion.Content = "Запуск?";  //свойству Content объекта Button задается текстовая строка
        qestion.Click += ButtonOnClick; //задаём обработчик события
        Grid.SetRow(qestion, 2);
        Grid.SetColumn(qestion, 1);

        grid.Children.Add(picture);
        grid.Children.Add(qestion);

        //создаём текстовое окно для ввода данных
        TextBlock message = new TextBlock();
        message.Text = "Введите начальную скорость и угол: ";
        Grid.SetRow(message, 0);
        Grid.SetColumn(message, 0);

        velocity = new TextBox();
        velocity.Text = "0";
        Grid.SetRow(velocity, 1);
        Grid.SetColumn(velocity, 0);

        corner = new TextBox();
        corner.Text = "0";
        Grid.SetRow(corner, 2);
        Grid.SetColumn(corner, 0);

        grid.Children.Add(velocity);
        grid.Children.Add(corner);
        grid.Children.Add(message);

        Content = grid;// делаем грид содержимым окна
    }

    public void Ops_Collision(object sender, EventArgs e)//обработчик события "столкновение с препятствием"
    {
        Canvas fignya = new Canvas();
        fignya.Height = 100;
        fignya.Width = 100;

        Canvas stena = new Canvas();
        stena.Height = fruit.h;
        stena.Width = fruit.w;

        Console.WriteLine("Ooooooops! Collision!");
        MessageBox.Show("Ooooooops! Collision!");
    }

    public void Ops_Earth(object sender, EventArgs e)//обработчик события "падение на землю"
    {
        Console.WriteLine("There in no collision!");
        MessageBox.Show("There in no collision!");
    }

    void ButtonOnClick(object sender, RoutedEventArgs args) //обработчик события нажатие на кнопку, создаёт объект класса "фигня"
    {
        

        double start_v = Convert.ToDouble(velocity.Text);
        double alfa = Convert.ToDouble(corner.Text);
        fruit = new letychaya_fignya();
        fruit.Earth += Ops_Earth;//назначаем обработчики события
        fruit.Collision += Ops_Collision;
        fruit.poletela(start_v,alfa);//запуск!
    }
}
