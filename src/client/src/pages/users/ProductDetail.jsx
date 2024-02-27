import React from "react";
import Header from "../../components/user/common/Header";
import Footer from "../../components/user/common/Footer";
import ProductDetails from "../../components/user/product/ProductDetails";
import Categoris from "../../components/user/categories/Categoris";
const ProductDetail = () => {
  return (
    <section>
      <Header />
      <div className="container">
        <div className="d-flex pt-3 category_slide justify-content-between">
          <div className="category col-3">
            <Categoris />
          </div>
          {/* slide advertising */}
          <div className="slider col-8">
            <ProductDetails />
          </div>
        </div>
      </div>
      <Footer />
    </section>
  );
};

export default ProductDetail;
