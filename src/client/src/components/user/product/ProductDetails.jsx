import React, { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import "./ProductDetail.css";
import dautay from "../../../assets/mutdau.jpg";
import { Image } from "react-bootstrap";
import { getDetailProductByUrlSlug, increaseView } from "../../../api/Product";
import { useCart } from "react-use-cart";
import { useSnackbar } from "notistack";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCartArrowDown } from "@fortawesome/free-solid-svg-icons";
import imagenotfound from "../../../assets/imagenotfound.jpg";
import iconSale from "../../../assets/sale-big.gif";

const ProductDetails = () => {
  const params = useParams();
  const [productDetail, setProductDetail] = useState(null);
  const [quantity, setQuantity] = useState(1);
  const { slug } = params;
  const { addItem, items, updateItemQuantity } = useCart();
  const { enqueueSnackbar } = useSnackbar();

  useEffect(() => {
    document.title = "Chi tiết sản phẩm";
    getDetailProductByUrlSlug(slug).then((data) => {
      if (data) {
        setProductDetail(data);
        // console.log("Check data detailsssss... . .", data);
      } else {
        setProductDetail({});
      }
      increaseView(slug)
      // console.log("Check slug: ", slug);
    });
  }, [slug]);

  const handleAddToCart = () => {
    // check item existing
    const existingItem = items.find((item) => item.id === productDetail.id);

    // if product existing in cart, update product quantity
    if (existingItem) {
      const updatedQuantity = existingItem.quantity + quantity;
      updateItemQuantity(existingItem.id, updatedQuantity);
    } else {
      addItem({
        // if product not existing in cart, add product to cart
        id: productDetail.id,
        name: productDetail.name,
        price: productDetail.price,
        quantity: quantity,
      });
    }

    enqueueSnackbar("Bạn đã thêm sản phẩm vào giỏ hàng", {
      variant: "success",
    });
  };

  // Hàm định dạng giá tiền thành VNĐ
  const formatCurrency = (number) => {
    return number.toLocaleString("vi-VN", {
      style: "currency",
      currency: "VND",
    });
  };

  if (productDetail) {
    return (
      <section>
        <div className="product_detail">
          <div className="product_detail_img col-5">
            {productDetail.images.length > 0 ? (
              <Image
                src={`https://localhost:7047/${productDetail.images[0].urlImage}`}
                className="image_avt_product"
              />
            ) : (
              <>
                <Image src={imagenotfound} width={300} />
              </>
            )}
          </div>
          <div className="product_detail_content col-7">
            <div className="product_detail_title">{productDetail.name}</div>
            <div className="product_price_virtual mt-3">
              {productDetail.priceVirtual !== 0 && (
                <h5>{formatCurrency(productDetail.priceVirtual)}</h5>
              )}
            </div>
            <h5 className="product_price_sell mt-3">
              {formatCurrency(productDetail.price)}
            </h5>
            <div className="product_detail_desc mt-3">
              {productDetail.description}
            </div>
            <div className="product_detail_quantity_cart">
              <div className="product_detail_quantity">
                <span>Số lượng</span>
                <input
                  className="box_quantity_product"
                  type="number"
                  id="quantity"
                  min="1"
                  max="100"
                  value={quantity}
                  onChange={(e) => setQuantity(parseInt(e.target.value))}
                />
              </div>
              <Link
                className="product_detail_addcart"
                to={"/cart"}
                onClick={handleAddToCart}
              >
                Thêm vào giỏ hàng
                <FontAwesomeIcon icon={faCartArrowDown} className="px-2" />
              </Link>
            </div>
          </div>
        </div>
        {productDetail ? (
          <div className="image_gallery">
            <div className="image_gallery_product">
              {productDetail.images.map((image, index) => (
                <Image
                  className="image_product_related"
                  key={index}
                  src={`https://localhost:7047/${image.urlImage}`}
                />
              ))}
            </div>
          </div>
        ) : (
          <>Ok</>
        )}
      </section>
    );
  }
};

export default ProductDetails;
