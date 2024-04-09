import React from "react";
import "./Advertise.css";
import FakeAd from "../../../data/FakeAd";
import AdvertiseItem from "./AdvertiesItem";

const Advertise = () => {
  return (
    <section>
      <div className="advertise">
        <div className="container">
          <div className="row">
            {FakeAd.map((item, index) => (
              <AdvertiseItem
                key={index}
                images={item.image}
                title={item.title}
                desc={item.description}
              />
            ))}
          </div>
        </div>
      </div>
    </section>
  );
};
export default Advertise;
