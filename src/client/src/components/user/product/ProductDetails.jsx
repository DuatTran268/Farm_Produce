import React, { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import "./ProductDetail.css";
import dautay from "../../../assets/mutdau.jpg";
import { Image } from "react-bootstrap";
import { getDetailProductByUrlSlug } from "../../../api/Product";
import { useCart } from "react-use-cart";
import { useSnackbar } from "notistack";

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
        console.log("Check data detailsssss... . .",data.id);
      } else {
        setProductDetail({});
      }
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

  if (productDetail) {
    return (
      <section>
        <div className="product_detail">
          <div className="product_detail_img col-5">
            <Image src={dautay} width={300} />
          </div>
          <div className="product_detail_content col-7">
            <div className="product_detail_title">{productDetail.name}</div>
            <div className="product_detail_price">{productDetail.price} VNĐ</div>

            <div className="product_detail_desc">
              {productDetail.description}
            </div>
            {/* <div className="product_detail_size">
              <label htmlFor="size">Lựa chọn size: </label>
              <select name="size" id="size">
                <option value="S">S</option>
                <option value="M">M</option>
                <option value="L">L</option>
                <option value="XL">XL</option>
              </select>
            </div> */}
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
