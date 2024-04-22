import { faCartShopping } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import "./NavProduct.css";
import "./NewProduct.css";

import Slider from "react-slick";
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";
import { getProductNewestLimit } from "../../../api/Product";
import ProductHeader from "./ProductHeader";
import { useCart } from "react-use-cart";
import { useSnackbar } from "notistack";

import { Swiper, SwiperSlide } from "swiper/react";
// Import Swiper styles
import "swiper/css";
import "swiper/css/effect-coverflow";
import "swiper/css/pagination";
import { EffectCoverflow, Pagination } from "swiper/modules";

import imagenotfound from "../../../assets/imagenotfound.jpg";
import { Image } from "react-bootstrap";

const NewProduct = () => {
  const { enqueueSnackbar, closeSnackbar } = useSnackbar();

  const [getProduct, setProduct] = useState([]);

  useEffect(() => {
    getProductNewestLimit().then((data) => {
      if (data) {
        setProduct(data);

        // console.log("Checkdata item", data[0].images[0].urlImage);
      } else {
        setProduct([]);
      }
    });
  }, []);

  const { addItem } = useCart();
  const handleAddToCart = (product) => {
    addItem(product);
    enqueueSnackbar("Bạn đã thêm sản phẩm vào giỏ hàng", {
      variant: "success",
    });
  };

  const settings = {
    dots: false,
    infinite: true,
    speed: 500,
    slidesToShow: 2,
    slidesToScroll: 1,

    // slider auto scroll
    autoplay: {
      delay: 3000, // delay 3s
      disableOnInteraction: false,
    },
  };

  const formatCurrency = (number) => {
    return number.toLocaleString("vi-VN", {
      style: "currency",
      currency: "VND",
    });
  };

  return (
    <div className="new_product">
      {/* reuse */}
      <ProductHeader name="Sản phẩm mới" />
      {/* new product body */}
      <div className="product_body">
        <div className="product_note">
          Các sản phẩm mới nhất của công ty Nông sản Đà Lạt
        </div>

        {/* <Slider {...settings}> */}

        <Swiper
          effect={"coverflow"}
          grabCursor={true}
          centeredSlides={true}
          slidesPerView={"auto"}
          coverflowEffect={{
            rotate: 50,
            stretch: 0,
            depth: 100,
            modifier: 1,
            slideShadows: true,
          }}
          pagination={true}
          modules={[EffectCoverflow, Pagination]}
          className="mySwiper"
        >
          {getProduct.map((product, index) => {
            return (
              <SwiperSlide>
                <div className="new_product_wrapper" key={index}>
                  <Link
                    className="product_item"
                    to={`/detail/${product.urlSlug}`}
                  >
                    <div className="new_product_image">
                      {/* <img
                          className="new_product_img"
                          src="https://nongsandalat.vn/wp-content/uploads/2021/10/mut-dau-tay-1-370x290.jpg"
                          alt={product.name}
                        ></img> */}

                      {product.images.length > 0 ? (
                        <Image
                          className="new_product_img"
                          src={`https://localhost:7047/${product.images[0].urlImage}`}
                          alt={product.name}
                          width={300}
                          height={300}
                        />
                      ) : (
                        <Image
                          className="new_product_img"
                          src={imagenotfound}
                          alt={product.name}
                          width={300}
                          height={300}
                        />
                      )}
                    </div>
                    <div className="new_product_content">
                      <div className="new_product_name">
                        <h4>{product.name}</h4>
                      </div>

                      <div className="new_product_desc">
                        {product.description}
                      </div>
                      <div className="new_product_buy">
                        <div className="new_product_price">
                          {formatCurrency(product.price)} / {product.unit.name}
                        </div>
                        <div className="new_product_add">
                          <Link
                            className="new_product_addcart"
                            to={"/cart"}
                            onClick={() => handleAddToCart(product)}
                          >
                            Mua ngay
                            <FontAwesomeIcon
                              icon={faCartShopping}
                              className="new_product_icon"
                            />
                          </Link>
                        </div>
                      </div>
                    </div>
                  </Link>
                </div>
              </SwiperSlide>
            );
          })}
        </Swiper>

        {/* </Slider> */}
      </div>
    </div>
  );
};

export default NewProduct;
