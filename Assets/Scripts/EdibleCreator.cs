using System;
using System.Collections.Generic;
class EdibleCreator
{
    IEdible Tomato, Onion, Potato, Pumpkin, Mushroom, Lettuce, Leek, Peas, Eggplant, Cucumber, Corn, Cauliflower, Carrot, Capsicum, Broccoli;
    IEdible Apple, Banana, Grape, Lemon, Mango, Orange, Pineapple, Strawberry, Watermelon;

    Random rnd = new Random();
    readonly Dictionary<string, Type> edibleObject = new Dictionary<string, Type>()
    {
        { "Tomato", typeof(Tomato) },
        { "Onion", typeof(Onion) },
        { "Potato", typeof(Potato) },
        { "Pumpkin", typeof(Pumpkin) },
        { "Mushroom", typeof(Mushroom) },
        { "Lettuce", typeof(Lettuce) },
        { "Leek", typeof(Leek) },
        { "Peas", typeof(Peas) },
        { "Eggplant", typeof(Eggplant) },
        { "Cucumber", typeof(Cucumber) },
        { "Corn", typeof(Corn) },
        { "Cauliflower", typeof(Cauliflower) },
        { "Carrot", typeof(Carrot) },
        { "Capsicum", typeof(Capsicum) },
        { "Broccoli", typeof(Broccoli) },

        { "Apple", typeof(Apple) },
        { "Banana", typeof(Banana) },
        { "Grape", typeof(Grape) },
        { "Lemon", typeof(Lemon) },
        { "Mango", typeof(Mango) },
        { "Orange", typeof(Orange) },
        { "Pineapple", typeof(Pineapple) },
        { "Strawberry", typeof(Strawberry) },
        { "Watermelon", typeof(Watermelon) }
    };

