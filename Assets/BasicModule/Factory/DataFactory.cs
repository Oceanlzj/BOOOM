﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.BasicModule.Model;
using Assets.BasicModule.Utility;
using UnityEngine;

namespace Assets.BasicModule.Factory
{
  public class DataFactory
  {
    private static DataFactory _DataFactory;

    private List<Ingredient> Ingredients;
    private List<Dish> Dishes;
    private List<Worker> Workers;
    private List<Model.Request> Tasks;
    private List<Recipe> RecipeFile;
    private Dictionary<(int, int), int> Recipes;
    private List<GameEventManager.GameEvent> GameEvents;
    private List<EventWorkerLine> EventWorkerLines;

    private DataFactory()
    {
      try
      {
        Ingredients = JsonReader<List<Ingredient>>.Read("Data\\Ingredients.json");
        Dishes = JsonReader<List<Dish>>.Read("Data\\Dishes.json");
        Workers = JsonReader<List<Worker>>.Read("Data\\Workers.json");
        Tasks = JsonReader<List<Model.Request>>.Read("Data\\Tasks.json");
        RecipeFile = JsonReader<List<Recipe>>.Read("Data\\Recipes.json");
        GameEvents = JsonReader<List<GameEventManager.GameEvent>>.Read("Data\\Events.json");
        EventWorkerLines = JsonReader<List<EventWorkerLine>>.Read("Data\\EventLineBinds.json");

        Recipes = new Dictionary<(int, int), int>();
        foreach (var rec in RecipeFile)
        {
          //smaller id first
          (int, int) Key = rec.Ingredient_ID1 < rec.Ingredient_ID2 ? (rec.Ingredient_ID1, rec.Ingredient_ID2) : (rec.Ingredient_ID2, rec.Ingredient_ID1);
          Recipes.Add(Key, rec.Dish_ID);
        }
        return;
      }
      catch
      {

      }
    }

    //return dish object with ID
    public Dish GetDishByID(int ID)
    {
      return Dishes.Find(x => x.ID == ID);
    }

    //Retuen Dish object when combine 2 Ingredient
    public Dish GetDishByRecipe(int Ingredient_ID1, int Ingredient_ID2)
    {
      (int, int) key = Ingredient_ID1 < Ingredient_ID2 ? (Ingredient_ID1, Ingredient_ID2) : (Ingredient_ID2, Ingredient_ID1);
      return Dishes.Find(x => x.ID == Recipes[key]);
    }


    public Worker GetWorkerByID(int ID)
    {
      return Workers.Find(x => x.ID == ID);
    }

    public Model.Request getTaskByID(int ID)
    {
      return Tasks.Find(x => x.ID == ID);
    }

    public Request getInitTaskByWorkerID(int ID)
    {
      return Tasks.Find(x => x.WorkerID == ID);
    }

    public Ingredient GetIngedientByID(int ID)
    {
      return Ingredients.Find(x => x.ID == ID);
    }

    public int GetIngredientCount()
    {
      return Ingredients.Count();
    }

    public static DataFactory Instance()
    {
      if (_DataFactory == null)
      {
        _DataFactory = new DataFactory();
      }
      return _DataFactory;
    }

    public List<Worker> GetWorksers()
    {
      return _DataFactory.Workers;
    }

    public SpecialWorker GetSpecialWorker(int ID)
    {
      SpecialWorker worker = new SpecialWorker(GetWorkerByID(ID));
      Request request = Tasks.Find(x => x.WorkerID == ID);
      worker.BeginingTask = request;
      worker.CurrentTask = request;

      return worker;
    }

    public List<GameEventManager.GameEvent> GetGameEvents()
    {
      return _DataFactory.GameEvents;
    }

  }
}
