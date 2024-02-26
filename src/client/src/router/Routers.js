import React from "react";
import { BrowserRouter, Route, Routes } from "react-router-dom";

import Home from "../pages/users/Home";
import Cart from "../pages/users/Cart";

const Routers = () => {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/home" element={<Home />} />
        <Route path="/cart" element={<Cart/>} />

      </Routes>
    </BrowserRouter>
  );
};

export default Routers;
