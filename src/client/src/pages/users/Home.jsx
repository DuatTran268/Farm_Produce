import React from "react";
import Header from "../../components/user/common/Header";
import "../../styles/Home.css";
import Categoris from "../../components/user/categories/Categoris";
import SlideBanner from "../../components/user/slide/SlideBanner";
import NewProduct from "../../components/user/product/NewProduct";
import FruitProduct from "../../components/user/product/FruitProduct";
import Footer from "../../components/user/common/Footer";

const Home = () => {
  return (
    <div className="home">
      {/* header */}
      <Header />
      {/* container */}
      <div className="container ">
        <div className="d-flex pt-3 category_slide justify-content-between">
          <div className="category col-3">
            <Categoris/>
          </div>
          {/* slide advertising */}
          <div className="slider col-8">
            <div className="banner">
              <SlideBanner />
            </div>
          </div>
        </div>

        {/* new product */}
        <div className="new_product">
          <NewProduct/>
        </div>
        <div className="fruit_product">
          <FruitProduct/>
        </div>
      </div>

      <Footer/>
    </div>
  );
};

export default Home;
