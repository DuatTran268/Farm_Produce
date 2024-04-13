import React from "react";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import Dashboard from "../pages/admin/dashboard/Dashboard"
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

const RouterAdmin = () => {
  return (
    <BrowserRouter>
      <Routes>

        <Route path="/admin/dashboard" element={<Dashboard/>} />
        <Route path="/admin/user" element={<AdUser/>} />

        <Route path="/admin/order" element={<AdOrder/>} />
        
        
        <Route path="/admin/category" element={<AdCategory/>} />
        
        <Route path="/admin/product" element={<AdProduct/>} />
        <Route path="/admin/product/edit" element={<AdProductEdit/>} />
        <Route path="/admin/product/edit/:id" element={<AdProductEdit/>} />

        <Route path="/admin/unit" element={<AdUnit/>} />
        <Route path="/admin/unit/edit" element={<AdUnitEdit/>} />
        <Route path="/admin/unit/edit/:id" element={<AdUnitEdit/>} />

        <Route path="/admin/category" element={<AdCategory/>} />
        <Route path="/admin/category/edit" element={<AdCategoryEdit/>} />
        <Route path="/admin/category/edit/:id" element={<AdCategoryEdit/>} />

        <Route path="/admin/comment" element={<AdComments/>} />
        <Route path="/admin/comment/edit" element={<AdCommentEdit/>} />
        <Route path="/admin/comment/edit/:id" element={<AdCommentEdit/>} />

        <Route path="/admin/discount" element={<AdDiscount/>} />
        <Route path="/admin/discount/edit" element={<AdDiscountEdit/>} />
        <Route path="/admin/discount/edit/:id" element={<AdDiscountEdit/>} />

      </Routes>
    </BrowserRouter>
  );
};

export default RouterAdmin;
