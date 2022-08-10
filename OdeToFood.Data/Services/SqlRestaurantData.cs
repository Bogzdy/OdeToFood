﻿using OdeToFood.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdeToFood.Data.Services
{
    public class SqlRestaurantData : IRestaurantData
    {
        private readonly OdeToFoodDbContext db;

        public SqlRestaurantData(OdeToFoodDbContext db)
        {
            this.db = db;
        }
        public void Add(Restaurant restaurant)
        {
            db.Restaurants.Add(restaurant);
            db.SaveChanges(); 
        }

        public void Delete(int id)
        {
            Debug.WriteLine("SqlRestaurantData: Delete() called");
            try
            {
                var restaurantToDelete = Get(id);
                db.Restaurants.Remove(restaurantToDelete);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.GetType());
            }
        }

        public Restaurant Get(int id)
        {
            return db.Restaurants.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return db.Restaurants.OrderBy(r => r.Id);
        }

        public void Update(Restaurant restaurant)
        {
            var entry = db.Entry(restaurant);
            entry.State = EntityState.Modified;
            db.SaveChanges();
           
        }
    }
}
