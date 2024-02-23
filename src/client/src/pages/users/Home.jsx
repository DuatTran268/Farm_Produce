import React from "react";
import Header from "../../components/user/common/Header";
import Categoris from "../../components/user/Categoris";
import "../../styles/Home.css";
import { Image } from "react-bootstrap";
import Slide from "../../assets/banner1.png"

const Home = () => {
  return (
    <div className="home">
      {/* header */}
      <Header />
      {/* container */}
      <div className="container ">
        <div className="d-flex pt-3 category_slide justify-content-between">
          <div className="category col-3">
            <Categoris />
          </div>
          {/* slide advertising */}
          <div className="slider col-8">
            <div className="banner">
              <Image src={Slide}/>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Home;
