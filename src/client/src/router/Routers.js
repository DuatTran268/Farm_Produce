import React from "react";
import { BrowserRouter, Route, Routes } from "react-router-dom";

import Home from "../pages/users/Home";
import Cart from "../pages/users/Cart";
import Policy from "../pages/users/Policy";
import Condition from "../pages/users/Condition";
import ProductDetail from "../pages/users/ProductDetail";

const Routers = () => {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/home" element={<Home />} />
        <Route path="/cart" element={<Cart/>} />
        <Route path="/detail/" element={<ProductDetail/>} />


        <Route path="/policys" element={<Policy/>} />
        <Route path="/condition" element={<Condition/>} />
      </Routes>
    </BrowserRouter>
  );
};

export default Routers;
