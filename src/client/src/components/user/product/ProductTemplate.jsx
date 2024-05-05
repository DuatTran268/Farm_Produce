import React from "react";
import { Link, useNavigate } from "react-router-dom";
import { Image } from "react-bootstrap";
import "./Product.css";
import { useCart } from "react-use-cart";
import { faCartArrowDown } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { useSnackbar } from "notistack";
import imagenotfound from "../../../assets/imagenotfound.jpg";
import iconSale from "../../../assets/sale-big.gif";
import iconSoldOut from "../../../assets/soldout.gif";

const ProductTemplate = (props) => {
  const { addItem } = useCart();
  const { enqueueSnackbar, closeSnackbar } = useSnackbar();

  const handleAddCart = () => {
    enqueueSnackbar("Bạn đã thêm sản phẩm vào giỏ hàng", {
      variant: "success",
    });
    addItem(props.item);
  };

  // Hàm định dạng giá tiền thành VNĐ
  const formatCurrency = (number) => {
    return number.toLocaleString("vi-VN", {
      style: "currency",
      currency: "VND",
    });
  };

  return (
    <section>
      <div>
        <Link to={`/detail/${props.urlSlug}`} className="product_link">
          <div className="card p-0 overflow-hidden shadow">
            

            <div className="product_image">
              {props.thumbnailUrl ? (
                <Image
                  src={`https://localhost:7047/${props.thumbnailUrl}`}
                  width={300}
                  height={300}
                  className="product_img"
                />
              ) : (
                // Nếu không có hình ảnh, hiển thị hình ảnh mặc định
                <Image
                  src={imagenotfound}
                  width={300}
                  height={300}
                  className="product_img"
                />
              )}
            </div>
            <div className="product_content">
            {/* {props.priceVirtual !== 0 && (
            )} */}
            <Image className="product_icon_sale" src={iconSale} width={80} />

            {props.quantityAvailable === 0 && (
              <Image
                className="product_icon_soldout"
                src={iconSoldOut}
                width={80}
              />
            )}
              <p className="product_title">{props.name}</p>
              <div className="product_bottom">
                <div className="product_price">
                  <div className="product_price_virtual">
                    {/* check value = 0 disable div virtual */}
                    {props.priceVirtual !== 0 && (
                      <div>
                        {formatCurrency(props.priceVirtual)} / {props.unit}
                      </div>
                    )}
                  </div>
                  <div className="product_price_sell">
                    {formatCurrency(props.price)} / {props.unit}
                  </div>
                </div>
                <Link
                  className="btn_addtocart"
                  onClick={() => {
                    if (props.quantityAvailable !== 0) {
                      handleAddCart();
                    } else {
                      enqueueSnackbar("Sản phẩm đã hết hàng", {
                        variant: "error",
                      });
                    }
                  }}
                >
                  <FontAwesomeIcon icon={faCartArrowDown} />
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
