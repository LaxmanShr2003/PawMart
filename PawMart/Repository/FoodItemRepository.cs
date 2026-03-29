using FoodyMan.Models;
using FoodyMan.Utility;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace FoodyMan.Repository
    {
        public class FoodItemRepository
        {
            private readonly string connectionString;

            public FoodItemRepository()
            {
                connectionString = ConfigurationManager.ConnectionStrings["FoodyLocalDBConnectionString"].ConnectionString;
            }

            // Get all food items
            public List<FoodItem> GetAllFoodItems()
            {
                List<FoodItem> foodItems = new List<FoodItem>();
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand("SELECT * FROM FoodItem ORDER BY FoodItemID DESC", connection);
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                FoodItem foodItem = new FoodItem
                                {
                                    FoodItemID = Convert.ToInt32(reader["FoodItemID"]),
                                    Name = reader["Name"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    Price = Convert.ToDecimal(reader["Price"]),
                                    DiscountPrice = Convert.ToDecimal(reader["DiscountPrice"]),
                                    ImageURL = reader["ImageURL"].ToString(),
                                    CategoryID = Convert.ToInt32(reader["CategoryID"]),
                                    IsAvailable = Convert.ToBoolean(reader["IsAvailable"]),
                                    IsFeatured = Convert.ToBoolean(reader["IsFeatured"]),
                                    CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                                    UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"])
                                };
                                foodItems.Add(foodItem);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error retrieving all food items: " + ex.Message);
                }
                return foodItems;
            }

            // Add a new food item
            public bool AddFoodItem(FoodItem foodItem)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(
                            "INSERT INTO FoodItem (FoodItemID, Name, Description, Price, DiscountPrice, ImageURL, CategoryID," +
                            " IsAvailable, IsFeatured, CreatedAt, UpdatedAt) " +
                            "VALUES (@FoodItemID, @Name, @Description, @Price, @DiscountPrice, @ImageURL, @CategoryID," +
                            " @IsAvailable, @IsFeatured, @CreatedAt, @UpdatedAt)", connection);

                        command.Parameters.AddWithValue("@FoodItemID", IdGenerator.GenerateFoodItemId()); 
                        command.Parameters.AddWithValue("@Name", foodItem.Name);
                        command.Parameters.AddWithValue("@Description", foodItem.Description);
                        command.Parameters.AddWithValue("@Price", foodItem.Price);
                        command.Parameters.AddWithValue("@DiscountPrice", foodItem.DiscountPrice);
                        command.Parameters.AddWithValue("@ImageURL", foodItem.ImageURL);
                        command.Parameters.AddWithValue("@CategoryID", foodItem.CategoryID);
                        command.Parameters.AddWithValue("@IsAvailable", foodItem.IsAvailable);
                        command.Parameters.AddWithValue("@IsFeatured", foodItem.IsFeatured);
                        command.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                        command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error adding food item: " + ex.Message);
                    throw;
                }
            }

            // Get a food item by ID
            public FoodItem GetFoodItemById(int foodItemId)
            {
                FoodItem foodItem = null;
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand("SELECT * FROM FoodItem WHERE FoodItemID = @FoodItemID", connection);
                        command.Parameters.AddWithValue("@FoodItemID", foodItemId);
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                foodItem = new FoodItem
                                {
                                    FoodItemID = Convert.ToInt32(reader["FoodItemID"]),
                                    Name = reader["Name"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    Price = Convert.ToDecimal(reader["Price"]),
                                    DiscountPrice = Convert.ToDecimal(reader["DiscountPrice"]),
                                    ImageURL = reader["ImageURL"].ToString(),
                                    CategoryID = Convert.ToInt32(reader["CategoryID"]),
                                    IsAvailable = Convert.ToBoolean(reader["IsAvailable"]),
                                    IsFeatured = Convert.ToBoolean(reader["IsFeatured"]),
                                    CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                                    UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"])
                                };
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error retrieving food item with ID {foodItemId}: " + ex.Message);
                }
                return foodItem;
            }

            // Update a food item
            public bool UpdateFoodItem(FoodItem foodItem)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(
                            "UPDATE FoodItem SET Name = @Name, Description = @Description, Price = @Price," +
                            " DiscountPrice = @DiscountPrice, " +
                            "ImageURL = @ImageURL, CategoryID = @CategoryID, IsAvailable = @IsAvailable, IsFeatured = @IsFeatured, UpdatedAt = @UpdatedAt " +
                            "WHERE FoodItemID = @FoodItemID", connection);

                        command.Parameters.AddWithValue("@Name", foodItem.Name);
                        command.Parameters.AddWithValue("@Description", foodItem.Description);
                        command.Parameters.AddWithValue("@Price", foodItem.Price);
                        command.Parameters.AddWithValue("@DiscountPrice", foodItem.DiscountPrice);
                        command.Parameters.AddWithValue("@ImageURL", foodItem.ImageURL);
                        command.Parameters.AddWithValue("@CategoryID", foodItem.CategoryID);
                        command.Parameters.AddWithValue("@IsAvailable", foodItem.IsAvailable);
                        command.Parameters.AddWithValue("@IsFeatured", foodItem.IsFeatured);
                        command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
                        command.Parameters.AddWithValue("@FoodItemID", foodItem.FoodItemID);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error updating food item: " + ex.Message);
                    throw;
                }
            }

            // Delete a food item by ID
            public bool DeleteFoodItem(int foodItemId)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand("DELETE FROM FoodItem WHERE FoodItemID = @FoodItemID", connection);
                        command.Parameters.AddWithValue("@FoodItemID", foodItemId);
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error deleting food item: " + ex.Message);
                    throw;
                }
            }

            // Check if a food item exists by name
            public bool IsFoodItemExistsByName(string name)
            {
                bool flag = false;
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand("SELECT * FROM FoodItem WHERE Name = @Name", connection);
                        command.Parameters.AddWithValue("@Name", name);
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                flag = true;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error checking if food item exists with name {name}: " + ex.Message);
                }
                return flag;
            }
        }
    }

