
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using FoodyMan.Models;
using FoodyMan.Utility;
using MySql.Data.MySqlClient;

namespace FoodyMan.Repositories
{
    public class CartRepository
    {
        private string connectionString;

        public CartRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["FoodyLocalDBConnectionString"].ConnectionString;
        }

        public Cart CreateCart(int userID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Check if cart already exists for user
                string checkQuery = "SELECT CartID FROM Cart WHERE UserID = @UserID";
                using (SqlCommand checkCmd = new SqlCommand(checkQuery, connection))
                {
                    checkCmd.Parameters.AddWithValue("@UserID", userID);
                    var existingCart = checkCmd.ExecuteScalar();

                    if (existingCart != null)
                    {
                        return GetCartByUserID(userID);
                    }
                }

                // Create new cart
                string query = @"
                    INSERT INTO Cart (CartID,UserID, CreatedAt, UpdatedAt) 
                    VALUES (@CartID,@UserID, @CreatedAt, @UpdatedAt);
                    ";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@CartID", IdGenerator.GenerateFoodItemId());
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                    cmd.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);

                    int cartID = Convert.ToInt32(cmd.ExecuteScalar());

                    return new Cart
                    {
                        CartID = cartID,
                        UserID = userID,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };
                }
            }
        }

        public Cart GetCartByUserID(int userID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Cart WHERE UserID = @UserID";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@UserID", userID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Cart
                            {
                                CartID = reader.GetInt32(reader.GetOrdinal("CartID")),
                                UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                                CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                                UpdatedAt = reader.GetDateTime(reader.GetOrdinal("UpdatedAt"))
                            };
                        }
                        return null;
                    }
                }
            }
        }

        public void AddItemToCart(CartItem cartItem)
        {

           
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Check if item already exists in cart
                string checkQuery = @"
                    SELECT Quantity FROM CartItem
                    WHERE CartID = @CartID AND FoodItemID = @FoodItemID";

                using (SqlCommand checkCmd = new SqlCommand(checkQuery, connection))
                {
                    checkCmd.Parameters.AddWithValue("@CartID", cartItem.CartID);
                    checkCmd.Parameters.AddWithValue("@FoodItemID", cartItem.FoodItemID);

                    var existingQuantity = checkCmd.ExecuteScalar();

                    if (existingQuantity != null)
                    {
                        // Update existing item quantity
                        string updateQuery = @"
                            UPDATE CartItem
                            SET Quantity = Quantity + @Quantity 
                            WHERE CartID = @CartID AND FoodItemID = @FoodItemID";

                        using (SqlCommand updateCmd = new SqlCommand(updateQuery, connection))
                        {
                            updateCmd.Parameters.AddWithValue("@CartID", cartItem.CartID);
                            updateCmd.Parameters.AddWithValue("@FoodItemID", cartItem.FoodItemID);
                            updateCmd.Parameters.AddWithValue("@Quantity", cartItem.Quantity);
                            updateCmd.ExecuteNonQuery();
                        }
                        
                    }
                    else
                    {
                        // Insert new cart item
                        string insertQuery = @"
                            INSERT INTO CartItem (CartItemID,CartID, FoodItemID, Quantity, AddedAt) 
                            VALUES (@CartItemID,@CartID, @FoodItemID, @Quantity, @AddedAt)";

                        using (SqlCommand insertCmd = new SqlCommand(insertQuery, connection))
                        {
                            insertCmd.Parameters.AddWithValue("@CartItemID", IdGenerator.GenerateCartItemID());
                            insertCmd.Parameters.AddWithValue("@CartID", cartItem.CartID);
                            insertCmd.Parameters.AddWithValue("@FoodItemID", cartItem.FoodItemID);
                            insertCmd.Parameters.AddWithValue("@Quantity", cartItem.Quantity);
                            insertCmd.Parameters.AddWithValue("@AddedAt", DateTime.Now);
                            insertCmd.ExecuteNonQuery();
                        }
                    }
                }

                // Update cart's UpdatedAt timestamp
                string updateCartQuery = @"
                    UPDATE Cart
                    SET UpdatedAt = @UpdatedAt 
                    WHERE CartID = @CartID";

                using (SqlCommand updateCartCmd = new SqlCommand(updateCartQuery, connection))
                {
                    updateCartCmd.Parameters.AddWithValue("@CartID", cartItem.CartID);
                    updateCartCmd.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
                    updateCartCmd.ExecuteNonQuery();
                }
            }
        }

        public List<CartItem> GetCartItems(int cartID)
        {
            List<CartItem> cartItems = new List<CartItem>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
                    SELECT * FROM CartItem
                    WHERE CartID = @CartID";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@CartID", cartID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cartItems.Add(new CartItem
                            {
                                CartItemID = reader.GetInt32(reader.GetOrdinal("CartItemID")),
                                CartID = reader.GetInt32(reader.GetOrdinal("CartID")),
                                FoodItemID = reader.GetInt32(reader.GetOrdinal("FoodItemID")),
                                Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
                                AddedAt = reader.GetDateTime(reader.GetOrdinal("AddedAt"))
                            });
                        }
                    }
                }
            }

            return cartItems;
        }
        public List<CartItem> GetCartItemsByCartItemID(int cartItemID)
        {
            List<CartItem> cartItems = new List<CartItem>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
                    SELECT * FROM CartItem
                    WHERE CartItemID = @CartItemID";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@CartItemID", cartItemID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cartItems.Add(new CartItem
                            {
                                CartItemID = reader.GetInt32(reader.GetOrdinal("CartItemID")),
                                CartID = reader.GetInt32(reader.GetOrdinal("CartID")),
                                FoodItemID = reader.GetInt32(reader.GetOrdinal("FoodItemID")),
                                Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
                                AddedAt = reader.GetDateTime(reader.GetOrdinal("AddedAt"))
                            });
                        }
                    }
                }
            }

            return cartItems;
        }

        public bool ClearCartItems(int cartId)
        {
         
      

            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("DELETE FROM CartItem WHERE CartID = @CartID", connection);
                    command.Parameters.AddWithValue("@CartID", cartId);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error deleting user with ID {cartId}: " + ex.Message);
                throw;
            }
        }



    }
}