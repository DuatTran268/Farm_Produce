import React from "react";
import { Link } from "react-router-dom";
import "../../../styles/user/Menu.css";

const MainMenu = () => {
  return (
    <>
      <ul className="list_menu">
        <li className="menu_item">
          <Link className="menu_link" to={"/home"}>
            Trang chủ
          </Link>
        </li>
        <li className="menu_item">
          <Link className="menu_link" to={"/product"}>
            Sản phẩm
          </Link>
        </li>
        <li className="menu_item">
          <Link className="menu_link" to={"/cart"}>
            Giỏ hàng
          </Link>
        </li>
        <li className="menu_item">
          <Link className="menu_link" to={"/order"}>
            Đơn hàng
          </Link>
        </li>
      </ul>
    </>
  );
};

export default MainMenu;
