import React from "react";
import { BrowserRouter, Route, Routes } from "react-router-dom";

import Home from "../pages/users/Home";

const Routers = () => {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/home" element={<Home />} />



        
      </Routes>
    </BrowserRouter>
  );
};

export default Routers;
