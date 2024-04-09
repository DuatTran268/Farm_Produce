import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import React from "react";
import { Link } from "react-router-dom";

const SidebarCommon = ({ slug, icon, title }) => {
  return (
    <div className="sidebar-wrapper">
      <Link className="sidebar-link" to={`/admin/${slug}`}>
        <li>
          <FontAwesomeIcon icon={icon} />
          <span className="px-3">{title}</span>
        </li>
      </Link>
    </div>
  );
};
export default SidebarCommon;
