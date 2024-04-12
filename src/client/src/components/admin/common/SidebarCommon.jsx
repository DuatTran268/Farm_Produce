import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import React, { useEffect } from "react";
import { Link } from "react-router-dom";

const SidebarCommon = ({ slug, icon, title, active, onClick }) => {



  return (
    <div className="sidebar-wrapper">
      <Link className="sidebar-link" to={`/admin/${slug}`}>
        <li >
          <FontAwesomeIcon icon={icon} />
          <span className="px-3">{title}</span>
        </li>
      </Link>
    </div>
  );
};
export default SidebarCommon;
