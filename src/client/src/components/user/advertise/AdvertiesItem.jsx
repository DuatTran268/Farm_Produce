import React from "react";
import "./Advertise.css";
import { Image } from "react-bootstrap";

const AdvertiseItem = ({images, title, desc}) => {
  return (
    <>
      <div className="col-md-6 col-lg-3 adver_item">
        <div className="adver_item_wrapper">
          <div className="adver_icon">
            <Image src={images}/>
          </div>
          <div className="adver_content">
            <div className="adver_title">{title}</div>
            <div className="adver_description">{desc}</div>
          </div>
        </div>
      </div>
    </>
  );
};
export default AdvertiseItem;
