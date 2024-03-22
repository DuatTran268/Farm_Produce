import React from "react";
import { Link } from "react-router-dom";
import { Image } from "react-bootstrap";
import "../../../styles/user/Product.css";
import { useCart } from "react-use-cart";
import { faCartArrowDown } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { useSnackbar } from "notistack";

const ProductTemplate = (props) => {
  const { addItem } = useCart();
  const { enqueueSnackbar, closeSnackbar } = useSnackbar();

  const handleAddCart = () => {
    enqueueSnackbar("Bạn đã thêm sản phẩm vào giỏ hàng", {
      variant: "success",
    });
  };

  return (
    <section>
      <div>
        <Link to={`/detail/${props.urlSlug}`} className="product_link">
          <div className="card p-0 overflow-hidden shadow">
            <div className="product_image">
              <Image
                src={
                  "https://nongsandalat.vn/wp-content/uploads/2021/10/mut-dau-tay-1-370x290.jpg"
                }
                className="product_img"
              />
            </div>
            <div className="product_content">
              <p className="product_title">{props.name}</p>
              <div className="product_bottom">
                <div className="product_price">
                  <div className="product_price_origin">{props.price} VNĐ</div>
                  <div className="product_price_discount">
                    {props.price} VNĐ
                  </div>
                </div>

                <Link
                  className="text-decoration-none btn btn-success"
                  to={"/cart"}
                  onClick={() => {
                    return handleAddCart(), addItem(props.item);
                  }}
                >
                  Mua ngay
                  <FontAwesomeIcon icon={faCartArrowDown} className="ms-2" />
                </Link>
              </div>
            </div>
          </div>
        </Link>
      </div>
    </section>
  );
};
export default ProductTemplate;
