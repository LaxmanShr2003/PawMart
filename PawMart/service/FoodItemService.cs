

using FoodyMan.Models;
using FoodyMan.Repository;
using System;
using System.Collections.Generic;

namespace FoodyMan.service
{
    public class FoodItemService
    {
        private readonly FoodItemRepository _foodItemRepository;

        public FoodItemService()
        {
            _foodItemRepository = new FoodItemRepository();
        }

        // Get all food items
        public List<FoodItem> GetAllFoodItems()
        {
            try
            {
                return _foodItemRepository.GetAllFoodItems();
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework like NLog, Serilog, etc.)
                Console.WriteLine("Error retrieving food items: " + ex.Message);
                throw; // Re-throw the exception for the caller to handle
            }
        }

        // Add a new food item
        public bool AddFoodItem(FoodItem foodItem)
        {
            try
            {
                // Check if a food item with the same name already exists
                if (_foodItemRepository.IsFoodItemExistsByName(foodItem.Name))
                {
                    throw new InvalidOperationException("A food item with the same name already exists.");
                }

                return _foodItemRepository.AddFoodItem(foodItem);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine("Error adding food item: " + ex.Message);
                throw; // Re-throw the exception for the caller to handle
            }
        }

        // Get a food item by ID
        public FoodItem GetFoodItemById(int foodItemId)
        {
            try
            {
                var foodItem = _foodItemRepository.GetFoodItemById(foodItemId);

                if (foodItem == null)
                {
                    throw new KeyNotFoundException($"Food item with ID '{foodItemId}' not found.");
                }
                return foodItem;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine("Error retrieving food item by ID: " + ex.Message);
                throw; // Re-throw the exception for the caller to handle
            }
        }

        public FoodItem GetFoodItemByCategoryId(int categoryId)
        {
            try
            {
                var foodItem = _foodItemRepository.GetFoodItemById(categoryId);

                if (foodItem == null)
                {
                    throw new KeyNotFoundException($"Food item with ID '{categoryId}' not found.");
                }
                return foodItem;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine("Error retrieving food item by ID: " + ex.Message);
                throw; // Re-throw the exception for the caller to handle
            }
        }

        // Update an existing food item
        public bool UpdateFoodItem(FoodItem foodItem)
        {
            try
            {
                // Check if the food item exists
                var existingFoodItem = _foodItemRepository.GetFoodItemById(foodItem.FoodItemID);
                if (existingFoodItem == null)
                {
                    throw new KeyNotFoundException($"Food item with ID '{foodItem.FoodItemID}' not found.");
                }

                // Check if the updated name conflicts with another food item
                if (_foodItemRepository.IsFoodItemExistsByName(foodItem.Name) && existingFoodItem.Name != foodItem.Name)
                {
                    throw new InvalidOperationException("A food item with the same name already exists.");
                }

                // Update the food item
                return _foodItemRepository.UpdateFoodItem(foodItem);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine("Error updating food item: " + ex.Message);
                throw; // Re-throw the exception for the caller to handle
            }
        }

        // Delete a food item by ID
        public bool DeleteFoodItem(int foodItemId)
        {
            try
            {
                // Check if the food item exists
                var foodItem = _foodItemRepository.GetFoodItemById(foodItemId);
                if (foodItem == null)
                {
                    throw new KeyNotFoundException($"Food item with ID '{foodItemId}' not found.");
                }

                // Delete the food item
                return _foodItemRepository.DeleteFoodItem(foodItemId);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine("Error deleting food item: " + ex.Message);
                throw; // Re-throw the exception for the caller to handle
            }
        }

        // Check if a food item exists by name
        public bool IsFoodItemExistsByName(string name)
        {
            try
            {
                return _foodItemRepository.IsFoodItemExistsByName(name);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine("Error checking if food item exists by name: " + ex.Message);
                throw; // Re-throw the exception for the caller to handle
            }
        }
    }
}