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
import ProductInCategory from "../pages/users/ProductInCategory";
import Recruitment from "../pages/users/Recruitment";
import AdUnit from "../pages/admin/unit/AdUnit";
import AdUnitEdit from "../pages/admin/unit/AdUnitEdit";
import AdCategoryEdit from "../pages/admin/categories/AdCategioriesEdit";
import AdComments from "../pages/admin/comment/AdComments";
import AdCommentEdit from "../pages/admin/comment/AdCommentEdit";

const Routers = () => {
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

        
        {/* Admin */} 
        <Route path="/admin" element={<Login/>} />
        <Route path="/admin/dashboard" element={<Dashboard/>} />
        <Route path="/admin/user" element={<AdUser/>} />
        <Route path="/admin/order" element={<AdOrder/>} />
        <Route path="/admin/category" element={<AdCategory/>} />
        <Route path="/admin/product" element={<AdProduct/>} />


        {/* unit */}
        <Route path="/admin/unit" element={<AdUnit/>} />
        <Route path="/admin/unit/edit" element={<AdUnitEdit/>} />
        <Route path="/admin/unit/edit/:id" element={<AdUnitEdit/>} />

        {/* category */}
        <Route path="/admin/category" element={<AdCategory/>} />
        <Route path="/admin/category/edit" element={<AdCategoryEdit/>} />
        <Route path="/admin/category/edit/:id" element={<AdCategoryEdit/>} />


        {/*comment */}
        <Route path="/admin/comment" element={<AdComments/>} />
        <Route path="/admin/comment/edit" element={<AdCommentEdit/>} />
        <Route path="/admin/comment/edit/:id" element={<AdCommentEdit/>} />





      </Routes>
    </BrowserRouter>
  );
};

export default Routers;
