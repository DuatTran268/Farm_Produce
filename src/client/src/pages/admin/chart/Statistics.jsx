import { faUser } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import React from "react";
import "./Statistics.css"


const Statistics = ({icon, title, value}) => {
  return (
    <div className="statistics col-3">
      <div className="statistics_wrapper">
        <FontAwesomeIcon className="statistics_icon" icon={icon}/>
        <div className="statistics_content">
          <h5 className="statistics_title">
            {title}
          </h5>
          <div className="statistics_value">
            {value}
          </div>
        </div>
      </div>
    </div>
  )
}
export default Statistics;