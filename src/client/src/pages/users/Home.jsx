import React from "react";
import Categoris from "../../components/user/categories/Categoris";
import SlideBanner from "../../components/user/slide/SlideBanner";
import NewProduct from "../../components/user/product/NewProduct";
import FruitProduct from "../../components/user/product/FruitProduct";
import Advertise from "../../components/user/advertise/Advertise";
import AdvertiseBanner from "../../components/user/advertise/AdvertiseBanner";
import LayoutClient from "../../components/user/common/LayoutClient";
import ProductCategory from "../../components/user/product/ProductCategory";
const Home = () => {
  return (
    <LayoutClient>
      <div className="d-flex pt-3 category_slide justify-content-between row">
        <div className="category col-3">
          <Categoris />
        </div>
        {/* slide advertising */}
        <div className="slider col-9">
          <div className="banner">
            <SlideBanner />
          </div>
        </div>
      </div>

      {/* new product */}
      <div className="new_product">
        <NewProduct />
      </div>
      <div className="advertise">
        <Advertise />
      </div>
      <div className="list_product">
        <FruitProduct />
      </div>
      <div className="advertise">
        <AdvertiseBanner />
      </div>
      <div className="list_product">
        <ProductCategory/>
      </div>
    </LayoutClient>
  );
};

export default Home;
