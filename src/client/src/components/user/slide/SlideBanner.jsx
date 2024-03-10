import React from "react";
import Slider from "react-slick";
import banner1 from "../../../assets/banner1.png";
import banner2 from "../../../assets/banner2.jpg";
import "../../../styles/user/Slider.css"
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";

const SlideBanner = () => {
  const settings = {
    dots: false,
    infinite: true,
    speed: 500,
    slidesToShow: 1,
    slidesToScroll: 1,

    // slider auto scroll
    autoplay: {
      delay: 3000, // delay 3s
      disableOnInteraction: false,
    },
  };

  return (
    <>
      <div className="image_banner">
        <Slider {...settings}>
          <img src={banner1} className="banner_img" alt="banner" />
          <img src={banner2} className="banner_img" alt="banner" />
        </Slider>
      </div>
    </>
  );
};

export default SlideBanner;
