import React from "react";
import { Link } from "react-router-dom";
import "./Menu.css";

const Information = () => {
  return (
    <>
      <ul className="list_menu">
        <li className="menu_item">
          <Link className="menu_link" to={"/policys"}>
            Chính sách
          </Link>
        </li>
        <li className="menu_item">
          <Link className="menu_link" to={"/condition"}>
            Điều khoản và điều kiện
          </Link>
        </li>
        <li className="menu_item">
          <Link className="menu_link" to={"/recruitment"}>
            Tuyển dụng
          </Link>
        </li>
        <li className="menu_item">
          <Link className="menu_link" to={"/contact"}>
            Liên hệ
          </Link>
        </li>
      </ul>
    </>
  );
};

export default Information;
