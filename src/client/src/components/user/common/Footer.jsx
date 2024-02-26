import React from "react";
import { Image } from "react-bootstrap";
import bgFooter from "../../../assets/bgfooter.jpg";
import "../../../styles/Footer.css";
import logo from "../../../assets/logo.png";
import MainMenu from "../menu/MainMenus";
import Policys from "../menu/Policys";
import "../../../styles/Footer.css";
import { Link } from "react-router-dom";
const Footer = () => {
  return (
    <footer className="footer">
      <Image className="bg_footer" src={bgFooter} />
      <div className=" footer_wrappers container">
        <div className=" footer_content row">
          <div className="col-md-6 col-lg-3 footer_logo">
            <h3 className=" text-white mt-3 footer_title">Nông sản Đà Lạt</h3>
            <div className="footer_image">
              <Link to={"/home"}>
                <Image className="footer_img" src={logo} width={100} />
              </Link>
            </div>
          </div>
          <div className="col-md-6 col-lg-3 footer_menu">
            <h3 className=" text-white mt-3 footer_title">Menu</h3>
            <MainMenu />
          </div>
          <div className="col-md-6 col-lg-3 footer_policy">
            <h3 className=" text-white mt-3 footer_title">Thông tin</h3>
            <Policys />
          </div>
          <div className="col-md-6 col-lg-3 footer_social">
            <h3 className=" text-white mt-3 footer_title">
              Theo dõi chúng tôi
            </h3>
            <div className="footer_iframe">
              <iframe
                src="https://www.facebook.com/plugins/page.php?href=https%3A%2F%2Fwww.facebook.com%2Fdalavi.vn&tabs&width=270&height=250&small_header=false&adapt_container_width=false&hide_cover=false&show_facepile=false&appId"
                width="270"
                height="250"
                allowfullscreen="true"
              ></iframe>
            </div>
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
              Hung Duat
            </Link>
          </div>
        </div>
    </footer>
  );
};
export default Footer;
