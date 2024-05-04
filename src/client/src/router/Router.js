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
import Profile from "../pages/users/Profile";

// admin router
import Dashboard from "../pages/admin/dashboard/Dashboard";
import AdUser from "../pages/admin/manageuser/AdUser";
import AdOrder from "../pages/admin/order/AdOrder";
import AdCategory from "../pages/admin/categories/AdCategories";
import AdProduct from "../pages/admin/product/AdProduct";
import AdUnit from "../pages/admin/unit/AdUnit";
import AdUnitEdit from "../pages/admin/unit/AdUnitEdit";
import AdCategoryEdit from "../pages/admin/categories/AdCategioriesEdit";
import AdComments from "../pages/admin/comment/AdComments";
import AdCommentEdit from "../pages/admin/comment/AdCommentEdit";
import AdProductEdit from "../pages/admin/product/AdProductEdit";
import AdDiscount from "../pages/admin/discount/AdDiscount";
import AdDiscountEdit from "../pages/admin/discount/AdDiscountEdit";
import AdImages from "../pages/admin/images/AdImages";
import AdImagesEdit from "../pages/admin/images/AdImagesEdit";

import NotFound from "../pages/NotFound";
import AdOrderEdit from "../pages/admin/order/AdOrderEdit";

const Router = () => {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/home" element={<Home />} />
        <Route path="/cart" element={<Cart />} />
        <Route path="/category/:slug" element={<ProductInCategory />} />

        <Route path="/detail/:slug" element={<ProductDetail />} />
        <Route path="/checkout" element={<Checkout />} />
        <Route path="/checkout/orderinfor" element={<OrderDetail />} />
        <Route path="/product/viewmore" element={<MoreAllProduct />} />

        <Route path="/policys" element={<Policy />} />
        <Route path="/condition" element={<Condition />} />
        <Route path="/contact" element={<Contact />} />
        <Route path="/recruitment" element={<Recruitment />} />

        <Route path="/profile/:id" element={<Profile />} />

        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />

        {/* Admin */}
        <Route path="/admin/dashboard" element={<Dashboard />} />
        <Route path="/admin/user" element={<AdUser />} />

        <Route path="/admin/order" element={<AdOrder />} />
        <Route path="/admin/order/edit" element={<AdOrderEdit />} />
        <Route path="/admin/order/edit:/id" element={<AdOrderEdit />} />


        <Route path="/admin/category" element={<AdCategory />} />

        <Route path="/admin/product" element={<AdProduct />} />
        <Route path="/admin/product/edit" element={<AdProductEdit />} />
        <Route path="/admin/product/edit/:id" element={<AdProductEdit />} />

        <Route path="/admin/unit" element={<AdUnit />} />
        <Route path="/admin/unit/edit" element={<AdUnitEdit />} />
        <Route path="/admin/unit/edit/:id" element={<AdUnitEdit />} />

        <Route path="/admin/category" element={<AdCategory />} />
        <Route path="/admin/category/edit" element={<AdCategoryEdit />} />
        <Route path="/admin/category/edit/:id" element={<AdCategoryEdit />} />

        <Route path="/admin/comment" element={<AdComments />} />
        <Route path="/admin/comment/edit" element={<AdCommentEdit />} />
        <Route path="/admin/comment/edit/:id" element={<AdCommentEdit />} />

        <Route path="/admin/discount" element={<AdDiscount />} />
        <Route path="/admin/discount/edit" element={<AdDiscountEdit />} />
        <Route path="/admin/discount/edit/:id" element={<AdDiscountEdit />} />

        <Route path="/admin/image" element={<AdImages />} />
        <Route path="/admin/image/edit" element={<AdImagesEdit />} />
        <Route path="/admin/image/edit/:id" element={<AdImagesEdit />} />


        <Route path='*' element={<NotFound />} />

      </Routes>
    </BrowserRouter>
  );
};

export default Router;
