import React from "react";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import Home from "../pages/users/Home";
import Cart from "../pages/users/Cart";
import Policy from "../pages/users/Policy";
import Condition from "../pages/users/Condition";
import ProductDetail from "../pages/users/ProductDetail";
import Checkout from "../pages/users/Checkout";
import Contact from "../pages/users/Contact";
import OrderDetail from "../pages/users/OrderDetail";
import MoreAllProduct from "../pages/users/MoreAllProduct";
import ProductInCategory from "../pages/users/ProductInCategory";
import Recruitment from "../pages/users/Recruitment";
import Login from "../pages/account/Login";
import Register from "../pages/account/Register";


const RouterUser = () => {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/home" element={<Home />} />
        <Route path="/cart" element={<Cart/>} />
        <Route path="/category/:slug" element={<ProductInCategory/>} />
        
        <Route path="/detail/:slug" element={<ProductDetail/>} />
        <Route path="/checkout" element={<Checkout/>} />
        <Route path="/checkout/orderinfor" element={<OrderDetail/>} />
        <Route path="/product/viewmore" element={<MoreAllProduct/>} />

        <Route path="/policys" element={<Policy/>} />
        <Route path="/condition" element={<Condition/>} />
        <Route path="/contact" element={<Contact/>} />
        <Route path="/recruitment" element={<Recruitment/>} />

        <Route path="/login" element={<Login/>} />
        
        <Route path="/register" element={<Register/>} />
      </Routes>
    </BrowserRouter>
  );
};

export default RouterUser;
