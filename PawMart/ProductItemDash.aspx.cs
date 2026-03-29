using PawMart.Models;
using PawMart.service;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PawMart
{
    public partial class FoodItemDash : System.Web.UI.Page
    {
        private readonly ProductService _productService;
        private readonly CategoryService _categoryService;

        public FoodItemDash()
        {
            _productService = new ProductService();
            _categoryService = new CategoryService();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadFoodItems();
                LoadCategories();
            }
        }

        private void LoadFoodItems()
        {
            // Bind the GridView with food items
            gvFoodItems.DataSource = _productService.GetAllFoodItems();
            gvFoodItems.DataBind();
        }

        private void LoadCategories()
        {
            // Bind the Category dropdowns
            ddlCategoryID.DataSource = _categoryService.GetAllCategories();
            ddlCategoryID.DataTextField = "Name";
            ddlCategoryID.DataValueField = "CategoryID";
            ddlCategoryID.DataBind();

            ddlEditCategoryID.DataSource = _categoryService.GetAllCategories();
            ddlEditCategoryID.DataTextField = "Name";
            ddlEditCategoryID.DataValueField = "CategoryID";
            ddlEditCategoryID.DataBind();
        }

        protected void btnAddFoodItem_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                try
                {

                    if (fileUploadImage.HasFile)
                    {
                        // Define the folder to save the uploaded image
                        string uploadFolder = Server.MapPath("~/Uploads/");

                        // Ensure the upload folder exists
                        if (!System.IO.Directory.Exists(uploadFolder))
                        {
                            System.IO.Directory.CreateDirectory(uploadFolder);
                        }

                        // Generate a unique file name to avoid conflicts
                        string fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(fileUploadImage.FileName);

                        // Save the file to the upload folder
                        string filePath = System.IO.Path.Combine(uploadFolder, fileName);
                     

                        Product newProduct = new Product
                        {

                            Name = txtName.Text.Trim(),
                            Description = txtDescription.Text.Trim(),
                            Price = Convert.ToDecimal(txtPrice.Text),
                            DiscountPrice = Convert.ToDecimal(txtDiscountPrice.Text),
                            ImageURL = "~/Uploads/" + fileName,
                            CategoryID = Convert.ToInt32(ddlCategoryID.SelectedValue),
                            IsAvailable = chkIsAvailable.Checked,
                            IsFeatured = chkIsFeatured.Checked,
                            CreatedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now
                        };

                        if (_productService.AddProductItem(newProduct))
                        {
                            fileUploadImage.SaveAs(filePath);
                            LoadFoodItems();
                            lblMessage.Text = "Product item added successfully!";
                            lblMessage.CssClass = "success-message";
                            ClearForm();

                            // Add JavaScript to close the modal after successful addition
                            ClientScript.RegisterStartupScript(this.GetType(), "closeModal", "closeAddModal();", true);
                        }
                        else
                        {
                            lblMessage.Text = "Failed to add product item. Please try again.";
                            lblMessage.CssClass = "error-message";
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "An error occurred: " + ex.Message;
                    lblMessage.CssClass = "error-message";
                }
            }
        }

        protected void btnUpdateFoodItem_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                try
                {
                    if (fileUploadImage.HasFile)
                    {
                        // Define the folder to save the uploaded image
                        string uploadFolder = Server.MapPath("~/Uploads/");

                        // Ensure the upload folder exists
                        if (!System.IO.Directory.Exists(uploadFolder))
                        {
                            System.IO.Directory.CreateDirectory(uploadFolder);
                        }

                        // Generate a unique file name to avoid conflicts
                        string fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(fileUploadImage.FileName);

                        // Save the file to the upload folder
                        string filePath = System.IO.Path.Combine(uploadFolder, fileName);
                        int productItemId = Convert.ToInt32(hdnFoodItemID.Value);
                    Product existingProduct = _productService.GetProductById(productItemId);

                        if (existingProduct != null)
                        {
                            existingProduct.Name = txtEditName.Text.Trim();
                            existingProduct.Description = txtEditDescription.Text.Trim();
                            existingProduct.Price = Convert.ToDecimal(txtEditPrice.Text);
                            existingProduct.DiscountPrice = Convert.ToDecimal(txtEditDiscountPrice.Text);
                            existingProduct.ImageURL = "~/Uploads/" + fileName;
                            existingProduct.CategoryID = Convert.ToInt32(ddlEditCategoryID.SelectedValue);
                            existingProduct.IsAvailable = chkEditIsAvailable.Checked;
                            existingProduct.IsFeatured = chkEditIsFeatured.Checked;
                            existingProduct.UpdatedAt = DateTime.Now;

                            if (_productService.UpdateProduct(existingProduct))
                            {
                                LoadFoodItems();
                                lblEditMessage.Text = "Product item updated successfully!";
                                lblEditMessage.CssClass = "success-message";

                                // Add JavaScript to close the modal after successful update
                                ClientScript.RegisterStartupScript(this.GetType(), "closeEditModal", "closeEditModal();", true);
                            }
                            else
                            {
                                lblEditMessage.Text = "Failed to update food item. Please try again.";
                                lblEditMessage.CssClass = "error-message";
                            }
                        }
                    }
                    else
                    {
                        lblEditMessage.Text = "Food item not found.";
                        lblEditMessage.CssClass = "error-message";
                    }
                }
                catch (Exception ex)
                {
                    lblEditMessage.Text = "An error occurred: " + ex.Message;
                    lblEditMessage.CssClass = "error-message";
                }
            }
        }

        protected void gvFoodItems_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteFoodItem")
            {
                try
                {
                    int productItemId = Convert.ToInt32(e.CommandArgument);
                    if (_productService.DeleteProduct(productItemId))
                    {
                        LoadFoodItems();
                        // Display a success message (optional)
                        ScriptManager.RegisterStartupScript(this, GetType(), "DeleteSuccess",
                            "alert('Product item deleted successfully.');", true);
                    }
                    else
                    {
                        // Display an error message
                        ScriptManager.RegisterStartupScript(this, GetType(), "DeleteError",
                            "alert('Failed to delete food item. Please try again.');", true);
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception and display an error message
                    ScriptManager.RegisterStartupScript(this, GetType(), "DeleteException",
                        $"alert('An error occurred: {ex.Message}');", true);
                }
            }
            else if (e.CommandName == "EditFoodItem")
            {
                int foodItemId = Convert.ToInt32(e.CommandArgument);
                Product productItem = _productService.GetProductById(foodItemId);

                if (productItem != null)
                {
                    // Populate the Edit modal fields
                    hdnFoodItemID.Value = productItem.ProductItemID.ToString();
                    txtEditName.Text = productItem.Name;
                    txtEditDescription.Text = productItem.Description;
                    txtEditPrice.Text = productItem.Price.ToString();
                    txtEditDiscountPrice.Text = productItem.DiscountPrice.ToString();
                    lblEditImageURL.Text = productItem.ImageURL;
                    ddlEditCategoryID.SelectedValue = productItem.CategoryID.ToString();
                    chkEditIsAvailable.Checked = productItem.IsAvailable;
                    chkEditIsFeatured.Checked = productItem.IsFeatured;

                    // Show the Edit modal
                    ClientScript.RegisterStartupScript(this.GetType(), "openEditModal", "openEditModal();", true);
                }
            }
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int productItemId = Convert.ToInt32(btn.CommandArgument);

            Product productItem = _productService.GetProductById(productItemId);

            if (productItem != null)
            {
                // Populate the Edit modal fields
                hdnFoodItemID.Value = productItem.ProductItemID.ToString();
                txtEditName.Text = productItem.Name;
                txtEditDescription.Text = productItem.Description;
                txtEditPrice.Text = productItem.Price.ToString();
                txtEditDiscountPrice.Text = productItem.DiscountPrice.ToString() ?? "";
                lblEditImageURL.Text = productItem.ImageURL;
                ddlEditCategoryID.SelectedValue = productItem.CategoryID.ToString();
                chkEditIsAvailable.Checked = productItem.IsAvailable;
                chkEditIsFeatured.Checked = productItem.IsFeatured;

                // Show the Edit modal using JavaScript
                ScriptManager.RegisterStartupScript(this, GetType(), "showEditModal",
                    "document.getElementById('editFoodItemModal').style.display = 'flex';", true);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            // Navigate back to the previous page
            Response.Redirect("Dashboard.aspx");
        }

        private void ClearForm()
        {
            txtName.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtPrice.Text = string.Empty;
            txtDiscountPrice.Text = string.Empty;
            lblImageURL.Text = string.Empty;
            ddlCategoryID.SelectedIndex = 0;
            chkIsAvailable.Checked = true;
            chkIsFeatured.Checked = false;
        }
    }
}