    public IEdible GetRandomVeggie(bool isDraggable)
    {
        switch (rnd.Next(1, 15))
        {
            case 1:
                Tomato = new Tomato(isDraggable);
                return Tomato;

            case 2:
                Onion = new Onion(isDraggable);
                return Onion;

            case 3:
                Potato = new Potato(isDraggable);
                return Potato;

            case 4:
                Pumpkin = new Pumpkin(isDraggable);
                return Pumpkin;

            case 5:
                Mushroom = new Mushroom(isDraggable);
                return Mushroom;

            case 6:
                Lettuce = new Lettuce(isDraggable);
                return Lettuce;

            case 7:
                Leek = new Leek(isDraggable);
                return Leek;

            case 8:
                Peas = new Peas(isDraggable);
                return Peas;

            case 9:
                Eggplant = new Eggplant(isDraggable);
                return Eggplant;

            case 10:
                Cucumber = new Cucumber(isDraggable);
                return Cucumber;

            case 11:
                Corn = new Corn(isDraggable);
                return Corn;

            case 12:
                Cauliflower = new Cauliflower(isDraggable);
                return Cauliflower;

            case 13:
                Carrot = new Carrot(isDraggable);
                return Carrot;

            case 14:
                Capsicum = new Capsicum(isDraggable);
                return Capsicum;

            case 15:
                Broccoli = new Broccoli(isDraggable);
                return Broccoli;

            default:
                Tomato = new Tomato(isDraggable);
                return Tomato;
        }
    }
    public IEdible GetRandomEdible(bool isDraggable)
    {
        switch (rnd.Next(1, 24))
        {
            case 1:
                Tomato = new Tomato(isDraggable);
                return Tomato;

            case 2:
                Onion = new Onion(isDraggable);
                return Onion;

            case 3:
                Potato = new Potato(isDraggable);
                return Potato;

            case 4:
                Pumpkin = new Pumpkin(isDraggable);
                return Pumpkin;

            case 5:
                Mushroom = new Mushroom(isDraggable);
                return Mushroom;

            case 6:
                Lettuce = new Lettuce(isDraggable);
                return Lettuce;

            case 7:
                Leek = new Leek(isDraggable);
                return Leek;

            case 8:
                Peas = new Peas(isDraggable);
                return Peas;

            case 9:
                Eggplant = new Eggplant(isDraggable);
                return Eggplant;

            case 10:
                Cucumber = new Cucumber(isDraggable);
                return Cucumber;

            case 11:
                Corn = new Corn(isDraggable);
                return Corn;

            case 12:
                Cauliflower = new Cauliflower(isDraggable);
                return Cauliflower;

            case 13:
                Carrot = new Carrot(isDraggable);
                return Carrot;

            case 14:
                Capsicum = new Capsicum(isDraggable);
                return Capsicum;

            case 15:
                Broccoli = new Broccoli(isDraggable);
                return Broccoli;






            case 16:
                Apple = new Apple(isDraggable);
                return Apple;

            case 17:
                Banana = new Banana(isDraggable);
                return Banana;

            case 18:
                Grape = new Grape(isDraggable);
                return Grape;

            case 19:
                Lemon = new Lemon(isDraggable);
                return Lemon;

            case 20:
                Mango = new Mango(isDraggable);
                return Mango;

            case 21:
                Orange = new Orange(isDraggable);
                return Orange;

            case 22:
                Pineapple = new Pineapple(isDraggable);
                return Pineapple;

            case 23:
                Strawberry = new Strawberry(isDraggable);
                return Strawberry;

            case 24:
                Watermelon = new Watermelon(isDraggable);
                return Watermelon;
            default:
                Tomato = new Tomato(isDraggable);
                return Tomato;
        }
    }
    public IEdible GetRandomFruit(bool isDraggable)
    {
        switch (rnd.Next(1, 9))
        {
            case 1:
                Apple = new Apple(isDraggable);
                return Apple;

            case 2:
                Banana = new Banana(isDraggable);
                return Banana;

            case 3:
                Grape = new Grape(isDraggable);
                return Grape;

            case 4:
                Lemon = new Lemon(isDraggable);
                return Lemon;

            case 5:
                Mango = new Mango(isDraggable);
                return Mango;

            case 6:
                Orange = new Orange(isDraggable);
                return Orange;

            case 7:
                Pineapple = new Pineapple(isDraggable);
                return Pineapple;

            case 8:
                Strawberry = new Strawberry(isDraggable);
                return Strawberry;

            case 9:
                Watermelon = new Watermelon(isDraggable);
                return Watermelon;

            default:
                Apple = new Apple(isDraggable);
                return Apple;
        }
    }
    public List<IEdible> GetAllEdible()
    {
        List<IEdible> allEdibles = new List<IEdible>();
        Apple = new Apple();
        allEdibles.Add(Apple);
        
        Banana = new Banana();
        allEdibles.Add(Banana);
        
        Grape = new Grape();
        allEdibles.Add(Grape);
    
        Lemon = new Lemon();
        allEdibles.Add(Lemon);
        
        Mango = new Mango();
        allEdibles.Add(Mango);
        
        Orange = new Orange();
        allEdibles.Add(Orange);
        
        Pineapple = new Pineapple();
        allEdibles.Add(Pineapple);
        
        Strawberry = new Strawberry();
        allEdibles.Add(Strawberry);
        
        Watermelon = new Watermelon();
        allEdibles.Add(Watermelon);


        Tomato = new Tomato();
        allEdibles.Add(Tomato);

        Onion = new Onion();
        allEdibles.Add(Onion);

        Potato = new Potato();
        allEdibles.Add(Potato);

        Pumpkin = new Pumpkin();
        allEdibles.Add(Pumpkin);

        Lettuce = new Lettuce();
        allEdibles.Add(Lettuce);

        Leek = new Leek();
        allEdibles.Add(Leek);

        Peas = new Peas();
        allEdibles.Add(Peas);

        Eggplant = new Eggplant();
        allEdibles.Add(Eggplant);

        Cucumber = new Cucumber();
        allEdibles.Add(Cucumber);

        Corn = new Corn();
        allEdibles.Add(Corn);

        Cauliflower = new Cauliflower();
        allEdibles.Add(Cauliflower);

        Carrot = new Carrot();
        allEdibles.Add(Carrot);

        Capsicum = new Capsicum();
        allEdibles.Add(Capsicum);

        Broccoli = new Broccoli();
        allEdibles.Add(Broccoli);

        return allEdibles;
    }
    public IEdible GetEdible(string name, bool isDraggable=false)
    {
        switch (name)
        {
            case "Apple":
                Apple = new Apple(isDraggable);
                return Apple;

            case "Banana":
                Banana = new Banana(isDraggable);
                return Banana;

            case "Grape":
                Grape = new Grape(isDraggable);
                return Grape;

            case "Lemon":
                Lemon = new Lemon(isDraggable);
                return Lemon;

            case "Mango":
                Mango = new Mango(isDraggable);
                return Mango;

            case "Orange":
                Orange = new Orange(isDraggable);
                return Orange;

            case "Pineapple":
                Pineapple = new Pineapple(isDraggable);
                return Pineapple;

            case "Strawberry":
                Strawberry = new Strawberry(isDraggable);
                return Strawberry;

            case "Watermelon":
                Watermelon = new Watermelon(isDraggable);
                return Watermelon;




            case "Tomato":
                Tomato = new Tomato(isDraggable);
                return Tomato;

                case "Onion":
                    Onion = new Onion(isDraggable);
                return Onion;

            case "Potato":
                Potato = new Potato(isDraggable);
                return Potato;

            case "Pumpkin":
                Pumpkin = new Pumpkin(isDraggable);
                return Pumpkin;

            case "Mushroom":
                Mushroom = new Mushroom(isDraggable);
                return Mushroom;

            case "Lettuce":
                Lettuce = new Lettuce(isDraggable);
                return Lettuce;

            case "Leek":
                Leek = new Leek(isDraggable);
                return Leek;

            case "Peas":
                Peas = new Peas(isDraggable);
                return Peas;

            case "Eggplant":
                Eggplant = new Eggplant(isDraggable);
                return Eggplant;

            case "Cucumber":
                Cucumber = new Cucumber(isDraggable);
                return Cucumber;

            case "Corn":
                Corn = new Corn(isDraggable);
                return Corn;

            case "Cauliflower":
                Cauliflower = new Cauliflower(isDraggable);
                return Cauliflower;

            case "Carrot":
                Carrot = new Carrot(isDraggable);
                return Carrot;

            case "Capsicum":
                Capsicum = new Capsicum(isDraggable);
                return Capsicum;

            case "Broccoli":
                Broccoli = new Broccoli(isDraggable);
                return Broccoli;

            default:
                Apple = new Apple(isDraggable);
                return Apple;
        }
    }
}