import React from "react";
import { BrowserRouter, Route, Routes } from "react-router-dom";
// import { lazy } from "react";
import Home from "../pages/users/Home";
import Cart from "../pages/users/Cart";
import Policy from "../pages/users/Policy";
import Condition from "../pages/users/Condition";
import ProductDetail from "../pages/users/ProductDetail";
import Checkout from "../pages/users/Checkout";
import Contact from "../pages/users/Contact";
import OrderDetail from "../pages/users/OrderDetail";
import MoreAllProduct from "../pages/users/MoreAllProduct";
import Dashboard from "../pages/admin/dashboard/Dashboard";
import AdUser from "../pages/admin/manageuser/AdUser";
import AdOrder from "../pages/admin/order/AdOrder";
import AdCategory from "../pages/admin/categories/AdCategories";
import AdProduct from "../pages/admin/product/AdProduct";
import Login from "../pages/login/Login";

const Routers = () => {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/home" element={<Home />} />
        <Route path="/cart" element={<Cart/>} />
        <Route path="/category/:slug" element={<MoreAllProduct/>} />
        
        <Route path="/detail/" element={<ProductDetail/>} />
        <Route path="/checkout" element={<Checkout/>} />
        <Route path="/checkout/orderinfor" element={<OrderDetail/>} />
        <Route path="/product/viewmore" element={<MoreAllProduct/>} />

        <Route path="/policys" element={<Policy/>} />
        <Route path="/condition" element={<Condition/>} />
        <Route path="/contact" element={<Contact/>} />
        
        {/* Admin */} 
        <Route path="/admin" element={<Login/>} />
        <Route path="/admin/dashboard" element={<Dashboard/>} />
        <Route path="/admin/user" element={<AdUser/>} />
        <Route path="/admin/order" element={<AdOrder/>} />
        <Route path="/admin/category" element={<AdCategory/>} />
        <Route path="/admin/product" element={<AdProduct/>} />



      </Routes>
    </BrowserRouter>
  );
};

export default Routers;
