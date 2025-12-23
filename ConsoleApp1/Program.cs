using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("admin123  => " + BCrypt.Net.BCrypt.HashPassword("admin123"));
        Console.WriteLine("gestor123 => " + BCrypt.Net.BCrypt.HashPassword("gestor123"));
        Console.WriteLine("morador123 => " + BCrypt.Net.BCrypt.HashPassword("morador123"));
        Console.ReadLine();
    }
}

