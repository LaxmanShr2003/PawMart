<%@ Page Title="Home" Language="C#" MasterPageFile="~/FoodyMan.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="FoodyMan.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* Hero Section */
        .hero-section {
            background: linear-gradient(rgba(0, 0, 0, 0.6), rgba(0, 0, 0, 0.6)), url('/Images/dash.jpeg') no-repeat center center;
            background-size: cover;
            color: white;
            padding: 100px 0;
            text-align: center;
        }

        .hero-title {
            font-size: 3rem;
            margin-bottom: 20px;
            font-weight: 700;
        }

        .hero-subtitle {
            font-size: 1.5rem;
            margin-bottom: 30px;
            max-width: 800px;
            margin-left: auto;
            margin-right: auto;
        }

        .hero-button {
            display: inline-block;
            background-color: #FF6B35;
            color: white;
            padding: 12px 30px;
            font-size: 1.2rem;
            border-radius: 4px;
            text-decoration: none;
            transition: all 0.3s ease;
            font-weight: 600;
            border: none;
            cursor: pointer;
        }

        .hero-button:hover {
            background-color: #e55a24;
            transform: translateY(-3px);
            box-shadow: 0 5px 15px rgba(0, 0, 0, 0.1);
        }

        /* Features Section */
        .features-section {
            padding: 80px 0;
            background-color: #f8f9fa;
        }

        .section-title {
            text-align: center;
            font-size: 2.5rem;
            margin-bottom: 50px;
            color: #333;
            position: relative;
            padding-bottom: 15px;
        }

        .section-title::after {
            content: '';
            position: absolute;
            width: 80px;
            height: 3px;
            background-color: #FF6B35;
            bottom: 0;
            left: 50%;
            transform: translateX(-50%);
        }

        .features-container {
            display: flex;
            flex-wrap: wrap;
            justify-content: center;
            gap: 30px;
        }

        .feature-box {
            flex-basis: calc(33.333% - 30px);
            min-width: 250px;
            background-color: white;
            border-radius: 10px;
            padding: 30px;
            text-align: center;
            box-shadow: 0 5px 15px rgba(0, 0, 0, 0.05);
            transition: all 0.3s ease;
        }

        .feature-box:hover {
            transform: translateY(-10px);
            box-shadow: 0 15px 30px rgba(0, 0, 0, 0.1);
        }

        .feature-icon {
            font-size: 3rem;
            color: #FF6B35;
            margin-bottom: 20px;
        }

        .feature-title {
            font-size: 1.5rem;
            margin-bottom: 15px;
            color: #333;
        }

        .feature-description {
            color: #666;
            line-height: 1.6;
        }

        /* Categories Section */
        .categories-section {
            padding: 80px 0;
        }

        .categories-container {
            display: flex;
            flex-wrap: wrap;
            justify-content: center;
            gap: 20px;
        }

        .category-card {
            flex-basis: calc(25% - 20px);
            min-width: 200px;
            border-radius: 10px;
            overflow: hidden;
            position: relative;
            box-shadow: 0 5px 15px rgba(0, 0, 0, 0.1);
            transition: all 0.3s ease;
        }

        .category-card:hover {
            transform: translateY(-5px);
            box-shadow: 0 15px 30px rgba(0, 0, 0, 0.15);
        }

        .category-image {
            width: 100%;
            height: 200px;
            object-fit: cover;
        }

        .category-overlay {
            position: absolute;
            bottom: 0;
            left: 0;
            right: 0;
            background-color: rgba(0, 0, 0, 0.7);
            padding: 15px;
            color: white;
            text-align: center;
        }

        .category-name {
            font-size: 1.2rem;
            margin-bottom: 5px;
        }

        .category-button {
            display: inline-block;
            background-color: #FF6B35;
            color: white;
            padding: 5px 15px;
            border-radius: 4px;
            text-decoration: none;
            font-size: 0.9rem;
            transition: all 0.3s ease;
        }

        .category-button:hover {
            background-color: white;
            color: #FF6B35;
        }

        /* Featured Products Section */
        .products-section {
            padding: 80px 0;
            background-color: #f8f9fa;
        }

        .products-container {
            display: flex;
            flex-wrap: wrap;
            justify-content: center;
            gap: 30px;
        }

        .product-card {
            flex-basis: calc(25% - 30px);
            min-width: 250px;
            background-color: white;
            border-radius: 10px;
            overflow: hidden;
            box-shadow: 0 5px 15px rgba(0, 0, 0, 0.05);
            transition: all 0.3s ease;
        }

        .product-card:hover {
            transform: translateY(-10px);
            box-shadow: 0 15px 30px rgba(0, 0, 0, 0.1);
        }

        .product-image-container {
            position: relative;
            height: 200px;
            overflow: hidden;
        }

        .product-image {
            width: 100%;
            height: 100%;
            object-fit: cover;
            transition: all 0.5s ease;
        }

        .product-card:hover .product-image {
            transform: scale(1.1);
        }

        .product-info {
            padding: 20px;
        }

        .product-category {
            color: #FF6B35;
            font-size: 0.9rem;
            margin-bottom: 5px;
        }

        .product-name {
            font-size: 1.2rem;
            margin-bottom: 10px;
            color: #333;
        }

        .product-price {
            font-size: 1.3rem;
            color: #333;
            font-weight: 600;
            margin-bottom: 15px;
        }

        .product-buttons {
            display: flex;
            justify-content: space-between;
        }

        .button {
            flex: 1;
            text-align: center;
            padding: 10px;
            border-radius: 4px;
            text-decoration: none;
            font-weight: 600;
            transition: all 0.3s ease;
        }

        .button-primary {
            background-color: #FF6B35;
            color: white;
            margin-right: 8px;
        }

        .button-primary:hover {
            background-color: #e55a24;
        }

        .button-secondary {
            background-color: #f1f1f1;
            color: #333;
        }

        .button-secondary:hover {
            background-color: #ddd;
        }

        /* Testimonials Section */
        .testimonials-section {
            padding: 80px 0;
            background: linear-gradient(rgba(0, 0, 0, 0.8), rgba(0, 0, 0, 0.8)), url('/Images/testimonial-bg.jpg') no-repeat center center;
            background-size: cover;
            color: white;
        }

        .testimonials-container {
            max-width: 800px;
            margin: 0 auto;
        }

        .testimonial {
            text-align: center;
            padding: 0 20px;
        }

        .testimonial-text {
            font-size: 1.2rem;
            font-style: italic;
            margin-bottom: 20px;
            line-height: 1.8;
        }

        .testimonial-author {
            font-weight: 600;
            font-size: 1.1rem;
        }

        .testimonial-role {
            font-size: 0.9rem;
            color: #ccc;
        }

        /* Download App Section */
        .app-section {
            padding: 80px 0;
            background-color: #f8f9fa;
            text-align: center;
        }

        .app-container {
            display: flex;
            flex-wrap: wrap;
            align-items: center;
            justify-content: space-between;
            max-width: 1000px;
            margin: 0 auto;
        }

        .app-info {
            flex-basis: 50%;
            padding: 0 20px;
            text-align: left;
        }

        .app-title {
            font-size: 2rem;
            margin-bottom: 20px;
            color: #333;
        }

        .app-description {
            color: #666;
            margin-bottom: 30px;
            line-height: 1.6;
        }

        .app-buttons {
            display: flex;
            gap: 15px;
        }

        .app-button {
            display: inline-flex;
            align-items: center;
            background-color: #333;
            color: white;
            padding: 10px 20px;
            border-radius: 5px;
            text-decoration: none;
            transition: all 0.3s ease;
        }

        .app-button:hover {
            background-color: #555;
            transform: translateY(-3px);
        }

        .app-button i {
            font-size: 1.8rem;
            margin-right: 10px;
        }

        .app-button-text {
            display: flex;
            flex-direction: column;
        }

        .app-button-small {
            font-size: 0.7rem;
        }

        .app-button-big {
            font-size: 1.1rem;
            font-weight: 600;
        }

        .app-image {
            flex-basis: 40%;
            padding: 0 20px;
        }

        .app-image img {
            max-width: 100%;
            border-radius: 20px;
            box-shadow: 0 15px 30px rgba(0, 0, 0, 0.1);
        }

        /* Responsive */
        @media (max-width: 992px) {
            .hero-title {
                font-size: 2.5rem;
            }
            
            .hero-subtitle {
                font-size: 1.2rem;
            }
            
            .app-info, .app-image {
                flex-basis: 100%;
            }
            
            .app-info {
                margin-bottom: 40px;
                text-align: center;
            }
            
            .app-buttons {
                justify-content: center;
            }
        }

        @media (max-width: 768px) {
            .hero-section {
                padding: 80px 0;
            }
            
            .hero-title {
                font-size: 2rem;
            }
            
            .section-title {
                font-size: 2rem;
            }
            
            .feature-box, .product-card {
                flex-basis: calc(50% - 30px);
            }
        }

        @media (max-width: 576px) {
            .hero-title {
                font-size: 1.8rem;
            }
            
            .hero-button {
                padding: 10px 20px;
                font-size: 1rem;
            }
            
            .feature-box, .product-card, .category-card {
                flex-basis: 100%;
            }
        }
        .error-message {
    display: block;
    padding: 10px;
    margin: 10px 0;
    background: #ffeeee;
    border-left: 4px solid #ff0000;
    font-weight: bold;
}
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Hero Section -->
    <section class="hero-section">
        <div class="container">
            <h1 class="hero-title">Delicious Food Delivered To Your Door</h1>
            <p class="hero-subtitle">Order your favorite meals from the best restaurants in town. Fast delivery, easy payment, and a wide variety of cuisines to choose from.</p>
            <asp:Button ID="btnOrderNow" runat="server" CssClass="hero-button" Text="Order Now"  />
        </div>
    </section>
    <div class="container" style="margin-top: 20px; margin-bottom: 20px;">
    <asp:Label ID="lblError" runat="server" CssClass="error-message" Visible="false" ForeColor="Red"></asp:Label>
