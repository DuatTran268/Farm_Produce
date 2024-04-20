import React from "react";
import { Image } from "react-bootstrap";
import logo from "../../../assets/logo.png";
import MainMenu from "../menu/MainMenus";
import Policys from "../menu/Information";
import "./Footer.css";
import "../menu/Menu.css";

import { Link } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faLocationDot,
  faMailBulk,
  faPhone,
} from "@fortawesome/free-solid-svg-icons";
const Footer = () => {
  return (
    <footer className=" footer">
      <div className=" container footer_wrapper pt-5">
        <div className="col-md-6 col-lg-1 footer_logo">
          <div className="footer_image">
            <Link to={"/home"}>
              <Image className="footer_img" src={logo} width={100} />
            </Link>
          </div>
        </div>
        <div className="col-md-6 col-lg-3 footer_menu">
          <ul className="list_menu">
            <li className="menu_item">
              <div className="menu_link text-center">Hung Duat Farm</div>
            </li>
            <li className="menu_item">
              <Link
                className="menu_link "
                to={`https://www.google.com/maps/place/Tr%C6%B0%E1%BB%9Dng+%C4%90%E1%BA%A1i+H%E1%BB%8Dc+%C4%90%C3%A0+L%E1%BA%A1t/@11.95456,108.444205,16z/data=!4m6!3m5!1s0x317112d959f88991:0x9c66baf1767356fa!8m2!3d11.9545604!4d108.4442049!16s%2Fm%2F02rtwnx?hl=vi`}
              >
                <FontAwesomeIcon icon={faLocationDot} color="blue" /> 95 Lý Nam
                Đế - P.8 - TP Đà Lạt
              </Link>
            </li>
            <li className="menu_item">
              <Link className="menu_link " to={"tel:0868658353"}>
                <FontAwesomeIcon icon={faPhone} color="green" /> 09666888
              </Link>
            </li>
            <li className="menu_item">
              <Link className="menu_link " to={"mailto:duatsen36@gmail.com"}>
                <FontAwesomeIcon icon={faMailBulk} color="red" />{" "}
                duatsen36@gmail.com
              </Link>
            </li>
          </ul>
        </div>

        <div className="col-md-6 col-lg-2 footer_menu">
          <MainMenu />
        </div>
        <div className="col-md-6 col-lg-3 footer_policy">
          <Policys />
        </div>
        <div className="col-md-6 col-lg-3 footer_social">
          <div className="footer_iframe">
            <iframe
              src="https://www.facebook.com/plugins/page.php?href=https%3A%2F%2Fwww.facebook.com%2Fdalavi.vn&tabs&width=270&height=250&small_header=false&adapt_container_width=false&hide_cover=false&show_facepile=false&appId"
              width="270"
              height="250"
              title="map"
            ></iframe>
          </div>
        </div>
      </div>
      <div className="footer_bottom">
        <div className="text-center">
          Coypyright &copy; 2024 by
          <Link
            to={"https://www.facebook.com/tran.duat.2368"}
            target="_blank"
            className="text-decoration-none px-1 text_copyright "
          >
            Hung Duat Corp
          </Link>
        </div>
      </div>
    </footer>
  );
};
export default Footer;
