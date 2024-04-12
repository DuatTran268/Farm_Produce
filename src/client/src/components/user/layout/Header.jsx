import React from "react";
import logo from "../../../assets/logo.png";
import { Image } from "react-bootstrap";
import { Link, useNavigate } from "react-router-dom";
import "../../../styles/user/Style.css";
import SearchBox from "../search/SearchBox";
import CartIcon from "../cart/CartIcon";
import BtnLogin from "../../common/BtnLogin";

const Header = () => {
  return (
    <header className="header_top sticky-top">
      <nav className="container navbar navbar-expand-lg navbar-dark">
        <div className="container">
          <Link to={"/"} className="text-danger text-decoration-none">
            <Image src={logo} alt="logo" className="logo" width={100} />
          </Link>
          <button
            className="navbar-toggler"
            type="button"
            data-bs-toggle="collapse"
            data-bs-target="#navbarSupportedContent"
            aria-controls="navbarSupportedContent"
            aria-expanded="false"
            aria-label="Toggle navigation"
          >
            <span className="navbar-toggler-icon"></span>
          </button>
          <div
            className="collapse navbar-collapse justify-content-around"
            id="navbarSupportedContent"
          >
            <div className="search_box" width="80%">
              <SearchBox />
            </div>
          </div>
          <div className="header_cart">
            <CartIcon />
          </div>
          <div className="button_login">
            <BtnLogin/>
          </div>
        </div>
      </nav>
    </header>
  );
};

export default Header;