</div>

    <!-- Features Section -->
    <section class="features-section">
        <div class="container">
            <h2 class="section-title">Why Choose Us</h2>
            <div class="features-container">
                <div class="feature-box">
                    <i class="fas fa-truck feature-icon"></i>
                    <h3 class="feature-title">Fast Delivery</h3>
                    <p class="feature-description">We deliver your food as quickly as possible. Our delivery partners ensure your food arrives hot and fresh.</p>
                </div>
                <div class="feature-box">
                    <i class="fas fa-utensils feature-icon"></i>
                    <h3 class="feature-title">Quality Food</h3>
                    <p class="feature-description">We partner with the best restaurants to ensure you receive high-quality, delicious meals every time.</p>
                </div>
                <div class="feature-box">
                    <i class="fas fa-mobile-alt feature-icon"></i>
                    <h3 class="feature-title">Easy To Order</h3>
                    <p class="feature-description">Our user-friendly platform makes ordering food quick and easy. Just a few clicks and your food is on its way.</p>
                </div>
            </div>
        </div>
    </section>

    <!-- Categories Section -->
    <section class="categories-section">
        <div class="container">
            <h2 class="section-title">Explore Categories</h2>
             <asp:Label ID="lblCategoryError" runat="server" CssClass="error-message" Visible="false" ForeColor="Red"></asp:Label>
            <div class="categories-container">
                <asp:Repeater ID="rptCategories" runat="server">
                    <ItemTemplate>
                        <div class="category-card">
                           <%-- <img src='<%# Eval("ImageUrl") %>' alt='<%# Eval("Name") %>' class="category-image" />--%>
                            <div class="category-overlay">
                                <h3 class="category-name"><%# Eval("Name") %></h3>
                                <a href='Products.aspx?category=<%# Eval("CategoryID") %>' class="category-button">View All</a>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </section>

    <!-- Featured Products Section -->
    <section class="products-section">
        <div class="container">
            <h2 class="section-title">Featured Dishes</h2>
              <asp:Label ID="lblProductError" runat="server" CssClass="error-message" Visible="false" ForeColor="Red"></asp:Label>
            <div class="products-container">
                <asp:Repeater ID="rptFeaturedProducts" runat="server">
                    <ItemTemplate>
                        <div class="product-card">
                            <div class="product-image-container">
                                <img src='<%#ResolveUrl(Eval("ImageURL").ToString()) %>' alt='<%# Eval("Name") %>' class="product-image" />
                            </div>
                            <div class="product-info">
                                <div class="product-category"><%# Eval("Name") %></div>
                                <h3 class="product-name"><%# Eval("Name") %></h3>
                                <div class="product-price">$<%# Eval("Price", "{0:0.00}") %></div>
                                <div class="product-buttons">
                                    <asp:LinkButton ID="btnAddToCart" runat="server" CssClass="button button-primary" 
                                        CommandName="AddToCart" CommandArgument='<%# Eval("ProductID") %>'>
                                        <i class="fas fa-shopping-cart"></i> Add to Cart
                                    </asp:LinkButton>
                                    <a href='ProductDetails.aspx?id=<%# Eval("ProductID") %>' class="button button-secondary">
                                        <i class="fas fa-info-circle"></i> Details
                                    </a>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </section>

    <!-- Testimonials Section -->
    <section class="testimonials-section">
        <div class="container">
            <h2 class="section-title">What Our Customers Say</h2>
            <div class="testimonials-container">
                <div class="testimonial">
                    <p class="testimonial-text">"FoodyMan has changed the way I enjoy food. Their service is exceptional, and the food always arrives hot and fresh. I can't recommend them enough!"</p>
                    <div class="testimonial-author">John Smith</div>
                    <div class="testimonial-role">Regular Customer</div>
                </div>
            </div>
        </div>
    </section>

    <!-- Download App Section -->
    <section class="app-section">
        <div class="container">
            <div class="app-container">
                <div class="app-info">
                    <h2 class="app-title">Download Our Mobile App</h2>
                    <p class="app-description">Get the full FoodyMan experience on your mobile device. Order food, track your delivery in real-time, and receive exclusive app-only offers.</p>
                    <div class="app-buttons">
                        <a href="#" class="app-button">
                            <i class="fab fa-google-play"></i>
                            <div class="app-button-text">
                                <span class="app-button-small">GET IT ON</span>
                                <span class="app-button-big">Google Play</span>
                            </div>
                        </a>
                        <a href="#" class="app-button">
                            <i class="fab fa-apple"></i>
                            <div class="app-button-text">
                                <span class="app-button-small">DOWNLOAD ON</span>
                                <span class="app-button-big">App Store</span>
                            </div>
                        </a>
                    </div>
                </div>
                <div class="app-image">
                    <img src="/Images/app-mockup.png" alt="FoodyMan Mobile App" />
                </div>
            </div>
        </div>
    </section>
</asp:Content>