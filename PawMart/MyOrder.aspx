<%@ Page Title="" Language="C#" MasterPageFile="~/PawMart.Master" AutoEventWireup="true" CodeBehind="MyOrder.aspx.cs" Inherits="PawMart.MyOrders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* Simplified Card Styling */
        .order-card {
            border: 1px solid #e0e0e0;
            border-radius: 8px;
            margin-bottom: 20px;
            transition: all 0.3s ease;
        }
        
        .order-card:hover {
            box-shadow: 0 5px 15px rgba(0,0,0,0.1);
        }
        
        .order-card .card-header {
            background-color: #f8f9fa;
            padding: 15px;
            border-bottom: 1px solid #e0e0e0;
            display: flex;
            justify-content: space-between;
            align-items: center;
        }
        
        .order-card .card-body {
            padding: 20px;
        }
        
        .order-summary {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(150px, 1fr));
            gap: 15px;
            margin-bottom: 15px;
        }
        
        .order-summary-item {
            padding: 10px;
        }
        
        .order-summary-label {
            font-size: 0.85rem;
            color: #6c757d;
            margin-bottom: 5px;
        }
        
        .order-summary-value {
            font-weight: 500;
        }
        
        /* Status Badges */
        .status-badge {
            display: inline-block;
            padding: 4px 10px;
            border-radius: 20px;
            font-size: 0.8rem;
            font-weight: 500;
        }
        
        .badge-pending { background-color: #FFF3CD; color: #856404; }
        .badge-processing { background-color: #D1ECF1; color: #0C5460; }
        .badge-delivery { background-color: #F8D7DA; color: #721C24; }
        .badge-delivered { background-color: #D4EDDA; color: #155724; }
        .badge-cancelled { background-color: #E2E3E5; color: #383D41; }
        
        /* Action Buttons */
        .order-actions {
            display: flex;
            gap: 10px;
            margin-top: 15px;
        }
        
        .btn-action {
            flex: 1;
            padding: 8px 15px;
            border-radius: 6px;
            text-align: center;
            font-size: 0.9rem;
            transition: all 0.2s ease;
            display: flex;
            align-items: center;
            justify-content: center;
            gap: 8px;
        }
        
        .btn-details {
            background-color: #f8f9fa;
            color: #495057;
            border: 1px solid #dee2e6;
        }
        
        .btn-details:hover {
            background-color: #e9ecef;
        }
        
        .btn-track {
            background-color: #0d6efd;
            color: white;
            border: 1px solid #0d6efd;
        }
        
        .btn-track:hover {
            background-color: #0b5ed7;
        }
        
        /* Modal Styling */
        .modal-order-items {
            max-height: 400px;
            overflow-y: auto;
            margin: 20px 0;
        }
        
        .order-item {
            display: flex;
            justify-content: space-between;
            padding: 12px 0;
            border-bottom: 1px solid #f1f1f1;
        }
        
        .order-item:last-child {
            border-bottom: none;
        }
        
        .item-info {
            display: flex;
            align-items: center;
            gap: 15px;
        }
        
        .item-image {
            width: 60px;
            height: 60px;
            border-radius: 8px;
            overflow: hidden;
        }
        
        .item-image img {
            width: 100%;
            height: 100%;
            object-fit: cover;
        }
        
        .item-name {
            font-weight: 500;
        }
        
        .item-price {
            color: #6c757d;
            font-size: 0.9rem;
        }
        
        .item-quantity {
            background-color: #f8f9fa;
            padding: 2px 8px;
            border-radius: 4px;
            font-size: 0.8rem;
            margin-left: 5px;
        }
        
        .item-total {
            font-weight: 600;
        }
        
        /* Empty State */
        .empty-orders {
            text-align: center;
            padding: 40px 20px;
            background-color: #f8f9fa;
            border-radius: 8px;
        }
        
        .empty-orders i {
            font-size: 48px;
            color: #adb5bd;
            margin-bottom: 15px;
        }
        
        /* Responsive Adjustments */
        @media (max-width: 768px) {
            .order-summary {
                grid-template-columns: 1fr 1fr;
            }
            
            .order-actions {
                flex-direction: column;
            }
        }
        
        @media (max-width: 576px) {
            .order-card .card-header {
                flex-direction: column;
                align-items: flex-start;
                gap: 5px;
            }
            
            .order-summary {
                grid-template-columns: 1fr;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container py-4">
        <div class="d-flex justify-content-between align-items-center mb-4">
            <h2 class="mb-0">My Orders</h2>
            
            <div class="d-flex gap-3">
                <asp:DropDownList ID="ddlStatusFilter" runat="server" CssClass="form-select form-select-sm" 
                    AutoPostBack="true" OnSelectedIndexChanged="ddlStatusFilter_SelectedIndexChanged">
                    <asp:ListItem Text="All Orders" Value="" Selected="True" />
                    <asp:ListItem Text="Pending" Value="Pending" />
                    <asp:ListItem Text="Processing" Value="Processing" />
                    <asp:ListItem Text="Out for Delivery" Value="Delivery" />
                    <asp:ListItem Text="Delivered" Value="Delivered" />
                    <asp:ListItem Text="Cancelled" Value="Cancelled" />
                </asp:DropDownList>
                
                <asp:DropDownList ID="ddlSortBy" runat="server" CssClass="form-select form-select-sm" 
                    AutoPostBack="true" OnSelectedIndexChanged="ddlSortBy_SelectedIndexChanged">
                    <asp:ListItem Text="Newest First" Value="DateDesc" Selected="True" />
                    <asp:ListItem Text="Oldest First" Value="DateAsc" />
                    <asp:ListItem Text="Amount (High to Low)" Value="AmountDesc" />
                    <asp:ListItem Text="Amount (Low to High)" Value="AmountAsc" />
                </asp:DropDownList>
            </div>
        </div>
        
        <!-- No Orders Message -->
        <asp:Panel ID="pnlNoOrders" runat="server" CssClass="empty-orders" Visible="false">
            <i class="fa fa-receipt"></i>
            <h4 class="mb-3">No orders found</h4>
            <p class="text-muted mb-4">You haven't placed any orders yet.</p>
            <asp:HyperLink ID="lnkBrowseMenu" NavigateUrl="~/ProductListing.aspx" runat="server" CssClass="btn btn-primary">
                Browse Menu
            </asp:HyperLink>
        </asp:Panel>
        
        <div class="row">
            <asp:Repeater ID="rptOrders" runat="server" OnItemCommand="rptOrders_ItemCommand">
                <ItemTemplate>
                    <div class="col-12 col-md-6 col-lg-4 mb-4">
                        <div class="order-card">
                            <div class="card-header">
                                <span class="fw-bold">Order #<%# Eval("OrderID") %></span>
                                <span class="text-muted"><%# GetFormattedDate(Eval("OrderDate")) %></span>
                            </div>
                            
                            <div class="card-body">
                                <div class="order-summary">
                                    <div class="order-summary-item">
                                        <div class="order-summary-label">Status</div>
                                        <div>
                                            <span class='status-badge badge-<%# GetStatusClass(Eval("OrderStatus").ToString()) %>'>
                                                <%# GetFormattedStatus(Eval("OrderStatus").ToString()) %>
                                            </span>
                                        </div>
                                    </div>
                                    
                                    <div class="order-summary-item">
                                        <div class="order-summary-label">Total</div>
                                        <div class="order-summary-value">$<%# Eval("TotalAmount", "{0:0.00}") %></div>
                                    </div>
                                    
                                    <div class="order-summary-item">
                                        <div class="order-summary-label">Payment</div>
                                        <div><%# Eval("PaymentMethod") %></div>
                                    </div>
                                    
                                    <div class="order-summary-item">
                                        <div class="order-summary-label">Items</div>
                                        <div><%# GetItemCount(Eval("OrderID")) %></div>
                                    </div>
                                </div>
                                
                                <div class="order-actions">
                                    <button type="button" class="btn-action btn-details" data-bs-toggle="modal" 
                                        data-bs-target="#orderModal<%# Eval("OrderID") %>">
                                        <i class="fa fa-list"></i> Details
                                    </button>
                                    
                                    <asp:LinkButton ID="btnTrack" runat="server" CssClass="btn-action btn-track" 
                                        CommandName="Track" CommandArgument='<%# Eval("OrderID") %>'>
                                        <i class="fa fa-truck"></i> Track
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                    
                    <!-- Order Details Modal -->
                    <div class="modal fade" id="orderModal<%# Eval("OrderID") %>" tabindex="-1" 
                        aria-labelledby="orderModalLabel<%# Eval("OrderID") %>" aria-hidden="true">
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title">Order #<%# Eval("OrderID") %></h5>
                                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                </div>
                                
                                <div class="modal-body">
                                    <div class="row mb-4">
                                        <div class="col-md-6">
                                            <h6>Order Information</h6>
                                            <div class="mb-2"><strong>Date:</strong> <%# GetFormattedDate(Eval("OrderDate")) %></div>
                                            <div class="mb-2">
                                                <strong>Status:</strong> 
                                                <span class='status-badge badge-<%# GetStatusClass(Eval("OrderStatus").ToString()) %>'>
                                                    <%# GetFormattedStatus(Eval("OrderStatus").ToString()) %>
                                                </span>
                                            </div>
                                            <div class="mb-2"><strong>Payment:</strong> <%# Eval("PaymentMethod") %> (<%# Eval("PaymentStatus") %>)</div>
                                        </div>
                                        
                                        <div class="col-md-6">
                                            <h6>Delivery Information</h6>
                                            <div class="mb-2"><strong>Address:</strong> <%# Eval("DeliveryAddress") %></div>
                                            <div class="mb-2"><strong>Contact:</strong> <%# Eval("ContactPhone") %></div>
                                            <div class="mb-2"><strong>Notes:</strong> <%# String.IsNullOrEmpty(Eval("Notes").ToString()) ? "None" : Eval("Notes") %></div>
                                        </div>
                                    </div>
                                    
                                    <h6 class="mb-3">Order Items</h6>
                                    <div class="modal-order-items">
                                        <asp:Repeater ID="rptOrderItems" runat="server">
                                            <ItemTemplate>
                                                <div class="order-item">
                                                    <div class="item-info">
                                                        <div class="item-image">
                                                            <img src='<%#ResolveUrl(Eval("ProductImage").ToString()) %>' 
                                                                alt='<%# Eval("ProductImage") %>' 
                                                                onerror="this.src='<%# ResolveUrl("~/Images/default-food.jpg") %>';" />
                                                        </div>
                                                        <div>
                                                            <div class="item-name"><%# Eval("ProductName") %></div>
                                                            <div class="item-price">
                                                                $<%# Eval("Price", "{0:0.00}") %>
                                                                <span class="item-quantity">× <%# Eval("Quantity") %></span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="item-total">$<%# Eval("Subtotal", "{0:0.00}") %></div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                    
                                    <div class="d-flex justify-content-between align-items-center mt-4 pt-3 border-top">
                                        <h6 class="mb-0">Total Amount</h6>
                                        <h5 class="mb-0 text-primary">$<%# Eval("TotalAmount", "{0:0.00}") %></h5>
                                    </div>
                                </div>
                                
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-outline-secondary" data-bs-dismiss="modal">Close</button>
                                    <asp:LinkButton ID="btnTrackModal" runat="server" CssClass="btn btn-primary" 
                                        CommandName="Track" CommandArgument='<%# Eval("OrderID") %>'>
                                        <i class="fa fa-truck me-2"></i>Track Order
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>