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

const Routers = () => {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/home" element={<Home />} />
        <Route path="/cart" element={<Cart/>} />
        <Route path="/detail/" element={<ProductDetail/>} />
        <Route path="/checkout" element={<Checkout/>} />
        <Route path="/checkout/orderinfor" element={<OrderDetail/>} />

        

        

        <Route path="/policys" element={<Policy/>} />
        <Route path="/condition" element={<Condition/>} />
        <Route path="/contact" element={<Contact/>} />


      </Routes>
    </BrowserRouter>
  );
};

export default Routers;
