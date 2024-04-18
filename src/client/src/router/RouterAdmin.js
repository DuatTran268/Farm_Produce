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
import NotFound from "../pages/NotFound";
import AdImages from "../pages/admin/images/AdImages";
import AdImagesEdit from "../pages/admin/images/AdImagesEdit";

const RouterAdmin = () => {
  return (
    <BrowserRouter>
      <Routes>
     

      </Routes>
    </BrowserRouter>
  );
};

export default RouterAdmin;
