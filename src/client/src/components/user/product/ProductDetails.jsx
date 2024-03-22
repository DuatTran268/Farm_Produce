import React, { useEffect, useState } from "react";
import { Link, Navigate, useParams } from "react-router-dom";
import "../../../styles/user/ProductDetail.css";
import dautay from "../../../assets/mutdau.jpg";
import { Image } from "react-bootstrap";
import { getDetailProductByUrlSlug } from "../../../api/Product";
import { useCart } from "react-use-cart";
import { useSnackbar } from "notistack";

const ProductDetails = () => {
  const params = useParams();
  const [productDetail, setProductDetail] = useState(null);
  const { slug } = params;
  const { addItem } = useCart(); // Truy cập hàm addItem từ useCart
  const { enqueueSnackbar, closeSnackbar } = useSnackbar();

  useEffect(() => {
    document.title = "Chi tiết sản phẩm";
    getDetailProductByUrlSlug(slug).then((data) => {
      if (data) {
        setProductDetail(data);
      } else {
        setProductDetail({});
      }
    });
  }, [slug]);

  const handleAddToCart = () => {
    // Hàm xử lý khi người dùng nhấn vào nút "Thêm vào giỏ hàng"
    addItem({
      id: productDetail.id, // ID của sản phẩm
      name: productDetail.name, // Tên sản phẩm
      price: productDetail.price, // Giá sản phẩm
      quantity: parseInt(document.getElementById("quantity").value), // Số lượng
    });

    enqueueSnackbar("Bạn đã thêm sản phẩm vào giỏ hàng", {
      variant: "success",
    });
  };

  if (productDetail) {
    return (
      <section>
        <div className="product_detail">
          <div className="product_detail_img col-5">
            <Image src={dautay} width={300} />
          </div>
          <div className="product_detail_content col-7">
            <div className="product_detail_title">{productDetail.name}</div>
            <div className="product_detail_price">{productDetail.price}</div>

            <div className="product_detail_desc">
              {productDetail.description}
            </div>
            <div className="product_detail_size">
              <label htmlFor="size">Lựa chọn size: </label>
              <select name="size" id="size">
                <option value="S">S</option>
                <option value="M">M</option>
                <option value="L">L</option>
                <option value="XL">XL</option>
              </select>
            </div>
            <div className="product_detail_quantity_cart">
              <div className="product_detail_quantity">
                <span>Số lượng</span>
                <input type="number" id="quantity" min="1" max="100" />
              </div>
              <Link
                className="btn btn-success product_detail_addcart"
                to={"/cart"}
                onClick={handleAddToCart}
              >
                Thêm vào giỏ hàng
              </Link>
            </div>
          </div>
        </div>
      </section>
    );
  }
};
export default ProductDetails;
