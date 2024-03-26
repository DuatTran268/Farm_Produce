import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import React from "react";
import { Link } from "react-router-dom";

const BtnSuccess = ({ slug, name, icon }) => {
  return (
    <Link className="btn btn-success" to={`${slug}`}>
      {name} <FontAwesomeIcon icon={icon} />
    </Link>
  );
};
export default BtnSuccess;
