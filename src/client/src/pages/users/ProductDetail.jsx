import React from "react";
import ProductDetails from "../../components/user/product/ProductDetails";
import Categoris from "../../components/user/categories/Categoris";
import LayoutClient from "../../components/user/common/LayoutClient";
import ProductComment from "../../components/user/commnent/ProductComment";
import "../../components/user/product/ProductDetail.css";
import CommentBox from "../../components/user/commnent/CommentBox";
import { useSelector } from "react-redux";
import { Link } from "react-router-dom";

const ProductDetail = () => {
  const currentUser = useSelector((state) => state.auth.login.currentUser);

  // kiem tra trang thai da dang nhap chua
  const isLoggedIn = !!currentUser;


  return (
    <LayoutClient>
      <div className="d-flex pt-3 category_slide justify-content-between">
        <div className="category col-3">
          <Categoris />
        </div>
        {/* slide advertising */}
        <div className="slider col-8">
          <ProductDetails />
        </div>
      </div>
      <div className="product_comment ">
        <div className=" product_comment_container row">
          <div className="product_col col-6">
            <ProductComment />
          </div>
          <div className="product_col col-6">
            <div className="comment_content">
              <h5 className="comment_title">Để lại bình luận của bạn</h5>
            </div>

            {/* Kiểm tra xem người dùng có đăng nhập hay không */}
            {isLoggedIn ? (
              <CommentBox /> //  đăng nhập=> hiển thị CommentBox
            ) : (
              <div className="comment_announced">
                <Link to={'/login'} className="message_announced">Bạn cần phải đăng nhập để bình luận</Link>
              </div> 
            )}

          </div>
        </div>
      </div>
    </LayoutClient>
  );
};

export default ProductDetail;
