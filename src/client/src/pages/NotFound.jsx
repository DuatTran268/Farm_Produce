import React from "react";
import { Image } from "react-bootstrap";
import IconImage from "../assets/dalavi.png";
import { Link } from "react-router-dom";


const NotFound = () => {
  return (
    <div className="not_found text-center">
      <div className="not_found_content mt-4">
        <h1 className=" text-danger">404 Not Found</h1>
        <Link to={'/'} className="btn btn-success mb-5">Về trang chủ</Link>
        <div>
          <Image src={IconImage}/>
        </div>
      </div>
    </div>
  )
}
export default NotFound;